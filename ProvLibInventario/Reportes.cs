﻿using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> Reportes_MaestroProducto(DtoLibInventario.Reportes.MaestroProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = "select p.codigo as codigoPrd , p.nombre as nombrePrd , p.referencia as referenciaPrd, p.modelo as modeloPrd, " +
                        " p.estatus as estatusPrd, p.estatus_divisa as estatusDivisaPrd, p.estatus_cambio as estatusCambioPrd, " +
                        " p.contenido_compras as contenidoPrd, p.origen as origenPrd, p.categoria as categoriaPrd, " +
                        " ed.nombre as departamento, " +
                        " pg.nombre as grupo, " +
                        " pm.nombre as empaque, " +
                        " etasa.tasa as tasaIva ";

                    var sql_2 = " from productos as p " +
                        " join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        " join productos_grupo as pg on p.auto_grupo=pg.auto " +
                        " join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        " join empresa_tasas as etasa on p.auto_tasa=etasa.auto ";

                    var sql_3 =" where 1=1 ";

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

                    if (filtro.autoDepartamento != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.admDivisa != DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var _f = "1";
                        if (filtro.admDivisa == DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No)
                            _f = "0";
                        sql_3 += " and p.estatus_divisa=@estatusDivisa ";
                        p2.ParameterName = "@estatusDivisa";
                        p2.Value = _f;
                    }
                    if (filtro.autoTasa != "")
                    {
                        sql_3 += " and p.auto_tasa=@autoTasa ";
                        p3.ParameterName = "@autoTasa";
                        p3.Value = filtro.autoTasa;
                    }
                    if (filtro.estatus != DtoLibInventario.Reportes.enumerados.EnumEstatus.SnDefinir)
                    {
                        var _f = "Activo";
                        if (filtro.estatus == DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo)
                        {
                            _f = "Inactivo";
                        }
                        sql_3 += " and p.estatus=@estatus ";
                        p4.ParameterName = "@estatus";
                        p4.Value = _f;
                    }
                    if (filtro.categoria != DtoLibInventario.Reportes.enumerados.EnumCategoria.SnDefinir)
                    {
                        var _f = "";
                        switch (filtro.categoria)
                        {
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.BienServicio:
                                _f = "Bien De Servicio";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.ProductoTerminado:
                                _f = "Producto Terminado";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.MateriaPrima:
                                _f = "Materia Prima";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.SubProducto:
                                _f = "Sub Producto";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.UsoInterno:
                                _f = "Uso Interno";
                                break;
                        }
                        sql_3 += " and p.categoria=@categoria ";
                        p5.ParameterName = "@categoria";
                        p5.Value = _f;
                    }
                    if (filtro.origen != DtoLibInventario.Reportes.enumerados.EnumOrigen.SnDefinir)
                    {
                        var _f = "Nacional";
                        if (filtro.origen == DtoLibInventario.Reportes.enumerados.EnumOrigen.Importado)
                        {
                            _f = "Importado";
                        }
                        sql_3 += " and p.origen=@origen ";
                        p6.ParameterName = "@origen";
                        p6.Value = _f;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql_2 += " join productos_deposito as pdeposito on p.auto=pdeposito.auto_producto and pdeposito.auto_deposito=@autoDeposito ";
                        p7.ParameterName = "@autoDeposito";
                        p7.Value = filtro.autoDeposito;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo";
                        p8.ParameterName = "@autoGrupo";
                        p8.Value = filtro.autoGrupo;
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroProducto.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9, pA, pB, pC, pD).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> Reportes_MaestroInventario(DtoLibInventario.Reportes.MaestroInventario.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    //var sql_1 = "select p.codigo as codigoPrd , p.nombre as nombrePrd , p.referencia as referenciaPrd, p.modelo as modeloPrd, " +
                    //    " p.estatus as estatusPrd, p.estatus_divisa as estatusDivisaPrd, p.estatus_cambio as estatusCambioPrd, " +
                    //    " p.costo_und as costoUnd, p.divisa as costoDivisa, p.contenido_compras as contenidoCompras, " +
                    //    " ed.nombre as departamento, " +
                    //    " pm.decimales as decimales, ";
                    //var sql_2 =" from productos as p " +
                    //    " join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                    //    " join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                    //    " where 1=1 ";


                    var sql_1 = "select " +
                        "p.codigo as codigoPrd, " +
                        "p.nombre as nombrePrd, " +
                        "p.referencia as referenciaPrd, " +
                        "p.modelo as modeloPrd, " +
                        "p.estatus as estatusPrd, " +
                        "p.estatus_divisa as estatusDivisaPrd, " +
                        "p.estatus_cambio as estatusCambioPrd, " +
                        "p.costo_und as costoUnd, " +
                        "p.divisa as costoDivisa, " +
                        "p.contenido_compras as contenidoCompras, " +
                        "ed.nombre as departamento, " +
                        "pg.nombre as grupo, "+
                        "pm.decimales as decimales, ";

                    var sql_2 = "from productos as p " +
                        "join empresa_tasas as et on et.auto=p.auto_tasa " +
                        "join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        "join productos_grupo as pg on p.auto_grupo=pg.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto ";

                    var sql_3 = "where p.estatus='Activo' and p.categoria<>'Bien de Servicio' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();


                    if (filtro.autoDeposito == "")
                    {
                        sql_1 += "(SELECT sum(fisica) from productos_deposito where auto_producto=p.auto) as existencia, " +
                            "0 as pn1, " +
                            "0 as pn2, " +
                            "0 as pn3, " +
                            "0 as pn4, " +
                            "0 as pn5, " +
                            "'' as codigoSuc, " +
                            "'' as nombreGrupo, " +
                            "'' as precioId ";
                    }
                    else 
                    {
                        sql_1 += "(SELECT sum(fisica) from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as existencia, " +
                            "(SELECT (p.pdf_1/ ((et.tasa/100)+1))  from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as pn1, " +
                            "(SELECT (p.pdf_2/ ((et.tasa/100)+1))  from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as pn2, " +
                            "(SELECT (p.pdf_3/ ((et.tasa/100)+1))  from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as pn3, " +
                            "(SELECT (p.pdf_4/ ((et.tasa/100)+1))  from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as pn4, " +
                            "(SELECT (p.pdf_4/ ((et.tasa/100)+1))  from productos_deposito where auto_producto=p.auto and auto_deposito=@autoDeposito) as pn5, " +
                            "es.codigo as codigoSuc, " +
                            "eg.nombre as nombreGrupo, " +
                            "eg.idprecio as precioId ";

                        sql_2 += "join empresa_depositos as edep on edep.auto=@autoDeposito " +
                            "join empresa_sucursal as es on edep.codigo_sucursal=es.codigo " +
                            "join empresa_grupo as eg on eg.auto=es.autoempresagrupo ";

                        p4.ParameterName = "@autoDeposito";
                        p4.Value = filtro.autoDeposito;
                    }

                    if (filtro.autoDepartamento != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }

                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo ";
                        p6.ParameterName = "@autoGrupo";
                        p6.Value = filtro.autoGrupo;
                    }

                    var sql = sql_1 + sql_2+ sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroInventario.Ficha>(sql, p1, p2, p3, p4, p5,p6).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT abs(sum(pk.cantidad_und*pk.signo)) as cntUnd, p.nombre, p.codigo, pm.decimales as decimales, " +
                        "p.estatus_pesado as estatusPesado, count(*) as cntDoc "+
                        "FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        "where pk.fecha>=@p1 and pk.fecha<=@p2 " +
                        "and pk.modulo=@p3 and pk.estatus_anulado='0' " ;
                    var sql2="group by pk.auto_producto ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.Desde.Date;

                    p2.ParameterName = "@p2";
                    p2.Value = filtro.Hasta.Date;

                    var modulo="";
                    switch(filtro.Modulo)
                    {
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Compras:
                            modulo="Compras";
                            break;
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Ventas:
                            modulo="Ventas";
                            break;
                        case DtoLibInventario.Reportes.enumerados.EnumModulo.Inventario:
                            modulo="Inventario";
                            sql += " and pk.siglas='AJU' ";
                            break;
                    }
                    p3.ParameterName = "@p3";
                    p3.Value = modulo;

                    if (filtro.autoDeposito!="")
                    {
                        sql += "and pk.auto_deposito=@p4 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.autoDeposito;
                    }
                    sql += sql2;

                    cnn.Database.CommandTimeout = 0;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.Top20.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha> Reportes_TopDepartUtilidad(DtoLibInventario.Reportes.TopDepartUtilidad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT  ed.nombre as Departamento , " +
                        //"abs(sum(((pk.precio_und- pk.costo_und) * (pk.cantidad_und*pk.signo)))) as utilidad, "+
                        "sum( pk.precio_und * (pk.cantidad_und*pk.signo) ) as venta, " +
                        "sum( pk.costo_und * (pk.cantidad_und*pk.signo) ) as costo, " +
                        "count(*) as cntMov, p.auto_departamento " +
                        "FROM productos_kardex as pk " +
                        "join productos as p on pk.auto_producto=p.auto " +
                        "join empresa_departamentos as ed on p.auto_departamento=ed.auto " +
                        "where pk.fecha>=@p1 and pk.fecha<=@p2 " +
                        "and pk.modulo='Ventas' and pk.estatus_anulado='0' " +
                        "and (pk.codigo='01' or pk.codigo='02' or pk.codigo='03') "+
                        "group by p.auto_departamento "; 

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.Desde;

                    p2.ParameterName = "@p2";
                    p2.Value = filtro.Hasta;

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha> Reportes_MaestroExistencia(DtoLibInventario.Reportes.MaestroExistencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    //var sql = "SELECT p.auto as autoprd, p.codigo as codigoPrd, p.nombre as nombrePrd, p.estatus as estatusPrd, " +
                    //    "pdep.fisica as exFisica, edep.auto as autoDep, edep.codigo as codigoDep, edep.nombre as nombreDep, " +
                    //    "pmed.decimales as decimales "+
                    //    "FROM productos as p " +
                    //    "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto "+
                    //    "left join productos_deposito as pdep on pdep.auto_producto=p.auto " +
                    //    "left join empresa_depositos as edep on edep.auto=pdep.auto_deposito " +
                    //    "where p.estatus='Activo' and p.categoria<>'Bien de Servicio' ";

                    var sql = "SELECT " +
                        "p.auto as autoprd, " +
                        "p.codigo as codigoPrd, " +
                        "p.nombre as nombrePrd, " +
                        "p.estatus as estatusPrd, " +
                        "pdep.fisica as exFisica, " +
                        "edep.auto as autoDep, " +
                        "edep.codigo as codigoDep, " +
                        "edep.nombre as nombreDep, " +
                        "pmed.decimales as decimales, " +
                        "p.divisa/p.contenido_compras as costoUndDivisa, " +
                        "p.pdf_1/ ((et.tasa/100)+1)  as pDivisaNeto_1, " +
                        "p.pdf_2/ ((et.tasa/100)+1)  as pDivisaNeto_2, " +
                        "p.pdf_3/ ((et.tasa/100)+1)  as pDivisaNeto_3, " +
                        "p.pdf_4/ ((et.tasa/100)+1)  as pDivisaNeto_4, " +
                        "p.pdf_pto/ ((et.tasa/100)+1)  as pDivisaNeto_5, " +
                        "esuc.codigo as codigoSuc, " +
                        "egru.idprecio as precioId, " +
                        "edpt.nombre as departamento, pg.nombre as grupo "+
                        "FROM productos as p " +
                        "join empresa_departamentos as edpt on p.auto_departamento=edpt.auto "+
                        "join productos_grupo as pg on p.auto_grupo=pg.auto " +
                        "join empresa_tasas as et on p.auto_tasa=et.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "left join productos_deposito as pdep on pdep.auto_producto=p.auto " +
                        "left join empresa_depositos as edep on edep.auto=pdep.auto_deposito " +
                        "left join empresa_sucursal as esuc on edep.codigo_sucursal=esuc.codigo " +
                        "left join empresa_grupo as egru on egru.auto=esuc.autoempresagrupo " +
                        "where p.estatus='Activo' and p.categoria<>'Bien de Servicio' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p2.ParameterName = "@autoDeposito";
                        p2.Value = filtro.autoDeposito;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql += " and p.auto_grupo=@autoGrupo ";
                        p3.ParameterName = "@autoGrupo";
                        p3.Value = filtro.autoGrupo;
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroExistencia.Ficha>(sql, p1, p2, p3).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha> Reportes_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = "SELECT p.codigo, p.nombre, p.tasa, p.referencia, p.modelo, p.estatus, p.estatus_divisa, p.fecha_cambio, " +
                        "edep.nombre as nombreDepartamento, pg.nombre as grupo,  ";
                    var sql_2="FROM productos as p "+
                        "join empresa_departamentos edep on p.auto_departamento=edep.auto "+
                        "join productos_grupo as pg on p.auto_grupo=pg.auto " +
                        "where 1=1 and estatus='Activo' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.autoDepartamento != "")
                    {
                        sql_2 += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_2 += " and p.auto_grupo=@autoGrupo ";
                        p2.ParameterName = "@autoGrupo";
                        p2.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoMarca != "")
                    {
                        sql_2 += " and p.auto_marca=@autoMarca ";
                        p3.ParameterName = "@autoMarca";
                        p3.Value = filtro.autoMarca;
                    }
                    if (filtro.autoTasa != "")
                    {
                        sql_2 += " and p.auto_tasa=@autoTasa ";
                        p4.ParameterName = "@autoTasa";
                        p4.Value = filtro.autoTasa;
                    }
                    if (filtro.origen != DtoLibInventario.Reportes.enumerados.EnumOrigen.SnDefinir )
                    {
                        var f = "Nacional";
                        if (filtro.origen == DtoLibInventario.Reportes.enumerados.EnumOrigen.Importado)
                            f = "Importado";
                        sql_2 += " and p.origen=@origen ";
                        p5.ParameterName = "@origen";
                        p5.Value = f ;
                    }
                    if (filtro.categoria !=  DtoLibInventario.Reportes.enumerados.EnumCategoria.SnDefinir)
                    {
                        var f = "";
                        switch (filtro.categoria) 
                        {
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.BienServicio:
                                f = "Bien De Servicio";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.ProductoTerminado:
                                f = "Producto Terminado";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.MateriaPrima:
                                f = "Materia Prima";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.SubProducto:
                                f = "Sub Producto";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumCategoria.UsoInterno:
                                f = "Uso Interno";
                                break;
                        }
                        sql_2 += " and p.categoria=@categoria ";
                        p6.ParameterName = "@categoria";
                        p6.Value = f;
                    }
                    if (filtro.admDivisa != DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var f = "1";
                        if (filtro.admDivisa == DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No)
                            f = "0";
                        sql_2 += " and p.estatus_divisa=@estatusDivisa ";
                        p7.ParameterName = "@estatusDivisa";
                        p7.Value = f;
                    }
                    if (filtro.precio == DtoLibInventario.Reportes.enumerados.EnumPrecio.SnDefinir)
                    {
                        sql_1 += "p.precio_1, p.precio_2, p.precio_3, p.precio_4, p.precio_pto, " +
                            "p.pdf_1, p.pdf_2, p.pdf_3, p.pdf_4, p.pdf_pto ";
                    }
                    else
                    {
                        switch (filtro.precio) 
                        {
                            case DtoLibInventario.Reportes.enumerados.EnumPrecio.P1:
                                sql_1 += "p.precio_1, p.pdf_1 ";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumPrecio.P2:
                                sql_1 += "p.precio_2, p.pdf_2 ";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumPrecio.P3:
                                sql_1 += "p.precio_3, p.pdf_3 ";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumPrecio.P4:
                                sql_1 += "p.precio_4, p.pdf_4 ";
                                break;
                            case DtoLibInventario.Reportes.enumerados.EnumPrecio.P5:
                                sql_1 += "p.precio_pto, p.pdf_pto ";
                                break;
                        }
                    }
                    var sql = sql_1 + sql_2;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroPrecio.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Kardex.Ficha> Reportes_Kardex(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Kardex.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var deposito = "";
                    if (filtro.autoDeposito != "") 
                    {
                        deposito += " and auto_deposito=@autoDeposito ";
                    }

                    var tsql_1 = @"select t1.autoPrd, 
                                   (select sum(cantidad_und*signo)
                                      from productos_kardex   
                                      where auto_producto=t1.autoPrd and fecha<@desde and estatus_anulado='0' "+deposito+@"
                                      group by auto_producto) as exInicial
                                 from (
                                        SELECT distinct kard.auto_producto as autoPrd
                                        FROM `productos_kardex` as kard ";
                    var tsql_2 = "where estatus_anulado='0' and fecha>=@desde and fecha<=@hasta ";
                    var tsql_3 = ") as t1";

                    var sql_1 = "SELECT p.auto, p.nombre, p.codigo, p.referencia, p.modelo, pmed.decimales, " +
                        "kard.fecha, kard.hora, kard.modulo, kard.siglas, kard.documento, kard.nombre_deposito, " +
                        "kard.cantidad_und, kard.nombre_concepto, kard.signo, kard.codigo_sucursal, kard.entidad ";

                    var sql_2="FROM `productos_kardex` as kard "+
                        "join productos p on p.auto=kard.auto_producto "+
                        "join productos_medida pmed on  pmed.auto=p.auto_empaque_compra "+
                        "where estatus_anulado='0' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();

                    var tp1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var tp2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var tp3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var tp4 = new MySql.Data.MySqlClient.MySqlParameter();
                    
                    sql_2 += " and fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    //
                    tp1.ParameterName = "@desde";
                    tp1.Value = filtro.desde;
                    
                    sql_2 += " and fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;
                    //
                    tp2.ParameterName = "@hasta";
                    tp2.Value = filtro.hasta;

                    if (filtro.autoProducto != "")
                    {
                        sql_2 += " and auto_producto=@autoProducto ";
                        p4.ParameterName = "@autoProducto";
                        p4.Value = filtro.autoProducto;

                        tsql_2 += " and auto_producto=@autoProducto ";
                        tp3.ParameterName = "@autoProducto";
                        tp3.Value = filtro.autoProducto;
                    }

                    if (filtro.autoDeposito != "")
                    {
                        sql_2 += " and auto_deposito=@autoDeposito ";
                        p3.ParameterName = "@autoDeposito";
                        p3.Value = filtro.autoDeposito;

                        tsql_2 += " and auto_deposito=@autoDeposito ";
                        tp4.ParameterName = "@autoDeposito";
                        tp4.Value = filtro.autoDeposito;
                    }

                    // EXISTENCIA INICIAL
                    var tsql = tsql_1 + tsql_2 + tsql_3;
                    cnn.Database.CommandTimeout = 0;
                    var tlist = cnn.Database.SqlQuery<DtoLibInventario.Reportes.Kardex.Existencia>(tsql, tp1, tp2, tp3, tp4).ToList();
                    // MOVIMIENTOS
                    var sql = sql_1 + sql_2;
                    cnn.Database.CommandTimeout=0;
                    var mov = cnn.Database.SqlQuery<DtoLibInventario.Reportes.Kardex.Mov>(sql, p1, p2, p3, p4).ToList();
                    rt.Entidad = new DtoLibInventario.Reportes.Kardex.Ficha()
                    {
                        exInicial = tlist,
                        movimientos = mov,
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha> Reportes_CompraVentaAlmacen(DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@autoPrd";
                    p1.Value = filtro.autoProducto;

                    var sql_1 = "SELECT p.nombre as prdNombre, p.codigo as prdCodigo, p.contenido_compras as contenido, "+
                        "p.divisa as costoDivisa, " +
                        "pm.nombre as empaque, (select sum(fisica) from productos_deposito where auto_producto=p.auto) as exUnd " +
                        "FROM productos as p " +
                        "join productos_medida as pm on p.auto_empaque_compra=pm.auto " +
                        "WHERE p.auto=@autoPrd";
                    var prd = cnn.Database.SqlQuery<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha>(sql_1, p1).FirstOrDefault();

                    var sql_2 = "SELECT c.documento as documento, c.fecha as fecha, cd.cantidad as cnt, cd.empaque as empaque, "+
                        "cd.contenido_empaque as contenido, cd.cantidad_und as cntUnd, cd.costo_und as xcostoUnd, cd.total_neto as tneto, "+
                        "c.factor_cambio as factor, cd.signo as signoDoc, c.serie as tipoDoc, cd.estatus_anulado as estatusAnulado "+
                        "FROM compras_detalle as cd "+
                        "join compras as c on c.auto=cd.auto_documento "+
                        "join productos as p on cd.auto_producto=p.auto "+
                        "where cd.auto_producto=@autoPrd";
                    var lCompras= cnn.Database.SqlQuery<DtoLibInventario.Reportes.CompraVentaAlmacen.FichaCompra>(sql_2, p1).ToList();

                    var sql_3 = "SELECT sum(vd.cantidad_und*vd.signo) as cnt, "+
                        "sum(vd.cantidad*vd.precio_final*vd.signo) as montoVenta, " +
                        "sum(vd.cantidad*vd.costo_und*vd.signo) as montoCosto, v.factor_cambio as factor, "+
                        "v.documento_nombre as tipoDoc " +
                        "from ventas_detalle as vd " +
                        "join productos as p on vd.auto_producto=p.auto " +
                        "join ventas as v on vd.auto_documento=v.auto " +
                        "where vd.estatus_anulado='0' and vd.auto_producto=@autoPrd " +
                        "group by vd.auto_producto,p.nombre, vd.signo, v.factor_cambio, v.documento_nombre ";
                    var lVentas = cnn.Database.SqlQuery<DtoLibInventario.Reportes.CompraVentaAlmacen.FichaVenta>(sql_3, p1).ToList();

                    var sql_4 = "SELECT pm.documento, " +
                        "pm.fecha, " +
                        "pm.estatus_anulado as estatusAnulado, " +
                        "pm.tipo as tipoDoc, " +
                        "pm.documento_nombre as nombreDoc, " +
                        "pm.nota, " +
                        "pmd.cantidad_und as cantUnd, " +
                        "pmd.costo_und as costoUnd, " +
                        "pmd.signo " +
                        "FROM productos_movimientos_detalle as pmd " +
                        "join productos_movimientos as pm on pm.auto=pmd.auto_documento " +
                        "where pmd.auto_producto=@autoPrd and pmd.tipo<>'03'";
                    var lAlm = cnn.Database.SqlQuery<DtoLibInventario.Reportes.CompraVentaAlmacen.FichaAlmacen>(sql_4, p1).ToList();

                    prd.compras = lCompras;
                    prd.ventas = lVentas;
                    prd.almacen=lAlm;
                    rt.Entidad = prd;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.DepositoResumen.Ficha> Reportes_DepositoResumen()
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.DepositoResumen.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 =
                        @"Select (select count(*) from productos_deposito as pd 
                        where pd.auto_deposito=a.autoDeposito and pd.fisica<>0) as cntStock , a.* 
                        from 
                        (
                        select 
                        ed.auto as autoDeposito, 
                        count(*) as cItem, 
                        ed.nombre as nombreDeposito, 
                        sum(pd.fisica*(p.Divisa/p.contenido_compras)) as costo, 
                        sum(pd.fisica*(p.pdf_1/ ((et.tasa/100)+1)  )) as pn1, 
                        sum(pd.fisica*(p.pdf_2/ ((et.tasa/100)+1)  )) as pn2, 
                        sum(pd.fisica*(p.pdf_3/ ((et.tasa/100)+1)  )) as pn3, 
                        sum(pd.fisica*(p.pdf_4/ ((et.tasa/100)+1)  )) as pn4, 
                        sum(pd.fisica*(p.pdf_pto/ ((et.tasa/100)+1)  )) as pn5, 
                        es.codigo as codigoSuc, 
                        eg.nombre as nombreGrupo, 
                        eg.idprecio as precioId 
                        FROM `productos_deposito` as pd 
                        join empresa_depositos as ed on ed.auto=pd.auto_deposito 
                        join empresa_depositos_ext as edExt on edExt.auto_deposito=ed.auto 
                        join productos as p on pd.auto_producto=p.auto 
                        join empresa_tasas as et on et.auto=p.auto_tasa 
                        join empresa_sucursal as es on ed.codigo_sucursal=es.codigo 
                        join empresa_grupo as eg on eg.auto=es.autoempresagrupo 
                        where p.estatus='Activo' and p.categoria<>'Bien de Servicio' and edExt.es_activo='1'
                        group by autoDeposito,nombreDeposito,codigoSuc,nombreGrupo,precioId
                        ) as a";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Reportes.DepositoResumen.Ficha>(sql_1, p1).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha> Reportes_NivelMinimo(DtoLibInventario.Reportes.MaestroNivelMinimo.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {

                    var sql_1 = "SELECT p.codigo as codigoPrd, p.nombre as nombrePrd, " +
                        "ed.nombre as nombreDep, ed.codigo as codigoDep, pd.fisica as existencia, " +
                        "pg.nombre as grupo, edpt.nombre as departamento, "+
                        "pd.nivel_minimo as nivelMin, pd.nivel_optimo as nivelMax ";

                    var sql_2 = " FROM productos_deposito as pd " +
                        "join productos as p on p.auto=pd.auto_producto " +
                        "join empresa_departamentos as edpt on edpt.auto=p.auto_departamento " +
                        "join productos_grupo as pg on pg.auto=p.auto_grupo " +
                        "join empresa_depositos as ed on pd.auto_deposito=ed.auto ";

                    var sql_3 = " where pd.fisica< pd.nivel_minimo " +
                        "and p.estatus='Activo' ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    if (filtro.autoDeposito != "")
                    {
                        sql_3 += " and pd.auto_deposito=@autoDeposito ";
                        p1.ParameterName = "@autoDeposito";
                        p1.Value = filtro.autoDeposito;
                    };

                    if (filtro.autoDepartamento != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p2.ParameterName = "@autoDepartamento";
                        p2.Value = filtro.autoDepartamento;
                    }

                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo ";
                        p3.ParameterName = "@autoGrupo";
                        p3.Value = filtro.autoGrupo;
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Valorizacion.Ficha> Reportes_Valorizacion(DtoLibInventario.Reportes.Valorizacion.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.Valorizacion.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {

                    var sql_2 = "";
                    var sql_3 = "";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    
                    var xdep = "";
                    if (filtro.idDeposito != "") 
                    {
                        xdep = " and  auto_deposito=@p2 ";
                        p2.ParameterName="@p2";
                        p2.Value = filtro.idDeposito;
                    };

                    var sql_1 = @"select v3.*,
                                    (select costo_divisa 
		                                    from productos_costos
		                                    where id=
                                        			(
				                                        SELECT max(id) as id
				                                        FROM productos_costos 
				                                        WHERE fecha<=@p1 and auto_producto=v3.auto and serie<>'MAN'
				                                        group by auto_producto
			                                        )
	                                ) as costoHist
                                  from
                                    (select v1.*, v2.cntUnd
		                                    from
			                                (
			                                    SELECT p.auto, p.codigo, p.nombre, p.costo_Und as costoUnd, p.contenido_compras as contEmpComp, ed.nombre as departamento, pg.nombre as grupo, p.divisa
			                                    FROM `productos` as p
                                                join empresa_departamentos as ed on p.auto_departamento=ed.auto
                                                join productos_grupo as pg on p.auto_grupo=pg.auto
			                                    where p.estatus='Activo' and p.categoria='Producto Terminado'
			                                ) as v1
                                            join 
			                                (
			                                    select auto_producto, sum(cantidad_und*signo) as cntUnd
			                                    from productos_kardex
			                                    where estatus_anulado='0' and fecha<=@p1 "+ xdep +@" 
			                                    group by auto_producto
			                                ) as v2 on v2.auto_producto=v1.auto
                                    ) as v3";

                    p1.ParameterName = "@p1";
                    p1.Value = filtro.hasta;

                    var sql = sql_1 + sql_2 + sql_3;
                    cnn.Database.CommandTimeout = 0;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.Valorizacion.Ficha>(sql, p1, p2, p3, p4, p5).ToList();
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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.KardexResumen.Ficha> Reportes_KardexResumen(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.KardexResumen.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1= @"SELECT sum(cantidad_und*signo) as cnt, nombre_concepto as concepto ";
                    var sql_2 = " from productos_kardex ";
                    var sql_3= @" where estatus_anulado='0' ";
                    var sql_4 = "group by nombre_concepto";

                    sql_3 += " and fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde.Date;

                    sql_3 += " and fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta.Date;

                    if (filtro.autoProducto != "")
                    {
                        sql_3 += " and auto_producto=@autoProducto ";
                        p3.ParameterName = "@autoProducto";
                        p3.Value = filtro.autoProducto;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql_1 += @", (select sum(cantidad_und*signo) from productos_kardex where auto_producto=@autoProducto and fecha<@desde 
                            and estatus_anulado='0' and auto_deposito=@autoDeposito) as exInicial ";
                        sql_3 += " and auto_deposito=@autoDeposito ";
                        p4.ParameterName = "@autoDeposito";
                        p4.Value = filtro.autoDeposito;
                    }
                    else
                    {
                        sql_1 += ", (select sum(cantidad_und*signo) from productos_kardex where auto_producto=@autoProducto and fecha<@desde " +
                            "and estatus_anulado='0') as exInicial ";
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.KardexResumen.Ficha>(sql, p1, p2, p3, p4).ToList();
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