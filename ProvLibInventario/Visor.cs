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

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha> 
            Visor_Existencia(DtoLibInventario.Visor.Existencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT 
                        p.nombre as nombrePrd, 
                        p.codigo as codigoPrd, 
                        p.auto as autoPrd, 
                        case when p.estatus='Activo' then '0' else '1' end as estatusActivo, 
                        p.estatus_cambio as estatusSuspendido, 
                        pdep.fisica as cntFisica, 
                        pdep.reservada as cntReserva, 
                        pdep.disponible as cntDisponible,
                        pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, 
                        case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, 
                        edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, 
                        edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, 
                        pmed.decimales 
                        FROM `productos_deposito` as pdep 
                        join empresa_depositos as edep on pdep.auto_deposito=edep.auto 
                        join productos as p on pdep.auto_producto=p.auto 
                        join empresa_departamentos as edepart on p.auto_departamento=edepart.auto 
                        join productos_medida as pmed on p.auto_empaque_compra=pmed.auto 
                        WHERE 1 = 1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p1.ParameterName = "@autoDeposito";
                        p1.Value = filtro.autoDeposito;
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p2.ParameterName = "@autoDepartamento";
                        p2.Value = filtro.autoDepartamento;
                    }
                    if (filtro.filtrarPor!= DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.SinDefinir )
                    {
                        switch (filtro.filtrarPor) 
                        {
                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaMayorCero:
                                sql += " and (pdep.fisica>0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaIgualCero:
                                sql += " and (pdep.fisica=0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaMenorCero:
                                sql += " and (pdep.fisica<0) ";
                                break;

                            case DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaPorDebajoNivelMinimo:
                                sql += " and (nivel_minimo>fisica AND nivel_minimo>0) ";
                                break;
                        }
                    }

                    var lst  = cnn.Database.SqlQuery<DtoLibInventario.Visor.Existencia.Ficha>(sql, p1, p2).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha> 
            Visor_CostoEdad(DtoLibInventario.Visor.CostoEddad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var sql = "SELECT p.nombre as nombrePrd, p.codigo as codigoPrd, p.auto as autoPrd, p.divisa as costoDivisa, " +
                        "p.contenido_compras as contenidoCompras, "+
                        "case when p.estatus='Activo' then '0' else '1' end as estatusActivo, " +
                        "p.estatus_cambio as estatusSuspendido, " +
                        "case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, " +
                        "case when p.estatus_divisa='0' then 'N' when p.estatus_divisa='1' then 'S' end as esAdmDivisa, " +
                        "pdep.fisica as cntFisica, pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, " +
                        "edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, " +
                        "edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, " +
                        "pmed.decimales, p.costo_und as costoUnd, p.fecha_ult_costo as fechaUltActCosto, p.fecha_ult_venta as fechaUltVenta  " +
                        "FROM `productos_deposito` as pdep " +
                        "join empresa_depositos as edep on pdep.auto_deposito=edep.auto " +
                        "join productos as p on pdep.auto_producto=p.auto " +
                        "join empresa_departamentos as edepart on p.auto_departamento=edepart.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "WHERE 1 = 1 and (pdep.fisica<>0) ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito";
                        p2.ParameterName = "@autoDeposito";
                        p2.Value = filtro.autoDeposito;
                    }

                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.CostoEddad.FichaDetalle>(sql, p1, p2).ToList();
                    rt.Entidad = new DtoLibInventario.Visor.CostoEddad.Ficha();
                    rt.Entidad.fechaServidor= fechaSistema.Date;
                    rt.Entidad.detalles=lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha> 
            Visor_Traslado(DtoLibInventario.Visor.Traslado.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha>();

            try
            {
                var list = new List<DtoLibInventario.Visor.Traslado.Ficha>();
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q= cnn.productos_movimientos_detalle.
                        Join(cnn.productos_movimientos, pmd=> pmd.auto_documento, pm=>pm.auto, (pmd,pm) => new {pmd,pm}).
                        Where(w=>w.pm.fecha.Year==filtro.ano && w.pm.estatus_anulado=="0" && w.pm.tipo=="03").
                        ToList();

                    if (filtro.mes != 0) 
                    {
                        q = q.Where(w => w.pm.fecha.Month == filtro.mes).ToList();
                    }

                    if (q != null) 
                    {
                        if (q.Count > 0) 
                        {
                            list = q.Select(s =>
                            {
                                var rg = new DtoLibInventario.Visor.Traslado.Ficha()
                                {
                                    autoDepositoDestino = s.pm.auto_destino,
                                    autoDepositoOrigen = s.pm.auto_deposito,
                                    autoPrd = s.pmd.auto_producto,
                                    autoUsuario = s.pm.auto_usuario,
                                    cantidad = s.pmd.cantidad_und,
                                    codigoDepositoDestino = s.pm.codigo_destino,
                                    codigoDepositoOrigen = s.pm.codigo_deposito,
                                    codigoPrd = s.pmd.codigo,
                                    codigoUsuario = s.pm.codigo_usuario,
                                    decimales = s.pmd.decimales,
                                    documentoNombre = s.pm.documento_nombre,
                                    documentoNro = s.pm.documento,
                                    fecha = s.pm.fecha,
                                    hora = s.pm.hora,
                                    nombreDepositoDestino = s.pm.destino,
                                    nombreDepositoOrigen = s.pm.deposito,
                                    nombrePrd = s.pmd.nombre,
                                    nombreUsuario = s.pm.usuario,
                                    nota = s.pm.nota,
                                };
                                return rg;
                            }).ToList();
                        }
                    }
                }
                rt.Lista = list;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha> 
            Visor_Ajuste(DtoLibInventario.Visor.Ajuste.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha>();

            try
            {
                var list = new List<DtoLibInventario.Visor.Ajuste.FichaDetalle>();
                var totalVentasNeta = 0.0m;
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", filtro.mes.ToString().Trim().PadLeft(2, '0'));
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", filtro.ano.ToString().Trim().PadLeft(4, '0'));
                    var sql = "select sum(neto*signo) as total from ventas "+
                        "where mes_relacion=@p1 and ano_relacion=@p2 "+
                        "and (tipo='01' or tipo='02' or tipo='03') "+
                        "and estatus_anulado='0' ";
                    var total= cnn.Database.SqlQuery<decimal?>(sql, p1, p2).FirstOrDefault();
                    if (total.HasValue)
                        totalVentasNeta = total.Value;

                    var q = cnn.productos_movimientos_detalle.
                        Join(cnn.productos_movimientos, pmd => pmd.auto_documento, pm => pm.auto, (pmd, pm) => new { pmd, pm }).
                        Where(w => w.pm.fecha.Year == filtro.ano && w.pm.estatus_anulado == "0" && 
                            (w.pm.tipo == "04" || ((w.pm.tipo=="01" || w.pm.tipo=="02") && w.pm.auto_concepto=="0000000007"))
                            ).ToList();

                    if (filtro.mes != 0)
                    {
                        q = q.Where(w => w.pm.fecha.Month == filtro.mes).ToList();
                    }

                    if (q != null)
                    {
                        if (q.Count > 0)
                        {
                            list = q.Select(s =>
                            {
                                var rg = new DtoLibInventario.Visor.Ajuste.FichaDetalle()
                                {
                                    autoDepositoOrigen = s.pm.auto_deposito,
                                    autoPrd = s.pmd.auto_producto,
                                    autoUsuario = s.pm.auto_usuario,
                                    cantidadUnd = s.pmd.cantidad_und,
                                    codigoDepositoOrigen = s.pm.codigo_deposito,
                                    codigoPrd = s.pmd.codigo,
                                    codigoUsuario = s.pm.codigo_usuario,
                                    decimales = s.pmd.decimales,
                                    documentoNro = s.pm.documento,
                                    fecha = s.pm.fecha,
                                    hora = s.pm.hora,
                                    nombreDepositoOrigen = s.pm.deposito,
                                    nombrePrd = s.pmd.nombre,
                                    nombreUsuario = s.pm.usuario,
                                    nota = s.pm.nota,
                                    costoUnd = s.pmd.costo_und,
                                    importe = s.pmd.total,
                                    signo = s.pmd.signo,
                                };
                                return rg;
                            }).ToList();
                        }
                    }
                }
                rt.Entidad = new DtoLibInventario.Visor.Ajuste.Ficha();
                rt.Entidad.detalles = list;
                rt.Entidad.montoVentaNeto = totalVentasNeta;
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha> 
            Visor_CostoExistencia(DtoLibInventario.Visor.CostoExistencia.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = "SELECT p.nombre as nombrePrd, p.codigo as codigoPrd, p.auto as autoPrd, p.divisa as costoDivisa, " +
                        "p.contenido_compras as contenidoCompras, "+
                        "case when p.estatus='Activo' then '0' else '1' end as estatusActivo, "+
                        "p.estatus_cambio as estatusSuspendido, "+
                        "case when p.estatus_pesado='0' then 'N' when p.estatus_pesado='1' then 'S' end as esPesado, " +
                        "case when p.estatus_divisa='0' then 'N' when p.estatus_divisa='1' then 'S' end as esAdmDivisa, " +
                        "pdep.fisica as cntFisica, pdep.nivel_minimo as nivelMinimo, pdep.nivel_optimo as nivelOptimo, " +
                        "edep.auto as autoDeposito, edep.nombre as nombreDeposito, edep.codigo as codigoDeposito, " +
                        "edepart.auto as autoDepart, edepart.codigo as codigoDepart, edepart.nombre as nombreDepart, " +
                        "pmed.decimales, p.costo_und as costoUnd, p.fecha_ult_costo as fechaUltActCosto, p.fecha_ult_venta as fechaUltVenta  " +
                        "FROM `productos_deposito` as pdep " +
                        "join empresa_depositos as edep on pdep.auto_deposito=edep.auto " +
                        "join productos as p on pdep.auto_producto=p.auto " +
                        "join empresa_departamentos as edepart on p.auto_departamento=edepart.auto " +
                        "join productos_medida as pmed on p.auto_empaque_compra=pmed.auto " +
                        "WHERE 1 = 1 and (pdep.fisica<>0) ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepartamento != "")
                    {
                        sql += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDeposito != "")
                    {
                        sql += " and pdep.auto_deposito=@autoDeposito ";
                        p2.ParameterName = "@autoDeposito";
                        p2.Value = filtro.autoDeposito;
                    }

                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.CostoExistencia.Ficha>(sql, p1,p2).ToList();
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
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.Ficha> 
            Visor_Precio(DtoLibInventario.Visor.Precio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT p.auto as autoPrd,p.codigo as codigoPrd,p.nombre as nombrePrd, 
                                  ed.nombre as nombreDep, ed.codigo as codigoDep,pg.codigo as codigoGrupo, 
                                  pg.nombre as nombreGrupo,p.costo_und as costoUnd, p.divisa as costoDivisa, 
                                  p.contenido_compras as contEmpCompra, p.precio_1, p.precio_2, p.precio_3, p.precio_4, 
                                  p.precio_pto as precio_5, p.estatus, p.estatus_divisa as estatusDivisa, 
                                  p.fecha_ult_costo as fechaUltCosto
                                  FROM productos as p ";
                    var sql_2 = @" join empresa_departamentos as ed on ed.auto=p.auto_departamento
                                  join productos_grupo as pg on pg.auto=p.auto_grupo ";
                    var sql_3 = @" WHERE 1 = 1 and categoria<>'Bien de Servicio' ";

                    if (filtro.autoDepart != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepart;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo ";
                        p2.ParameterName = "@autoGrupo";
                        p2.Value = filtro.autoGrupo;
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.Precio.Ficha>(sql, p1, p2).ToList();
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
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.PrecioAjuste.Ficha> 
            Visor_PrecioAjuste(DtoLibInventario.Visor.PrecioAjuste.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.PrecioAjuste.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idEmpresaGrupo", filtro.idEmpresaGrrupo);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 
                                    distinct 
                                    p.auto, p.nombre, 

                                    p.pdf_1 as pFDivEmp1_1, 
                                    p.pdf_2 as pFDivEmp1_2, 
                                    p.pdf_3 as pFDivEmp1_3, 
                                    p.pdf_4 as pFDivEmp1_4, 
                                    p.pdf_pto as pFDivEmp1_5, 
                                    p.contenido_1 as contEmp1_1, 
                                    p.contenido_2 as contEmp1_2, 
                                    p.contenido_3 as contEmp1_3, 
                                    p.contenido_4 as contEmp1_4, 
                                    p.contenido_pto as contEmp1_5, 

                                    pExt.pdmf_1 as pFDivEmp2_1, 
                                    pExt.pdmf_2 as pFDivEmp2_2, 
                                    pExt.pdmf_3 as pFDivEmp2_3, 
                                    pExt.pdmf_4 as pFDivEmp2_4, 
                                    pExt.contenido_may_1 as contEmp2_1,
                                    pExt.contenido_may_2 as contEmp2_2,
                                    pExt.contenido_may_3 as contEmp2_3,
                                    pExt.cont_may_4 as contEmp2_4,

                                    pExt.pdivisafull_dsp_1 as pFDivEmp3_1, 
                                    pExt.pdivisafull_dsp_2 as pFDivEmp3_2, 
                                    pExt.pdivisafull_dsp_3 as pFDivEmp3_3, 
                                    pExt.pdivisafull_dsp_4 as pFDivEmp3_4, 
                                    pExt.cont_dsp_1 as contEmp3_1,
                                    pExt.cont_dsp_2 as contEmp3_2,
                                    pExt.cont_dsp_3 as contEmp3_3,
                                    pExt.cont_dsp_4 as contEmp3_4

                                    from productos as p
                                    join productos_ext as pExt on pExt.auto_producto=p.auto
                                    join productos_deposito as pDep on pDep.auto_producto=p.auto ";

                    var sql_2 = @" where 
                                        p.estatus='Activo' and
                                        pDep.fisica>0 and
                                        pDep.auto_deposito in 
                                        (
                                            SELECT autoDepositoPrincipal as idDeposito
                                            FROM empresa_sucursal
                                            where autoEmpresaGrupo=@idEmpresaGrupo
                                        )" ;
                    var sql_3 = @"";
                    if (filtro.autoDepart != "")
                    {
                        sql_2 += " and p.auto_departamento=@autoDepartamento ";
                        p2.ParameterName = "@autoDepartamento";
                        p2.Value = filtro.autoDepart;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_2 += " and p.auto_grupo=@autoGrupo ";
                        p3.ParameterName = "@autoGrupo";
                        p3.Value = filtro.autoGrupo;
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.PrecioAjuste.Ficha>(sql, p1, p2, p3).ToList();
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
        //
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.SoloReporte.Ficha> 
            Visor_Precio_Modo_SoloReporte(DtoLibInventario.Visor.Precio.SoloReporte.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.SoloReporte.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@dias", filtro.desdeCntDias);
                    var _fecha = cnn.Database.SqlQuery<DateTime>("select now() - interval @dias day", p1).FirstOrDefault();

                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", _fecha.Date);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@autoDep", filtro.autoDeposito);
                    var sql_1 = @"SELECT 
                                    p.codigo, p.nombre, eTasa.tasa,
                                    p.precio_1 as p1, 
                                    p.precio_2 as p2, 
                                    p.precio_3 as p3, 
                                    p.precio_4 as p4,
                                    p.pdf_1 as p1_FD,
                                    p.pdf_2 as p2_FD,
                                    p.pdf_3 as p3_FD,
                                    p.pdf_4 as p4_FD,
                                    pExt.precio_may_1 as pM1,
                                    pExt.precio_may_2 as pM2,
                                    pExt.precio_may_3 as pM3,
                                    pExt.precio_may_4 as pM4,
                                    pExt.pdmf_1 as pM1_FD, 
                                    pExt.pdmf_2 as pM2_FD, 
                                    pExt.pdmf_3 as pM3_FD, 
                                    pExt.pdmf_4 as pM4_FD, 
                                    pExt.precio_dsp_1 as pDSP1, 
                                    pExt.precio_dsp_2 as pDSP2, 
                                    pExt.precio_dsp_3 as pDSP3, 
                                    pExt.precio_dsp_4 as pDSP4, 
                                    pExt.pdivisafull_dsp_1 as pDSP1_FD, 
                                    pExt.pdivisafull_dsp_2 as pDSP2_FD, 
                                    pExt.pdivisafull_dsp_3 as pDSP3_FD, 
                                    pExt.pdivisafull_dsp_4 as pDSP4_FD, 
                                    pExt.cont_emp_venta_tipo_1 as cont_emp_1, 
                                    pExt.cont_emp_venta_tipo_2 as cont_emp_2, 
                                    pExt.cont_emp_venta_tipo_3 as cont_emp_3,
                                    tipo_1.nombre as emp_1, 
                                    tipo_2.nombre as emp_2, 
                                    tipo_3.nombre as emp_3 ";
                    var sql_2= @"
                                from 
                                    (
                                        SELECT 
                                            distinct pPrec.auto_producto 
                                        FROM productos_precios as pPrec
                                        join productos_deposito as pDep on pDep.auto_producto=pPrec.auto_producto ";
                    var sql_3=@"
                                        where pPrec.fecha>=@fecha and pDep.auto_deposito=@autoDep ";
                    if (filtro.excluirCambMasivo)
                    {
                        sql_3 += "and pPrec.nota<>'CAMBIO MASIVO' ";
                    }
                    var sql_4=@"
                                    ) as c1
                                join productos as p on c1.auto_producto = p.auto
                                join productos_ext as pExt on pExt.auto_producto=p.auto
                                join empresa_tasas as eTasa on eTasa.auto=p.auto_tasa
                                join productos_medida as tipo_1 on pExt.auto_emp_venta_tipo_1=tipo_1.auto
                                join productos_medida as tipo_2 on pExt.auto_emp_venta_tipo_2=tipo_2.auto
                                join productos_medida as tipo_3 on pExt.auto_emp_venta_tipo_3=tipo_3.auto";
                    var sql = sql_1+sql_2+sql_3+sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.Precio.SoloReporte.Ficha>(sql, p1, p2).ToList();
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
        public DtoLib.ResultadoLista<DtoLibInventario.Visor.EntradaxCompra.Ficha> 
            Visor_EntradasxCompra(DtoLibInventario.Visor.EntradaxCompra.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Visor.EntradaxCompra.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", filtro.idDeposito);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@mes", filtro.mes);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("@ano", filtro.ano);
                    var sql_1 = @"SELECT 
                                    p.codigo as codigoPrd, 
                                    p.nombre as nombrePrd, 
                                    pKard.documento as nroDoc,
                                    pKard.fecha,
                                    pkard.hora,
                                    pkard.entidad as entidadProv,
                                    pkard.codigo_deposito as codDeposito,
                                    pkard.nombre_deposito as descDeposito,
                                    pkard.signo as signoDoc,
                                    pkard.cantidad_und as cantUnd,
                                    pkard.siglas as siglasDoc,
                                    pkard.codigo_concepto as codConcepto,
                                    pkard.nombre_concepto as descConcepto
                                FROM productos_kardex as pKard
                                join productos as p on p.auto=pKard.auto_producto ";
                    var sql_2 = @" where modulo='Compras'
                                    and estatus_anulado='0'
                                    and auto_deposito=@idDeposito
                                    and year(fecha)=@ano
                                    and month(fecha)=@mes";
                    var sql = sql_1 + sql_2;
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Visor.EntradaxCompra.Ficha>(sql, p1, p2, p3).ToList();
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