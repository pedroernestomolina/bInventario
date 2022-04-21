using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        //INSERTAR
        public DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_ajustes=a_productos_movimientos_ajustes+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovAjuste = cnn.Database.SqlQuery<int>("select a_productos_movimientos_ajustes from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovAjuste.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.movDetalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.movDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.autoDeposito;
                                return result;
                            }
                            var _ex = false;
                            if (entPrdDep.fisica >= 0) { _ex = true; }

                            entPrdDep.fisica += dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();

                            if (_ex && entPrdDep.fisica < 0)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PROBLEMA CON AJUSTE EXISTENCIA ]" + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_traslados=a_productos_movimientos_traslados+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovTraslado = cnn.Database.SqlQuery<int>("select a_productos_movimientos_traslados from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovTraslado.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.detalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDepOrigen = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDepOrigen == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ REGISTRO NO ENCONTRADO ] " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            if (dt.cantidadUnd > entPrdDepOrigen.fisica)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ EXISTENCIA NO DISPONIBLE ] " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            entPrdDepOrigen.fisica -= dt.cantidadUnd;
                            entPrdDepOrigen.disponible = entPrdDepOrigen.fisica;
                            cnn.SaveChanges();

                            var entPrdDepDestino = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDepositoDestino);
                            if (entPrdDepDestino == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ REGISTRO NO ENCONTRADO ] " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.depositoDestino;
                                return result;
                            }
                            entPrdDepDestino.fisica += dt.cantidadUnd;
                            entPrdDepDestino.disponible = entPrdDepDestino.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Devolucion_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_traslados_dev=a_productos_movimientos_traslados_dev+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovTraslado = cnn.Database.SqlQuery<int>("select a_productos_movimientos_traslados_dev from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovTraslado.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.detalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.prdDeposito)
                        {
                            var entPrdDepOrigen = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDepOrigen == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ REGISTRO NO ENCONTRADO ]" + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            if (dt.cantidadUnd > entPrdDepOrigen.fisica)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ EXISTENCIA NO DISPONIBLE ]" + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            entPrdDepOrigen.fisica -= dt.cantidadUnd;
                            entPrdDepOrigen.disponible = entPrdDepOrigen.fisica;
                            cnn.SaveChanges();

                            var entPrdDepDestino = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDepositoDestino);
                            if (entPrdDepDestino == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ REGISTRO NO ENCONTRADO ]" + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.depositoDestino;
                                return result;
                            }
                            entPrdDepDestino.fisica += dt.cantidadUnd;
                            entPrdDepDestino.disponible = entPrdDepDestino.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_descargos=a_productos_movimientos_descargos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovDesCargo = cnn.Database.SqlQuery<int>("select a_productos_movimientos_descargos from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovDesCargo.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.movDetalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.movDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            if (dt.cantidadUnd > entPrdDep.fisica)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ EXISTENCIA NO DISPONIBLE ] " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }

                            entPrdDep.fisica -= dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        };


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_cargos=a_productos_movimientos_cargos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovCargo = cnn.Database.SqlQuery<int>("select a_productos_movimientos_cargos from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovCargo.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.movDetalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };


                        ////ACTUALIZAR COSTO
                        //foreach (var dt in ficha.prdCosto) 
                        //{
                        //    var entPrd = cnn.productos.Find(dt.autoProducto);
                        //    if (entPrd == null)
                        //    {
                        //        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO "+Environment.NewLine+dt.autoProducto;
                        //        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        //        return result;
                        //    }

                        //    var montoActualInvPromedioUnd = 0.0m;
                        //    var cp=dt.costoFinal;
                        //    var cpu=dt.costoFinalUnd;
                        //    var existenciaActualUnd = cnn.productos_deposito.Where(s=>s.auto_producto==dt.autoProducto).Sum(s=>s.fisica);
                        //    if (existenciaActualUnd  > 0) 
                        //    {
                        //        montoActualInvPromedioUnd = existenciaActualUnd * entPrd.costo_promedio_und;
                        //        var x1 = montoActualInvPromedioUnd + dt.importeEntradaUnd;
                        //        var x2 = existenciaActualUnd + dt.cantidadEntranteUnd;

                        //        cpu = x1 / x2 ;
                        //        cp = cpu / entPrd.contenido_compras;
                        //    }

                        //    entPrd.costo = dt.costoFinal;
                        //    entPrd.costo_und = dt.costoFinalUnd;
                        //    entPrd.costo_promedio = cp ;
                        //    entPrd.costo_promedio_und = cpu ;
                        //    entPrd.divisa = dt.costoDivisa;
                        //    entPrd.fecha_ult_costo = fechaSistema.Date;
                        //    entPrd.fecha_cambio = fechaSistema.Date;
                        //    cnn.SaveChanges();
                        //}

                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.movDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                            entPrdDep.fisica += dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        };

                        //// REGISTRAR HISTORICO COSTO
                        //foreach (var dt in ficha.prdCostoHistorico)
                        //{
                        //    var entPrdCostoHistorico = new productos_costos()
                        //    {
                        //        auto_producto = dt.autoProducto,
                        //        nota = dt.nota,
                        //        fecha = fechaSistema.Date,
                        //        estacion = ficha.estacion,
                        //        hora = fechaSistema.ToShortTimeString(),
                        //        usuario = ficha.usuario,
                        //        costo = dt.costo,
                        //        costo_divisa = dt.divisa,
                        //        divisa = dt.tasaCambio,
                        //        serie = dt.serie,
                        //        documento = numDoc,
                        //    };
                        //    cnn.productos_costos.Add(entPrdCostoHistorico);
                        //    cnn.SaveChanges();
                        //}


                        //// ACTUALIZAR PRECIOS 
                        //foreach (var dt in ficha.prdPrecio)
                        //{
                        //    var entPrd = cnn.productos.Find(dt.autoProducto);
                        //    if (entPrd == null)
                        //    {
                        //        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO " + Environment.NewLine + dt.autoProducto;
                        //        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        //        return result;
                        //    }

                        //    if (dt.precio_1 != null) 
                        //    {
                        //        entPrd.precio_1 = dt.precio_1.precioNeto;
                        //        entPrd.pdf_1 = dt.precio_1.precio_divisa_full;
                        //    }
                        //    if (dt.precio_2 != null)
                        //    {
                        //        entPrd.precio_2 = dt.precio_2.precioNeto;
                        //        entPrd.pdf_2 = dt.precio_2.precio_divisa_full;
                        //    }
                        //    if (dt.precio_3 != null)
                        //    {
                        //        entPrd.precio_3 = dt.precio_3.precioNeto;
                        //        entPrd.pdf_3 = dt.precio_3.precio_divisa_full;
                        //    }
                        //    if (dt.precio_4 != null)
                        //    {
                        //        entPrd.precio_4 = dt.precio_4.precioNeto;
                        //        entPrd.pdf_4 = dt.precio_4.precio_divisa_full;
                        //    }
                        //    if (dt.precio_5 != null)
                        //    {
                        //        entPrd.precio_pto = dt.precio_5 .precioNeto;
                        //        entPrd.pdf_pto = dt.precio_5 .precio_divisa_full;
                        //    }

                        //    cnn.SaveChanges();
                        //}


                        //// ACTUALIZAR MARGEN UTILIDAD
                        //foreach (var dt in ficha.prdPrecioMargen)
                        //{
                        //    var entPrd = cnn.productos.Find(dt.autoProducto);
                        //    if (entPrd == null)
                        //    {
                        //        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO " + Environment.NewLine + dt.autoProducto;
                        //        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        //        return result;
                        //    }

                        //    if (dt.precio_1 != null)
                        //    {
                        //        entPrd.utilidad_1 = dt.precio_1.utilidad;
                        //    }
                        //    if (dt.precio_2 != null)
                        //    {
                        //        entPrd.utilidad_2 = dt.precio_2.utilidad;
                        //    }
                        //    if (dt.precio_3 != null)
                        //    {
                        //        entPrd.utilidad_3 = dt.precio_3.utilidad;
                        //    }
                        //    if (dt.precio_4 != null)
                        //    {
                        //        entPrd.utilidad_4 = dt.precio_4.utilidad;
                        //    }
                        //    if (dt.precio_5 != null)
                        //    {
                        //        entPrd.utilidad_pto = dt.precio_5.utilidad;
                        //    }

                        //    cnn.SaveChanges();
                        //}


                        //// REGISTRAR HISTORICO PRECIO
                        //foreach (var dt in ficha.prdPrecioHistorico)
                        //{
                        //    var entPrdPrecioHistorico = new productos_precios()
                        //    {
                        //        auto_producto = dt.autoProducto,
                        //        nota = dt.nota,
                        //        fecha = fechaSistema.Date,
                        //        estacion = ficha.estacion,
                        //        hora = fechaSistema.ToShortTimeString(),
                        //        usuario = ficha.usuario,
                        //        precio_id=dt.precio_id,
                        //        precio=dt.precio,
                        //    };
                        //    cnn.productos_precios.Add(entPrdPrecioHistorico);
                        //    cnn.SaveChanges();
                        //}

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_AjusteInvCero_Insertar(DtoLibInventario.Movimiento.AjusteInvCero.Insertar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "";

                        sql = "update sistema_contadores set a_productos_movimientos=a_productos_movimientos+1, a_productos_movimientos_ajustes=a_productos_movimientos_ajustes+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aMov = cnn.Database.SqlQuery<int>("select a_productos_movimientos from sistema_contadores").FirstOrDefault();
                        var aMovAjuste = cnn.Database.SqlQuery<int>("select a_productos_movimientos_ajustes from sistema_contadores").FirstOrDefault();
                        var autoMov = aMov.ToString().Trim().PadLeft(10, '0');
                        var numDoc = aMovAjuste.ToString().Trim().PadLeft(10, '0');
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var xficha = ficha.mov;
                        var entMov = new productos_movimientos()
                        {
                            auto = autoMov,
                            auto_concepto = xficha.autoConcepto,
                            auto_deposito = xficha.autoDepositoOrigen,
                            auto_destino = xficha.autoDepositoDestino,
                            auto_remision = xficha.autoRemision,
                            auto_usuario = xficha.autoUsuario,
                            autorizado = xficha.autorizado,
                            cierre_ftp = xficha.cierreFtp,
                            codigo_concepto = xficha.codConcepto,
                            codigo_deposito = xficha.codDepositoOrigen,
                            codigo_destino = xficha.codDepositoDestino,
                            codigo_sucursal = xficha.codigoSucursal,
                            codigo_usuario = xficha.codUsuario,
                            concepto = xficha.desConcepto,
                            deposito = xficha.desDepositoOrigen,
                            destino = xficha.desDepositoDestino,
                            documento = numDoc,
                            documento_nombre = xficha.documentoNombre,
                            estacion = xficha.estacion,
                            estatus_anulado = xficha.estatusAnulado,
                            estatus_cierre_contable = xficha.estatusCierreContable,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = xficha.nota,
                            renglones = xficha.renglones,
                            situacion = xficha.situacion,
                            tipo = xficha.tipo,
                            total = xficha.total,
                            usuario = xficha.usuario,
                        };
                        cnn.productos_movimientos.Add(entMov);
                        cnn.SaveChanges();

                        var entMovExtra = new productos_movimientos_extra()
                        {
                            auto_movimiento = entMov.auto,
                            factor_cambio = xficha.factorCambio,
                            monto_divisa = xficha.montoDivisa,
                        };
                        cnn.productos_movimientos_extra.Add(entMovExtra);
                        cnn.SaveChanges();

                        var sql1 = @"INSERT INTO productos_movimientos_detalle (auto_documento, auto_producto, codigo, nombre, " +
                            "cantidad, cantidad_bono, cantidad_und, categoria, fecha, tipo, estatus_anulado, contenido_empaque, " +
                            "empaque, decimales, auto, costo_und, total, costo_compra, estatus_unidad, signo, existencia, " +
                            "fisica, auto_departamento, auto_grupo, cierre_ftp) " +
                            "VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, " +
                            "{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24})";

                        var _auto = 0;
                        foreach (var det in ficha.movDetalles)
                        {
                            _auto += 1;
                            var xauto = _auto.ToString().Trim().PadLeft(10, '0');

                            var vk = cnn.Database.ExecuteSqlCommand(sql1, autoMov, det.autoProducto, det.codigoProducto, det.nombreProducto,
                                det.cantidad, det.cantidadBono, det.cantidadUnd, det.categoria, fechaSistema.Date, det.tipo,
                                det.estatusAnulado, det.contEmpaque, det.empaque, det.decimales, xauto, det.costoUnd, det.total,
                                det.costoCompra, det.estatusUnidad, det.signo, 0, 0, det.autoDepartamento, det.autoGrupo, xficha.cierreFtp);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + det.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };


                        //ACTUALIZAR DEPOSITO-ENTRADA MERCANCIA
                        foreach (var dt in ficha.movDeposito)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == dt.autoProducto && f.auto_deposito == dt.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PRODUCTO - DEPOSITO ] NO ENCONTRADO " + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.autoDeposito;
                                return result;
                            }
                            var _ex = false;
                            if (entPrdDep.fisica >= 0) { _ex = true; }

                            entPrdDep.fisica += dt.cantidadUnd;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();

                            if (_ex && entPrdDep.fisica < 0)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ PROBLEMA CON AJUSTE EXISTENCIA ]" + Environment.NewLine + "Producto: " + dt.nombreProducto + ", Deposito: " + dt.nombreDeposito;
                                return result;
                            }
                        };

                        //
                        var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDeposito", ficha.mov.autoDepositoOrigen);
                        var xsql = @"select count(*) as cnt 
                                    from productos_deposito 
                                    where auto_deposito=@autoDeposito and fisica<>0";
                        var entCnt = cnn.Database.SqlQuery<int?>(xsql, xp1).FirstOrDefault();
                        if (entCnt != null)
                        {
                            if (entCnt.HasValue)
                            {
                                if (entCnt.Value > 0) 
                                {
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    result.Mensaje = "[ PROBLEMA CON DEPOSITO ]" + Environment.NewLine + "Existe Mercancia Disponible";
                                    return result;
                                }
                            }
                        }

                        var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@autoDeposito", ficha.mov.autoDepositoOrigen);
                        xsql = @"update productos_deposito set fisica=0, reservada=0, disponible=0 
                                    where auto_deposito=@autoDeposito";
                        var vxp = cnn.Database.ExecuteSqlCommand(xsql, xp2);


                        //KARDEX MOV=> ITEMS
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                            fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                            nota,precio_und,codigo,siglas,codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, 
                            codigo_concepto, nombre_concepto) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                            {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        foreach (var dt in ficha.movKardex)
                        {
                            var vk = cnn.Database.ExecuteSqlCommand(sql2, dt.autoProducto, dt.total, dt.autoDeposito,
                                dt.autoConcepto, autoMov, fechaSistema.Date, fechaSistema.ToShortTimeString(), numDoc,
                                dt.modulo, dt.entidad, dt.signoMov, dt.cantidad, dt.cantidadBono, dt.cantidadUnd, dt.costoUnd, dt.estatusAnulado,
                                dt.nota, dt.precioUnd, dt.codigoMov, dt.siglasMov, dt.codigoSucursal, xficha.cierreFtp, dt.codigoDeposito,
                                dt.nombreDeposito, dt.codigoConcepto, dt.nombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.autoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.SaveChanges();
                        };

                        ts.Complete();
                        result.Auto = autoMov;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }


        //GET
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> 
            Producto_Movimiento_GetFicha(string autoDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_movimientos.Find(autoDoc);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DOCUMENTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.SinDefinir;
                    switch (ent.tipo)
                    {
                        case "01":
                            tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Cargo;
                            break;
                        case "02":
                            tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Descargo;
                            break;
                        case "03":
                            tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Traslado;
                            break;
                        case "04":
                            tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Ajuste;
                            break;
                    }

                    var entDet = cnn.productos_movimientos_detalle.Where(f => f.auto_documento == autoDoc).ToList();
                    var nr = new DtoLibInventario.Movimiento.Ver.Ficha()
                    {
                        autorizadoPor = ent.autorizado,
                        codigoConcepto = ent.codigo_concepto,
                        codigoDepositoDestino = ent.codigo_destino,
                        codigoDepositoOrigen = ent.codigo_deposito,
                        concepto = ent.concepto,
                        depositoDestino = ent.destino,
                        depositoOrigen = ent.deposito,
                        documentoNro = ent.documento,
                        estacion = ent.estacion,
                        fecha = ent.fecha,
                        hora = ent.hora,
                        notas = ent.nota,
                        tipoDocumento = ent.tipo,
                        total = ent.total,
                        usuario = ent.usuario,
                        usuarioCodigo = ent.codigo_usuario,
                        nombreDocumento = ent.documento_nombre,
                        docTipo=tipo,
                        estatusAnulado=ent.estatus_anulado,
                    };

                    var det = entDet.Select(s =>
                    {
                        var dt = new DtoLibInventario.Movimiento.Ver.Detalle()
                        {
                            cantidadUnd = s.cantidad_und,
                            codigo = s.codigo,
                            costoUnd = s.costo_und,
                            descripcion = s.nombre,
                            importe = s.total,
                            signo = s.signo,
                            cantidad = s.cantidad,
                            contenido = s.contenido_empaque,
                            empaque = s.empaque,
                            esUnidad = s.estatus_unidad == "1" ? true : false,
                            decimales=s.decimales,
                        };
                        return dt;
                    }).ToList();
                    nr.detalles = det;

                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen> 
            Producto_Movimiento_GetLista(DtoLibInventario.Movimiento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"select distinct m.auto as autoId, m.fecha, m.hora, 
                                m.concepto as docConcepto, m.total as docMonto, m.nota as docMotivo, m.documento as docNro,
                                m.renglones as docRenglones, m.situacion as docSituacion, m.codigo_sucursal as docSucursal,
                                m.tipo, m.estacion, m.estatus_anulado as estatusAnulado, m.usuario, 
                                m.deposito as depositoOrigen, m.destino as depositoDestino
                                from productos_movimientos as m ";
                    var sql_2 = @"";
                    var sql_3 = @" where 1=1 ";

                    if (filtro.Desde.HasValue)
                    {
                        p1.ParameterName = "@desde";
                        p1.Value = filtro.Desde.Value;
                        sql_3+= " and m.fecha >= @desde ";
                    }
                    if (filtro.Hasta.HasValue)
                    {
                        p2.ParameterName = "@hasta";
                        p2.Value = filtro.Hasta.Value;
                        sql_3 += " and m.fecha <= @hasta ";
                    }
                    if (filtro.IdSucursal != "")
                    {
                        p3.ParameterName = "@IdSucursal";
                        p3.Value = filtro.IdSucursal;
                        sql_3 += " and m.codigo_sucursal=@IdSucursal ";
                    }
                    if (filtro.IdDepOrigen != "")
                    {
                        p4.ParameterName = "@IdDepOrigen";
                        p4.Value = filtro.IdDepOrigen;
                        sql_3 += " and m.auto_deposito=@IdDepOrigen ";
                    }
                    if (filtro.IdDepDestino != "")
                    {
                        p5.ParameterName = "@IdDepDestino";
                        p5.Value = filtro.IdDepDestino;
                        sql_3 += " and m.auto_destino =@IdDepDestino ";
                    }
                    if (filtro.IdConcepto != "")
                    {
                        p6.ParameterName = "@IdConcepto";
                        p6.Value = filtro.IdConcepto;
                        sql_3 += " and m.auto_concepto=@IdConcepto ";
                    }
                    if (filtro.Estatus != DtoLibInventario.Movimiento.enumerados.EnumEstatus.SinDefinir)
                    {
                        var estatus = "";
                        switch (filtro.Estatus)
                        {
                            case DtoLibInventario.Movimiento.enumerados.EnumEstatus.Activo:
                                estatus = "0";
                                break;
                            case DtoLibInventario.Movimiento.enumerados.EnumEstatus.Anulado:
                                estatus = "1";
                                break;
                        }
                        p7.ParameterName = "@Estatus";
                        p7.Value = estatus;
                        sql_3 += " and m.estatus_anulado=@Estatus ";
                    }
                    if (filtro.TipoDocumento != DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.SinDefinir)
                    {
                        var tipo = "";
                        switch (filtro.TipoDocumento)
                        {
                            case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Cargo:
                                tipo = "01";
                                break;
                            case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Descargo:
                                tipo = "02";
                                break;
                            case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Traslado:
                                tipo = "03";
                                break;
                            case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Ajuste:
                                tipo = "04";
                                break;
                        }
                        p8.ParameterName = "@Tipo";
                        p8.Value = tipo;
                        sql_3 += " and m.tipo=@Tipo ";
                    }
                    if (filtro.IdProducto != "")
                    {
                        p9.ParameterName = "@IdProducto";
                        p9.Value = filtro.IdProducto;
                        sql_2 += " join productos_movimientos_detalle as md on m.auto=md.auto_documento and md.auto_producto=@IdProducto ";
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Movimiento.Lista.Resumen>(sql,p1,p2,p3,p4,p5,p6,p7,p8,p9).ToList();
                    result.Lista = list;

                    //var q = cnn.productos_movimientos.ToList();
                    //if (filtro.Desde.HasValue) 
                    //{
                    //    q = q.Where(w => w.fecha >= filtro.Desde.Value).ToList();
                    //}
                    //if (filtro.Hasta.HasValue)
                    //{
                    //    q = q.Where(w => w.fecha <= filtro.Hasta.Value).ToList();
                    //}
                    //if (filtro.IdSucursal!="")
                    //{
                    //    q = q.Where(w => w.codigo_sucursal==filtro.IdSucursal).ToList();
                    //}
                    //if (filtro.IdDepOrigen != "")
                    //{
                    //    q = q.Where(w => w.auto_deposito  == filtro.IdDepOrigen).ToList();
                    //}
                    //if (filtro.IdDepDestino != "")
                    //{
                    //    q = q.Where(w => w.auto_destino == filtro.IdDepDestino).ToList();
                    //}
                    //if (filtro.IdConcepto != "")
                    //{
                    //    q = q.Where(w => w.auto_concepto == filtro.IdConcepto).ToList();
                    //}
                    //if (filtro.Estatus != DtoLibInventario.Movimiento.enumerados.EnumEstatus.SinDefinir)
                    //{
                    //    var estatus = "";
                    //    switch (filtro.Estatus) 
                    //    {
                    //        case DtoLibInventario.Movimiento.enumerados.EnumEstatus.Activo:
                    //            estatus = "0";
                    //            break;
                    //        case DtoLibInventario.Movimiento.enumerados.EnumEstatus.Anulado:
                    //            estatus = "1";
                    //            break;
                    //    }
                    //    q = q.Where(w => w.estatus_anulado == estatus).ToList();
                    //}
                    //if (filtro.TipoDocumento!= DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.SinDefinir)
                    //{
                    //    var tipo = "";
                    //    switch (filtro.TipoDocumento) 
                    //    {
                    //        case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Cargo:
                    //            tipo = "01";
                    //            break;
                    //        case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Descargo:
                    //            tipo="02";
                    //            break;
                    //        case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Traslado:
                    //            tipo="03";
                    //            break;
                    //        case DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Ajuste:
                    //            tipo = "04";
                    //            break;
                    //    }
                    //    q = q.Where(w => w.tipo==tipo).ToList();
                    //}

                    //var list = new List<DtoLibInventario.Movimiento.Lista.Resumen >();
                    //if (q != null)
                    //{
                    //    if (q.Count() > 0)
                    //    {
                    //        list = q.Select(s =>
                    //        {
                    //            var tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.SinDefinir;
                    //            switch (s.tipo) 
                    //            {
                    //                case "01":
                    //                    tipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Cargo;
                    //                    break;
                    //                case "02":
                    //                    tipo= DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Descargo;
                    //                    break;
                    //                case "03":
                    //                    tipo= DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Traslado;
                    //                    break;
                    //                case "04":
                    //                    tipo= DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Ajuste;
                    //                    break;
                    //            }
                    //            var isAnulado = false;
                    //            if (s.estatus_anulado == "1") { isAnulado = true; }

                    //            var nr = new DtoLibInventario.Movimiento.Lista.Resumen()
                    //            {
                    //                autoId = s.auto,
                    //                fecha = s.fecha,
                    //                hora = s.hora,
                    //                docConcepto = s.concepto,
                    //                docMonto = s.total,
                    //                docMotivo = s.nota,
                    //                docNro = s.documento,
                    //                docRenglones = s.renglones,
                    //                docSituacion = s.situacion,
                    //                docSucursal = s.codigo_sucursal,
                    //                docTipo = tipo,
                    //                estacion = s.estacion,
                    //                isDocAnulado = isAnulado,
                    //                usuario = s.usuario,
                    //                depositoOrigen=s.deposito,
                    //                depositoDestino=s.destino,
                    //            };
                    //            return nr;
                    //        }).ToList();
                    //    }
                    //}
                    //result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo>
            Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_deposito.ToList();
                    q = q.Where(f => f.fisica < f.nivel_minimo && f.productos.estatus.Trim().ToUpper() == "ACTIVO").ToList();
                    if (filtro.autoDeposito != "")
                    {
                        q = q.Where(f => f.auto_deposito == filtro.autoDeposito).ToList();
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        q = q.Where(f => f.productos.auto_departamento == filtro.autoDepartamento).ToList();
                    }

                    var list = new List<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _decimales = "";
                                var _empaque = "";
                                //var ent = cnn.productos_medida.Find(s.productos.auto_empaque_compra);
                                var ent = s.productos.productos_medida2;
                                if (ent != null)
                                {
                                    _decimales = ent.decimales;
                                    _empaque = ent.nombre;
                                }

                                var nr = new DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo()
                                {
                                    autoProducto = s.auto_producto,
                                    autoDepartamento = s.productos.auto_departamento,
                                    autoGrupo = s.productos.auto_grupo,
                                    categoria = s.productos.categoria,
                                    codigoProducto = s.productos.codigo,
                                    contenidEmpCompra = s.productos.contenido_compras,
                                    costoFinalCompra = s.productos.costo,
                                    costoFinalUnd = s.productos.costo_und,
                                    cntUndReponer = s.nivel_optimo - s.fisica,
                                    decimales = _decimales,
                                    empaqueCompra = _empaque,
                                    nombreProducto = s.productos.nombre,
                                };
                                return nr;
                            }).ToList();
                        }
                    }
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        //ANULAR
        public DtoLib.Resultado Producto_Movimiento_Cargo_Anular(DtoLibInventario.Movimiento.Anular.Cargo.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, " +
                            "`auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.estacion);
                        var vk = cnn.Database.ExecuteSqlCommand(sql, p1,p2,p3,p4,p5,p6,p7,p8,p9);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entMov = cnn.productos_movimientos.Find(ficha.autoDocumento);
                        if (entMov == null) 
                        {
                            result.Mensaje = "DOCUMENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entMov.estatus_anulado = "1";
                        cnn.SaveChanges();

                        sql = "update productos_movimientos_detalle set estatus_anulado='1' where auto_documento=@p1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1) ;
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR DETALLES DEL MOVIMIENTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Inventario'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var entKardex = cnn.productos_kardex.Where(w => w.auto_documento == ficha.autoDocumento && w.modulo=="Inventario").ToList();
                        foreach (var rg in entKardex)
                        {
                            var autoDeposito = rg.auto_deposito;
                            var autoProducto = rg.auto_producto;
                            var cnt = rg.cantidad_und;

                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == autoDeposito && f.auto_producto == autoProducto);
                            if (entPrdDep == null) 
                            {
                                result.Mensaje = "PRODUCTO / DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            entPrdDep.fisica -= cnt;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.Resultado Producto_Movimiento_Descargo_Anular(DtoLibInventario.Movimiento.Anular.Descargo.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, " +
                            "`auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.estacion);
                        var vk = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entMov = cnn.productos_movimientos.Find(ficha.autoDocumento);
                        if (entMov == null)
                        {
                            result.Mensaje = "DOCUMENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entMov.estatus_anulado = "1";
                        cnn.SaveChanges();

                        sql = "update productos_movimientos_detalle set estatus_anulado='1' where auto_documento=@p1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR DETALLES DEL MOVIMIENTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Inventario'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var entKardex = cnn.productos_kardex.Where(w => w.auto_documento == ficha.autoDocumento && w.modulo == "Inventario").ToList();
                        foreach (var rg in entKardex)
                        {
                            var autoDeposito = rg.auto_deposito;
                            var autoProducto = rg.auto_producto;
                            var cnt = rg.cantidad_und;

                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == autoDeposito && f.auto_producto == autoProducto);
                            if (entPrdDep == null)
                            {
                                result.Mensaje = "PRODUCTO / DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            entPrdDep.fisica += cnt;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.Resultado Producto_Movimiento_Traslado_Anular(DtoLibInventario.Movimiento.Anular.Traslado.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, " +
                            "`auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.estacion);
                        var vk = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entMov = cnn.productos_movimientos.Find(ficha.autoDocumento);
                        if (entMov == null)
                        {
                            result.Mensaje = "DOCUMENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entMov.estatus_anulado = "1";
                        cnn.SaveChanges();

                        sql = "update productos_movimientos_detalle set estatus_anulado='1' where auto_documento=@p1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR DETALLES DEL MOVIMIENTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Inventario'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var entKardex = cnn.productos_kardex.Where(w => w.auto_documento == ficha.autoDocumento && w.modulo == "Inventario").ToList();
                        foreach (var rg in entKardex)
                        {
                            var autoDeposito = rg.auto_deposito;
                            var autoProducto = rg.auto_producto;
                            var cnt = (rg.cantidad_und*rg.signo)*-1;

                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == autoDeposito && f.auto_producto == autoProducto);
                            if (entPrdDep == null)
                            {
                                result.Mensaje = "PRODUCTO / DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            entPrdDep.fisica += cnt;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado Producto_Movimiento_Ajuste_Anular(DtoLibInventario.Movimiento.Anular.Ajuste.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, " +
                            "`auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) " +
                            "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.estacion);
                        var vk = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var entMov = cnn.productos_movimientos.Find(ficha.autoDocumento);
                        if (entMov == null)
                        {
                            result.Mensaje = "DOCUMENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entMov.estatus_anulado = "1";
                        cnn.SaveChanges();

                        sql = "update productos_movimientos_detalle set estatus_anulado='1' where auto_documento=@p1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR DETALLES DEL MOVIMIENTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Inventario'";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        vk = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        var entKardex = cnn.productos_kardex.Where(w => w.auto_documento == ficha.autoDocumento && w.modulo == "Inventario").ToList();
                        foreach (var rg in entKardex)
                        {
                            var autoDeposito = rg.auto_deposito;
                            var autoProducto = rg.auto_producto;
                            var cnt = (rg.cantidad_und * rg.signo) * -1;

                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == autoDeposito && f.auto_producto == autoProducto);
                            if (entPrdDep == null)
                            {
                                result.Mensaje = "PRODUCTO / DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            entPrdDep.fisica += cnt;
                            entPrdDep.disponible = entPrdDep.fisica;
                            cnn.SaveChanges();
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        // CAPTURE DE DATA PARA MOVIMIENTO TRASLADO PRODUCTOS POR DEBAJO DEL NIVEL MINIMO 
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha> 
            Capturar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"select p.auto as autoPrd, p.codigo as codigoPrd, p.nombre as nombrePrd, p.contenido_compras as empCompraCont, 
                                  p.auto_departamento as autoDepartamento, p.auto_grupo as autoGrupo, p.categoria, 
                                  p.costo as costo, p.costo_und as costoUnd, p.divisa as costoDivisa, p.estatus_divisa as estatusDivisa,
                                  p.fecha_ult_costo as fechaUltActualizacion,
                                  eTasa.tasa as tasaIva, eTasa.nombre as tasaIvaNombre, eTasa.auto as tasaAuto,
                                  eDepo.auto as autoDeposito, eDepo.codigo as codigoDeposito, eDepo.nombre as nombreDeposito, 
                                  pDepo.fisica as exFisica, pDepo.disponible as exDisponible, pDepo.reservada as exReservada,
                                  pDepo.nivel_minimo as nivelMinimo, pDepo.nivel_optimo as nivelOptimo,
                                  eDepoOrigen.auto as autoDepositoOrigen, eDepoOrigen.codigo as codigoDepositoOrigen, eDepoOrigen.nombre as nombreDepositoOrigen, 
                                  pDepoOrigen.fisica as exFisicaOrigen, pDepoOrigen.reservada as exReservaOrigen, pDepoOrigen.disponible as exDisponibleOrigen,
                                  pMed.decimales, pMed.nombre as empCompra
                                  from productos as p 
                                  join productos_medida as pMed on p.auto_empaque_compra=pMed.auto 
                                  join productos_deposito as pDepo on p.auto=pDepo.auto_producto 
                                  join productos_deposito as pDepoOrigen on p.auto=pDepoOrigen.auto_producto and pDepoOrigen.auto_deposito=@autoDepOrigen
                                  join empresa_depositos as eDepo on pDepo.auto_deposito=eDepo.auto
                                  join empresa_depositos as eDepoOrigen on pDepoOrigen.auto_deposito=eDepoOrigen.auto
                                  join empresa_departamentos as eDepart on p.auto_departamento=eDepart.auto
                                  join empresa_tasas as eTasa on p.auto_tasa=eTasa.auto ";
                    p3.ParameterName = "@autoDepOrigen";
                    p3.Value = filtro.autoDepositoOrigen;

                    var sql_2 = @" where p.estatus='ACTIVO' and pDepo.fisica<pDepo.nivel_minimo ";
                    if (filtro.autoDepositoVerificarNivel != "")
                    {
                        p1.ParameterName = "@autoDeposito";
                        p1.Value = filtro.autoDepositoVerificarNivel;
                        sql_2 += " and pDepo.auto_deposito=@autoDeposito ";
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        p2.ParameterName = "@autoDepartamento";
                        p2.Value = filtro.autoDepartamento;
                        sql_2 += " and p.auto_departamento=@autoDepartamento ";
                    }
                    var sql = sql_1 + sql_2;
                    var lt=cnn.Database.SqlQuery<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha>(sql, p1, p2,p3).ToList();
                    result.Lista = lt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        // CAPTURE DE DATA PARA MOVIMIENTO DESCARGO 
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha> 
            Producto_Movimiento_Descargo_Capture(DtoLibInventario.Movimiento.DesCargo.CapturaMov.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@autoDeposito";
                    p1.Value = filtro.idDeposito;
                    p2.ParameterName = "@autoProducto";
                    p2.Value = filtro.idProducto;
                    var sql_1 = @"SELECT 
                                    p.auto autoPrd, 
                                    p.auto_departamento autoDepart, 
                                    p.auto_grupo autoGrupo, 
                                    p.codigo codigoPrd, 
                                    p.nombre nombrePrd, 
                                    p.categoria catPrd, 
                                    pdepo.fisica as exFisica, 
                                    p.contenido_compras contEmp, 
                                    pmed.nombre nombreEmp, 
                                    pmed.decimales, 
                                    p.costo_und costoUnd, 
                                    p.costo,
                                    p.estatus_divisa as estatusDivisa,
                                    p.divisa as costoDivisa,
                                    etasa.auto as autoTasa,
                                    etasa.nombre as descTasa,
                                    etasa.tasa as valorTasa,
                                    p.fecha_ult_costo as fechaUltActCosto
                                    from productos_deposito as pdepo
                                    join productos as p on p.auto=pdepo.auto_producto
                                    join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                                    join empresa_tasas as etasa on etasa.auto=p.auto_tasa
                                    where pdepo.auto_deposito=@autoDeposito and pdepo.auto_producto=@autoProducto";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Data>(sql, p1, p2, p3).FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Mensaje = "[ PRODUCTO / DEPOSITO ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha() { data = ent };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        // CAPTURE DE DATA PARA MOVIMIENTO CARGO
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha> 
            Producto_Movimiento_Cargo_Capture(DtoLibInventario.Movimiento.Cargo.CapturaMov.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@autoDeposito";
                    p1.Value = filtro.idDeposito;
                    p2.ParameterName = "@autoProducto";
                    p2.Value = filtro.idProducto;
                    var sql_1 = @"SELECT 
                                    p.auto autoPrd, 
                                    p.auto_departamento autoDepart, 
                                    p.auto_grupo autoGrupo, 
                                    p.codigo codigoPrd, 
                                    p.nombre nombrePrd, 
                                    p.categoria catPrd, 
                                    pdepo.fisica as exFisica, 
                                    p.contenido_compras contEmp, 
                                    pmed.nombre nombreEmp, 
                                    pmed.decimales, 
                                    p.costo_und costoUnd, 
                                    p.costo,
                                    p.estatus_divisa as estatusDivisa,
                                    p.divisa as costoDivisa,
                                    etasa.auto as autoTasa,
                                    etasa.nombre as descTasa,
                                    etasa.tasa as valorTasa,
                                    p.fecha_ult_costo as fechaUltActCosto
                                    from productos_deposito as pdepo
                                    join productos as p on p.auto=pdepo.auto_producto
                                    join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                                    join empresa_tasas as etasa on etasa.auto=p.auto_tasa
                                    where pdepo.auto_deposito=@autoDeposito and pdepo.auto_producto=@autoProducto";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Movimiento.Cargo.CapturaMov.Data>(sql, p1, p2, p3).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ PRODUCTO / DEPOSITO ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha() { data = ent };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        // CAPTURE DE DATA PARA MOVIMIENTO TRASLADO ENTRE DEPOSITO
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha> 
            Producto_Movimiento_Traslado_Capture(DtoLibInventario.Movimiento.Traslado.CapturaMov.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var xent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == filtro.idProducto 
                        && f.auto_deposito == filtro.idDepDestino);
                    if (xent == null) 
                    {
                        result.Mensaje = "[ PRODUCTO / DEPOSITO DESTINO ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@autoDeposito";
                    p1.Value = filtro.idDepOrigen;
                    p2.ParameterName = "@autoProducto";
                    p2.Value = filtro.idProducto;
                    var sql_1 = @"SELECT 
                                    p.auto autoPrd, 
                                    p.auto_departamento autoDepart, 
                                    p.auto_grupo autoGrupo, 
                                    p.codigo codigoPrd, 
                                    p.nombre nombrePrd, 
                                    p.categoria catPrd, 
                                    pdepo.fisica as exFisica, 
                                    p.contenido_compras contEmp, 
                                    pmed.nombre nombreEmp, 
                                    pmed.decimales, 
                                    p.costo_und costoUnd, 
                                    p.costo,
                                    p.estatus_divisa as estatusDivisa,
                                    p.divisa as costoDivisa,
                                    etasa.auto as autoTasa,
                                    etasa.nombre as descTasa,
                                    etasa.tasa as valorTasa,
                                    p.fecha_ult_costo as fechaUltActCosto
                                    from productos_deposito as pdepo
                                    join productos as p on p.auto=pdepo.auto_producto
                                    join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                                    join empresa_tasas as etasa on etasa.auto=p.auto_tasa
                                    where pdepo.auto_deposito=@autoDeposito and pdepo.auto_producto=@autoProducto";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Movimiento.Traslado.CapturaMov.Data>(sql, p1, p2, p3).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ PRODUCTO / DEPOSITO ORIGEN ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha() { data = ent };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        // CAPTURE DE DATA PARA MOVIMIENTO AJUSTE INVENTARIO EN CERO(0)
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha> 
            Producto_Movimiento_AjusteInventarioCero_Capture(DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@autoDeposito";
                    p1.Value = filtro.idDeposito;
                    var sql_1 = @"SELECT 
                                    p.auto autoPrd, 
                                    p.auto_departamento autoDepart, 
                                    p.auto_grupo autoGrupo, 
                                    p.codigo codigoPrd, 
                                    p.nombre nombrePrd, 
                                    p.categoria catPrd, 
                                    pdepo.fisica as exFisica, 
                                    p.contenido_compras contEmp, 
                                    pmed.nombre nombreEmp, 
                                    pmed.decimales, 
                                    p.costo_und costoUnd, 
                                    p.costo,
                                    p.estatus_divisa as estatusDivisa,
                                    p.divisa as costoDivisa,
                                    etasa.auto as autoTasa,
                                    etasa.nombre as descTasa,
                                    etasa.tasa as valorTasa
                                    from productos_deposito as pdepo
                                    join productos as p on p.auto=pdepo.auto_producto
                                    join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                                    join empresa_tasas as etasa on etasa.auto=p.auto_tasa
                                    where pdepo.auto_deposito=@autoDeposito and pdepo.fisica<>0";
                    var sql = sql_1;
                    var lt = cnn.Database.SqlQuery<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Data>(sql, p1, p2, p3).ToList();
                    result.Entidad = new DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha()
                    {
                        data = lt,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        //VERIFICAR
        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_AnularDocumento(string autoDocumento)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    rt.Entidad = true;
                    var entMov = cnn.productos_movimientos.Find(autoDocumento);
                    if (entMov == null)
                    {
                        rt.Mensaje = "[ ID ] MOVIMIENTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Entidad = false;
                        return rt;
                    }
                    if (entMov.estatus_anulado == "1")
                    {
                        rt.Mensaje = "DOCUMENTO YA ANULADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Entidad = false;
                        return rt;
                    }
                    if (entMov.fecha.Year != fechaSistema.Year || entMov.fecha.Month != fechaSistema.Month)
                    {
                        rt.Mensaje = "DOCUMENTO SE ENCUENTRA EN OTRO PERIODO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Entidad = false;
                        return rt;
                    }
                    if (entMov.estatus_cierre_contable == "1")
                    {
                        rt.Mensaje = "DOCUMENTO SE ENCUENTRA BLOQUEADO CONTABLEMENTE";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Entidad = false;
                        return rt;
                    }

                    var idDepOrigen = entMov.auto_deposito;
                    var idDepDestino = entMov.auto_destino;
                    var codSucursal = entMov.codigo_sucursal;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDepOrigen", idDepOrigen);
                    var sql1 = @"select es_activo
                                from empresa_depositos_ext as eDepExt 
                                join empresa_depositos as eDep on eDep.auto=eDepExt.auto_deposito
                                where eDep.auto=@idDepOrigen";
                    var entDepOrigen = cnn.Database.SqlQuery<string>(sql1, p1).FirstOrDefault();
                    if (entDepOrigen == null)
                    {
                        rt.Mensaje = "DEPOSITO ORIGEN NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entDepOrigen == "0")
                    {
                        rt.Mensaje = "DEPOSITO ORIGEN ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDepDestino", idDepDestino);
                    var sql2 = @"select es_activo
                                from empresa_depositos_ext as eDepExt 
                                join empresa_depositos as eDep on eDep.auto=eDepExt.auto_deposito
                                where eDep.auto=@idDepDestino";
                    var entDepDestino = cnn.Database.SqlQuery<string>(sql2, p2).FirstOrDefault();
                    if (entDepDestino == null)
                    {
                        rt.Mensaje = "DEPOSITO DESTINO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entDepDestino == "0")
                    {
                        rt.Mensaje = "DEPOSITO DESTINO ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("@codSuc", codSucursal);
                    var sql = @"select es_activo
                                from empresa_sucursal_ext as eSucExt
                                join empresa_sucursal as eSuc on eSuc.auto=eSucExt.auto_sucursal
                                where eSuc.codigo=@codSuc";
                    var entSuc = cnn.Database.SqlQuery<string>(sql, p3).FirstOrDefault();
                    if (entSuc == null)
                    {
                        rt.Mensaje = "SUCURSAL NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entSuc == "0")
                    {
                        rt.Mensaje = "SUCURSAL ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_ExistenciaDisponible(List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha> lista)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    rt.Entidad = true;
                    foreach (var rg in lista)
                    {
                        var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == rg.autoProducto && f.auto_deposito == rg.autoDeposito);
                        if (ent == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO" +
                                Environment.NewLine + "Producto: " + rg.autoProducto +
                                Environment.NewLine + "Deposito: " + rg.autoDeposito;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            rt.Entidad = false;
                            return rt;
                        }

                        if (rg.cantidadUnd > ent.fisica)
                        {
                            rt.Mensaje = "NO HAY DISPONIBILIDAD PARA " +
                                Environment.NewLine + "Producto: " + ent.productos.nombre;
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            rt.Entidad = false;
                            return rt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_CostoEdad(DtoLibInventario.Movimiento.Verificar.CostoEdad.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    if (ficha.detalles != null)
                    {
                        foreach (var rg in ficha.detalles)
                        {
                            rt.Entidad = true;
                            var ent = cnn.productos.Find(rg.autoProducto);
                            if (ent == null)
                            {
                                rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO " + rg.autoProducto;
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                rt.Entidad = false;
                                return rt;
                            }

                            if ((fechaSistema.Subtract(ent.fecha_ult_costo).Days) > ficha.dias)
                            {
                                rt.Mensaje = "COSTO EDAD PRODUCTO INCORRECTO" +
                                    Environment.NewLine + "Producto: " + ent.nombre;
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                rt.Entidad = false;
                                return rt;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_DepositoSucursalActivo(string idDepOrigen, string idDepDestino, string codSucursal)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDepOrigen", idDepOrigen);
                    var sql1 = @"select es_activo
                                from empresa_depositos_ext as eDepExt 
                                join empresa_depositos as eDep on eDep.auto=eDepExt.auto_deposito
                                where eDep.auto=@idDepOrigen";
                    var entDepOrigen = cnn.Database.SqlQuery<string>(sql1, p1).FirstOrDefault();
                    if (entDepOrigen == null)
                    {
                        rt.Mensaje = "DEPOSITO ORIGEN NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entDepOrigen == "0")
                    {
                        rt.Mensaje = "DEPOSITO ORIGEN ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDepDestino", idDepDestino);
                    var sql2 = @"select es_activo
                                from empresa_depositos_ext as eDepExt 
                                join empresa_depositos as eDep on eDep.auto=eDepExt.auto_deposito
                                where eDep.auto=@idDepDestino";
                    var entDepDestino = cnn.Database.SqlQuery<string>(sql2, p2).FirstOrDefault();
                    if (entDepDestino == null)
                    {
                        rt.Mensaje = "DEPOSITO DESTINO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entDepDestino == "0")
                    {
                        rt.Mensaje = "DEPOSITO DESTINO ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("@codSuc", codSucursal);
                    var sql = @"select es_activo
                                from empresa_sucursal_ext as eSucExt
                                join empresa_sucursal as eSuc on eSuc.auto=eSucExt.auto_sucursal
                                where eSuc.codigo=@codSuc";
                    var entSuc = cnn.Database.SqlQuery<string>(sql, p3).FirstOrDefault();
                    if (entSuc == null) 
                    {
                        rt.Mensaje = "SUCURSAL NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entSuc == "0")
                    {
                        rt.Mensaje = "SUCURSAL ESTATUS INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    rt.Entidad = true;
                    return rt;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}