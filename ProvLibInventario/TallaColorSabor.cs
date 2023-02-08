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
        public DtoLib.ResultadoEntidad<DtoLibInventario.TallaColorSabor.Existencia.Ficha> 
            TallaColorSabor_ExDep(DtoLibInventario.TallaColorSabor.Existencia.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.TallaColorSabor.Existencia.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", filtro.autoPrd);
                    var xp2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var xsql_1 = @"select 
                                    prd.auto as idPrd,
                                    prd.nombre as nombrePrd, 
                                    prdTalla.id  as idTCS,
                                    prdTalla.descripcion as nombreTCS,
                                    prd.estatus_talla_color_sabor as estatusTCS,
                                    empDep.auto as idDep,
                                    empDep.nombre as nombreDep,
                                    sum(prdDepTalla.fisica) as fisica, 
                                    sum(prdDepTalla.reservada) as reservada, 
                                    sum(prdDepTalla.disponible) as disponible
                                from productos_deposito_talla_color_sabor as prdDepTalla
                                join productos_talla_color_sabor as prdTalla on prdDepTalla.id_prd_talla_color_sabor=prdTalla.id
                                join productos as prd on prd.auto=prdDepTalla.auto_producto
                                join empresa_depositos as empDep on empDep.auto=prdDepTalla.auto_deposito ";
                    var xsql_2 = @" where prdDepTalla.auto_producto=@autoPrd ";
                    var xsql_3 = @" group by prd.nombre, prdTalla.descripcion, empDep.nombre, prd.auto, prdTalla.id, empDep.auto ";
                    if (filtro.autoDep.Trim() != "") 
                    {
                        xp2.ParameterName = "@autoDep";
                        xp2.Value = filtro.autoDep;
                        xsql_2 += " and prdDepTalla.auto_deposito=@autoDep ";
                    }
                    var xsql = xsql_1 + xsql_2 + xsql_3;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TallaColorSabor.Existencia.ExDepTCS>(xsql, xp1, xp2).ToList();
                    result.Entidad = new DtoLibInventario.TallaColorSabor.Existencia.Ficha()
                    {
                        ExDep = _lst,
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
    }
}