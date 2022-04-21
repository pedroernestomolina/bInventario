using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.General.Ficha> Producto_Analisis_General(DtoLibInventario.Analisis.General.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Analisis.General.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaServidor = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    DateTime? desde = fechaServidor.Date;

                    var sql_1 = "SELECT abs(sum(pk.cantidad_und*pk.signo)) as cntUnd, p.auto as autoPrd, " +
                        "p.nombre as nombrePrd, p.codigo as codigoPrd, pm.decimales as decimales, " +
                        "count(*) as cntDoc ";
                    var sql_2 = " FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto ";
                    var sql_3=" where 1=1 and pk.estatus_anulado='0' ";
                    var sql_4 = " group by pk.auto_producto ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.ultimosXDias != 0) 
                    {
                        sql_3 += " and pk.fecha>=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = desde.Value.AddDays(filtro.ultimosXDias *-1);
                    }

                    if (filtro.modulo != DtoLibInventario.Analisis.Enumerados.EnumModulo.SnDefinir)
                    {
                        var modulo = "";
                        switch (filtro.modulo)
                        {
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Compras:
                                break;
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Ventas:
                                modulo = "Ventas";
                                sql_3 += " and pk.modulo=@p3 ";
                                break;
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Inventario:
                                break;
                        }
                        p3.ParameterName = "@p3";
                        p3.Value = modulo;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql_3 += "and pk.auto_deposito=@p4 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.autoDeposito;
                    }
                    var sql = sql_1+sql_2+sql_3+sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Analisis.General.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Detallado.Ficha> Producto_Analisis_Detallado(DtoLibInventario.Analisis.Detallado.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Analisis.Detallado.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaServidor = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    DateTime? desde = fechaServidor.Date;

                    var sql_1 = "SELECT abs(sum(pk.cantidad_und*pk.signo)) as cntUnd, p.auto as autoPrd, " +
                        "p.nombre as nombrePrd, p.codigo as codigoPrd, pm.decimales as decimales, " +
                        "count(*) as cntDoc, pk.fecha as fecha ";
                    var sql_2 = " FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto ";
                    var sql_3 = " where 1=1 and pk.estatus_anulado='0' ";
                    var sql_4 = " group by pk.auto_producto, pk.fecha ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.ultimosXDias != 0)
                    {
                        sql_3 += " and pk.fecha>=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = desde.Value.AddDays(filtro.ultimosXDias * -1);
                    }

                    if (filtro.modulo != DtoLibInventario.Analisis.Enumerados.EnumModulo.SnDefinir)
                    {
                        var modulo = "";
                        switch (filtro.modulo)
                        {
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Compras:
                                break;
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Ventas:
                                modulo = "Ventas";
                                sql_3 += " and pk.modulo=@p3 ";
                                break;
                            case DtoLibInventario.Analisis.Enumerados.EnumModulo.Inventario:
                                break;
                        }
                        p3.ParameterName = "@p3";
                        p3.Value = modulo;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql_3 += "and pk.auto_deposito=@p4 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.autoDeposito;
                    }
                    if (filtro.autoProducto != "")
                    {
                        sql_3 += "and pk.auto_producto=@p5 ";
                        p5.ParameterName = "@p5";
                        p5.Value = filtro.autoProducto;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Analisis.Detallado.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Existencia.Ficha> Producto_Analisis_Existencia(DtoLibInventario.Analisis.Existencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Analisis.Existencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = "SELECT p.auto as autoPrd, p.codigo as codigoPrd, p.nombre as nombrePrd, pd.fisica as cantUnd, "+
                        "pm.decimales as decimales ";
                    var sql_2 = " FROM productos_deposito as pd "+
                        " join productos as p on p.auto=pd.auto_producto "+
                        " join productos_medida as pm on p.auto_empaque_compra=pm.auto ";
                    var sql_3 = " where auto_deposito=@p1 ";
                    var sql_4 = "";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.autoDeposito;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Analisis.Existencia.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
                    rt.Lista = list;
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