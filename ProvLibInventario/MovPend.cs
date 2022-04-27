using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.MovPend.Lista.Ficha> 
            MovPend_GetLista(DtoLibInventario.MovPend.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.MovPend.Lista.Ficha>();

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

                    var sql_1 = @"SELECT 
                                    id,
                                    fecha, 
                                    autoriza, 
                                    motivo, 
                                    monto,
                                    montoDivisa, 
                                    factorCambio,
                                    cntRenglones, 
                                    codigoMov, 
                                    tipoMov, 
                                    desMov as descripcionMov,
                                    desSucOrigen as sucOrigen, 
                                    desSucDestino as sucDestino,
                                    desDepOrigen as depOrigen, 
                                    desDepDestino as depDestino,
                                    desConcepto as concepto, 
                                    desUsuario as usuario
                                    FROM productos_movimientos_transito";
                    var sql_2 = @"";
                    var sql_3 = @" where 1=1 ";
                    if (filtro.codMov!="")
                    {
                        p1.ParameterName = "@codMov";
                        p1.Value = filtro.codMov;
                        sql_3 += " and codigoMov = @codMov ";
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.MovPend.Lista.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
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
        public DtoLib.Resultado
            MovPend_Anular(int idMov)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entMov = cnn.productos_movimientos_transito.Find(idMov);
                        if (entMov == null)
                        {
                            result.Mensaje = "MOVIMIENTO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var sql = "delete from productos_movimientos_transito_detalle where idTransito=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", idMov);
                        var v1 = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR MOVIMIENTO PENDIENTE DETALLE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        sql = "delete from productos_movimientos_transito where id=@p2";
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", idMov);
                        v1 = cnn.Database.ExecuteSqlCommand(sql, p2);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR MOVIMIENTO PENDIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
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