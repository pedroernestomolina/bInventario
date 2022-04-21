using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {
        
        public DtoLib.ResultadoLista<DtoLibInventario.MonitorPos.Entidad.Ficha> MonitorPos_VentaResumen_GetLista(DtoLibInventario.MonitorPos.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.MonitorPos.Entidad.Ficha> ();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    MySqlTransaction tr = null;
                    cn.Open();

                    var lst = new List<DtoLibInventario.MonitorPos.Entidad.Ficha>();
                    try
                    {
                        var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                        p0.ParameterName = "codSuc";
                        p0.Value = filtro.codSucursal;
                        var sql0 = @"SELECT autoProducto, cnt
                                FROM venta_resumen 
                                where codSucursal=@codSuc";
                        var comando1 = new MySqlCommand(sql0, cn);
                        comando1.Parameters.Clear();
                        comando1.Parameters.Add(p0);
                        var rd = comando1.ExecuteReader();
                        while (rd.Read())
                        {
                            var nr = new DtoLibInventario.MonitorPos.Entidad.Ficha()
                            {
                                autoProducto = rd.GetString("autoProducto"),
                                cnt = rd.GetDecimal("cnt"),
                            };
                            lst.Add(nr);
                        }
                        rd.Close();

                        tr = cn.BeginTransaction();
                        var sql1 = @"delete from venta_resumen where codSucursal=@codSuc";
                        var comando2 = new MySqlCommand(sql1, cn, tr);
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "codSuc";
                        p1.Value = filtro.codSucursal;
                        comando2.Parameters.Clear();
                        comando2.Parameters.Add(p1);
                        comando2.ExecuteNonQuery();
                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }

                    result.Lista = lst;
                };
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