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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen> 
            HistoricoPrecio_GetLista(DtoLibInventario.Precio.Historico.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var prd = cnn.productos.Find(filtro.autoProducto);
                    if (prd == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        return result;
                    }
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", filtro.autoProducto);
                    var sql = @"SELECT prdPrecio.nota,  
                                        prdPrecio.fecha,
                                        prdPrecio.hora,
                                        prdPrecio.estacion,
                                        prdPrecio.usuario,
                                        prdPrecio.precio_id,
                                        prdPrecio.precio,
                                        prdPrecioExt.empaque,
                                        prdPrecioExt.contenido,
                                        prdPrecioExt.factor_cambio
                                FROM productos_precios as prdPrecio
                                    join productos_precios_ext as prdPrecioExt on prdPrecioExt.id_producto_precio=prdPrecio.id
                                    where prdPrecio.auto_producto=@idPrd";
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Precio.Historico.Data>(sql, p1).ToList();
                    var nr = new DtoLibInventario.Precio.Historico.Resumen();
                    result.Entidad = new DtoLibInventario.Precio.Historico.Resumen()
                    {
                        codigo = prd.codigo,
                        descripcion = prd.nombre,
                        data = _lst,
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha>
            PrecioCosto_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entPrdExt = cnn.productos_ext.Find(autoPrd);
                    if (entPrdExt== null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO PRECIO MAYOR NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entEmpresa = cnn.empresa.FirstOrDefault();
                    if (entEmpresa == null)
                    {
                        rt.Mensaje = "ENTIDAD [ EMPRESA ] NO ENCONTRADA";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var entTasa = cnn.empresa_tasas.Find(entPrd.auto_tasa);
                    var _fechaUltActCosto = "";
                    if (entPrd.fecha_ult_costo!=new DateTime(2000,01,01))
                    {
                        _fechaUltActCosto = entPrd.fecha_ult_costo.ToShortDateString();
                    }

                    var precio = new DtoLibInventario.Precio.PrecioCosto.Ficha()
                    {
                        codigo = entPrd.codigo,
                        nombre = entPrd.nombre_corto,
                        descripcion = entPrd.nombre,
                        tasaIva = entPrd.tasa,
                        nombreTasaIva = entTasa.nombre,
                        admDivisa = entPrd.estatus_divisa,

                        etiqueta1 = entEmpresa.precio_1,
                        etiqueta2 = entEmpresa.precio_2,
                        etiqueta3 = entEmpresa.precio_3,
                        etiqueta4 = entEmpresa.precio_4,
                        etiqueta5 = entEmpresa.precio_5,

                        contenido1 = entPrd.contenido_1,
                        contenido2 = entPrd.contenido_2,
                        contenido3 = entPrd.contenido_3,
                        contenido4 = entPrd.contenido_4,
                        contenido5 = entPrd.contenido_pto,

                        precioNeto1 = entPrd.precio_1,
                        precioNeto2 = entPrd.precio_2,
                        precioNeto3 = entPrd.precio_3,
                        precioNeto4 = entPrd.precio_4,
                        precioNeto5 = entPrd.precio_pto,

                        precioFullDivisa1 = entPrd.pdf_1,
                        precioFullDivisa2 = entPrd.pdf_2,
                        precioFullDivisa3 = entPrd.pdf_3,
                        precioFullDivisa4 = entPrd.pdf_4,
                        precioFullDivisa5 = entPrd.pdf_pto,

                        utilidad1 = entPrd.utilidad_1,
                        utilidad2 = entPrd.utilidad_2,
                        utilidad3 = entPrd.utilidad_3,
                        utilidad4 = entPrd.utilidad_4,
                        utilidad5 = entPrd.utilidad_pto,

                        autoEmp1 = entPrd.auto_precio_1,
                        autoEmp2 = entPrd.auto_precio_2,
                        autoEmp3 = entPrd.auto_precio_3,
                        autoEmp4 = entPrd.auto_precio_4,
                        autoEmp5 = entPrd.auto_precio_pto,

                        costo = entPrd.costo,
                        costoUnd = entPrd.costo_und,
                        costoDivisa = entPrd.divisa,
                        contempCompra = entPrd.contenido_compras,
                        empCompra = entPrd.productos_medida2.nombre,
                        fechaUltActualizacion = _fechaUltActCosto,

                        // PRECIOS DE MAYOR
                        autoEmpMay1 = entPrdExt.auto_precio_may_1,
                        autoEmpMay2 = entPrdExt.auto_precio_may_2,
                        autoEmpMay3 = entPrdExt.auto_precio_may_3,

                        contenidoMay1 = entPrdExt.contenido_may_1,
                        contenidoMay2 = entPrdExt.contenido_may_2,
                        contenidoMay3 = entPrdExt.contenido_may_3,

                        utilidadMay1 = entPrdExt.utilidad_may_1,
                        utilidadMay2 = entPrdExt.utilidad_may_2,
                        utilidadMay3 = entPrdExt.utilidad_may_3,

                        precioNetoMay1 = entPrdExt.precio_may_1,
                        precioNetoMay2 = entPrdExt.precio_may_2,
                        precioNetoMay3 = entPrdExt.precio_may_3,

                        precioFullDivisaMay1 = entPrdExt.pdmf_1,
                        precioFullDivisaMay2 = entPrdExt.pdmf_2,
                        precioFullDivisaMay3 = entPrdExt.pdmf_3,
                    };

                    rt.Entidad = precio;
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
            PrecioProducto_Actualizar(DtoLibInventario.Precio.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var entPrd = cnn.productos.Find(ficha.autoProducto);
                        if (entPrd == null)
                        {
                            result.Mensaje = "[ ID ] Producto, No Encontrado";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var entPrdExt = cnn.productos_ext.Find(ficha.autoProducto);
                        if (entPrdExt== null)
                        {
                            result.Mensaje = "[ ID ] Producto Precios Mayor, No Encontrado";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entPrd.fecha_cambio = fechaSistema.Date;
                        if (ficha.precio_1 != null)
                        {
                            entPrd.auto_precio_1 = ficha.precio_1.autoEmp;
                            entPrd.precio_1 = ficha.precio_1.precioNeto;
                            entPrd.utilidad_1 = ficha.precio_1.utilidad;
                            entPrd.pdf_1 = ficha.precio_1.precio_divisa_Neto;
                            entPrd.contenido_1 = ficha.precio_1.contenido;
                        }
                        if (ficha.precio_2 != null)
                        {
                            entPrd.auto_precio_2 = ficha.precio_2.autoEmp;
                            entPrd.precio_2 = ficha.precio_2.precioNeto;
                            entPrd.utilidad_2 = ficha.precio_2.utilidad;
                            entPrd.pdf_2 = ficha.precio_2.precio_divisa_Neto;
                            entPrd.contenido_2 = ficha.precio_2.contenido;
                        }
                        if (ficha.precio_3 != null)
                        {
                            entPrd.auto_precio_3 = ficha.precio_3.autoEmp;
                            entPrd.precio_3 = ficha.precio_3.precioNeto;
                            entPrd.utilidad_3 = ficha.precio_3.utilidad;
                            entPrd.pdf_3 = ficha.precio_3.precio_divisa_Neto;
                            entPrd.contenido_3 = ficha.precio_3.contenido;
                        }
                        if (ficha.precio_4 != null)
                        {
                            entPrd.auto_precio_4 = ficha.precio_4.autoEmp;
                            entPrd.precio_4 = ficha.precio_4.precioNeto;
                            entPrd.utilidad_4 = ficha.precio_4.utilidad;
                            entPrd.pdf_4 = ficha.precio_4.precio_divisa_Neto;
                            entPrd.contenido_4 = ficha.precio_4.contenido;
                        }
                        if (ficha.precio_5 != null)
                        {
                            entPrd.auto_precio_pto = ficha.precio_5.autoEmp;
                            entPrd.precio_pto = ficha.precio_5.precioNeto;
                            entPrd.utilidad_pto = ficha.precio_5.utilidad;
                            entPrd.pdf_pto = ficha.precio_5.precio_divisa_Neto;
                            entPrd.contenido_pto = ficha.precio_5.contenido;
                        }
                        cnn.SaveChanges();

                        if (ficha.may_1 != null)
                        {
                            entPrdExt.auto_precio_may_1 = ficha.may_1.autoEmp;
                            entPrdExt.precio_may_1 = ficha.may_1.precioNeto;
                            entPrdExt.utilidad_may_1 = ficha.may_1.utilidad;
                            entPrdExt.pdmf_1 = ficha.may_1.precio_divisa_Neto;
                            entPrdExt.contenido_may_1 = ficha.may_1.contenido;
                        }
                        if (ficha.may_2 != null)
                        {
                            entPrdExt.auto_precio_may_2 = ficha.may_2.autoEmp;
                            entPrdExt.precio_may_2 = ficha.may_2.precioNeto;
                            entPrdExt.utilidad_may_2 = ficha.may_2.utilidad;
                            entPrdExt.pdmf_2 = ficha.may_2.precio_divisa_Neto;
                            entPrdExt.contenido_may_2 = ficha.may_2.contenido;
                        }
                        if (ficha.may_3 != null)
                        {
                            entPrdExt.auto_precio_may_3 = ficha.may_3.autoEmp;
                            entPrdExt.precio_may_3 = ficha.may_3.precioNeto;
                            entPrdExt.utilidad_may_3 = ficha.may_3.utilidad;
                            entPrdExt.pdmf_3 = ficha.may_3.precio_divisa_Neto;
                            entPrdExt.contenido_may_3 = ficha.may_3.contenido;
                        }
                        if (ficha.may_4 != null)
                        {
                            entPrdExt.auto_precio_may_4 = ficha.may_4.autoEmp;
                            entPrdExt.precio_may_4 = ficha.may_4.precioNeto;
                            entPrdExt.utilidad_may_4 = ficha.may_4.utilidad;
                            entPrdExt.pdmf_4 = ficha.may_4.precio_divisa_Neto;
                            entPrdExt.cont_may_4 = ficha.may_4.contenido;
                        }
                        if (ficha.dsp_1 != null)
                        {
                            entPrdExt.auto_precio_dsp_1 = ficha.dsp_1.autoEmp;
                            entPrdExt.precio_dsp_1 = ficha.dsp_1.precioNeto;
                            entPrdExt.utilidad_dsp_1 = ficha.dsp_1.utilidad;
                            entPrdExt.pdivisafull_dsp_1 = ficha.dsp_1.precio_divisa_Neto;
                            entPrdExt.cont_dsp_1 = ficha.dsp_1.contenido;
                        }
                        if (ficha.dsp_2 != null)
                        {
                            entPrdExt.auto_precio_dsp_2 = ficha.dsp_2.autoEmp;
                            entPrdExt.precio_dsp_2 = ficha.dsp_2.precioNeto;
                            entPrdExt.utilidad_dsp_2 = ficha.dsp_2.utilidad;
                            entPrdExt.pdivisafull_dsp_2 = ficha.dsp_2.precio_divisa_Neto;
                            entPrdExt.cont_dsp_2 = ficha.dsp_2.contenido;
                        }
                        if (ficha.dsp_3 != null)
                        {
                            entPrdExt.auto_precio_dsp_3 = ficha.dsp_3.autoEmp;
                            entPrdExt.precio_dsp_3 = ficha.dsp_3.precioNeto;
                            entPrdExt.utilidad_dsp_3 = ficha.dsp_3.utilidad;
                            entPrdExt.pdivisafull_dsp_3 = ficha.dsp_3.precio_divisa_Neto;
                            entPrdExt.cont_dsp_3 = ficha.dsp_3.contenido;
                        }
                        if (ficha.dsp_4 != null)
                        {
                            entPrdExt.auto_precio_dsp_4 = ficha.dsp_4.autoEmp;
                            entPrdExt.precio_dsp_4 = ficha.dsp_4.precioNeto;
                            entPrdExt.utilidad_dsp_4 = ficha.dsp_4.utilidad;
                            entPrdExt.pdivisafull_dsp_4 = ficha.dsp_4.precio_divisa_Neto;
                            entPrdExt.cont_dsp_4 = ficha.dsp_4.contenido;
                        }
                        cnn.SaveChanges();

                        if (ficha.historia != null)
                        {
                            foreach (var it in ficha.historia)
                            {
                                var entHist = new productos_precios()
                                {
                                    auto_producto = ficha.autoProducto,
                                    estacion = ficha.estacion,
                                    fecha = fechaSistema.Date,
                                    hora = fechaSistema.ToShortTimeString(),
                                    usuario = ficha.nombreUsuario,
                                    nota = it.nota,
                                    precio = it.precio,
                                    precio_id = it.precio_id,
                                };
                                cnn.productos_precios.Add(entHist);
                                cnn.SaveChanges();

                                var entHistExt = new productos_precios_ext()
                                {
                                    factor_cambio= it.factorCambio,
                                    contenido = it.contenido,
                                    empaque = it.empaque,
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.PrecioCosto.Entidad.Ficha> 
            PrecioCosto_GetData(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.PrecioCosto.Entidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = @"select 
                                    p.auto, p.codigo, p.nombre as descripcion, divisa as costoMonedaDivisa, costo as costoMonedaLocal,
                                    estatus_divisa as estatusDivisa, eTasa.nombre as nombreTasa, eTasa.tasa as tasaIva,
                                    pMedCompra. nombre as empCompraDesc, p.contenido_compras as contEmpCompra,

                                    auto_precio_1 as autoEmp_1, contenido_1 as cont_1, utilidad_1, precio_1 as pNeto_1, pdf_1 as pfd_1,
                                    auto_precio_2 as autoEmp_2, contenido_2 as cont_2, utilidad_2, precio_2 as pNeto_2, pdf_2 as pfd_2,
                                    auto_precio_3 as autoEmp_3, contenido_3 as cont_3, utilidad_3, precio_3 as pNeto_3, pdf_3 as pfd_3,
                                    auto_precio_4 as autoEmp_4, contenido_4 as cont_4, utilidad_4, precio_4 as pNeto_4, pdf_4 as pfd_4,
                                    auto_precio_pto as autoEmp_5, contenido_pto as cont_5, utilidad_pto as utilidad_5, 
                                    precio_pto as pNeto_5, pdf_pto as pfd_5,

                                    auto_precio_may_1 as autoEmp_M1, contenido_may_1 as cont_M1, utilidad_may_1 as utilidad_M1, precio_may_1 as pNeto_M1, pdmf_1 as pfd_M1,
                                    auto_precio_may_2 as autoEmp_M2, contenido_may_2 as cont_M2, utilidad_may_2 as utilidad_M2, precio_may_2 as pNeto_M2, pdmf_2 as pfd_M2,
                                    auto_precio_may_3 as autoEmp_M3, contenido_may_3 as cont_M3, utilidad_may_3 as utilidad_M3, precio_may_3 as pNeto_M3, pdmf_3 as pfd_M3,
                                    auto_precio_may_4 as autoEmp_M4, cont_may_4 as cont_M4, utilidad_may_4 as utilidad_M4, precio_may_4 as pNeto_M4, pdmf_4 as pfd_M4,

                                    auto_precio_dsp_1 as autoEmp_D1, cont_dsp_1 as cont_D1, utilidad_dsp_1 as utilidad_D1, precio_dsp_1 as pNeto_D1, pdivisafull_dsp_1 as pfd_D1,
                                    auto_precio_dsp_2 as autoEmp_D2, cont_dsp_2 as cont_D2, utilidad_dsp_2 as utilidad_D2, precio_dsp_2 as pNeto_D2, pdivisafull_dsp_2 as pfd_D2,
                                    auto_precio_dsp_3 as autoEmp_D3, cont_dsp_3 as cont_D3, utilidad_dsp_3 as utilidad_D3, precio_dsp_3 as pNeto_D3, pdivisafull_dsp_3 as pfd_D3,
                                    auto_precio_dsp_4 as autoEmp_D4, cont_dsp_4 as cont_D4, utilidad_dsp_4 as utilidad_D4, precio_dsp_4 as pNeto_D4, pdivisafull_dsp_4 as pfd_D4

                                    from productos as p 
                                    join empresa_tasas as eTasa on eTasa.auto=p.auto_tasa
                                    join productos_medida as pMedCompra on pMedCompra.auto=p.auto_empaque_compra
                                    join productos_ext as pExt on pExt.auto_producto=p.auto
                                    where p.auto=@autoPrd";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("autoPrd", autoPrd);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.PrecioCosto.Entidad.Ficha>(sql, p1, p2).FirstOrDefault();
                    if (ent == null) 
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    rt.Entidad = ent;
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