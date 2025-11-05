using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{
    public partial class Provider : ILibInventario.IProvider
    {
        private DateTime
            obtenerFechaDelSistema(invEntities cnn)
        {
            var rst = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
            if (rst == null)
            {
                throw new Exception("PROBLEMA AL OBTENER FECHA DEL SISTEMA");
            }
            return rst;
        }
        private void
            actualizarContadores_MovCargo(invEntities cnn)
        {
            var _sql = @"update sistema_contadores set 
                            a_productos_movimientos=a_productos_movimientos+1, 
                            a_productos_movimientos_cargos=a_productos_movimientos_cargos+1";
            var rst = cnn.Database.ExecuteSqlCommand(_sql);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL ACTUALIZAR CONTADORES");
            }
        }
        private void
            actualizarContadores_MovDescargo(invEntities cnn)
        {
            var _sql = @"update sistema_contadores set 
                            a_productos_movimientos=a_productos_movimientos+1, 
                            a_productos_movimientos_descargos=a_productos_movimientos_descargos+1";
            var rst = cnn.Database.ExecuteSqlCommand(_sql);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL ACTUALIZAR CONTADORES");
            }
        }
        private void
            actualizarContadores_MovTraslado(invEntities cnn)
        {
            var _sql = @"update sistema_contadores set 
                            a_productos_movimientos=a_productos_movimientos+1, 
                            a_productos_movimientos_traslados=a_productos_movimientos_traslados+1";
            var rst = cnn.Database.ExecuteSqlCommand(_sql);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL ACTUALIZAR CONTADORES");
            }
        }
        private void
            actualizarContadores_MovTrasladoPorDevolucion(invEntities cnn)
        {
            var _sql = @"update sistema_contadores set 
                            a_productos_movimientos=a_productos_movimientos+1, 
                            a_productos_movimientos_traslados_dev=a_productos_movimientos_traslados_dev+1";
            var rst = cnn.Database.ExecuteSqlCommand(_sql);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL ACTUALIZAR CONTADORES");
            }
        }
        private DtoLibInventario.MovimientoInsertar.contadorMovimiento
            obtenerContadores_MovCargo(invEntities cnn)
        {
            var _sql = @"select 
                            a_productos_movimientos as movimiento,
                            a_productos_movimientos_cargos as numeroMovimiento
                        from sistema_contadores";
            var _ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoInsertar.contadorMovimiento>(_sql).FirstOrDefault();
            if (_ent == null)
            {
                throw new Exception("PROBLEMA AL OBTENER CONTADOR MOVIMIENTO");
            }
            return _ent;
        }
        private DtoLibInventario.MovimientoInsertar.contadorMovimiento
            obtenerContadores_MovDescargo(invEntities cnn)
        {
            var _sql = @"select 
                            a_productos_movimientos as movimiento,
                            a_productos_movimientos_descargos as numeroMovimiento
                        from sistema_contadores";
            var _ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoInsertar.contadorMovimiento>(_sql).FirstOrDefault();
            if (_ent == null)
            {
                throw new Exception("PROBLEMA AL OBTENER CONTADOR MOVIMIENTO");
            }
            return _ent;
        }
        private DtoLibInventario.MovimientoInsertar.contadorMovimiento
            obtenerContadores_MovTraslado(invEntities cnn)
        {
            var _sql = @"select 
                            a_productos_movimientos as movimiento,
                            a_productos_movimientos_traslados as numeroMovimiento
                        from sistema_contadores";
            var _ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoInsertar.contadorMovimiento>(_sql).FirstOrDefault();
            if (_ent == null)
            {
                throw new Exception("PROBLEMA AL OBTENER CONTADOR MOVIMIENTO");
            }
            return _ent;
        }
        private DtoLibInventario.MovimientoInsertar.contadorMovimiento
            obtenerContadores_MovTrasladoPorDevolucion(invEntities cnn)
        {
            var _sql = @"select 
                            a_productos_movimientos as movimiento,
                            a_productos_movimientos_traslados_dev as numeroMovimiento
                        from sistema_contadores";
            var _ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoInsertar.contadorMovimiento>(_sql).FirstOrDefault();
            if (_ent == null)
            {
                throw new Exception("PROBLEMA AL OBTENER CONTADOR MOVIMIENTO");
            }
            return _ent;
        }
        private void
            insertarEncabezadoMov(
            invEntities cnn,
            DateTime fechaSistema,
            string autoMov,
            string numDoc,
            DtoLibInventario.MovimientoInsertar.Encabezado ficha)
        {
            var _sql = @"INSERT INTO productos_movimientos (
                            auto, 
                            documento, 
                            fecha, 
                            nota, 
                            estatus_anulado, 
                            usuario, 
                            codigo_usuario, 
                            hora, 
                            estacion, 
                            concepto, 
                            auto_concepto, 
                            codigo_concepto, 
                            auto_usuario, 
                            auto_deposito, 
                            codigo_deposito, 
                            deposito, 
                            auto_destino,
                            codigo_destino, 
                            destino, 
                            tipo, 
                            renglones,
                            documento_nombre, 
                            autorizado, 
                            total, 
                            estatus_cierre_contable, 
                            situacion, 
                            auto_remision, 
                            codigo_sucursal, 
                            cierre_ftp)
                        VALUES (
                            @auto, 
                            @documento, 
                            @fecha, 
                            @nota, 
                            @estatus_anulado, 
                            @usuario, 
                            @codigo_usuario, 
                            @hora, 
                            @estacion, 
                            @concepto, 
                            @auto_concepto, 
                            @codigo_concepto, 
                            @auto_usuario, 
                            @auto_deposito, 
                            @codigo_deposito, 
                            @deposito, 
                            @auto_destino,
                            @codigo_destino, 
                            @destino, 
                            @tipo, 
                            @renglones,
                            @documento_nombre, 
                            @autorizado, 
                            @total, 
                            @estatus_cierre_contable, 
                            @situacion, 
                            @auto_remision, 
                            @codigo_sucursal, 
                            @cierre_ftp)";
            //
            var p00 = new MySql.Data.MySqlClient.MySqlParameter("@auto", autoMov);
            var p01 = new MySql.Data.MySqlClient.MySqlParameter("@documento", numDoc);
            var p02 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
            var p03 = new MySql.Data.MySqlClient.MySqlParameter("@nota", ficha.nota);
            var p04 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado", ficha.estatusAnulado);
            var p05 = new MySql.Data.MySqlClient.MySqlParameter("@usuario", ficha.usuario);
            var p06 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_usuario", ficha.codUsuario);
            var p07 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
            var p08 = new MySql.Data.MySqlClient.MySqlParameter("@estacion", ficha.estacion);
            var p09 = new MySql.Data.MySqlClient.MySqlParameter("@concepto", ficha.desConcepto);
            //
            var p10 = new MySql.Data.MySqlClient.MySqlParameter("@auto_concepto", ficha.autoConcepto);
            var p11 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_concepto", ficha.codConcepto);
            var p12 = new MySql.Data.MySqlClient.MySqlParameter("@auto_usuario", ficha.autoUsuario);
            var p13 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", ficha.autoDepositoOrigen);
            var p14 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_deposito", ficha.codDepositoOrigen);
            var p15 = new MySql.Data.MySqlClient.MySqlParameter("@deposito", ficha.desDepositoOrigen);
            var p16 = new MySql.Data.MySqlClient.MySqlParameter("@auto_destino", ficha.autoDepositoDestino);
            var p17 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_destino", ficha.codDepositoDestino);
            var p18 = new MySql.Data.MySqlClient.MySqlParameter("@destino", ficha.desDepositoDestino);
            var p19 = new MySql.Data.MySqlClient.MySqlParameter("@tipo", ficha.tipo);
            //
            var p20 = new MySql.Data.MySqlClient.MySqlParameter("@renglones", ficha.renglones);
            var p21 = new MySql.Data.MySqlClient.MySqlParameter("@documento_nombre", ficha.documentoNombre);
            var p22 = new MySql.Data.MySqlClient.MySqlParameter("@autorizado", ficha.autorizado);
            var p23 = new MySql.Data.MySqlClient.MySqlParameter("@total", ficha.total);
            var p24 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_cierre_contable", ficha.estatusCierreContable);
            var p25 = new MySql.Data.MySqlClient.MySqlParameter("@situacion", ficha.situacion);
            var p26 = new MySql.Data.MySqlClient.MySqlParameter("@auto_remision", ficha.autoRemision);
            var p27 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_sucursal", ficha.codigoSucursal);
            var p28 = new MySql.Data.MySqlClient.MySqlParameter("@cierre_ftp", ficha.cierreFtp);
            //
            var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                            p00, p01, p02, p03, p04, p05, p06, p07, p08, p09,
                                            p10, p11, p12, p13, p14, p15, p16, p17, p18, p19,
                                            p20, p21, p22, p23, p24, p25, p26, p27, p28);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL INSERTAR MOVIMIENTO PASO 1");
            }
            cnn.SaveChanges();
            //
            _sql = @"INSERT INTO productos_movimientos_extra (
                        id, 
                        auto_movimiento, 
                        factor_cambio, 
                        monto_divisa) 
                    VALUES (
                        NULL, 
                        @auto_movimiento, 
                        @factor_cambio, 
                        @monto_divisa)";
            //
            p00 = new MySql.Data.MySqlClient.MySqlParameter("@auto_movimiento", autoMov);
            p01 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio", ficha.factorCambio);
            p02 = new MySql.Data.MySqlClient.MySqlParameter("@monto_divisa", ficha.montoDivisa);
            rst = cnn.Database.ExecuteSqlCommand(_sql, p00, p01, p02);
            if (rst == 0)
            {
                throw new Exception("PROBLEMA AL INSERTAR MOVIMIENTO PASO 2");
            }
            cnn.SaveChanges();
        }
        private void
            insertarDetallesMov(
            invEntities cnn,
            DateTime fechaSistema,
            string autoMov,
            string numDoc,
            List<DtoLibInventario.MovimientoInsertar.Detalle> list)
        {
            var _sql = @"INSERT INTO productos_movimientos_detalle (
                            auto_documento, 
                            auto_producto, 
                            codigo, 
                            nombre,
                            cantidad, 
                            cantidad_bono, 
                            cantidad_und, 
                            categoria, 
                            fecha, 
                            tipo, 
                            estatus_anulado, 
                            contenido_empaque, 
                            empaque, 
                            decimales, 
                            auto, 
                            costo_und, 
                            total, 
                            costo_compra, 
                            estatus_unidad, 
                            signo, 
                            existencia, 
                            fisica, 
                            auto_departamento, 
                            auto_grupo, 
                            cierre_ftp)
                        VALUES ( 
                            @auto_documento, 
                            @auto_producto, 
                            @codigo, 
                            @nombre,
                            @cantidad, 
                            @cantidad_bono, 
                            @cantidad_und, 
                            @categoria, 
                            @fecha, 
                            @tipo, 
                            @estatus_anulado, 
                            @contenido_empaque, 
                            @empaque, 
                            @decimales, 
                            @auto, 
                            @costo_und, 
                            @total, 
                            @costo_compra, 
                            @estatus_unidad, 
                            @signo, 
                            @existencia, 
                            @fisica, 
                            @auto_departamento, 
                            @auto_grupo, 
                            @cierre_ftp)";
            var _auto = 0;
            foreach (var rg in list)
            {
                _auto += 1;
                var xauto = _auto.ToString().Trim().PadLeft(10, '0');
                //
                var p00 = new MySql.Data.MySqlClient.MySqlParameter("@auto_documento", autoMov);
                var p01 = new MySql.Data.MySqlClient.MySqlParameter("@auto_producto", rg.autoProducto);
                var p02 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", rg.codigoProducto);
                var p03 = new MySql.Data.MySqlClient.MySqlParameter("@nombre", rg.nombreProducto);
                var p04 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad", rg.cantidad);
                var p05 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad_bono", rg.cantidadBono);
                var p06 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad_und", rg.cantidadUnd);
                var p07 = new MySql.Data.MySqlClient.MySqlParameter("@categoria", rg.categoria);
                var p08 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                var p09 = new MySql.Data.MySqlClient.MySqlParameter("@tipo", rg.tipo);
                //
                var p10 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado", rg.estatusAnulado);
                var p11 = new MySql.Data.MySqlClient.MySqlParameter("@contenido_empaque", rg.contEmpaque);
                var p12 = new MySql.Data.MySqlClient.MySqlParameter("@empaque", rg.empaque);
                var p13 = new MySql.Data.MySqlClient.MySqlParameter("@decimales", rg.decimales);
                var p14 = new MySql.Data.MySqlClient.MySqlParameter("@auto", xauto);
                var p15 = new MySql.Data.MySqlClient.MySqlParameter("@costo_und", rg.costoUnd);
                var p16 = new MySql.Data.MySqlClient.MySqlParameter("@total", rg.total);
                var p17 = new MySql.Data.MySqlClient.MySqlParameter("@costo_compra", rg.costoCompra);
                var p18 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_unidad", rg.estatusUnidad);
                var p19 = new MySql.Data.MySqlClient.MySqlParameter("@signo", rg.signo);
                //
                var p20 = new MySql.Data.MySqlClient.MySqlParameter("@existencia", MySql.Data.MySqlClient.MySqlDbType.Decimal);
                p20.Value = 0m;
                var p21 = new MySql.Data.MySqlClient.MySqlParameter("@fisica", MySql.Data.MySqlClient.MySqlDbType.Decimal);
                p21.Value = 0m;
                var p22 = new MySql.Data.MySqlClient.MySqlParameter("@auto_departamento", rg.autoDepartamento);
                var p23 = new MySql.Data.MySqlClient.MySqlParameter("@auto_grupo", rg.autoGrupo);
                var p24 = new MySql.Data.MySqlClient.MySqlParameter("@cierre_ftp", rg.cierreFtp);
                //
                var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                                p00, p01, p02, p03, p04, p05, p06, p07, p08, p09,
                                                p10, p11, p12, p13, p14, p15, p16, p17, p18, p19,
                                                p20, p21, p22, p23, p24);
                if (rst == 0)
                {
                    throw new Exception("PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE [ " + Environment.NewLine + rg.nombreProducto + " ]");
                }
            };
        }
        private void
            insertarMovKardex(
            invEntities cnn,
            DateTime fechaSistema,
            string autoMov,
            string numDoc,
            List<DtoLibInventario.MovimientoInsertar.Kardex> list)
        {
            var _sql = @"INSERT INTO productos_kardex (
                                                        auto_producto, 
                                                        total, 
                                                        auto_deposito, 
                                                        auto_concepto, 
                                                        auto_documento,
                                                        fecha, 
                                                        hora, 
                                                        documento, 
                                                        modulo, 
                                                        entidad,
                                                        signo, 
                                                        cantidad, 
                                                        cantidad_bono, 
                                                        cantidad_und, 
                                                        costo_und,
                                                        estatus_anulado, 
                                                        nota, 
                                                        precio_und, 
                                                        codigo, 
                                                        siglas,
                                                        codigo_sucursal, 
                                                        cierre_ftp, 
                                                        codigo_deposito, 
                                                        nombre_deposito, 
                                                        codigo_concepto, 
                                                        nombre_concepto, 
                                                        factor_cambio) 
                                                    VALUES (
                                                        @auto_producto, 
                                                        @total, 
                                                        @auto_deposito, 
                                                        @auto_concepto, 
                                                        @auto_documento,
                                                        @fecha, 
                                                        @hora, 
                                                        @documento, 
                                                        @modulo, 
                                                        @entidad,
                                                        @signo, 
                                                        @cantidad, 
                                                        @cantidad_bono, 
                                                        @cantidad_und, 
                                                        @costo_und,
                                                        @estatus_anulado, 
                                                        @nota, 
                                                        @precio_und, 
                                                        @codigo, 
                                                        @siglas,
                                                        @codigo_sucursal, 
                                                        @cierre_ftp, 
                                                        @codigo_deposito, 
                                                        @nombre_deposito, 
                                                        @codigo_concepto, 
                                                        @nombre_concepto, 
                                                        @factor_cambio)";
            foreach (var rg in list)
            {
                var p00 = new MySql.Data.MySqlClient.MySqlParameter("@auto_producto", rg.autoProducto);
                var p01 = new MySql.Data.MySqlClient.MySqlParameter("@total", rg.total);
                var p02 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", rg.autoDeposito);
                var p03 = new MySql.Data.MySqlClient.MySqlParameter("@auto_concepto", rg.autoConcepto);
                var p04 = new MySql.Data.MySqlClient.MySqlParameter("@auto_documento", autoMov);
                var p05 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                var p06 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                var p07 = new MySql.Data.MySqlClient.MySqlParameter("@documento", numDoc);
                var p08 = new MySql.Data.MySqlClient.MySqlParameter("@modulo", rg.modulo);
                var p09 = new MySql.Data.MySqlClient.MySqlParameter("@entidad", rg.entidad);
                //
                var p10 = new MySql.Data.MySqlClient.MySqlParameter("@signo", rg.signoMov);
                var p11 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad", rg.cantidad);
                var p12 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad_bono", rg.cantidadBono);
                var p13 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad_und", rg.cantidadUnd);
                var p14 = new MySql.Data.MySqlClient.MySqlParameter("@costo_und", rg.costoUnd);
                var p15 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado", rg.estatusAnulado);
                var p16 = new MySql.Data.MySqlClient.MySqlParameter("@nota", rg.nota);
                var p17 = new MySql.Data.MySqlClient.MySqlParameter("@precio_und", rg.precioUnd);
                var p18 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", rg.codigoMov);
                var p19 = new MySql.Data.MySqlClient.MySqlParameter("@siglas", rg.siglasMov);
                //
                var p20 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_sucursal", rg.codigoSucursal);
                var p21 = new MySql.Data.MySqlClient.MySqlParameter("@cierre_ftp", rg.cierreFtp);
                var p22 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_deposito", rg.codigoDeposito);
                var p23 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_deposito", rg.nombreDeposito);
                var p24 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_concepto", rg.codigoConcepto);
                var p25 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_concepto", rg.nombreConcepto);
                var p26 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio", rg.factorCambio);
                //
                var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                                        p00, p01, p02, p03, p04, p05, p06, p07, p08, p09,
                                                        p10, p11, p12, p13, p14, p15, p16, p17, p18, p19,
                                                        p20, p21, p22, p23, p24, p25, p26);
                if (rst == 0)
                {
                    throw new Exception("PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + rg.nombreProducto + " ]");
                }
            };
            cnn.SaveChanges();
        }
        private void
            actualizarDeposito(
            invEntities cnn,
            List<DtoLibInventario.MovimientoInsertar.Deposito> list)
        {
            var _sql = @"update productos_deposito set 
                                fisica=fisica+@cntUnd,
                                disponible=disponible+@cntUnd
                            where auto_producto=@idPrd and auto_deposito=@idDeposito";
            foreach (var rg in list)
            {
                var p01 = new MySql.Data.MySqlClient.MySqlParameter("@cntUnd", rg.cantidadUnd);
                var p02 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", rg.autoProducto);
                var p03 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", rg.autoDeposito);
                var rst = cnn.Database.ExecuteSqlCommand(_sql, p01, p02, p03);
                if (rst == 0)
                {
                    throw new Exception("PROBLEMA AL ACTUALIZAR DEPOSITO, PRODUCTO: " + rg.nombreProducto + Environment.NewLine + ", DEPOSITO: " + rg.nombreDeposito);
                }
            };
            cnn.SaveChanges();
        }
        private void 
            verificarDepositoExistenciaNegativa(
            invEntities cnn, 
            List<DtoLibInventario.MovimientoInsertar.Deposito> list)
        {
            var _sql = @"select 
                            fisica as exFisica, 
                            disponible as exDisponible
                        from productos_deposito 
                        where auto_producto=@idPrd and auto_deposito=@idDeposito";
            foreach (var rg in list)
            {
                var p01 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", rg.autoProducto);
                var p02 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", rg.autoDeposito);
                var _ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoInsertar.verificarDeposito>(_sql, p01, p02).FirstOrDefault();
                if (_ent== null)
                {
                    throw new Exception("PROBLEMA AL VERIFICAR DEPOSITO " + Environment.NewLine + "PRODUCTO: " + rg.nombreProducto + Environment.NewLine + "DEPOSITO: " + rg.nombreDeposito);
                }
                if (_ent.exFisica < 0m) 
                {
                    throw new Exception("PROBLEMA AL VERIFICAR EXISTENCIA DEPOSITO (FISICO) " + Environment.NewLine + "PRODUCTO: " + rg.nombreProducto + Environment.NewLine + "DEPOSITO: " + rg.nombreDeposito);
                }
                if (_ent.exDisponible < 0m)
                {
                    throw new Exception("PROBLEMA AL VERIFICAR EXISTENCIA DEPOSITO (DISPONIBLE) "+Environment.NewLine+"PRODUCTO: " + rg.nombreProducto + Environment.NewLine + "DEPOSITO: " + rg.nombreDeposito);
                }
            };
            cnn.SaveChanges();
        }
        //
        public DtoLib.ResultadoEntidad<string>
            insertarMovimientoCargo(DtoLibInventario.MovimientoInsertar.Cargo.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _fechaSistema = obtenerFechaDelSistema(cnn);
                        actualizarContadores_MovCargo(cnn);
                        var _contadores = obtenerContadores_MovCargo(cnn);
                        var autoMov = _contadores.movimiento.ToString().Trim().PadLeft(10, '0');
                        var numDoc = _contadores.numeroMovimiento.ToString().Trim().PadLeft(10, '0');
                        insertarEncabezadoMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movEncabezado);
                        insertarDetallesMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movDetalles);
                        insertarMovKardex(cnn, _fechaSistema, autoMov, numDoc, ficha.movKardex);
                        actualizarDeposito(cnn, ficha.movDeposito);
                        ts.Complete();
                        //
                        rt.Entidad = autoMov;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(Helpers.MYSQL_VerificaError(ex));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(Helpers.ENTITY_VerificaError(ex));
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoEntidad<string>
            insertarMovimientoDescargo(DtoLibInventario.MovimientoInsertar.Descargo.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _fechaSistema = obtenerFechaDelSistema(cnn);
                        actualizarContadores_MovDescargo(cnn);
                        var _contadores = obtenerContadores_MovDescargo(cnn);
                        var autoMov = _contadores.movimiento.ToString().Trim().PadLeft(10, '0');
                        var numDoc = _contadores.numeroMovimiento.ToString().Trim().PadLeft(10, '0');
                        insertarEncabezadoMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movEncabezado);
                        insertarDetallesMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movDetalles);
                        insertarMovKardex(cnn, _fechaSistema, autoMov, numDoc, ficha.movKardex);
                        actualizarDeposito(cnn, ficha.movDeposito);
                        verificarDepositoExistenciaNegativa(cnn, ficha.movDeposito);
                        ts.Complete();
                        //
                        rt.Entidad = autoMov;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(Helpers.MYSQL_VerificaError(ex));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(Helpers.ENTITY_VerificaError(ex));
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoEntidad<string>
            insertarMovimientoTraslado(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _fechaSistema = obtenerFechaDelSistema(cnn);
                        actualizarContadores_MovTraslado(cnn);
                        var _contadores = obtenerContadores_MovTraslado(cnn);
                        var autoMov = _contadores.movimiento.ToString().Trim().PadLeft(10, '0');
                        var numDoc = _contadores.numeroMovimiento.ToString().Trim().PadLeft(10, '0');
                        insertarEncabezadoMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movEncabezado);
                        insertarDetallesMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movDetalles);
                        insertarMovKardex(cnn, _fechaSistema, autoMov, numDoc, ficha.movKardex);
                        actualizarDeposito(cnn, ficha.movDepositoOrigen);
                        actualizarDeposito(cnn, ficha.movDepositoDestino);
                        verificarDepositoExistenciaNegativa(cnn, ficha.movDepositoOrigen);
                        ts.Complete();
                        //
                        rt.Entidad = autoMov;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(Helpers.MYSQL_VerificaError(ex));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(Helpers.ENTITY_VerificaError(ex));
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoEntidad<string> 
            insertarMovimientoTrasladoPorDevolucion(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _fechaSistema = obtenerFechaDelSistema(cnn);
                        actualizarContadores_MovTrasladoPorDevolucion(cnn);
                        var _contadores = obtenerContadores_MovTrasladoPorDevolucion(cnn);
                        var autoMov = _contadores.movimiento.ToString().Trim().PadLeft(10, '0');
                        var numDoc = _contadores.numeroMovimiento.ToString().Trim().PadLeft(10, '0');
                        insertarEncabezadoMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movEncabezado);
                        insertarDetallesMov(cnn, _fechaSistema, autoMov, numDoc, ficha.movDetalles);
                        insertarMovKardex(cnn, _fechaSistema, autoMov, numDoc, ficha.movKardex);
                        actualizarDeposito(cnn, ficha.movDepositoOrigen);
                        actualizarDeposito(cnn, ficha.movDepositoDestino);
                        verificarDepositoExistenciaNegativa(cnn, ficha.movDepositoOrigen);
                        ts.Complete();
                        //
                        rt.Entidad = autoMov;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(Helpers.MYSQL_VerificaError(ex));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(Helpers.ENTITY_VerificaError(ex));
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
    }
}
