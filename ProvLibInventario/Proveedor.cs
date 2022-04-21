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

        public DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Lista.Resumen> Proveedor_GetLista(DtoLibInventario.Proveedor.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Lista.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "select auto, codigo, ci_rif as ciRif, razon_social as nombreRazonSocial "+
                        "from proveedores as prv " +
                        " where 1=1 ";

                    var valor = "";
                    if (filtro.cadena != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibInventario.Proveedor.Enumerados.EnumMetodoBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and prv.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and prv.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibInventario.Proveedor.Enumerados.EnumMetodoBusqueda.Nombre )
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and prv.razon_social like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and prv.razon_social like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda ==  DtoLibInventario.Proveedor.Enumerados.EnumMetodoBusqueda.Rif )
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql += " and prv.ci_rif like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql += " and prv.ci_rif like @p";
                                valor = cad + "%";
                            }
                        }
                    }
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p", valor);
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Proveedor.Lista.Resumen>(sql, p1).ToList();
                    rt.Lista = lst;
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