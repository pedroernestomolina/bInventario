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
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen> 
            Producto_GetLista(DtoLibInventario.Producto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {

                    var xsql1 = @"select p.auto, p.codigo, p.nombre_corto as nombre, p.nombre as descripcion, ed.nombre as departamento, pg.nombre as grupo, 
                        pmarca.nombre as marca, p.modelo, p.referencia, p.categoria as xcategoria, p.origen as xorigen, 
                        p.contenido_compras as contenido, p.estatus_cambio, 
                        pm.nombre as empaque,  pm.decimales as decimales, 
                        etasa.tasa as tasaIva, etasa.nombre as tasaIvaDescripcion, 
                        p.estatus as xestatus, p.estatus_divisa, p.estatus_pesado, p.estatus_catalogo, p.estatus_oferta, 
                        p.fecha_alta as fechaAlta, p.fecha_cambio as fechaUltActualizacion, p.fecha_ult_costo as fechaUltCambioCosto, 
                        p.pdf_1 as pDivisaFull_1, p.pdf_2 as pDivisaFull_2, p.pdf_3 as pDivisaFull_3, 
                        p.pdf_4 as pDivisaFull_4, p.pdf_pto as pDivisaFull_5, 
                        p.precio_1 as pNeto1, p.precio_2 as pNeto2, p.precio_3 as pNeto3, p.precio_4 as pNeto4, 
                        p.precio_pto as pNeto5, p.costo, 
                        pext.precio_may_1 as pNetoMay1, pext.precio_may_2 as pNetoMay2, 
                        pext.contenido_may_1 as contMay1, pext.contenido_may_2 as contMay2, 
                        pext.pdmf_1 as pDivisaFullMay_1,
                        pext.pdmf_2 as pDivisaFullMay_2,
                        p.divisa as costoDivisa,
                        pext.cont_emp_inv_1 as contEmpInv ";
                    var xsql2 = @" join empresa_departamentos as ed on p.auto_departamento=ed.auto
                                join productos_grupo as pg on p.auto_grupo=pg.auto 
                                join productos_medida as pm on p.auto_empaque_compra=pm.auto 
                                join productos_marca as pmarca on p.auto_marca=pmarca.auto 
                                join empresa_tasas as etasa on p.auto_tasa=etasa.auto 
                                join productos_ext as pext on p.auto=pext.auto_producto ";
                    var xsql3 = "where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var pA = new MySql.Data.MySqlClient.MySqlParameter();
                    var pB = new MySql.Data.MySqlClient.MySqlParameter();
                    var pC = new MySql.Data.MySqlClient.MySqlParameter();
                    var pD = new MySql.Data.MySqlClient.MySqlParameter();
                    var pE = new MySql.Data.MySqlClient.MySqlParameter();
                    var pF = new MySql.Data.MySqlClient.MySqlParameter();
                    var pG = new MySql.Data.MySqlClient.MySqlParameter();

                    var valor = "";
                    if (filtro.cadena != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                xsql3 += " and p.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                xsql3 += " and p.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                xsql3 += " and p.nombre like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                xsql3 += " and p.nombre like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Referencia)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                xsql3 += " and p.referencia like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                xsql3 += " and p.referencia like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }
                    if (filtro.autoProducto != "")
                    {
                        xsql3 += " and p.auto=@autoProducto";
                        p2.ParameterName = "@autoProducto";
                        p2.Value = filtro.autoProducto;
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        xsql3 += " and p.auto_departamento=@autoDepartamento ";
                        p3.ParameterName = "@autoDepartamento";
                        p3.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        xsql3 += " and p.auto_grupo=@autoGrupo ";
                        p4.ParameterName = "@autoGrupo";
                        p4.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoMarca != "")
                    {
                        xsql3 += " and p.auto_marca=@autoMarca ";
                        p5.ParameterName = "@autoMarca";
                        p5.Value = filtro.autoMarca;
                    }
                    if (filtro.autoTasa != "")
                    {
                        xsql3 += " and p.auto_tasa=@autoTasa ";
                        p6.ParameterName = "@autoTasa";
                        p6.Value = filtro.autoTasa;
                    }
                    if (filtro.estatus != DtoLibInventario.Producto.Enumerados.EnumEstatus.SnDefinir)
                    {
                        if (filtro.estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido)
                        {
                            xsql3 += " and p.estatus_cambio='1' ";
                        }
                        else
                        {
                            var _f = "Activo";
                            if (filtro.estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo)
                            {
                                _f = "Inactivo";
                            }
                            xsql3 += " and p.estatus=@estatus ";
                            p7.ParameterName = "@estatus";
                            p7.Value = _f;
                        }
                    }
                    if (filtro.admPorDivisa != DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.admPorDivisa == DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No)
                        {
                            _f = "0";
                        }
                        xsql3 += " and p.estatus_divisa=@estatusDivisa ";
                        p8.ParameterName = "@estatusDivisa";
                        p8.Value = _f;
                    }
                    if (filtro.categoria != DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir)
                    {
                        var _f = "";
                        switch (filtro.categoria)
                        {
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio:
                                _f = "Bien De Servicio";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado:
                                _f = "Producto Terminado";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima:
                                _f = "Materia Prima";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto:
                                _f = "Sub Producto";
                                break;
                            case DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno:
                                _f = "Uso Interno";
                                break;
                        }
                        xsql3 += " and p.categoria=@categoria ";
                        p9.ParameterName = "@categoria";
                        p9.Value = _f;
                    }
                    if (filtro.origen != DtoLibInventario.Producto.Enumerados.EnumOrigen.SnDefinir)
                    {
                        var _f = "Nacional";
                        if (filtro.origen == DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado)
                        {
                            _f = "Importado";
                        }
                        xsql3 += " and p.origen=@origen ";
                        pA.ParameterName = "@origen";
                        pA.Value = _f;
                    }
                    if (filtro.pesado != DtoLibInventario.Producto.Enumerados.EnumPesado.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.pesado == DtoLibInventario.Producto.Enumerados.EnumPesado.No)
                        {
                            _f = "0";
                        }
                        xsql3 += " and p.estatus_pesado=@estatusPesado ";
                        pB.ParameterName = "@estatusPesado";
                        pB.Value = _f;
                    }
                    if (filtro.oferta != DtoLibInventario.Producto.Enumerados.EnumOferta.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.oferta == DtoLibInventario.Producto.Enumerados.EnumOferta.No)
                        {
                            _f = "0";
                        }
                        xsql3 += " and p.estatus_oferta=@estatusOferta ";
                        pC.ParameterName = "@estatusOferta";
                        pC.Value = _f;
                    }
                    if (filtro.catalogo != DtoLibInventario.Producto.Enumerados.EnumCatalogo.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.catalogo == DtoLibInventario.Producto.Enumerados.EnumCatalogo.No)
                        {
                            _f = "0";
                        }
                        xsql3 += " and p.estatus_catalogo=@estatusCatalogo ";
                        pD.ParameterName = "@estatusCatalogo";
                        pD.Value = _f;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        xsql1 += @",(select sum(fisica) from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as existencia from productos as p ";
                        xsql2 += " join productos_deposito as pdeposito on pdeposito.auto_producto=p.auto ";
                        xsql3 += " and pdeposito.auto_deposito=@autoDeposito ";
                        pE.ParameterName = "@autoDeposito";
                        pE.Value = filtro.autoDeposito;
                    }
                    else 
                    {
                        xsql1 += ",(select sum(fisica) from productos_deposito where auto_producto=p.auto) as existencia from productos as p ";
                    }
                    if (filtro.autoProveedor != "")
                    {
                        xsql2 += " join productos_proveedor as pproveedor on pproveedor.auto_producto=p.auto ";
                        xsql3 += " and pproveedor.auto_proveedor=@autoProveedor ";
                        pF.ParameterName = "@autoProveedor";
                        pF.Value = filtro.autoProveedor;
                    }
                    if (filtro.estatusTCS != "")
                    {
                        xsql3 += " and p.estatus_talla_color_sabor=@estatusTCS ";
                        pG.ParameterName = "@estatusTCS";
                        pG.Value = filtro.estatusTCS;
                    }

                    var t1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var t2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.activarBusquedaPorTrasalado)
                    {
                        t1.ParameterName ="@t1";
                        t1.Value = filtro.autoDepOrigen;
                        t2.ParameterName = "t2";
                        t2.Value = filtro.autoDepDestino;

                        xsql2 += " join productos_deposito as origen on origen.auto_producto=p.auto and origen.auto_deposito=@t1 ";
                        xsql2 += " join productos_deposito as destino on destino.auto_producto=p.auto and destino.auto_deposito=@t2 ";
                        xsql3 += " and origen.fisica > 0 ";
                    }

                    var xsql = xsql1 + xsql2 + xsql3;
                    var q = cnn.Database.SqlQuery<DtoLibInventario.Producto.Resumen>(xsql, p1, p2, p3, p4, p5, p6, p7, p8, p9, pA, pB, pC, pD, pE, pF, pG, t1, t2).ToList();
                    rt.Lista = q;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha> 
            Producto_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };

                    var f = new DtoLibInventario.Producto.VerData.Ficha();
                    var _depart = entPrd.empresa_departamentos.nombre;
                    var _codDepart = entPrd.empresa_departamentos.codigo;
                    var _grupo = entPrd.productos_grupo.nombre;
                    var _codGrupo = entPrd.productos_grupo.codigo;
                    var _marca = entPrd.productos_marca.nombre;
                    var _nombreTasaIva = entPrd.empresa_tasas.nombre;
                    var _tasaIva = entPrd.empresa_tasas.tasa;
                    var _empCompra = entPrd.productos_medida2.nombre;
                    var _decimales = entPrd.productos_medida2.decimales;
                    var _origen = entPrd.origen.Trim().ToUpper() == "NACIONAL" ?
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Nacional :
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado;
                    var _estatus = entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo :
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo &&
                        entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                    var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                    switch (entPrd.categoria.Trim().ToUpper())
                    {
                        case "PRODUCTO TERMINADO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                            break;
                        case "BIEN DE SERVICIO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                            break;
                        case "MATERIA PRIMA":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                            break;
                        case "USO INTERNO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                            break;
                        case "SUB PRODUCTO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                            break;
                    }

                    var id = new DtoLibInventario.Producto.VerData.Identificacion()
                    {
                        AdmPorDivisa = _admDivisa,
                        advertencia = entPrd.advertencia,
                        auto = entPrd.auto,
                        categoria = _categoria,
                        codigo = entPrd.codigo,
                        codigoDepartamento = _codDepart,
                        codigoGrupo = _codGrupo,
                        comentarios = entPrd.comentarios,
                        contenidoCompra = entPrd.contenido_compras,
                        departamento = _depart,
                        descripcion = entPrd.nombre,
                        empaqueCompra = _empCompra,
                        estatus = _estatus,
                        fechaAlta = entPrd.fecha_alta,
                        fechaBaja = entPrd.fecha_baja,
                        fechaUltActualizacion = entPrd.fecha_cambio,
                        grupo = _grupo,
                        marca = _marca,
                        modelo = entPrd.modelo,
                        nombre = entPrd.nombre_corto,
                        nombreTasaIva = _nombreTasaIva,
                        origen = _origen,
                        presentacion = entPrd.presentacion,
                        referencia = entPrd.referencia,
                        tasaIva = _tasaIva,
                        tipoABC = entPrd.abc,
                        estatusPesado = entPrd.estatus_pesado,
                        plu = entPrd.plu,
                        diasEmpaque = entPrd.dias_garantia,
                    };
                    f.identidad = id;

                    var fechaV = new DateTime(2000, 01, 01);
                    var costo = new DtoLibInventario.Producto.VerData.Costo()
                    {
                        costoUnd = entPrd.costo_und,
                        costoDivisa = entPrd.divisa,
                        costoImportacionUnd = entPrd.costo_importacion_und,
                        costoPromedioUnd = entPrd.costo_promedio_und,
                        costoProveedorUnd = entPrd.costo_proveedor_und,
                        costoVarioUnd = entPrd.costo_varios_und,
                        fechaUltCambio = entPrd.fecha_ult_costo == fechaV ? (DateTime?)null : entPrd.fecha_ult_costo,
                    };
                    f.costo = costo;

                    var dep = cnn.productos_deposito.Where(w => w.auto_producto == autoPrd).ToList();
                    var ex = new DtoLibInventario.Producto.VerData.Existencia()
                    {
                        depositos = dep.Select(s =>
                        {
                            var dp = new DtoLibInventario.Producto.VerData.Deposito()
                            {
                                codigo = s.empresa_depositos.codigo,
                                exDisponible = s.disponible,
                                exFisica = s.fisica,
                                exReserva = s.reservada,
                                nombre = s.empresa_depositos.nombre,
                            };
                            return dp;
                        }).ToList(),
                        decimales = _decimales,
                    };
                    f.existencia = ex;

                    var precio = new DtoLibInventario.Producto.VerData.Precio()
                    {
                        contenido1 = entPrd.contenido_1,
                        contenido2 = entPrd.contenido_2,
                        contenido3 = entPrd.contenido_3,
                        contenido4 = entPrd.contenido_4,
                        contenido5 = entPrd.contenido_pto,
                        finOferta = entPrd.fin,
                        inicioOferta = entPrd.inicio,
                        ofertaActiva = entPrd.estatus_oferta.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumOferta.Si :
                        DtoLibInventario.Producto.Enumerados.EnumOferta.No,
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
                        precioOferta = entPrd.precio_oferta,
                        precioSugerido = entPrd.precio_sugerido,
                        utilidad1 = entPrd.utilidad_1,
                        utilidad2 = entPrd.utilidad_2,
                        utilidad3 = entPrd.utilidad_3,
                        utilidad4 = entPrd.utilidad_4,
                        utilidad5 = entPrd.utilidad_pto,
                    };
                    f.precio = precio;

                    List<string> alternos = cnn.productos_alterno.
                        Where(w => w.auto_producto == autoPrd).
                        Select(new Func<productos_alterno, String>(s =>
                        {
                            return s.codigo_alterno;
                        })).ToList();

                    var extra = new DtoLibInventario.Producto.VerData.Extra()
                    {
                        codigosAlterno = alternos,
                        diasEmpaque = entPrd.dias_garantia,
                        esPesado = entPrd.estatus_pesado.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumPesado.Si :
                        DtoLibInventario.Producto.Enumerados.EnumPesado.No,
                        imagen = null,
                        lugar = entPrd.lugar,
                        plu = entPrd.plu,
                    };
                    f.extra = extra;

                    rt.Entidad = f;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Estatus.Lista.Resumen> 
            Producto_Estatus_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Estatus.Lista.Resumen>();
            var list = new List<DtoLibInventario.Producto.Estatus.Lista.Resumen>();

            var nr = new DtoLibInventario.Producto.Estatus.Lista.Resumen() { Id = 1, Descripcion = "Activo" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Estatus.Lista.Resumen() { Id = 2, Descripcion = "Suspendido" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Estatus.Lista.Resumen() { Id = 3, Descripcion = "Inactivo" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen> 
            Producto_Origen_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen>();
            var list = new List<DtoLibInventario.Producto.Origen.Resumen>();

            var nr = new DtoLibInventario.Producto.Origen.Resumen() { Id = 1, Descripcion = "Nacional" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Origen.Resumen() { Id = 2, Descripcion = "Importado" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen> 
            Producto_Categoria_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen>();
            var list = new List<DtoLibInventario.Producto.Categoria.Resumen>();

            var nr = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 1, Descripcion = "Producto Terminado" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 2, Descripcion = "Bien de Servicio" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 3, Descripcion = "Materia Prima" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 4, Descripcion = "Uso Interno" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 5, Descripcion = "Sub Producto" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen> 
            Producto_AdmDivisa_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen>();
            var list = new List<DtoLibInventario.Producto.AdmDivisa.Resumen>();
            var nr = new DtoLibInventario.Producto.AdmDivisa.Resumen() { Id = 1, Descripcion = "Si" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.AdmDivisa.Resumen() { Id = 2, Descripcion = "No" };
            list.Add(nr);
            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Pesado.Resumen> 
            Producto_Pesado_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Pesado.Resumen>();
            var list = new List<DtoLibInventario.Producto.Pesado.Resumen>();

            var nr = new DtoLibInventario.Producto.Pesado.Resumen() { Id = 1, Descripcion = "Si" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Pesado.Resumen() { Id = 2, Descripcion = "No" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Oferta.Resumen> 
            Producto_Oferta_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Oferta.Resumen>();
            var list = new List<DtoLibInventario.Producto.Oferta.Resumen>();

            var nr = new DtoLibInventario.Producto.Oferta.Resumen() { Id = 1, Descripcion = "Si" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Oferta.Resumen() { Id = 2, Descripcion = "No" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Existencia> 
            Producto_GetExistencia(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Existencia>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var sql = @"select 
                                p.codigo as codigoPrd,
                                p.nombre as nombrePrd,
                                p.contenido_compras as empaqueCompraCont,
                                pmCompra.nombre as empaqueCompra,
                                pmCompra.decimales as decimales,
                                pExt.cont_emp_inv_1 as contEmpInv,
                                pmInv.nombre as descEmpInv
                                from productos as p 
                                join productos_medida as pmCompra on pmCompra.auto=p.auto_empaque_compra 
                                join productos_ext as pExt on pExt.auto_producto=p.auto
                                join productos_medida as pmInv on pmInv.auto=pExt.auto_emp_inv_1
                                where p.auto=@autoPrd";
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.Existencia>(sql,p1).FirstOrDefault();
                    if (ent== null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var sql2 = @"select 
                                    eDep.auto as autoId,
                                    eDep.codigo as codigo,
                                    eDep.nombre as nombre,
                                    pDep.disponible as exDisponible,
                                    pDep.fisica as exFisica,
                                    pDep.reservada as exReserva
                                    from productos_deposito as pDep
                                    join empresa_depositos as eDep on eDep.auto=pDep.auto_deposito
                                    where pDep.auto_producto=@autoPrd";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Producto.VerData.Deposito>(sql2, p2).ToList();
                    ent.depositos = lst;
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Precio> 
            Producto_GetPrecio(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Precio>();

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

                    var _enOferta = DtoLibInventario.Producto.Enumerados.EnumOferta.No;
                    if (entPrd.estatus_oferta == "1")
                    {
                        _enOferta = DtoLibInventario.Producto.Enumerados.EnumOferta.Si;
                    }

                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;

                    var _estatus = entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo :
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo &&
                        entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }

                    var entTasa = cnn.empresa_tasas.Find(entPrd.auto_tasa);
                    var emp1 = cnn.productos_medida.Find(entPrd.auto_precio_1);
                    var emp2 = cnn.productos_medida.Find(entPrd.auto_precio_2);
                    var emp3 = cnn.productos_medida.Find(entPrd.auto_precio_3);
                    var emp4 = cnn.productos_medida.Find(entPrd.auto_precio_4);
                    var emp5 = cnn.productos_medida.Find(entPrd.auto_precio_pto);
                    var empMay1 = cnn.productos_medida.Find(entPrdExt.auto_precio_may_1);
                    var empMay2 = cnn.productos_medida.Find(entPrdExt.auto_precio_may_2);

                    var precio = new DtoLibInventario.Producto.VerData.Precio()
                    {
                        codigo = entPrd.codigo,
                        nombre = entPrd.nombre_corto,
                        descripcion = entPrd.nombre,
                        tasaIva = entPrd.tasa,
                        nombreTasaIva = entTasa.nombre,
                        admDivisa = _admDivisa,
                        estatus = _estatus,

                        etiqueta1 = entEmpresa.precio_1,
                        etiqueta2 = entEmpresa.precio_2,
                        etiqueta3 = entEmpresa.precio_3,
                        etiqueta4 = entEmpresa.precio_4,
                        etiqueta5 = entEmpresa.precio_5,
                        etiquetaMay1 = "Mayor 1",
                        etiquetaMay2 = "Mayor 2",

                        contenido1 = entPrd.contenido_1,
                        contenido2 = entPrd.contenido_2,
                        contenido3 = entPrd.contenido_3,
                        contenido4 = entPrd.contenido_4,
                        contenido5 = entPrd.contenido_pto,
                        contenidoMay1 = entPrdExt.contenido_may_1,
                        contenidoMay2 = entPrdExt.contenido_may_2,

                        finOferta = entPrd.fin,
                        inicioOferta = entPrd.inicio,
                        ofertaActiva = entPrd.estatus_oferta.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumOferta.Si :
                        DtoLibInventario.Producto.Enumerados.EnumOferta.No,

                        precioNeto1 = entPrd.precio_1,
                        precioNeto2 = entPrd.precio_2,
                        precioNeto3 = entPrd.precio_3,
                        precioNeto4 = entPrd.precio_4,
                        precioNeto5 = entPrd.precio_pto,
                        precioNetoMay1 = entPrdExt.precio_may_1,
                        precioNetoMay2 = entPrdExt.precio_may_2,

                        precioFullDivisa1 = entPrd.pdf_1,
                        precioFullDivisa2 = entPrd.pdf_2,
                        precioFullDivisa3 = entPrd.pdf_3,
                        precioFullDivisa4 = entPrd.pdf_4,
                        precioFullDivisa5 = entPrd.pdf_pto,
                        precioFullDivisaMay1 = entPrdExt.pdmf_1,
                        precioFullDivisaMay2 = entPrdExt.pdmf_2,

                        precioOferta = entPrd.precio_oferta,
                        precioSugerido = entPrd.precio_sugerido,

                        utilidad1 = entPrd.utilidad_1,
                        utilidad2 = entPrd.utilidad_2,
                        utilidad3 = entPrd.utilidad_3,
                        utilidad4 = entPrd.utilidad_4,
                        utilidad5 = entPrd.utilidad_pto,
                        utilidadMay1 = entPrdExt.utilidad_may_1,
                        utilidadMay2 = entPrdExt.utilidad_may_2,

                        empaque1 = emp1.nombre,
                        empaque2 = emp2.nombre,
                        empaque3 = emp3.nombre,
                        empaque4 = emp4.nombre,
                        empaque5 = emp5.nombre,
                        empaqueMay1 = empMay1.nombre,
                        empaqueMay2 = empMay2.nombre,

                        estatusOferta = _enOferta,
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Costo>
            Producto_GetCosto(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Costo>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                    var _estatus = entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo :
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo &&
                        entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    var entTasa = cnn.empresa_tasas.Find(entPrd.auto_tasa);
                    var entMedidaCompra = cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    var fechaV = new DateTime(2000, 01, 01);
                    var edad = fechaSistema.Subtract(entPrd.fecha_ult_costo).Days;

                    var costo = new DtoLibInventario.Producto.VerData.Costo()
                    {
                        admDivisa = _admDivisa,
                        codigo = entPrd.codigo,
                        contEmpaqueCompra = entPrd.contenido_compras,
                        descripcion = entPrd.nombre,
                        empaqueCompra = entMedidaCompra.nombre,
                        estatus = _estatus,
                        nombre = entPrd.nombre_corto,
                        nombreTasaIva = entTasa.nombre,
                        tasaIva = entTasa.tasa,
                        costoUnd = entPrd.costo_und,
                        costoDivisa = entPrd.divisa,
                        costoImportacionUnd = entPrd.costo_importacion_und,
                        costoPromedioUnd = entPrd.costo_promedio_und,
                        costoProveedorUnd = entPrd.costo_proveedor_und,
                        costoVarioUnd = entPrd.costo_varios_und,
                        fechaUltCambio = entPrd.fecha_ult_costo == fechaV ? (DateTime?)null : entPrd.fecha_ult_costo,
                        Edad = edad,
                    };
                    rt.Entidad = costo;
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
            Producto_AsignarRemoverDepositos(DtoLibInventario.Producto.Depositos.Asignar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        if (ficha.depAsignar != null)
                        {
                            foreach (var it in ficha.depAsignar)
                            {
                                var entPrdDep = new productos_deposito()
                                {
                                    auto_deposito = it.autoDeposito,
                                    auto_producto = ficha.autoProducto,
                                    averia = it.averia,
                                    disponible = it.disponible,
                                    fecha_conteo = it.fechaUltConteo,
                                    fisica = it.fisica,
                                    nivel_minimo = it.nivel_minimo,
                                    nivel_optimo = it.nivel_optimo,
                                    pto_pedido = it.pto_pedido,
                                    reservada = it.reservada,
                                    resultado_conteo = it.resultadoUltConteo,
                                    ubicacion_1 = it.ubicacion_1,
                                    ubicacion_2 = it.ubicacion_2,
                                    ubicacion_3 = it.ubicacion_3,
                                    ubicacion_4 = it.ubicacion_4,
                                };
                                cnn.productos_deposito.Add(entPrdDep);
                                cnn.SaveChanges();
                            }
                        }

                        if (ficha.depRemover != null)
                        {
                            foreach (var it in ficha.depRemover)
                            {
                                var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoProducto && f.auto_deposito == it.autoDeposito);
                                if (entPrdDep == null)
                                {
                                    rt.Mensaje = "[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO";
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                                if (entPrdDep.fisica != 0.0m)
                                {
                                    rt.Mensaje = "PRODUCTO / DEPOSITO CON EXISTENCIA";
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }

                                cnn.productos_deposito.Remove(entPrdDep);
                                cnn.SaveChanges();
                            }
                        }
                        ts.Complete();
                    }
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
            Producto_Verificar_DepositoRemover(string autoPrd, string autoDeposito)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == autoPrd && f.auto_deposito == autoDeposito);
                    if (entPrdDep == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (entPrdDep.fisica != 0.0m)
                    {
                        rt.Mensaje = "PRODUCTO / DEPOSITO CON EXISTENCIA";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Clasificacion.Resumen>
            Producto_Clasificacion_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Clasificacion.Resumen>();
            var list = new List<DtoLibInventario.Producto.Clasificacion.Resumen>();

            var nr = new DtoLibInventario.Producto.Clasificacion.Resumen() { Id = 1, Descripcion = "A" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Clasificacion.Resumen() { Id = 2, Descripcion = "B" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Clasificacion.Resumen() { Id = 3, Descripcion = "C" };
            list.Add(nr);
            nr = new DtoLibInventario.Producto.Clasificacion.Resumen() { Id = 4, Descripcion = "D" };
            list.Add(nr);

            result.Lista = list;
            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Editar.Obtener.Ficha>
            Producto_Editar_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Editar.Obtener.Ficha>();

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
                    };
                    var entPrdExtra = cnn.productos_extra.Find(autoPrd);
                    if (entPrdExtra == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO_EXTRA NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    var entPrdExt = cnn.productos_ext.Find(autoPrd);
                    if (entPrdExt == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO_EXT NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    //
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd",autoPrd);
                    var entPrdAlterno = cnn.productos_alterno.Where(w => w.auto_producto == autoPrd).ToList();
                    var _sql =@"select 
                                    id, 
                                    descripcion
                                from productos_talla_color_sabor
                                where auto_producto=@autoPrd";
                    var _lstTallaColorSabor = cnn.Database.SqlQuery<DtoLibInventario.Producto.Editar.Obtener.TallaColorSabor>(_sql, _p1).ToList();
                    //

                    var _origen = entPrd.origen.Trim().ToUpper() == "NACIONAL" ?
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Nacional :
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado;
                    var _estatus = entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo :
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo &&
                        entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                    var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                    switch (entPrd.categoria.Trim().ToUpper())
                    {
                        case "PRODUCTO TERMINADO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                            break;
                        case "BIEN DE SERVICIO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                            break;
                        case "MATERIA PRIMA":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                            break;
                        case "USO INTERNO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                            break;
                        case "SUB PRODUCTO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                            break;
                    }
                    var _clasificacion = DtoLibInventario.Producto.Enumerados.EnumClasificacionABC.SnDefinir;
                    switch (entPrd.abc.Trim().ToUpper())
                    {
                        case "A":
                            _clasificacion = DtoLibInventario.Producto.Enumerados.EnumClasificacionABC.A;
                            break;
                        case "B":
                            _clasificacion = DtoLibInventario.Producto.Enumerados.EnumClasificacionABC.B;
                            break;
                        case "C":
                            _clasificacion = DtoLibInventario.Producto.Enumerados.EnumClasificacionABC.C;
                            break;
                        case "D":
                            _clasificacion = DtoLibInventario.Producto.Enumerados.EnumClasificacionABC.D;
                            break;
                    }
                    var _pesado = DtoLibInventario.Producto.Enumerados.EnumPesado.No;
                    if (entPrd.estatus_pesado.Trim().ToUpper() == "1")
                    {
                        _pesado = DtoLibInventario.Producto.Enumerados.EnumPesado.Si;
                    }
                    var _imagen = new byte[] { };
                    if (entPrdExtra != null)
                    {
                        _imagen = entPrdExtra.imagen;
                    }
                    var _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.No;
                    if (entPrd.estatus_catalogo.Trim().ToUpper() == "1")
                    {
                        _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.Si;
                    }

                    var f = new DtoLibInventario.Producto.Editar.Obtener.Ficha()
                    {
                        auto = entPrd.auto,
                        autoDepartamento = entPrd.auto_departamento,
                        autoGrupo = entPrd.auto_grupo,
                        autoMarca = entPrd.auto_marca,
                        autoEmpCompra = entPrd.auto_empaque_compra,
                        autoTasaImpuesto = entPrd.auto_tasa,
                        codigo = entPrd.codigo,
                        nombre = entPrd.nombre_corto,
                        descripcion = entPrd.nombre,
                        modelo = entPrd.modelo,
                        referencia = entPrd.referencia,
                        contenidoCompra = entPrd.contenido_compras,
                        origen = _origen,
                        categoria = _categoria,
                        AdmPorDivisa = _admDivisa,
                        Clasificacion = _clasificacion,
                        imagen = _imagen,
                        esPesado = _pesado,
                        plu = entPrd.plu,
                        diasEmpaque = entPrd.dias_garantia,
                        activarCatalogo = _catalogo,
                        autoEmpInv= entPrdExt.auto_emp_inv_1,
                        contEmpInv = entPrdExt.cont_emp_inv_1,
                        peso=entPrd.peso,
                        volumen=entPrd.volumen,
                        alto=entPrd.alto,
                        largo= entPrd.largo,
                        ancho= entPrd.ancho,
                        autoEmpVentaTipo_1 = entPrdExt.auto_emp_venta_tipo_1,
                        autoEmpVentaTipo_2 = entPrdExt.auto_emp_venta_tipo_2,
                        autoEmpVentaTipo_3 = entPrdExt.auto_emp_venta_tipo_3,
                        contEmpVentaTipo_1 = entPrdExt.cont_emp_venta_tipo_1,
                        contEmpVentaTipo_2 = entPrdExt.cont_emp_venta_tipo_2,
                        contEmpVentaTipo_3 = entPrdExt.cont_emp_venta_tipo_3,
                        estatusTallaColorSabor=entPrd.estatus_talla_color_sabor,
                    };
                    var listPrdAlt = new List<DtoLibInventario.Producto.Editar.Obtener.FichaAlterno>();
                    foreach (var rg in entPrdAlterno)
                    {
                        listPrdAlt.Add(new DtoLibInventario.Producto.Editar.Obtener.FichaAlterno() { Codigo = rg.codigo_alterno });
                    }
                    f.CodigosAlterno = listPrdAlt;
                    f.tallaColorSabor = _lstTallaColorSabor;
                    rt.Entidad = f;
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
            Producto_Editar_Actualizar(DtoLibInventario.Producto.Editar.Actualizar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entPrd = cnn.productos.Find(ficha.auto);
                        if (entPrd == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };
                        var entPrdAlterno = cnn.productos_alterno.Where(w => w.auto_producto == ficha.auto).ToList();
                        var entPrdExtra = cnn.productos_extra.Find(ficha.auto);

                        entPrd.codigo = ficha.codigo;
                        entPrd.nombre = ficha.descripcion;
                        entPrd.nombre_corto = ficha.nombre;
                        entPrd.contenido_compras = ficha.contenidoCompra;
                        entPrd.modelo = ficha.modelo;
                        entPrd.referencia = ficha.referencia;
                        entPrd.auto_departamento = ficha.autoDepartamento;
                        entPrd.auto_grupo = ficha.autoGrupo;
                        entPrd.auto_marca = ficha.autoMarca;
                        entPrd.auto_empaque_compra = ficha.autoEmpCompra;
                        entPrd.auto_tasa = ficha.autoTasaImpuesto;
                        entPrd.tasa = ficha.tasaImpuesto;
                        entPrd.origen = ficha.origen;
                        entPrd.abc = ficha.abc;
                        entPrd.estatus_divisa = ficha.estatusDivisa;
                        entPrd.categoria = ficha.categoria;
                        entPrd.fecha_cambio = fechaSistema.Date;
                        entPrd.estatus_pesado = ficha.esPesado;
                        entPrd.plu = ficha.plu;
                        entPrd.dias_garantia = ficha.diasEmpaque;
                        entPrd.estatus_catalogo = ficha.estatusCatalogo;
                        entPrd.peso = ficha.peso;
                        entPrd.volumen = ficha.volumen;
                        entPrd.alto = ficha.alto;
                        entPrd.ancho = ficha.ancho;
                        entPrd.largo = ficha.largo;
                        //
                        entPrd.auto_precio_1= ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_2= ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_3 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_4 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_pto = ficha.autoEmpVentaTipo_1;
                        entPrd.contenido_1 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_2 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_3 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_4 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_pto = ficha.contEmpVentaTipo_1;
                        //
                        entPrd.estatus_talla_color_sabor = "0";
                        if (ficha.tallaColorSabor != null)
                        {
                            entPrd.estatus_talla_color_sabor = "1";
                        }
                        cnn.SaveChanges();

                        if (entPrdExtra != null)
                        {
                            entPrdExtra.imagen = ficha.imagen;
                        }
                        cnn.SaveChanges();

                        cnn.productos_alterno.RemoveRange(entPrdAlterno);
                        cnn.SaveChanges();

                        foreach (var rg in ficha.codigosAlterno)
                        {
                            var codAlterno = new productos_alterno()
                            {
                                auto_producto = ficha.auto,
                                codigo_alterno = rg.codigo,
                            };
                            cnn.productos_alterno.Add(codAlterno);
                            cnn.SaveChanges();
                        }

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmpInv", ficha.autoEmpInv);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@contEmpInv", ficha.contenidoInv);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@auto", ficha.auto);
                        //
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmpVentaTipo1", ficha.autoEmpVentaTipo_1);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@contEmpVentaTipo1", ficha.contEmpVentaTipo_1);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmpVentaTipo2", ficha.autoEmpVentaTipo_2);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@contEmpVentaTipo2", ficha.contEmpVentaTipo_2);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmpVentaTipo3", ficha.autoEmpVentaTipo_3);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@contEmpVentaTipo3", ficha.contEmpVentaTipo_3);
                        var xsql = @"update productos_ext 
                                        set 
                                            auto_emp_inv_1=@autoEmpInv, 
                                            cont_emp_inv_1=@contEmpInv,

                                            auto_emp_venta_tipo_1=@autoEmpVentaTipo1,
                                            auto_emp_venta_tipo_2=@autoEmpVentaTipo2,
                                            auto_emp_venta_tipo_3=@autoEmpVentaTipo3,
                                            cont_emp_venta_tipo_1=@contEmpVentaTipo1,
                                            cont_emp_venta_tipo_2=@contEmpVentaTipo2,
                                            cont_emp_venta_tipo_3=@contEmpVentaTipo3,   

                                            auto_precio_may_1 = @autoEmpVentaTipo2,
                                            auto_precio_may_2 = @autoEmpVentaTipo2,
                                            auto_precio_may_3 = @autoEmpVentaTipo2,
                                            auto_precio_may_4 = @autoEmpVentaTipo2,
                                            contenido_may_1 = @contEmpVentaTipo2,
                                            contenido_may_2 = @contEmpVentaTipo2,
                                            contenido_may_3 = @contEmpVentaTipo2,
                                            cont_may_4 = @contEmpVentaTipo2,

                                            auto_precio_dsp_1 = @autoEmpVentaTipo3,
                                            auto_precio_dsp_2 = @autoEmpVentaTipo3,
                                            auto_precio_dsp_3 = @autoEmpVentaTipo3,
                                            auto_precio_dsp_4 = @autoEmpVentaTipo3,
                                            cont_dsp_1 = @contEmpVentaTipo3,
                                            cont_dsp_2 = @contEmpVentaTipo3,
                                            cont_dsp_3 = @contEmpVentaTipo3,
                                            cont_dsp_4 = @contEmpVentaTipo3

                                        where auto_producto=@auto";
                        var r1 = cnn.Database.ExecuteSqlCommand(xsql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (r1 == 0) 
                        {
                            rt.Mensaje = "PRODUCTO_EXT NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        // MANEJA TALLA / COLOR / SABOR
                        if (ficha.tallaColorSabor != null)
                        {
                            foreach (var rg in ficha.tallaColorSabor.ListaTallaColorSabor)
                            {
                                if (rg.Accion == DtoLibInventario.Producto.Editar.Actualizar.Enumerados.EnumAccionTallaColorSabor.Agregar) 
                                {
                                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", entPrd.auto);
                                    var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@desc", rg.Descripcion);
                                    var xsql_1 = @"INSERT INTO `productos_talla_color_sabor` (
                                                    `id` ,
                                                    `auto_producto` ,
                                                    `descripcion`
                                            )
                                            VALUES (
                                                NULL,
                                                @autoPrd,
                                                @desc
                                            )";
                                    var xrt = cnn.Database.ExecuteSqlCommand(xsql_1, xp1, xp2);
                                    cnn.SaveChanges();
                                }
                                if (rg.Accion == DtoLibInventario.Producto.Editar.Actualizar.Enumerados.EnumAccionTallaColorSabor.Eliminar)
                                {
                                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idTallaColorSabor", rg.Id);
                                    var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", ficha.auto);
                                    var xsql_1 = @"select 
                                            count(*) as cnt 
                                        FROM `productos_deposito_talla_color_sabor`
                                        where auto_producto=@autoPrd
                                        and id_prd_talla_color_sabor=@idTallaColorSabor";
                                    var cnt = cnn.Database.SqlQuery<int>(xsql_1, xp1, xp2).FirstOrDefault();
                                    if (cnt > 0)
                                    {
                                        rt.Mensaje = "[RELACION CON DEPOSITO EXISTENTE] ID TALLA COLOR SABOR NO PUEDE SER ELIMNADO (" + rg.Descripcion + ")";
                                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return rt;
                                    }
                                    xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idTallaColorSabor", rg.Id);
                                    xsql_1 = @"DELETE FROM `productos_talla_color_sabor` 
                                             WHERE id=@idTallaColorSabor";
                                    var xrt = cnn.Database.ExecuteSqlCommand(xsql_1, xp1);
                                    cnn.SaveChanges();
                                }
                            }
                        }

                        if (ficha.hndEmpVenta != null)
                        {
                            foreach (var rg in ficha.hndEmpVenta.ListaEmpVenta)
                            {
                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@id", rg.id);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmp", rg.autoEmp);
                                var xp3 = new MySql.Data.MySqlClient.MySqlParameter("@cont", rg.contEmp);
                                var xsql_1 = @"UPDATE productos_ext_hnd_empventa  
                                                SET `auto_empaque`=@autoEmp,
                                                    `contenido_empaque`=@cont
                                            where id=@id";
                                var xrt = cnn.Database.ExecuteSqlCommand(xsql_1, xp1, xp2, xp3);
                                cnn.SaveChanges();
                            }
                        }
                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                rt.Mensaje = Helpers.MYSQL_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                rt.Mensaje = Helpers.ENTITY_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_EsBienServicio(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

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
                    };

                    rt.Entidad = (entPrd.categoria.Trim().ToUpper() == "BIEN DE SERVICIO");
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_HayDepositosAsignado(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

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
                    };

                    var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == autoPrd);
                    rt.Entidad = false;
                    if (ent != null)
                    {
                        rt.Entidad = true;
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoAuto 
            Producto_Nuevo_Agregar(DtoLibInventario.Producto.Agregar.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        var sql = "update sistema_contadores set a_productos=a_productos+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            rt.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        var aPrd = cnn.Database.SqlQuery<int>("select a_productos from sistema_contadores").FirstOrDefault();
                        var autoPrd = aPrd.ToString().Trim().PadLeft(10, '0');

                        var entPrd = new productos();
                        entPrd.auto = autoPrd;
                        entPrd.codigo = ficha.codigo;
                        entPrd.nombre = ficha.descripcion;
                        entPrd.nombre_corto = ficha.nombre;
                        entPrd.contenido_compras = ficha.contenidoCompra;
                        entPrd.modelo = ficha.modelo;
                        entPrd.referencia = ficha.referencia;
                        entPrd.auto_departamento = ficha.autoDepartamento;
                        entPrd.auto_grupo = ficha.autoGrupo;
                        entPrd.auto_marca = ficha.autoMarca;
                        entPrd.auto_empaque_compra = ficha.autoEmpCompra;
                        entPrd.auto_tasa = ficha.autoTasaImpuesto;
                        entPrd.origen = ficha.origen;
                        entPrd.abc = ficha.abc;
                        entPrd.estatus_divisa = ficha.estatusDivisa;
                        entPrd.categoria = ficha.categoria;
                        entPrd.fecha_cambio = fechaSistema.Date;
                        entPrd.fecha_alta = fechaSistema.Date;
                        entPrd.fecha_baja = fechaNula.Date;
                        entPrd.estatus = ficha.estatus;
                        entPrd.tasa = ficha.tasa;
                        entPrd.estatus_catalogo = ficha.estatusCatalogo;

                        entPrd.costo_proveedor = 0.0m;
                        entPrd.costo_proveedor_und = 0.0m;
                        entPrd.costo_importacion = 0.0m;
                        entPrd.costo_importacion_und = 0.0m;
                        entPrd.costo_varios = 0.0m;
                        entPrd.costo_varios_und = 0.0m;
                        entPrd.costo = 0.0m;
                        entPrd.costo_und = 0.0m;
                        entPrd.costo_promedio = 0.0m;
                        entPrd.costo_promedio_und = 0.0m;
                        entPrd.divisa = 0.0m;
                        entPrd.utilidad_1 = 0.0m;
                        entPrd.utilidad_2 = 0.0m;
                        entPrd.utilidad_3 = 0.0m;
                        entPrd.utilidad_4 = 0.0m;
                        entPrd.utilidad_pto = 0.0m;
                        entPrd.precio_1 = 0.0m;
                        entPrd.precio_2 = 0.0m;
                        entPrd.precio_3 = 0.0m;
                        entPrd.precio_4 = 0.0m;
                        entPrd.precio_pto = 0.0m;
                        entPrd.precio_sugerido = 0.0m;
                        entPrd.precio_oferta = 0.0m;
                        entPrd.inicio = fechaNula;
                        entPrd.fin = fechaNula;
                        entPrd.estatus_garantia = "0";
                        entPrd.dias_garantia = ficha.diasEmpaque;
                        entPrd.advertencia = "";
                        entPrd.comentarios = "";
                        entPrd.auto_subgrupo = "0000000001";
                        entPrd.auto_codigo_plan = "0000000001";
                        entPrd.alto = ficha.alto;
                        entPrd.largo = ficha.largo;
                        entPrd.ancho = ficha.ancho;
                        entPrd.peso = 0.0m;
                        entPrd.codigo_arancel = "";
                        entPrd.tasa_arancel = 0.0m;
                        entPrd.estatus_serial = "0";
                        entPrd.estatus_oferta = "0";
                        entPrd.estatus_web = "0";
                        entPrd.estatus_corte = "0";
                        entPrd.auto_precio_1 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_2 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_3 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_4 = ficha.autoEmpVentaTipo_1;
                        entPrd.auto_precio_pto = ficha.autoEmpVentaTipo_1; 
                        entPrd.memo = "";
                        entPrd.contenido_1 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_2 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_3 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_4 = ficha.contEmpVentaTipo_1;
                        entPrd.contenido_pto = ficha.contEmpVentaTipo_1;
                        entPrd.corte = "";
                        entPrd.estatus_pesado = ficha.esPesado;
                        entPrd.plu = ficha.plu;
                        entPrd.estatus_compuesto = "0";
                        entPrd.estatus_cambio = "0";
                        entPrd.fecha_movimiento = fechaNula;
                        entPrd.fecha_ult_venta = fechaNula;
                        entPrd.presentacion = "";
                        entPrd.lugar = "";
                        entPrd.fecha_ult_costo = fechaNula;
                        entPrd.fecha_lote = fechaNula;
                        entPrd.estatus_lote = "0";
                        entPrd.pdf_1 = 0.0m;
                        entPrd.pdf_2 = 0.0m;
                        entPrd.pdf_3 = 0.0m;
                        entPrd.pdf_4 = 0.0m;
                        entPrd.pdf_pto = 0.0m;
                        //
                        entPrd.peso= ficha.peso;
                        entPrd.volumen = ficha.volumen;
                        //
                        entPrd.estatus_talla_color_sabor = ficha.tallaColorSabor == null ? "0" : "1";
                        cnn.productos.Add(entPrd);
                        cnn.SaveChanges();

                        var entPrdExtra = new productos_extra()
                        {
                            auto_productos = autoPrd,
                            firma = new byte[] { },
                            imagen = ficha.imagen,
                        };
                        cnn.productos_extra.Add(entPrdExtra);
                        cnn.SaveChanges();

                        var entPrdExt = new productos_ext()
                        {
                            auto_producto = autoPrd,
                            auto_precio_may_1 = ficha.autoEmpVentaTipo_2,
                            auto_precio_may_2 = ficha.autoEmpVentaTipo_2,
                            auto_precio_may_3 = ficha.autoEmpVentaTipo_2,
                            auto_precio_may_4 = ficha.autoEmpVentaTipo_2,
                            contenido_may_1 = ficha.contEmpVentaTipo_2,
                            contenido_may_2 = ficha.contEmpVentaTipo_2,
                            contenido_may_3 = ficha.contEmpVentaTipo_2,
                            cont_may_4 = ficha.contEmpVentaTipo_2,
                            pdmf_1 = 0.0m,
                            pdmf_2 = 0.0m,
                            pdmf_3 = 0.0m,
                            pdmf_4 = 0.0m,
                            precio_may_1 = 0.0m,
                            precio_may_2 = 0.0m,
                            precio_may_3 = 0.0m,
                            precio_may_4 = 0.0m,
                            utilidad_may_1 = 0.0m,
                            utilidad_may_2 = 0.0m,
                            utilidad_may_3 = 0.0m,
                            utilidad_may_4 = 0.0m,
                            auto_emp_inv_1 = ficha.autoEmpInv,
                            cont_emp_inv_1 = ficha.contEmpInv,
                            //
                            auto_precio_dsp_1 = ficha.autoEmpVentaTipo_3,
                            cont_dsp_1 = ficha.contEmpVentaTipo_3,
                            utilidad_dsp_1 = 0m,
                            precio_dsp_1 = 0m,
                            pdivisafull_dsp_1 = 0m,
                            //
                            auto_precio_dsp_2 = ficha.autoEmpVentaTipo_3,
                            cont_dsp_2 = ficha.contEmpVentaTipo_3,
                            utilidad_dsp_2 = 0m,
                            precio_dsp_2 = 0m,
                            pdivisafull_dsp_2 = 0m,
                            //
                            auto_precio_dsp_3 = ficha.autoEmpVentaTipo_3,
                            cont_dsp_3 = ficha.contEmpVentaTipo_3,
                            utilidad_dsp_3 = 0m,
                            precio_dsp_3 = 0m,
                            pdivisafull_dsp_3 = 0m,
                            //
                            auto_precio_dsp_4 = ficha.autoEmpVentaTipo_3,
                            cont_dsp_4 = ficha.contEmpVentaTipo_3,
                            utilidad_dsp_4 = 0m,
                            precio_dsp_4 = 0m,
                            pdivisafull_dsp_4 = 0m,
                            //
                            auto_emp_venta_tipo_1 = ficha.autoEmpVentaTipo_1,
                            auto_emp_venta_tipo_2 = ficha.autoEmpVentaTipo_2,
                            auto_emp_venta_tipo_3 = ficha.autoEmpVentaTipo_3,
                            cont_emp_venta_tipo_1 = ficha.contEmpVentaTipo_1,
                            cont_emp_venta_tipo_2 = ficha.contEmpVentaTipo_2,
                            cont_emp_venta_tipo_3 = ficha.contEmpVentaTipo_3,
                        };
                        cnn.productos_ext.Add(entPrdExt);
                        cnn.SaveChanges();

                        foreach (var rg in ficha.codigosAlterno)
                        {
                            var codAlterno = new productos_alterno()
                            {
                                auto_producto = autoPrd,
                                codigo_alterno = rg.codigo,
                            };
                            cnn.productos_alterno.Add(codAlterno);
                            cnn.SaveChanges();
                        }

                        // MANEJA TALLA / COLOR / SABOR
                        if (ficha.tallaColorSabor != null) 
                        {
                            foreach (var rg in ficha.tallaColorSabor.ListaTallaColorSabor) 
                            {
                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd",entPrd.auto);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@desc",rg.Descripcion);
                                var xsql = @"INSERT INTO `productos_talla_color_sabor` (
                                                    `id` ,
                                                    `auto_producto` ,
                                                    `descripcion`
                                            )
                                            VALUES (
                                                NULL,
                                                @autoPrd,
                                                @desc
                                            )";
                                var xrt = cnn.Database.ExecuteSqlCommand(xsql, xp1, xp2);
                                cnn.SaveChanges();
                            }
                        }

                        if (ficha.hndEmpVenta != null) 
                        {
                            foreach (var rg in ficha.hndEmpVenta.ListaEmpVenta)
                            {
                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", entPrd.auto);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@autoEmp", rg.autoEmp);
                                var xp3 = new MySql.Data.MySqlClient.MySqlParameter("@cont", rg.contEmp);
                                var xp4 = new MySql.Data.MySqlClient.MySqlParameter("@tipo", rg.tipoEmp);
                                var xsql = @"INSERT INTO productos_ext_hnd_empventa 
                                                (
                                                    `id` ,
                                                    `auto_producto` ,
                                                    `auto_empaque` ,
                                                    `contenido_empaque` ,
                                                    `tipo_empaque`
                                                )
                                            VALUES (
                                                NULL , 
                                                @autoPrd,
                                                @autoEmp,
                                                @cont,
                                                @tipo
                                            )";
                                var xrt = cnn.Database.ExecuteSqlCommand(xsql, xp1, xp2, xp3, xp4);
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.hndPrecioVenta != null)
                        {
                            foreach (var rg in ficha.hndPrecioVenta.ListaPrecioVenta)
                            {
                                var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", entPrd.auto);
                                var p2 = new MySql.Data.MySqlClient.MySqlParameter("@tipoEmp", rg.tipoEmp);
                                var xsql = @"SELECT id  FROM `productos_ext_hnd_empventa`
                                                where auto_producto=@autoPrd
                                                and tipo_empaque=@tipoEmp";
                                var id = cnn.Database.SqlQuery<int?>(xsql, p1, p2).FirstOrDefault();
                                if (id == null) 
                                {
                                    throw new Exception("PROBLEMA AL REGISTRAR EMPAQUE VENTA");
                                }

                                var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", entPrd.auto);
                                var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@idTipoPrecio", rg.idHndTipoPrecio);
                                var xp3 = new MySql.Data.MySqlClient.MySqlParameter("@idTipoEmpaque", id.Value);
                                var xp4 = new MySql.Data.MySqlClient.MySqlParameter("@netoMonedaLocal", rg.netoMonedaLocal);
                                var xp5 = new MySql.Data.MySqlClient.MySqlParameter("@fullDivisa", rg.fullDivisa);
                                var xp6 = new MySql.Data.MySqlClient.MySqlParameter("@utilidadPorc", rg.utilidadPorc);
                                var xp7 = new MySql.Data.MySqlClient.MySqlParameter("@estatusOferta", rg.ofertaEstatus);
                                var xp8 = new MySql.Data.MySqlClient.MySqlParameter("@desdeOferta", rg.ofertaDesde);
                                var xp9 = new MySql.Data.MySqlClient.MySqlParameter("@hastaOferta", rg.ofertaHasta);
                                var xp10 = new MySql.Data.MySqlClient.MySqlParameter("@porcOferta", rg.ofertaPorc);
                                xsql = @"INSERT INTO `productos_ext_hnd_precioventa`
                                                (
                                                    `id` ,
                                                    `id_empresa_hnd_precio` ,
                                                    `id_prd_hnd_empVenta` ,
                                                    `auto_producto` ,
                                                    `neto_monedaLocal` ,
                                                    `full_divisa` ,
                                                    `utilidad_porct` ,
                                                    `estatus_oferta` ,
                                                    `desde_oferta` ,
                                                    `hasta_oferta` ,
                                                    `porct_oferta`
                                                )
                                            VALUES (
                                                NULL , 
                                                @idTipoPrecio,
                                                @idTipoEmpaque,
                                                @autoPrd,
                                                @netoMonedaLocal,
                                                @fullDivisa,
                                                @utilidadPorc,
                                                @estatusOferta,
                                                @desdeOferta,
                                                @hastaOferta,
                                                @porcOferta
                                            )";
                                var xrt = cnn.Database.ExecuteSqlCommand(xsql, xp1, xp2, xp3, xp4, xp5, xp6, xp7, xp8, xp9, xp10);
                                cnn.SaveChanges();
                            }
                        }
                        ts.Complete();
                        rt.Auto = autoPrd;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                rt.Mensaje = Helpers.MYSQL_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                rt.Mensaje = Helpers.ENTITY_VerificaError(ex);
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_CodigoProductoYaRegistrado(string codigo, string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    if (autoPrd == "")
                    {
                        if (codigo.Trim() != "")
                        {
                            var entPrd = cnn.productos.FirstOrDefault(f => f.codigo.Trim().ToUpper() == codigo);
                            rt.Entidad = true;
                            if (entPrd == null)
                            {
                                rt.Entidad = false;
                                return rt;
                            };
                        }
                    }
                    else
                    {
                        if (codigo.Trim() != "")
                        {
                            var entPrd = cnn.productos.FirstOrDefault(f => f.codigo.Trim().ToUpper() == codigo && f.auto != autoPrd);
                            rt.Entidad = true;
                            if (entPrd == null)
                            {
                                rt.Entidad = false;
                                return rt;
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_CodigoPluProductoYaRegistrado(string codigo, string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    if (autoPrd == "")
                    {
                        if (codigo.Trim() != "")
                        {
                            var entPrd = cnn.productos.FirstOrDefault(f => f.plu.Trim().ToUpper() == codigo);
                            rt.Entidad = true;
                            if (entPrd == null)
                            {
                                rt.Entidad = false;
                                return rt;
                            };
                        }
                    }
                    else
                    {
                        if (codigo.Trim() != "")
                        {
                            var entPrd = cnn.productos.FirstOrDefault(f => f.plu.Trim().ToUpper() == codigo && f.auto != autoPrd);
                            rt.Entidad = true;
                            if (entPrd == null)
                            {
                                rt.Entidad = false;
                                return rt;
                            };
                        }
                    }
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
            Producto_CambiarEstatusA_Activo(string auto)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entPrd = cnn.productos.Find(auto);
                        if (entPrd == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };

                        entPrd.estatus = "Activo";
                        entPrd.estatus_cambio = "0";
                        entPrd.fecha_cambio = fechaSistema.Date;
                        entPrd.fecha_baja = new DateTime(2000, 01, 01);

                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado
            Producto_CambiarEstatusA_Inactivo(string auto)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entPrd = cnn.productos.Find(auto);
                        if (entPrd == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };

                        entPrd.estatus = "Inactivo";
                        entPrd.fecha_cambio = fechaSistema.Date;
                        entPrd.estatus_cambio = "0";
                        entPrd.fecha_baja = fechaSistema.Date;

                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado 
            Producto_CambiarEstatusA_Suspendido(string auto)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var entPrd = cnn.productos.Find(auto);
                        if (entPrd == null)
                        {
                            rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };

                        entPrd.estatus = "Activo";
                        entPrd.fecha_cambio = fechaSistema.Date;
                        entPrd.estatus_cambio = "1";
                        entPrd.fecha_baja = new DateTime(2000, 01, 01);

                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                rt.Mensaje = msg;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_ExistenciaEnCero(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    rt.Entidad = true;
                    var ent = cnn.productos.Find(autoPrd);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Entidad = false;
                        return rt;
                    }

                    var lst = cnn.productos_deposito.Where(w => w.auto_producto == autoPrd).ToList();
                    if (lst != null)
                    {
                        if (lst.Count() > 0)
                        {
                            if (lst.Sum(s => Math.Abs(s.fisica)) != 0) { rt.Result = DtoLib.Enumerados.EnumResult.isError; rt.Entidad = false; rt.Mensaje = "EXISTENCIA [ FISICA ] NO ES IGUAL A CERO (0)"; return rt; }
                            if (lst.Sum(s => Math.Abs(s.disponible)) != 0) { rt.Result = DtoLib.Enumerados.EnumResult.isError; rt.Entidad = false; rt.Mensaje = "EXISTENCIA [ DISPONIBLE ] NO ES IGUAL A CERO (0)"; return rt; }
                            if (lst.Sum(s => Math.Abs(s.reservada)) != 0) { rt.Result = DtoLib.Enumerados.EnumResult.isError; rt.Entidad = false; rt.Mensaje = "EXISTENCIA [ RESERVADA ] NO ES IGUAL A CERO (0)"; return rt; }
                            if (lst.Sum(s => Math.Abs(s.averia)) != 0) { rt.Result = DtoLib.Enumerados.EnumResult.isError; rt.Entidad = false; rt.Mensaje = "EXISTENCIA [ AVERIA ] NO ES IGUAL A CERO (0)"; return rt; }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<bool> 
            Producto_Verificar_QueExista_EstatusActivo_NoSeaBienServicio(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();

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
                    };

                    if (entPrd.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        rt.Mensaje = "PRODUCTO EN ESTADO INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    if (entPrd.categoria.Trim().ToUpper() == "BIEN DE SERVICIO")
                    {
                        rt.Mensaje = "CATEGORIA DEL PRODUCTO NO PERMITE ASIGNAR/CAMBIAR DATOS A DEPOSITO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    rt.Entidad = true;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Ver.Ficha> 
            Producto_GetDeposito(DtoLibInventario.Producto.Depositos.Ver.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Ver.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(filtro.autoProducto);
                    if (entPrd == null)
                    {
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var entDep = cnn.empresa_depositos.Find(filtro.autoDeposito);
                    if (entDep == null)
                    {
                        result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == filtro.autoProducto && f.auto_deposito == filtro.autoDeposito);
                    if (ent == null)
                    {
                        result.Mensaje = "PROBLEMA AL ENCONTRAR PRODUCTO / DEPOSITO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _empaque = "";
                    var entMed = cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    if (entMed != null) { _empaque = entMed.nombre; }

                    var fnula = new DateTime(2000, 01, 01);
                    var fconteo = ent.fecha_conteo;
                    var nr = new DtoLibInventario.Producto.Depositos.Ver.Ficha()
                    {
                        disponible = ent.disponible,
                        fechaUltConteo = ent.fecha_conteo == fnula ? (DateTime?)null : fconteo,
                        fisica = ent.fisica,
                        nivelMinimo = ent.nivel_minimo,
                        nivelOptimo = ent.nivel_optimo,
                        ptoPedido = ent.pto_pedido,
                        reservada = ent.reservada,
                        resultadoUltConteo = ent.resultado_conteo,
                        ubicacion_1 = ent.ubicacion_1,
                        ubicacion_2 = ent.ubicacion_2,
                        ubicacion_3 = ent.ubicacion_3,
                        ubicacion_4 = ent.ubicacion_4,
                        autoDeposito = ent.auto_deposito,
                        autoProducto = ent.auto_producto,
                        averia = ent.averia,
                        codigoDeposito = entDep.codigo,
                        codigoProducto = entPrd.codigo,
                        contenidoCompra = entPrd.contenido_compras,
                        empaqueCompra = _empaque,
                        nombreDeposito = entDep.nombre,
                        nombreProducto = entPrd.nombre,
                        referenciaProducto = entPrd.referencia,
                    };
                    result.Entidad = nr;
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
            Producto_EditarDeposito(DtoLibInventario.Producto.Depositos.Editar.Ficha ficha)
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
                        entPrd.fecha_cambio = fechaSistema.Date;
                        cnn.SaveChanges();

                        var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoProducto && f.auto_deposito == ficha.autoDeposito);
                        if (entPrdDep == null)
                        {
                            result.Mensaje = "DEPOSITO NO DEFINIDO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        entPrdDep.nivel_minimo = ficha.nivelMinimo;
                        entPrdDep.nivel_optimo = ficha.nivelOptimo;
                        entPrdDep.pto_pedido = ficha.ptoPedido;
                        entPrdDep.ubicacion_1 = ficha.ubicacion_1;
                        entPrdDep.ubicacion_2 = ficha.ubicacion_2;
                        entPrdDep.ubicacion_3 = ficha.ubicacion_3;
                        entPrdDep.ubicacion_4 = ficha.ubicacion_4;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Lista.Ficha> 
            ILibInventario.IProducto.Producto_GetDepositos(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Lista.Ficha>();

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

                    var list = new List<DtoLibInventario.Producto.Depositos.Lista.Data>();
                    var entDep = cnn.productos_deposito.Where(w => w.auto_producto == autoPrd).ToList();
                    if (entDep != null)
                    {
                        if (entDep.Count > 0)
                        {
                            foreach (var it in entDep)
                            {
                                var nr = new DtoLibInventario.Producto.Depositos.Lista.Data()
                                {
                                    autoDeposito = it.auto_deposito,
                                    codigoDeposito = it.empresa_depositos.codigo,
                                    nombreDeposito = it.empresa_depositos.nombre,
                                };
                                list.Add(nr);
                            }
                        }
                    };
                    var ficha = new DtoLibInventario.Producto.Depositos.Lista.Ficha()
                    {
                        autoPrd = entPrd.auto,
                        codigoPrd = entPrd.codigo,
                        descripcionPrd = entPrd.nombre,
                        nombrePrd = entPrd.nombre_corto,
                        referenciaPrd = entPrd.referencia,
                    };
                    ficha.depositos = list;

                    rt.Entidad = ficha;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Estatus.Actual.Ficha> 
            Producto_Estatus_GetFicha(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Estatus.Actual.Ficha>();

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
                    };

                    var _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
                    if (entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    else if (entPrd.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    }

                    var nr = new DtoLibInventario.Producto.Estatus.Actual.Ficha()
                    {
                        autoProducto = entPrd.auto,
                        codigoProducto = entPrd.codigo,
                        nombreProducto = entPrd.nombre,
                        referenciaProducto = entPrd.referencia,
                        estatus = _estatus,
                    };
                    rt.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Imagen> 
            Producto_GetImagen(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Imagen>();

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
                    };

                    var _imagen = new byte[] { };
                    var entPrdExtra = cnn.productos_extra.Find(autoPrd);
                    if (entPrdExtra != null)
                    {
                        _imagen = entPrdExtra.imagen;
                    }

                    var nr = new DtoLibInventario.Producto.VerData.Imagen()
                    {
                        codigo = entPrd.codigo,
                        descripcion = entPrd.nombre,
                        imagen = _imagen
                    };
                    rt.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Plu.Lista.Resumen> 
            Producto_Plu_Lista()
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Plu.Lista.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos.Where(f => f.plu != "").ToList();
                    var list = new List<DtoLibInventario.Producto.Plu.Lista.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Producto.Plu.Lista.Resumen()
                                {
                                    autoId = s.auto,
                                    codigo = s.codigo,
                                    nombre = s.nombre_corto,
                                    descripcion = s.nombre,
                                    plu = s.plu,

                                };
                                return r;
                            }).ToList();
                        }
                    }
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Identificacion> 
            Producto_GetIdentificacion(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Identificacion>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };
                    var entPrdExt = cnn.productos_ext.Find(autoPrd);
                    if (entPrdExt == null)
                    {
                        rt.Mensaje = "PRODUCTO_EXT NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    };
                    var entPrdAlterno = cnn.productos_alterno.Where(w => w.auto_producto == autoPrd).ToList();
                    var entPrdMedidaEmpInv = cnn.productos_medida.Find(entPrdExt.auto_emp_inv_1);

                    var entPrdMed = cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    var _depart = entPrd.empresa_departamentos.nombre;
                    var _codDepart = entPrd.empresa_departamentos.codigo;
                    var _grupo = entPrd.productos_grupo.nombre;
                    var _codGrupo = entPrd.productos_grupo.codigo;
                    var _marca = entPrd.productos_marca.nombre;
                    var _nombreTasaIva = entPrd.empresa_tasas.nombre;
                    var _empCompra = entPrdMed.nombre;
                    var _decimales = entPrdMed.decimales;
                    var _origen = entPrd.origen.Trim().ToUpper() == "NACIONAL" ?
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Nacional :
                        DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado;
                    var _estatus = entPrd.estatus.Trim().ToUpper() == "ACTIVO" ?
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo :
                        DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                    if (_estatus == DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo &&
                        entPrd.estatus_cambio.Trim().ToUpper() == "1")
                    {
                        _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                    }
                    var _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.No;
                    if (entPrd.estatus_catalogo.Trim().ToUpper() == "1")
                    {
                        _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.Si;
                    }
                    var _admDivisa = entPrd.estatus_divisa.Trim().ToUpper() == "1" ?
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si :
                        DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                    var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                    switch (entPrd.categoria.Trim().ToUpper())
                    {
                        case "PRODUCTO TERMINADO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                            break;
                        case "BIEN DE SERVICIO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                            break;
                        case "MATERIA PRIMA":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                            break;
                        case "USO INTERNO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                            break;
                        case "SUB PRODUCTO":
                            _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                            break;
                    }

                    var id = new DtoLibInventario.Producto.VerData.Identificacion()
                    {
                        AdmPorDivisa = _admDivisa,
                        advertencia = entPrd.advertencia,
                        auto = entPrd.auto,
                        categoria = _categoria,
                        codigo = entPrd.codigo,
                        codigoDepartamento = _codDepart,
                        codigoGrupo = _codGrupo,
                        comentarios = entPrd.comentarios,
                        contenidoCompra = entPrd.contenido_compras,
                        departamento = _depart,
                        descripcion = entPrd.nombre,
                        empaqueCompra = _empCompra,
                        estatus = _estatus,
                        fechaAlta = entPrd.fecha_alta,
                        fechaBaja = entPrd.fecha_baja,
                        fechaUltActualizacion = entPrd.fecha_cambio,
                        grupo = _grupo,
                        marca = _marca,
                        modelo = entPrd.modelo,
                        nombre = entPrd.nombre_corto,
                        nombreTasaIva = _nombreTasaIva,
                        origen = _origen,
                        presentacion = entPrd.presentacion,
                        referencia = entPrd.referencia,
                        tasaIva = entPrd.tasa,
                        tipoABC = entPrd.abc,
                        autoDepartamento = entPrd.auto_departamento,
                        autoGrupo = entPrd.auto_grupo,
                        autoMarca = entPrd.auto_marca,
                        decimales = _decimales,
                        activarCatalogo = _catalogo,
                        estatusPesado = entPrd.estatus_pesado,
                        plu = entPrd.plu,
                        diasEmpaque = entPrd.dias_garantia,
                        empInventario = entPrdMedidaEmpInv.nombre,
                        contEmpInv= entPrdExt.cont_emp_inv_1,
                        codAlterno = entPrdAlterno.Select(s =>
                        {
                            var nr = new DtoLibInventario.Producto.VerData.CodAlterno()
                            {
                                codigo = s.codigo_alterno,
                            };
                            return nr;
                        }).ToList(),
                    };

                    rt.Entidad = id;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Proveedor.Ficha> 
            Producto_GetProveedores(string autoPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Proveedor.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entPrd = cnn.productos.Find(autoPrd);
                    if (entPrd == null)
                    {
                        rt.Mensaje = "PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var list = new List<DtoLibInventario.Producto.VerData.Proveedor.Detalle>();
                    var ng = new DtoLibInventario.Producto.VerData.Proveedor.Ficha()
                    {
                        autoProducto = entPrd.auto,
                        codigoProducto = entPrd.codigo,
                        nombreProducto = entPrd.nombre,
                        referenciaProducto = entPrd.referencia,
                    };
                    var q = cnn.productos_proveedor.Where(f => f.auto_producto == autoPrd).ToList();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Producto.VerData.Proveedor.Detalle()
                                {
                                    ciRif = s.proveedores.ci_rif,
                                    codigo = s.proveedores.codigo,
                                    codigoRefPrd = s.codigo_producto,
                                    direccionFiscal = s.proveedores.dir_fiscal,
                                    idAuto = s.auto_proveedor,
                                    razonSocial = s.proveedores.razon_social,
                                    telefonos = s.proveedores.telefono + s.proveedores.telefono,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    ng.proveedores = list;
                    rt.Entidad = ng;
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
            Producto_Deposito_AsignacionMasiva(DtoLibInventario.Producto.Depositos.AsignacionMasiva.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                var listDep = "";
                if (ficha.departamentosNoIncluir.Count > 0)
                {
                    foreach (var rg in ficha.departamentosNoIncluir.ToList())
                    {
                        if (listDep.Trim() != "")
                        {
                            listDep += ", ";
                        }
                        listDep += "'" + rg.auto + "'";
                    }
                }
                else 
                {
                    listDep = "''";
                }
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql_1 = @"select p.auto from productos as p
                                    where auto not in (
                                                        SELECT pd.auto_producto 
                                                        FROM productos_deposito as pd
                                                        where pd.auto_deposito=@autoDeposito
                                                        )
                                    and p.estatus='ACTIVO'
                                    and p.categoria ='Producto Terminado'
                                    and p.auto_departamento NOT IN ("+listDep+")";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoDeposito", ficha.depositoDestino.autoDeposito);
                        var lst = cnn.Database.SqlQuery<string>(sql_1, p1).ToList();
                        if (lst.Count > 0)
                        {
                            var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@xp1", "");
                            var xp2 = new MySql.Data.MySqlClient.MySqlParameter("@xp2", ficha.depositoDestino.autoDeposito);
                            var xsql_2 = @"INSERT INTO productos_deposito (
                                            auto_producto ,
                                            auto_deposito ,
                                            fisica ,
                                            reservada ,
                                            disponible ,
                                            ubicacion_1 ,
                                            ubicacion_2 ,
                                            ubicacion_3 ,
                                            ubicacion_4 ,
                                            nivel_minimo ,
                                            pto_pedido ,
                                            nivel_optimo ,
                                            fecha_conteo ,
                                            resultado_conteo ,
                                            averia)
                                         VALUES (@xp1, @xp2, '0.000', '0.000', '0.000', '', '', '', '', '0.000', '0.000', '0.000', '2000-01-01', '0.000', '0.000')";
                            foreach (var it in lst)
                            {
                                xp1.Value = it;
                                var res = cnn.Database.ExecuteSqlCommand(xsql_2, xp1, xp2);
                                if (res == 0) 
                                {
                                    rt.Mensaje = "PRODUCTO NO ASIGNADO AL DEPOSITO";
                                    rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return rt;
                                }
                            }
                            cnn.SaveChanges();
                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<string> 
            Producto_GetId_ByCodigoBarra(string codBarra)
        {
            var rt = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codBarra",codBarra);
                    var sql_1 = @"select auto_producto from productos_alterno where codigo_alterno=@codBarra";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<string>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        rt.Entidad = "";
                    }
                    else 
                    {
                        rt.Entidad = ent;
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Precio.Ficha> 
            Producto_Precio_GetById(string idPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Precio.Ficha>();

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
                                    p.auto_precio_1 as idEmp1_1, 
                                    p.auto_precio_2 as idEmp1_2, 
                                    p.auto_precio_3 as idEmp1_3,  
                                    p.auto_precio_4 as idEmp1_4, 
                                    p.auto_precio_pto as idEmp1_5,
                                    p.contenido_1 as contEmp1_1, 
                                    p.contenido_2 as contEmp1_2, 
                                    p.contenido_3 as contEmp1_3, 
                                    p.contenido_4 as contEmp1_4,  
                                    p.contenido_pto as contEmp1_5,  
                                    pMedEmp1_1.nombre as descEmp1_1,
                                    pMedEmp1_2.nombre as descEmp1_2,
                                    pMedEmp1_3.nombre as descEmp1_3,
                                    pMedEmp1_4.nombre as descEmp1_4,
                                    pMedEmp1_5.nombre as descEmp1_5,
                                    p.utilidad_1 as utEmp1_1,
                                    p.utilidad_2 as utEmp1_2,
                                    p.utilidad_3 as utEmp1_3,
                                    p.utilidad_4 as utEmp1_4,
                                    p.utilidad_pto as utEmp1_5,
                                    p.precio_1 as pnEmp1_1,
                                    p.precio_2 as pnEmp1_2,
                                    p.precio_3 as pnEmp1_3,
                                    p.precio_4 as pnEmp1_4,
                                    p.precio_pto as pnEmp1_5,
                                    p.pdf_1 as pfdEmp1_1,
                                    p.pdf_2 as pfdEmp1_2,
                                    p.pdf_3 as pfdEmp1_3,
                                    p.pdf_4 as pfdEmp1_4,
                                    p.pdf_pto as pfdEmp1_5,

                                    pExt.auto_precio_may_1 as idEmp2_1, 
                                    pExt.auto_precio_may_2 as idEmp2_2, 
                                    pExt.auto_precio_may_3 as idEmp2_3, 
                                    pExt.auto_precio_may_4 as idEmp2_4, 
                                    pExt.contenido_may_1 as contEmp2_1, 
                                    pExt.contenido_may_2 as contEmp2_2, 
                                    pExt.contenido_may_3 as contEmp2_3, 
                                    pExt.cont_may_4 as contEmp2_4, 
                                    pMedEmp2_1.nombre as descEmp2_1,
                                    pMedEmp2_2.nombre as descEmp2_2,
                                    pMedEmp2_3.nombre as descEmp2_3,
                                    pMedEmp2_4.nombre as descEmp2_4,
                                    pExt.utilidad_may_1 as utEmp2_1,
                                    pExt.utilidad_may_2 as utEmp2_2,
                                    pExt.utilidad_may_3 as utEmp2_3,
                                    pExt.utilidad_may_4 as utEmp2_4,
                                    pExt.precio_may_1 as pnEmp2_1,
                                    pExt.precio_may_2 as pnEmp2_2,
                                    pExt.precio_may_3 as pnEmp2_3,
                                    pExt.precio_may_4 as pnEmp2_4,
                                    pExt.pdmf_1 as pfdEmp2_1,
                                    pExt.pdmf_2 as pfdEmp2_2,
                                    pExt.pdmf_3 as pfdEmp2_3,
                                    pExt.pdmf_4 as pfdEmp2_4,
        
                                    pExt.auto_precio_dsp_1 as idEmp3_1, 
                                    pExt.auto_precio_dsp_2 as idEmp3_2, 
                                    pExt.auto_precio_dsp_3 as idEmp3_3, 
                                    pExt.auto_precio_dsp_4 as idEmp3_4, 
                                    pExt.cont_dsp_1 as contEmp3_1, 
                                    pExt.cont_dsp_2 as contEmp3_2, 
                                    pExt.cont_dsp_3 as contEmp3_3, 
                                    pExt.cont_dsp_4 as contEmp3_4, 
                                    pMedEmp3_1.nombre as descEmp3_1,
                                    pMedEmp3_2.nombre as descEmp3_2,
                                    pMedEmp3_3.nombre as descEmp3_3,
                                    pMedEmp3_4.nombre as descEmp3_4,
                                    pExt.utilidad_dsp_1 as utEmp3_1,
                                    pExt.utilidad_dsp_2 as utEmp3_2,
                                    pExt.utilidad_dsp_3 as utEmp3_3,
                                    pExt.utilidad_dsp_4 as utEmp3_4,
                                    pExt.precio_dsp_1 as pnEmp3_1,
                                    pExt.precio_dsp_2 as pnEmp3_2,
                                    pExt.precio_dsp_3 as pnEmp3_3,
                                    pExt.precio_dsp_4 as pnEmp3_4,
                                    pExt.pdivisafull_dsp_1 as pfdEmp3_1,
                                    pExt.pdivisafull_dsp_2 as pfdEmp3_2,
                                    pExt.pdivisafull_dsp_3 as pfdEmp3_3,
                                    pExt.pdivisafull_dsp_4 as pfdEmp3_4

                                    FROM productos as p
                                    join empresa_tasas as eTasa on eTasa.auto=p.auto_tasa
                                    join productos_ext as pExt on pExt.auto_producto=p.auto

                                    join productos_medida as pMedEmp1_1 on pMedEmp1_1.auto=p.auto_precio_1
                                    join productos_medida as pMedEmp1_2 on pMedEmp1_2.auto=p.auto_precio_2
                                    join productos_medida as pMedEmp1_3 on pMedEmp1_3.auto=p.auto_precio_3
                                    join productos_medida as pMedEmp1_4 on pMedEmp1_4.auto=p.auto_precio_4
                                    join productos_medida as pMedEmp1_5 on pMedEmp1_5.auto=p.auto_precio_pto

                                    join productos_medida as pMedEmp2_1 on pMedEmp2_1.auto=pExt.auto_precio_may_1
                                    join productos_medida as pMedEmp2_2 on pMedEmp2_2.auto=pExt.auto_precio_may_2
                                    join productos_medida as pMedEmp2_3 on pMedEmp2_3.auto=pExt.auto_precio_may_3
                                    join productos_medida as pMedEmp2_4 on pMedEmp2_4.auto=pExt.auto_precio_may_4

                                    join productos_medida as pMedEmp3_1 on pMedEmp3_1.auto=pExt.auto_precio_dsp_1
                                    join productos_medida as pMedEmp3_2 on pMedEmp3_2.auto=pExt.auto_precio_dsp_2
                                    join productos_medida as pMedEmp3_3 on pMedEmp3_3.auto=pExt.auto_precio_dsp_3
                                    join productos_medida as pMedEmp3_4 on pMedEmp3_4.auto=pExt.auto_precio_dsp_4

                                    where p.auto=@id";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idPrd);
                    var _ent = cnn.Database.SqlQuery<DtoLibInventario.Producto.Precio.Ficha>(sql, p1).FirstOrDefault();
                    if (_ent == null)
                    {
                        rt.Mensaje = "PRODUCTO [ ID ] NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
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
    }
}