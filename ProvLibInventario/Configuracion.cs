using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ModuloInventario_Modo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL60");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario.Trim().ToString();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> 
            Configuracion_PreferenciaBusqueda()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f=>f.codigo=="GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo=DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper()) 
                    {
                        case "CODIGO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorCodigo;
                            break;
                        case "NOMBRE":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorNombre;
                            break;
                        case "REFERENCIA":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda.PorReferencia;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> 
            Configuracion_MetodoCalculoUtilidad()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL13");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "LINEAL":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Lineal;
                            break;
                        case "FINANCIERO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad.Financiero;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_TasaCambioActual()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL12");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                    //var m1 = 0.0m;
                    //var cnf = ent.usuario;
                    //if (cnf.Trim() != "")
                    //{
                    //    var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                    //    //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                    //    var culture = CultureInfo.CreateSpecificCulture("en-EN");
                    //    Decimal.TryParse(cnf, style, culture, out m1);
                    //}
                    //result.Entidad = m1;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> 
            Configuracion_ForzarRedondeoPrecioVenta()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL46");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "SIN REDONDEO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.SinRedeondeo;
                            break;
                        case "UNIDAD":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Unidad;
                            break;
                        case "DECENA":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta.Decena;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> 
            Configuracion_PreferenciaRegistroPrecio()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL41");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.SinDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "PRECIO NETO":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Neto;
                            break;
                        case "PRECIO+IVA":
                            modo = DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio.Full;
                            break;
                    }

                    result.Entidad = modo;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<int> 
            Configuracion_CostoEdadProducto()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL50");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var m1 = 0;
                    var cnf = ent.usuario;
                    if (cnf.Trim() != "")
                    {
                        var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                        //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                        var culture = CultureInfo.CreateSpecificCulture("en-EN");
                        int.TryParse(cnf, style, culture, out m1);
                    }
                    result.Entidad = m1;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_VisualizarProductosInactivos()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL52");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_CantDocVisualizar()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL53");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario;
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
            Configuracion_SetCostoEdadProducto(DtoLibInventario.Configuracion.CostoEdad.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL50");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.usuario = ficha.Edad.ToString("n0");
                    cnn.SaveChanges();
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
            Configuracion_SetRedondeoPrecioVenta(DtoLibInventario.Configuracion.RedondeoPrecio.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL46");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.usuario = ficha.Redondeo;
                    cnn.SaveChanges();
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
            Configuracion_SetPreferenciaRegistroPrecio(DtoLibInventario.Configuracion.PreferenciaPrecio.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL41");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.usuario = ficha.Preferencia;
                    cnn.SaveChanges();
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
            Configuracion_SetMetodoCalculoUtilidad(DtoLibInventario.Configuracion.MetodoCalculoUtilidad.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL13");
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.usuario = ficha.Metodo;
                        cnn.SaveChanges();

                        var sql = @"update productos set precio_1=@p1, precio_2=@p2, precio_3=@p3, precio_4=@p4, precio_pto=@p5,
                                    pdf_1=@pd1, pdf_2=@pd2, pdf_3=@pd3, pdf_4=@pd4, pdf_pto=@pd5 where auto=@auto";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                        var pd1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var pd2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var pd3 = new MySql.Data.MySqlClient.MySqlParameter();
                        var pd4 = new MySql.Data.MySqlClient.MySqlParameter();
                        var pd5 = new MySql.Data.MySqlClient.MySqlParameter();
                        var auto= new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "@p1";
                        p2.ParameterName = "@p2";
                        p3.ParameterName = "@p3";
                        p4.ParameterName = "@p4";
                        p5.ParameterName = "@p5";
                        pd1.ParameterName = "@pd1";
                        pd2.ParameterName = "@pd2";
                        pd3.ParameterName = "@pd3";
                        pd4.ParameterName = "@pd4";
                        pd5.ParameterName = "@pd5";
                        auto.ParameterName = "@auto";

                        foreach (var it in ficha.Precio) 
                        {
                            p1.Value = it.Precio_1.pneto;
                            p2.Value = it.Precio_2.pneto;
                            p3.Value = it.Precio_3.pneto;
                            p4.Value = it.Precio_4.pneto;
                            p5.Value = it.Precio_5.pneto;
                            pd1.Value = it.Precio_1.pdf;
                            pd2.Value = it.Precio_2.pdf;
                            pd3.Value = it.Precio_3.pdf;
                            pd4.Value = it.Precio_4.pdf;
                            pd5.Value = it.Precio_5.pdf;
                            auto.Value = it.idProducto;
                            var xsql = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, pd1, pd2, pd3, pd4, pd5, auto);
                            if (xsql == 0)
                            {
                                result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //var entPrd = cnn.productos.Find(it.idProducto);
                            //if (entPrd == null)
                            //{
                            //    result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                            //    result.Result = DtoLib.Enumerados.EnumResult.isError;
                            //    return result;
                            //}

                            //entPrd.precio_1 = it.Precio_1.pneto;
                            //entPrd.pdf_1 = it.Precio_1.pdf;
                            //entPrd.precio_2 = it.Precio_2.pneto;
                            //entPrd.pdf_2 = it.Precio_2.pdf;
                            //entPrd.precio_3 = it.Precio_3.pneto;
                            //entPrd.pdf_3 = it.Precio_3.pdf;
                            //entPrd.precio_4 = it.Precio_4.pneto;
                            //entPrd.pdf_4 = it.Precio_4.pdf;
                            //entPrd.precio_pto = it.Precio_5.pneto;
                            //entPrd.pdf_pto = it.Precio_5.pdf;
                            //cnn.SaveChanges();
                        }
                        ts.Commit();
                    }
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
            Configuracion_SetBusquedaPredeterminada(DtoLibInventario.Configuracion.BusquedaPredeterminada.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.usuario = ficha.Busqueda;
                    cnn.SaveChanges();
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
            Configuracion_SetDepositosPreDeterminado(DtoLibInventario.Configuracion.DepositoPredeterminado.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var sql = @"update empresa_depositos_ext set es_predeterminado=@p2 
                                    where auto_deposito=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "@p1";
                        p2.ParameterName = "@p2";
                        foreach (var it in ficha.ListaPreDet)
                        {
                            p1.Value = it.AutoDeposito;
                            p2.Value = it.Estatus;
                            var xsql = cnn.Database.ExecuteSqlCommand(sql, p1, p2);
                            if (xsql == 0)
                            {
                                result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }
                        ts.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData.Ficha> 
            Configuracion_MetodoCalculoUtilidad_CapturarData()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT auto as idProducto, costo_und as costoUnd, divisa as costoDivisa, estatus_divisa as estatusDivisa, 
                                    contenido_compras as contenidoEmpCompra, tasa as tasaIva, 
                                    utilidad_1, utilidad_2, utilidad_3, utilidad_4, utilidad_pto as utilidad_5,
                                    contenido_1, contenido_2, contenido_3, contenido_4, contenido_pto as contenido_5,
                                    precio_1, precio_2, precio_3, precio_4 , precio_pto as precio_5
                                from productos 
                                where estatus='Activo' and categoria='Producto Terminado'";
                    var lst= cnn.Database.SqlQuery<DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData.Ficha>(sql).ToList();
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
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_HabilitarPrecio_5_ParaVentaMayorPos()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL51");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        //
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.DepositoConceptoDev.Captura.Ficha> 
            Configuracion_DepositoConceptoPreDeterminadoParaDevolucion()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.DepositoConceptoDev.Captura.Ficha>(); 

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = @"select usuario from sistema_configuracion where codigo='GLOBAL54'";
                    var entDeposito= cnn.Database.SqlQuery<string>(sql).FirstOrDefault();
                    if (entDeposito == null) 
                    {
                        result.Mensaje = "[ GLOBAL 54 ] DEPOSITO PREDETERMINADO PARA MOVIMIENTO DEVOLUCION INVENTARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    sql = @"select usuario from sistema_configuracion where codigo='GLOBAL55'";
                    var entConcepto= cnn.Database.SqlQuery<string>(sql).FirstOrDefault();
                    if (entConcepto == null)
                    {
                        result.Mensaje = "[ GLOBAL 55 ] CONCEPTO PREDETERMINADO PARA MOVIMIENTO DEVOLUCION INVENTARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var ent = new DtoLibInventario.Configuracion.DepositoConceptoDev.Captura.Ficha()
                    {
                        IdConcepto = entConcepto,
                        IdDeposito = entDeposito,
                    };
                    result.Entidad=ent;
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
            Configuracion_SetDepositoConceptoPreDeterminadoParaDevolucion(DtoLibInventario.Configuracion.DepositoConceptoDev.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.IdDeposito);
                        var sql = @"update sistema_configuracion set usuario=@p1 
                                    where codigo='GLOBAL54'";
                        var i = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (i == 0) 
                        {
                            result.Mensaje = "[ GLOBAL 54 ] DEPOSITO PREDETERMINADO PARA MOVIMIENTO DEVOLUCION INVENTARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.IdConcepto);
                        sql = @"update sistema_configuracion set usuario=@p2 
                                    where codigo='GLOBAL55'";
                        i = cnn.Database.ExecuteSqlCommand(sql, p2);
                        if (i == 0)
                        {
                            result.Mensaje = "[ GLOBAL 55 ] CONCEPTO PREDETERMINADO PARA MOVIMIENTO DEVOLUCION INVENTARIO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ts.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_PermitirCambiarPrecioAlModificarCosto()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL56");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario.Trim().ToString();
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
            Configuracion_SetPermitirCambiarPrecioAlModificarCosto(string conf)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = cnn.Database.BeginTransaction())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", conf);
                        var sql = @"update sistema_configuracion set usuario=@p1 
                                    where codigo='GLOBAL56'";
                        var i = cnn.Database.ExecuteSqlCommand(sql, p1);
                        if (i == 0)
                        {
                            result.Mensaje = "[ GLOBAL 56 ] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ts.Commit();
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