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
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta>
            Producto_ModoAdm_GetEmpaqueVenta_By(string autoPrd)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var _sql = @"select 
                                    id, 
                                    auto_empaque as autoEmp, 
                                    contenido_empaque as contEmp, 
                                    tipo_empaque as tipoEmp 
                                FROM productos_ext_hnd_empventa
                                where auto_producto=@autoPrd";
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.EmpaqueVenta>(_sql, _p1).ToList();
                    rt.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha>
            Producto_ModoAdm_GetPrecio_By(string autoPrd, string tipoPrecio)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    p.auto as auto, 
                                    p.codigo as codigo, 
                                    p.nombre as descripcion, 
                                    eTasa.tasa as tasaIva,
                                    p.estatus_divisa as estatusDivisa
                                from productos as p
                                join empresa_tasas as eTasa on eTasa.auto=p.auto_tasa
                                where p.auto=@autoPrd";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _ent = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Entidad>(sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Mensaje = "PRODUCTO [ID] NO ENCONTRADO";
                        return rt;
                    }

                    sql_1 = @"select 
                                x1.id as idHndPrecioVenta,
                                x1.id_prd_hnd_empVenta as idHndEmpVenta,
                                x1.id_empresa_hnd_precio as idHndEmpresaPrecio,
                                x1.neto_monedaLocal as pnEmp, 
                                x1.full_divisa as pfdEmp,
                                x1.utilidad_porct as utEmp,
                                x3.auto as autoEmp,
                                x3.nombre as descEmp,
                                x2.contenido_empaque as contEmp,
                                x2.tipo_empaque as tipoEmp,
                                x1.estatus_oferta as ofertaEstatus,
                                x1.desde_oferta as ofertaDesde,
                                x1.hasta_oferta as ofertaHasta,
                                x1.porct_oferta as ofertaPorct
                            from productos_ext_hnd_precioventa as x1 
                            join productos_ext_hnd_empventa as x2 on x2.id=x1.id_prd_hnd_empventa
                            join productos_medida as x3 on x3.auto=x2.auto_empaque
                            where x1.auto_producto=@autoPrd
                            and x1.id_empresa_hnd_precio=@idPrecio";
                    sql = sql_1;
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrecio", tipoPrecio);
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Precio>(sql, p1, p2).ToList();
                    rt.Entidad = new DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha()
                    {
                        entidad = _ent,
                        precios = _lst
                    };
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha> 
            Producto_ModoAdm_GetCosto_By(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    p.auto as auto, 
                                    p.codigo as codigo, 
                                    p.nombre as descripcion, 
                                    eTasa.tasa as tasaIva,
                                    p.estatus_divisa as estatusDivisa,
                                    pmed.auto as autoEmpCompra,
                                    pmed.nombre as descEmpCompra,
                                    p.contenido_compras as contEmpCompra,
                                    p.divisa as costoMonedaDivisa,
                                    p.costo as costoMonedaLocal
                                from productos as p
                                join empresa_tasas as eTasa on eTasa.auto=p.auto_tasa
                                join productos_medida as pmed on pmed.auto=p.auto_empaque_compra
                                where p.auto=@autoPrd";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var _ent = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha>(sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Mensaje = "PRODUCTO [ID] NO ENCONTRADO";
                        return rt;
                    }
                    rt.Entidad = _ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
        public DtoLib.Resultado 
            Producto_ModoAdm_ActualizarPrecio(DtoLibInventario.Producto.ActualizarPrecio.ModoAdm.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var x1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd",ficha.autoPrd);
                        var x2 = new MySql.Data.MySqlClient.MySqlParameter("@fechasist", fechaSistema.Date);
                        var _sql = @"update productos set 
                                        fecha_cambio=@fechaSist
                                    where auto=@autoPrd";
                        var cnt = cnn.Database.ExecuteSqlCommand(_sql, x1, x2);
                        cnn.SaveChanges();

                        if (ficha.precios != null)
                        {
                            foreach (var rg in ficha.precios) 
                            {
                                var p1= new MySql.Data.MySqlClient.MySqlParameter("@id",rg.id);
                                var p2 = new MySql.Data.MySqlClient.MySqlParameter("@netoMonLocal", rg.netoMonedaLocal);
                                var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fullDivisa", rg.fullDivisa);
                                var p4 = new MySql.Data.MySqlClient.MySqlParameter("@utilidad", rg.utilidad);
                                var sql = @"update productos_ext_hnd_precioventa set
                                                neto_monedaLocal=@netoMonLocal,
                                                full_divisa=@fullDivisa,
                                                utilidad_porct=@utilidad
                                            where id=@id";
                                var _cnt= cnn.Database.ExecuteSqlCommand(sql, p1,p2,p3,p4);
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.historia != null)
                        {
                            foreach (var rg in ficha.historia)
                            {
                                var entHist = new productos_precios()
                                {
                                    auto_producto = ficha.autoPrd,
                                    estacion = ficha.estacion,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    usuario = ficha.nombreUsuario,
                                    nota = rg.nota,
                                    precio = rg.precio,
                                    precio_id = rg.precio_id,
                                };
                                cnn.productos_precios.Add(entHist);
                                cnn.SaveChanges();

                                var entHistExt = new productos_precios_ext()
                                {
                                    factor_cambio = rg.factorCambio,
                                    contenido = rg.contenido,
                                    empaque = rg.empaque,
                                    id_producto_precio = entHist.id,
                                };
                                cnn.productos_precios_ext.Add(entHistExt);
                                cnn.SaveChanges();
                            }
                        }
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