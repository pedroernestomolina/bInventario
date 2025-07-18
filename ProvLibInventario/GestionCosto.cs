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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto.Ficha> 
            Producto_GestionCosto_CapturarDataPrdEditarCosto(string idPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto.Ficha>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd",idPrd);
                    var _sql_1 = @"select 
                                        prd.auto as idPrd,
                                        prd.codigo as codigoPrd,
                                        prd.nombre_corto as nombrePrd,
                                        prd.nombre as descPrd,
                                        prd.estatus_divisa as estatusDivisaPrd,
                                        prd.costo_proveedor as costoProvPrd,
                                        prd.costo_importacion as costoImportacionPrd,
                                        prd.costo_varios as costoVarioPrd,
                                        prd.costo as costoFinalPrd,
                                        prd.costo_promedio as costoPromedioPrd,
                                        prd.divisa as costoDivisaPrd,
                                        prd.fecha_ult_costo as fechaUltCambio, 
                                        prd.contenido_compras as contEmqCompra, 
                                        fis.nombre as descTasaIva,
                                        fis.tasa as porcTasaIva,
                                        emp.nombre as descEmqCompra
                                    from productos as prd
                                    join empresa_tasas as fis on fis.auto=prd.auto_tasa
                                    join productos_medida as emp on emp.auto=prd.auto_empaque_compra
                                    where prd.auto=@idPrd";
                    var _sql = _sql_1;
                    var _ent = cnn.
                        Database.
                        SqlQuery<DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto.Ficha>(_sql, p1).
                        FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("[ ID ] PRODUCTO NO ENCONTRADO");
                    }
                    rt.Entidad= _ent;
                }
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