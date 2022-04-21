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

        public DtoLib.ResultadoId 
            Transito_Movimiento_Agregar(DtoLibInventario.Transito.Movimiento.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var s=ficha.mov;
                        var entMov = new productos_movimientos_transito()
                        {
                            autoriza = s.autoriza,
                            cntRenglones = s.cntRenglones,
                            codigoMov = s.codigoMov,
                            desConcepto = s.descConcepto,
                            desDepDestino = s.descDepDestino,
                            desDepOrigen = s.descDepOrigen,
                            desMov = s.descMov,
                            desSucDestino = s.descSucDestino,
                            desSucOrigen = s.descSucOrigen,
                            desUsuario = s.descUsuario,
                            estacionEquipo = s.estacionEquipo,
                            factorCambio = s.factorCambio,
                            fecha = fechaSistema.Date,
                            idConcepto = s.idConcepto,
                            idDepDestino = s.idDepDestino,
                            idDepOrigen = s.idDeOrigen,
                            idSucDestino = s.idSucDestino,
                            idSucOrigen = s.idSucOrigen,
                            monto = s.monto,
                            montoDivisa = s.montoDivisa,
                            motivo = s.motivo,
                            tipoMov = s.tipoMov,
                        };
                        cnn.productos_movimientos_transito.Add(entMov);
                        cnn.SaveChanges();

                        foreach (var rg in ficha.detalles) 
                        {
                            var det = new productos_movimientos_transito_detalle()
                            {
                                idTransito = entMov.id,
                                autoDepart = rg.autoDepart,
                                autoGrupo = rg.autoGrupo,
                                autoProd = rg.autoPrd,
                                autoTasa = rg.autoTasa,
                                categoriaProd = rg.catPrd,
                                codigoProd = rg.codigoPrd,
                                contEmpaque = rg.contEmp,
                                costo = rg.costo,
                                costoUnd = rg.costoUnd,
                                costoDivisa = rg.costoDivisa,
                                costoDivisaUnd = rg.costoDivisaUnd,
                                decimales = rg.decimales,
                                descEmpaque = rg.nombreEmp,
                                descTasa = rg.descTasa,
                                esAdmDivisa = rg.estatusDivisa,
                                exFisica = rg.exFisica,
                                fechaUltActCosto = rg.fechaUltActCosto,
                                nombreProd = rg.nombrePrd,
                                valorTasa = rg.valorTasa,
                                exFisicaDestino= rg.exFisicaDestino,
                                nivelMinimo = rg.nivelMinimo,
                                nivelOptimo = rg.nivelOptimo,
                                //
                                cantSolicitada = rg.cantidadSolicitada,
                                costoSolicitado = rg.costoSolicitada,
                                empaqueIdSolicitado = rg.empaqueIdSolicitada,
                                ajusteIdSolicitado = rg.ajusteIdSolicitada,
                            };
                            cnn.productos_movimientos_transito_detalle.Add(det);
                            cnn.SaveChanges();
                        }

                        ts.Complete();
                        result.Id = entMov.id;
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha> 
            Transito_Movimiento_GetById(int idMov)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idMov);
                    var sql_1 = @"select * 
                                    from productos_movimientos_transito
                                    where id=@idMov";
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Transito.Movimiento.Entidad.Mov>(sql_1, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Mensaje = "ID MOVIMIENTO TRANSITO NO REGISTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idMov);
                    var sql_2 = @"select * 
                                    from productos_movimientos_transito_detalle
                                    where idTransito=@idMov";
                    var entDet = cnn.Database.SqlQuery<DtoLibInventario.Transito.Movimiento.Entidad.Detalle>(sql_2, p2).ToList();
                    if (entDet == null)
                    {
                        result.Mensaje = "ID MOVIMIENTO TRANSITO NO REGISTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (entDet.Count==0)
                    {
                        result.Mensaje = "MOVIMIENTO TRANSITO NO POSEE DETALLES REGISTRADOS";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = new DtoLibInventario.Transito.Movimiento.Entidad.Ficha()
                    {
                        mov = ent,
                        detalles = entDet,
                    };
                    return result;
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
        public DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha> 
            Transito_Movimiento_GetLista(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_movimientos_transito.
                        Where(w => w.codigoMov == filtro.codMov && w.tipoMov == filtro.tipMov).
                        ToList();
                    var lst = new List<DtoLibInventario.Transito.Movimiento.Lista.Ficha>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            lst = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Transito.Movimiento.Lista.Ficha()
                                {
                                    id = s.id,
                                    fecha = s.fecha,
                                    cntRenglones = s.cntRenglones,
                                    descConcepto = s.desConcepto,
                                    descDepDestino = s.desDepDestino,
                                    descDepOrigen = s.desDepOrigen,
                                    descMov = s.desMov,
                                    descSucDestino = s.desSucDestino,
                                    descSucOrigen = s.desSucOrigen,
                                    descUsuario = s.desUsuario,
                                    factorCambio = s.factorCambio,
                                    monto = s.monto,
                                    montoDivisa = s.montoDivisa,
                                    motivo = s.motivo,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado 
            Transito_Movimiento_AnularById(int idMov)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idMov);
                        var sql_1 = "delete from productos_movimientos_transito_detalle where idTransito=@idMov";
                        var r= cnn.Database.ExecuteSqlCommand(sql_1, p1);
                        if (r == 0) 
                        {
                            result.Mensaje = "MOVIMIENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", idMov);
                        var sql_2 = "delete from productos_movimientos_transito where id=@idMov";
                        var r2 = cnn.Database.ExecuteSqlCommand(sql_2, p2);
                        if (r2 == 0)
                        {
                            result.Mensaje = "MOVIMIENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ts.Complete();
                        return result;
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
        public DtoLib.ResultadoEntidad<int> 
            Transito_Movimiento_GetCnt(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_movimientos_transito.
                        Where(w=>w.codigoMov==filtro.codMov && w.tipoMov==filtro.tipMov).
                        ToList();
                    result.Entidad = q.Count();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}