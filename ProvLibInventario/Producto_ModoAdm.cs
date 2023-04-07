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
                        var x1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", ficha.autoPrd);
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
                                var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", rg.id);
                                var p2 = new MySql.Data.MySqlClient.MySqlParameter("@netoMonLocal", rg.netoMonedaLocal);
                                var p3 = new MySql.Data.MySqlClient.MySqlParameter("@fullDivisa", rg.fullDivisa);
                                var p4 = new MySql.Data.MySqlClient.MySqlParameter("@utilidad", rg.utilidad);
                                var sql = @"update productos_ext_hnd_precioventa set
                                                neto_monedaLocal=@netoMonLocal,
                                                full_divisa=@fullDivisa,
                                                utilidad_porct=@utilidad
                                            where id=@id";
                                var _cnt = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4);
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.historia != null)
                        {
                            foreach (var rg in ficha.historia)
                            {
                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", ficha.autoPrd);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@empDesc", rg.empDesc);
                                var xp3 = new MySql.Data.MySqlClient.MySqlParameter("@empCont", rg.empCont);
                                var xp4 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                                var xp5 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                                var xp6 = new MySql.Data.MySqlClient.MySqlParameter("@usuCodigo", ficha.usuCodigo);
                                var xp7 = new MySql.Data.MySqlClient.MySqlParameter("@usuNombre", ficha.usuNombre);
                                var xp8 = new MySql.Data.MySqlClient.MySqlParameter("@estacion", ficha.estacion);
                                var xp9 = new MySql.Data.MySqlClient.MySqlParameter("@factorCambio", ficha.factorCambio);
                                var xp10 = new MySql.Data.MySqlClient.MySqlParameter("@netoMonLocal", rg.netoMonLocal);
                                var xp11 = new MySql.Data.MySqlClient.MySqlParameter("@fullDivisa", rg.fullDivisa);
                                var xp12 = new MySql.Data.MySqlClient.MySqlParameter("@tipoEmpVenta", rg.tipoEmpaqueVenta);
                                var xp13 = new MySql.Data.MySqlClient.MySqlParameter("@tipoPrecioVenta", rg.tipoPrecioVenta);
                                var xp14 = new MySql.Data.MySqlClient.MySqlParameter("@prdCodigo", ficha.prdCodigo);
                                var xp15 = new MySql.Data.MySqlClient.MySqlParameter("@prdDesc", ficha.prdDesc);
                                var xp16 = new MySql.Data.MySqlClient.MySqlParameter("@nota", ficha.nota);
                                var _sql2 = @"INSERT INTO `productos_precios_historia` (
                                                `id` ,
                                                `auto_producto` ,
                                                `empaque_desc` ,
                                                `empaque_cont` ,
                                                `fecha` ,
                                                `hora` ,
                                                `usuario_codigo` ,
                                                `usuario_nombre` ,
                                                `estacion` ,
                                                `factor_cambio` ,
                                                `neto` ,
                                                `full_Divisa` ,
                                                `tipoempVenta` ,
                                                `tipo_precio` ,
                                                `prd_codigo` ,
                                                `prd_descripcion` ,
                                                `nota`
                                            )
                                            VALUES (
                                                NULL, 
                                                @autoPrd, @empDesc, @empCont, @fecha, @hora, @usuCodigo, @usuNombre, @estacion,
                                                @factorCambio, @netoMonLocal, @fullDivisa, @tipoEmpVenta, @tipoPrecioVenta, 
                                                @prdCodigo, @prdDesc, @nota)";
                                var _nr = cnn.Database.ExecuteSqlCommand(_sql2, xp1, xp2, xp3, xp4,
                                                                            xp5, xp6, xp7, xp8, xp9,
                                                                            xp10, xp11, xp12, xp13,
                                                                            xp14, xp15, xp16);
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha>
            Producto_ModoAdm_HistoricoPrecio_By(DtoLibInventario.Producto.HistoricoPrecio.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", filtro.autoProducto);
                    var _sql = @"select 
                                    codigo as prdCodigo, nombre as prdDescripcion 
                                from productos 
                                where auto=@autoPrd";
                    var _ficha = cnn.Database.SqlQuery<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha>(_sql, p1).FirstOrDefault();
                    if (_ficha == null)
                    {
                        result = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha>();
                        return result;
                    }
                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", filtro.autoProducto);
                    var _sql2 = @"SELECT 
                                    empaque_desc as empDescripcion,
                                    empaque_cont as empCont,
                                    fecha,
                                    hora,
                                    usuario_codigo as usuCodigo,
                                    usuario_nombre as usuNombre,
                                    estacion,
                                    factor_cambio as factorCambio,
                                    neto as netoMonLocal,
                                    full_divisa as fullDivisa,
                                    tipoempVenta as tipoEmpVta,
                                    tipo_precio as tipoPrecioVta,
                                    nota
                                FROM productos_precios_historia
                                where auto_producto=@autoPrd";
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Data>(_sql2, xp1).ToList();
                    _ficha.data = _lst;
                    result = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha>()
                    {
                        Entidad = _ficha,
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
        public DtoLib.Resultado 
            Producto_ModoAdm_ActualizarOferta(DtoLibInventario.Producto.ActualizarOferta.ModoAdm.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var x1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", ficha.autoPrd);
                        var x2 = new MySql.Data.MySqlClient.MySqlParameter("@fechasist", fechaSistema.Date);
                        var x3 = new MySql.Data.MySqlClient.MySqlParameter("@estatusOferta", ficha.estatusOferta);
                        var _sql = @"update productos set 
                                        fecha_cambio=@fechaSist,
                                        estatus_oferta=@estatusOferta
                                    where auto=@autoPrd";
                        var cnt = cnn.Database.ExecuteSqlCommand(_sql, x1, x2, x3);
                        cnn.SaveChanges();

                        if (ficha.ofertas != null)
                        {
                            foreach (var rg in ficha.ofertas)
                            {
                                var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", rg.idPrecioVta);
                                var p2 = new MySql.Data.MySqlClient.MySqlParameter("@estatus", rg.estatus);
                                var p3 = new MySql.Data.MySqlClient.MySqlParameter("@desde", rg.desde);
                                var p4 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", rg.hasta);
                                var p5 = new MySql.Data.MySqlClient.MySqlParameter("@porct", rg.portc);
                                var sql = @"update productos_ext_hnd_precioventa set
                                                estatus_oferta=@estatus,
                                                desde_oferta=@desde,
                                                hasta_oferta=@hasta,
                                                porct_oferta=@porct
                                            where id=@id";
                                var _cnt = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5);
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