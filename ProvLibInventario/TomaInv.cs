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
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.ObtenerToma.Ficha> 
            TomaInv_GetListaPrd(DtoLibInventario.TomaInv.ObtenerToma.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.TomaInv.ObtenerToma.Ficha>();
            try
            {
                var _departExcluir = new StringBuilder();
                if (filtro.idDepartExcluir.Count > 0)
                {
                    var _firts = true;
                    foreach (var r in filtro.idDepartExcluir)
                    {
                        if (!_firts)
                        {
                            _departExcluir.Append(",");
                        }
                        _departExcluir.Append("'" + r + "'");
                        _firts = false;
                    }
                }
                else 
                {
                    _departExcluir.Append("'0000000000'");
                }
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", filtro.idDeposito);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@periodoDias", filtro.periodoDias);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("@departExcluir", _departExcluir);

                    var cmd = @"select 
                                    auto as idPrd, 
                                    codigo as codigoPrd, 
                                    nombre as descPrd, 
                                    (divisa/contenido_compras) as costoPrd,
                                    kardex.cnt as cnt, 
                                    vtas.margen as margen, 
                                    movKardex.cntMov,
                                    deposito.fecha_conteo as fechaUltConteo,
                                    deposito.resultado_conteo as ultConteo


                                from productos as p 
                                join productos_deposito as deposito on deposito.auto_producto=p.auto and 
                                                                        deposito.auto_deposito=@idDeposito

                                left join (
	                                        select 
                                                auto_producto, 
                                                sum(cantidad_und) as cnt
                                            from productos_kardex 
                                            where auto_deposito=@idDeposito
                                            and modulo='Ventas'
                                            and estatus_anulado='0' 
                                            and fecha>=DATE_SUB(NOW(), INTERVAL @periodoDias DAY)
                                            group by auto_producto
                                            ) as kardex on kardex.auto_producto=p.auto

                                left join (
                                            select 
                                                auto_producto, 
                                                sum(utilidad*signo) as margen
                                            from ventas_detalle
                                            where auto_deposito=@idDeposito
                                            and estatus_anulado='0' 
                                            and fecha>=DATE_SUB(NOW(), INTERVAL @periodoDias DAY)
                                            group by auto_producto
                                        ) as vtas on vtas.auto_producto=p.auto

                                left join (
                                            select 
                                                count(*) as cntMov, auto_producto
                                            from productos_kardex
                                            where auto_deposito=@idDeposito
                                            and estatus_anulado='0'
                                            and modulo in ('Compras', 'Inventario', 'Ventas')
                                            group by auto_producto
                                            ) as movKardex on movKardex.auto_producto = p.auto 

                                where auto_departamento not IN(" + _departExcluir.ToString() + @")
                                        and p.estatus<>'Inactivo' 
                                        and p.categoria<>'Bien de Servicio'";
                    var _sql= cmd;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.ObtenerToma.Ficha>(_sql, p1, p2, p3).ToList();
                    result.Lista = _lst;
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
            TomaInv_GenerarToma(DtoLibInventario.TomaInv.GenerarToma.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                var _prdLista = new StringBuilder();
                var _firts = true;
                foreach (var rg in ficha.ProductosTomarInv) 
                {
                    if (!_firts)
                    {
                        _prdLista.Append(",");
                    }
                    _prdLista.Append("'" + rg.idPrd + "'");
                    _firts = false;
                }

                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var cmd= "UPDATE sistema_contadores set a_toma_inventario=a_toma_inventario+1";
                        var _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql);
                        cnn.SaveChanges();

                        cmd= "select a_toma_inventario from sistema_contadores";
                        _sql = cmd;
                        var nroDoc= cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (nroDoc==null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var snroDoc= nroDoc.ToString().Trim().PadLeft(10,'0');

                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@idSucursal", ficha.idSucursal);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", ficha.idDeposito);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSucursal", ficha.codigoSucursal);
                        var t4 = new MySql.Data.MySqlClient.MySqlParameter("@descSucursal", ficha.descSucursal);
                        var t5 = new MySql.Data.MySqlClient.MySqlParameter("@codigoDeposito", ficha.codigoDeposito);
                        var t6 = new MySql.Data.MySqlClient.MySqlParameter("@descDeposito", ficha.descDeposito);
                        var t7 = new MySql.Data.MySqlClient.MySqlParameter("@autorizadoPor", ficha.autorizadoPor);
                        var t8 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", ficha.motivo);
                        var t9 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmision", fechaSistema.Date);
                        var ta = new MySql.Data.MySqlClient.MySqlParameter("@documentoNro", snroDoc);
                        var tb = new MySql.Data.MySqlClient.MySqlParameter("@cantItems",ficha.cantItems );
                        cmd = @"INSERT INTO tomainv (
                                        `id`, 
                                        `idSucursal`, 
                                        `idDeposito`, 
                                        `estatusAnulado`, 
                                        `estatusProcesado`, 
                                        `observaciones_result`, 
                                        `autoriza_result`, 
                                        `cntItem_result`, 
                                        `fecha_result`, 
                                        `hora_result`, 
                                        `codigoSucursal`, 
                                        `descSucursal`, 
                                        `codigoDeposito`, 
                                        `descDeposito`, 
                                        `autorizadoPor`, 
                                        `motivo`, 
                                        `fechaEmision`, 
                                        `documentoNro`, 
                                        `cantItems`) 
                                    VALUES (
                                        NULL,
                                        @idSucursal, 
                                        @idDeposito,
                                        '0',
                                        '0',
                                        '', 
                                        '', 
                                        0, 
                                        '2000/01/01', 
                                        '',
                                        @codigoSucursal, 
                                        @descSucursal, 
                                        @codigoDeposito, 
                                        @descDeposito, 
                                        @autorizadoPor, 
                                        @motivo, 
                                        @fechaEmision,
                                        @documentoNro,
                                        @cantItems)";
                        _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql, t1,t2,t3,t4,t5,t6,t7,t8,t9,ta,tb);
                        cnn.SaveChanges();

                        _sql = "SELECT LAST_INSERT_ID()";
                        var idEnt = cnn.Database.SqlQuery<int>(_sql).FirstOrDefault();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDepositoTomaInv", ficha.idDeposito);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", idEnt);
                        cmd = @"INSERT INTO tomainv_detalle (
                                    `id`,
                                    `idTomaInv`, 
                                    `idPrd`, 
                                    `codPrd`, 
                                    `descPrd`, 
                                    `idEmpCompra`, 
                                    `descEmpCompra`, 
                                    `decimalEmpCompra`, 
                                    `contEmpCompra`, 
                                    `exFisica`, 
                                    `fecUltConteo`, 
                                    `ultDocVta`, 
                                    `ultDocComp`, 
                                    `ultDocInv`, 
                                    `estatusAnulado`,
                                    `estatusToma`, 
                                    `idEmpInv`, 
                                    `descEmpInv`, 
                                    `decimalEmpInv`, 
                                    `contEmpInv`) 
                                select 
                                    null, 
                                    @idToma,
                                    prod.auto idPrd, 
                                    prod.codigo as codPrd, 
                                    prod.nombre as descPrd, 
                                    prodMed.auto as idEmpCompra, 
                                    prodMed.nombre as descEmpCompra, 
                                    prodMed.decimales as decimalEmpCompra,
                                    prod.contenido_compras as contEmpCompra,
                                    prodDep.fisica as exFisica,
                                    prodDep.fecha_conteo as fecUltConteo,
                                    (CASE WHEN kardex.ultDocVta IS NULL THEN '' ELSE kardex.ultDocVta END),
                                    (CASE WHEN kardex.ultDocComp IS NULL THEN '' ELSE kardex.ultDocComp END),
                                    (CASE WHEN kardex.ultDocInv IS NULL THEN '' ELSE kardex.ultDocInv END),
                                    '0',
                                    '0',
                                    prodExt.auto_emp_inv_1 as idEmpInv,
                                    prodMedExt.nombre as descEmpInv,
                                    prodMedExt.decimales as decimalEmpInv,
                                    prodExt.cont_emp_inv_1 as contEmpInv

                                from productos as prod
                                join productos_medida as prodMed on prodMed.auto=prod.auto_empaque_compra
                                join productos_deposito as prodDep on prodDep.auto_producto=prod.auto and prodDep.auto_deposito=@idDepositoTomaInv
                                join productos_ext as prodExt on prodExt.auto_producto=prod.auto
                                join productos_medida  as prodMedExt on prodMedExt.auto=prodExt.auto_emp_inv_1

                                LEFT join (
                                            SELECT
                                                p.auto_producto,
                                                MAX(CASE WHEN p.modulo = 'Ventas' THEN substring(p.auto_documento,4) END) AS ultDocVta,
                                                MAX(CASE WHEN p.modulo = 'Compras' THEN p.auto_documento END) AS ultDocComp,
                                                MAX(CASE WHEN p.modulo = 'Inventario' THEN p.auto_documento END) AS ultDocInv
                                            FROM
                                                productos_kardex AS p
                                            WHERE
                                                p.auto_deposito = @idDepositoTomaInv
                                                AND p.modulo IN ('Ventas', 'Compras', 'Inventario')
                                                and p.estatus_anulado='0'
                                            GROUP BY
                                                p.auto_producto
                                        ) as kardex on kardex.auto_producto = prod.auto

                                WHERE prod.auto IN (" + _prdLista.ToString() + ")";
                        _sql = cmd;
                        var v1 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        cnn.SaveChanges();
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
        public DtoLib.Resultado 
            TomaInv_RechazarItemsToma(DtoLibInventario.TomaInv.RechazarItem.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var cmd1 = @"update tomainv_detalle set estatusToma='' where auto_tomainv=@idToma and idPrd=@idPrd";
                        var cmd2 = @"delete from tomainv_conteo where auto_tomainv=@idToma and idPrd=@idPrd";
                        foreach (var rg in ficha.Items) 
                        {
                            p1.ParameterName = "@idToma";
                            p1.Value = ficha.IdToma;
                            p2.ParameterName = "@idPrd";
                            p2.Value = rg.IdPrd;
                            //
                            cnn.Database.ExecuteSqlCommand(cmd1, p1,p2);
                            cnn.SaveChanges();
                            //
                            cnn.Database.ExecuteSqlCommand(cmd2, p1, p2);
                            cnn.SaveChanges();
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
        public DtoLib.Resultado 
            TomaInv_ProcesarToma(DtoLibInventario.TomaInv.Procesar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", ficha.idToma);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@observaciones", ficha.observaciones);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@autoriza", ficha.autoriza);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@cntItem", ficha.cntItems);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var cmd = @"update tomainv set 
                                        estatusProcesado='1',
                                        observaciones_result=@observaciones,
                                        autoriza_result=@autoriza,
                                        cntItem_result=@cntItem,
                                        fecha_result=@fecha,
                                        hora_result=@hora
                                    where auto=@idToma and estatusProcesado='0'";
                        var v1 = cnn.Database.ExecuteSqlCommand(cmd, p1, p2, p3, p4, p5, p6);
                        if (v1 == 0) 
                        {
                            throw new Exception("ERROR AL PROCESAR TOMA");
                        }
                        cnn.SaveChanges();

                        var xp1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp3 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp4 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp5 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp6 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp7 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp8 = new MySql.Data.MySqlClient.MySqlParameter();
                        var xp9 = new MySql.Data.MySqlClient.MySqlParameter();
                        cmd = @"INSERT INTO tomainv_result (
                                        idPrd,
                                        diferencia_und,
                                        signo,
                                        descripcion, 
                                        auto_tomainv,
                                        costoMonDivisa,
                                        costoMonLocal,
                                        contEmpCompra,
                                        estatusDivisa) 
                                    VALUES (
                                        @idPrd,
                                        @diferencia,
                                        @signo, 
                                        @descripcion,
                                        @idToma,
                                        @costoMonDivisa,
                                        @costoMonLocal,
                                        @contEmpCompra,
                                        @estatusDivisa)";
                        foreach (var rg in ficha.items) 
                        {
                            xp1.ParameterName = "@idToma";
                            xp1.Value = ficha.idToma;
                            xp2.ParameterName = "@idPrd";
                            xp2.Value = rg.idProducto;
                            xp3.ParameterName = "@diferencia";
                            xp3.Value = rg.diferencia;
                            xp4.ParameterName = "@signo";
                            xp4.Value = rg.signo;
                            xp5.ParameterName = "@descripcion";
                            xp5.Value = rg.estadoDesc;
                            xp6.ParameterName = "@costoMonDivisa";
                            xp6.Value = rg.costoMonDivisa;
                            xp7.ParameterName = "@costoMonLocal";
                            xp7.Value = rg.costoMonLocal;
                            xp8.ParameterName = "@contEmpCompra";
                            xp8.Value = rg.contEmpCompra;
                            xp9.ParameterName = "@estatusDivisa";
                            xp9.Value = rg.estatusDivisa;
                            v1 = cnn.Database.ExecuteSqlCommand(cmd, xp1, xp2, xp3, xp4, xp5, xp6, xp7, xp8, xp9);
                            if (v1 == 0)
                            {
                                throw new Exception("ERROR AL PROCESAR TOMA");
                            }
                            cnn.SaveChanges();
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
        //
        public DtoLib.Resultado 
            TomaInv_GenerarSolicitud(DtoLibInventario.TomaInv.Solicitud.Generar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                var _prdLista = new StringBuilder();
                var _firts = true;
                foreach (var rg in ficha.ProductosTomarInv)
                {
                    if (!_firts)
                    {
                        _prdLista.Append(",");
                    }
                    _prdLista.Append("'" + rg.idPrd + "'");
                    _firts = false;
                }

                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var cmd = @"UPDATE sistema_contadores set 
                                            a_toma_inventario_solicitud=a_toma_inventario_solicitud+1, 
                                            a_toma_inventario_solicitud_numero=a_toma_inventario_solicitud_numero+1";
                        var _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql);
                        cnn.SaveChanges();

                        cmd = "select a_toma_inventario_solicitud from sistema_contadores";
                        _sql = cmd;
                        var autoDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (autoDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var sAutoDoc = autoDoc.ToString().Trim().PadLeft(10, '0');

                        cmd = "select a_toma_inventario_solicitud_numero from sistema_contadores";
                        _sql = cmd;
                        var nroDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (nroDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var sNroDoc = nroDoc.ToString().Trim().PadLeft(10, '0');

                        var t0 = new MySql.Data.MySqlClient.MySqlParameter("@auto", sAutoDoc);
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@idSucursal", ficha.idSucursal);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", ficha.idDeposito);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSucursal", ficha.codigoSucursal);
                        var t4 = new MySql.Data.MySqlClient.MySqlParameter("@descSucursal", ficha.descSucursal);
                        var t5 = new MySql.Data.MySqlClient.MySqlParameter("@codigoDeposito", ficha.codigoDeposito);
                        var t6 = new MySql.Data.MySqlClient.MySqlParameter("@descDeposito", ficha.descDeposito);
                        var t7 = new MySql.Data.MySqlClient.MySqlParameter("@autorizadoPor", ficha.autorizadoPor);
                        var t8 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", ficha.motivo);
                        var t9 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmision", fechaSistema.Date);
                        var ta = new MySql.Data.MySqlClient.MySqlParameter("@documentoNro", sNroDoc);
                        var tb = new MySql.Data.MySqlClient.MySqlParameter("@cntItems", ficha.cantItems);
                        cmd = @"INSERT INTO tomainv_solicitud (
                                        `auto`, 
                                        `idSucursal`, 
                                        `idDeposito`, 
                                        `estatusAnulado`, 
                                        `codigoSucursal`, 
                                        `descSucursal`, 
                                        `codigoDeposito`, 
                                        `descDeposito`, 
                                        `autorizadoPor`, 
                                        `motivo`, 
                                        `fechaEmision`, 
                                        `documentoNro`, 
                                        `cntItems`,
                                        `estatusTransmitida`) 
                                    VALUES (
                                        @auto,
                                        @idSucursal, 
                                        @idDeposito,
                                        '0',
                                        @codigoSucursal, 
                                        @descSucursal, 
                                        @codigoDeposito, 
                                        @descDeposito, 
                                        @autorizadoPor, 
                                        @motivo, 
                                        @fechaEmision,
                                        @documentoNro,
                                        @cntItems,
                                        '0')";
                        _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, ta, tb);
                        cnn.SaveChanges();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoSolicitud", sAutoDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDepositoTomaInv", ficha.idDeposito);
                        cmd = @"INSERT INTO tomainv_solicitud_detalle (
                                    `auto_solicitud`,
                                    `idPrd`, 
                                    `codPrd`, 
                                    `descPrd`, 
                                    `idEmpCompra`, 
                                    `descEmpCompra`, 
                                    `decimalEmpCompra`, 
                                    `contEmpCompra`, 
                                    `exFisica`, 
                                    `fecUltConteo`, 
                                    `ultDocVta`, 
                                    `ultDocComp`, 
                                    `ultDocInv`, 
                                    `idEmpInv`, 
                                    `descEmpInv`, 
                                    `decimalEmpInv`, 
                                    `contEmpInv`) 
                                select 
                                    @autoSolicitud,
                                    prod.auto idPrd, 
                                    prod.codigo as codPrd, 
                                    prod.nombre as descPrd, 
                                    prodMed.auto as idEmpCompra, 
                                    prodMed.nombre as descEmpCompra, 
                                    prodMed.decimales as decimalEmpCompra,
                                    prod.contenido_compras as contEmpCompra,
                                    prodDep.fisica as exFisica,
                                    prodDep.fecha_conteo as fecUltConteo,
                                    (CASE WHEN kardex.ultDocVta IS NULL THEN '' ELSE kardex.ultDocVta END),
                                    (CASE WHEN kardex.ultDocComp IS NULL THEN '' ELSE kardex.ultDocComp END),
                                    (CASE WHEN kardex.ultDocInv IS NULL THEN '' ELSE kardex.ultDocInv END),
                                    prodExt.auto_emp_inv_1 as idEmpInv,
                                    prodMedExt.nombre as descEmpInv,
                                    prodMedExt.decimales as decimalEmpInv,
                                    prodExt.cont_emp_inv_1 as contEmpInv

                                from productos as prod
                                join productos_medida as prodMed on prodMed.auto=prod.auto_empaque_compra
                                join productos_deposito as prodDep on prodDep.auto_producto=prod.auto and prodDep.auto_deposito=@idDepositoTomaInv
                                join productos_ext as prodExt on prodExt.auto_producto=prod.auto
                                join productos_medida  as prodMedExt on prodMedExt.auto=prodExt.auto_emp_inv_1

                                LEFT join (
                                            SELECT
                                                p.auto_producto,
                                                MAX(CASE WHEN p.modulo = 'Ventas' THEN substring(p.auto_documento,4) END) AS ultDocVta,
                                                MAX(CASE WHEN p.modulo = 'Compras' THEN p.auto_documento END) AS ultDocComp,
                                                MAX(CASE WHEN p.modulo = 'Inventario' THEN p.auto_documento END) AS ultDocInv
                                            FROM
                                                productos_kardex AS p
                                            WHERE
                                                p.auto_deposito = @idDepositoTomaInv
                                                AND p.modulo IN ('Ventas', 'Compras', 'Inventario')
                                                and p.estatus_anulado='0'
                                            GROUP BY
                                                p.auto_producto
                                        ) as kardex on kardex.auto_producto = prod.auto

                                WHERE prod.auto IN (" + _prdLista.ToString() + ")";
                        _sql = cmd;
                        var v1 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        cnn.SaveChanges();
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
        public DtoLib.Resultado 
            TomaInv_ConvertirSolicitud_EnToma(DtoLibInventario.TomaInv.ConvertirSolicitud.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var cmd = @"UPDATE sistema_contadores set 
                                            a_toma_inventario=a_toma_inventario+1, 
                                            a_toma_inventario_numero=a_toma_inventario_numero+1";
                        var _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql);
                        cnn.SaveChanges();

                        cmd = "select a_toma_inventario from sistema_contadores";
                        _sql = cmd;
                        var autoDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (autoDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var codigoEmpSuc = ficha.codigoEmpSuc.Trim();
                        var largoCodigoEmpSuc = codigoEmpSuc.Length; 
                        var sAutoDoc = codigoEmpSuc+autoDoc.ToString().Trim().PadLeft(10-largoCodigoEmpSuc, '0');

                        cmd = "select a_toma_inventario_numero from sistema_contadores";
                        _sql = cmd;
                        var nroDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (nroDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var sNroDoc = nroDoc.ToString().Trim().PadLeft(10, '0');

                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoToma", sAutoDoc);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@autoSolicitud", ficha.autoSolicitud);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@docNroToma", sNroDoc);
                        cmd = @"INSERT INTO tomainv(
                                        `idSucursal`, 
                                        `idDeposito`, 
                                        `estatusProcesado`, 
                                        `observaciones_result`, 
                                        `autoriza_result`, 
                                        `cntItem_result`, 
                                        `fecha_result`, 
                                        `hora_result`,
                                        `codigoSucursal`, 
                                        `descSucursal`, 
                                        `codigoDeposito`, 
                                        `descDeposito`, 
                                        `autorizadoPor`, 
                                        `motivo`, 
                                        `fechaEmision`, 
                                        `cantItems`,
                                        `auto`, 
                                        `auto_solicitud`, 
                                        `documento_toma_nro`, 
                                        `documento_solicitud_nro`
                                        ) 
                                    select 
                                        idSucursal, 
                                        idDeposito,
                                        '0',
                                        '',
                                        '',
                                        0,
                                        '2000/01/01',
                                        '',
                                        codigoSucursal,
                                        descSucursal,
                                        codigoDeposito,
                                        descDeposito,
                                        autorizadoPor,
                                        motivo,
                                        fechaEmision,
                                        cntItems,
                                        @autoToma,
                                        @autoSolicitud,
                                        @docNroToma,
                                        documentoNro
                                    from tomainv_solicitud
                                    where auto=@autoSolicitud";
                        _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql, t1, t2, t3);
                        cnn.SaveChanges();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoSolicitud", ficha.autoSolicitud);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@autoToma", sAutoDoc);
                        cmd = @"INSERT INTO tomainv_detalle (
                                    `idPrd`, 
                                    `codPrd`, 
                                    `descPrd`, 
                                    `idEmpCompra`, 
                                    `descEmpCompra`, 
                                    `decimalEmpCompra`, 
                                    `contEmpCompra`, 
                                    `exFisica`, 
                                    `fecUltConteo`, 
                                    `ultDocVta`, 
                                    `ultDocComp`, 
                                    `ultDocInv`, 
                                    `estatusToma`, 
                                    `idEmpInv`, 
                                    `descEmpInv`, 
                                    `decimalEmpInv`, 
                                    `contEmpInv`,
                                    `auto_tomainv`) 
                                select 
                                    idPrd, 
                                    codPrd, 
                                    descPrd, 
                                    idEmpCompra, 
                                    descEmpCompra, 
                                    decimalEmpCompra,
                                    contEmpCompra,
                                    exFisica,
                                    fecUltConteo,
                                    ultDocVta,
                                    ultDocComp,
                                    ultDocInv,
                                    '',
                                    idEmpInv,
                                    descEmpInv,
                                    decimalEmpInv,
                                    contEmpInv,
                                    @autoToma
                                from tomainv_solicitud_detalle 
                                WHERE auto_solicitud=@autoSolicitud";
                        _sql = cmd;
                        var v1 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        cnn.SaveChanges();
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

        private class solicitud_toma
        {
            public string autoSolicitud { get; set; }
            public string autoToma { get; set; }
        }
        public DtoLib.ResultadoEntidad<string> 
            TomaInv_EncontrarSolicitudActiva(string codigoEmpSuc)
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codigoEmpSuc", codigoEmpSuc);
                    var cmd = @"select 
                                    solicitud.auto as autoSolicitud,
                                    toma.auto as autoToma
                                FROM tomainv_solicitud as solicitud
                                left join tomainv as toma on toma.auto_solicitud=solicitud.auto
                                WHERE solicitud.codigoSucursal=@codigoEmpSuc";
                    var _sql = cmd;
                    var _lst = cnn.Database.SqlQuery<solicitud_toma>(_sql, p1).ToList();
                    if (_lst.Count >0)
                    {
                        var _ent = _lst.FirstOrDefault(f => f.autoToma == null);
                        if (_ent == null)
                        { result.Entidad = ""; }
                        else
                        { result.Entidad = _ent.autoSolicitud; }
                    }
                    else
                    { result.Entidad = ""; }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha>
            TomaInv_AnalizarToma(string idToma)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", idToma);
                    var cmd = @"SELECT 
                                    det.idPrd as idPrd, 
                                    det.codPrd as codPrd,
                                    det.descPrd as descPrd,
                                    det.exFisica as fisico,
                                    det.contEmpCompra as contEmpCompra,
                                    det.contEmpInv as contEmpInv ,
                                    det.descEmpCompra as descEmpCompra,
                                    det.descEmpInv as descEmpInv ,
                                    conteo.cant as conteo,
                                    conteo.cnVta_und as cntVenta,
                                    conteo.cnComp_und as cntCompra,
                                    conteo.cnInv_und as cntMovInv,
                                    conteo.cnDesp_und as cntPorDespachar,
                                    conteo.cnDeposito_und as exDeposito,
                                    conteo.motivo as motivo,
                                    conteo.idTerminal as idTerminal,
                                    product.divisa as costoMonDivisa, 
                                    product.costo as costoMonLocal,
                                    product.estatus_divisa as estatusDivisa
                                FROM tomainv_detalle det
                                join tomainv as toma on det.auto_tomainv=toma.auto
                                join productos as product on product.auto=det.idPrd
                                left join tomainv_conteo as conteo on conteo.idPrd=det.idPrd and conteo.auto_tomainv=toma.auto
                                where toma.auto=@idToma
                                    and toma.estatusProcesado='0'";
                    var _sql = cmd;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.Analisis.Item>(_sql, p1).ToList();

                    var xp1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", idToma);
                    _sql = @"select 
                                descSucursal as sucursal,
                                descDeposito as deposito,
                                documento_solicitud_nro as solicitudNro,
                                documento_toma_nro as tomaNro
                            from tomainv
                            where auto=@idToma";
                    var _ent = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.Analisis.Ficha>(_sql, xp1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        throw new Exception("TOMA NO ENCONTRADA");
                    }
                    _ent.Items = _lst;
                    result.Entidad = _ent;
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
            TomaInv_Analizar_TomaDisponible()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var cmd = @"select 
                                    auto as autoToma
                                FROM tomainv 
                                WHERE estatusProcesado='0'";
                    var _sql = cmd;
                    var _ent = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (_ent!=null)
                    { result.Entidad = _ent; }
                    else
                    { result.Entidad = ""; }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Ficha> 
            TomaInv_GetLista_PorMovAjuste(DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idSucursal", filtro.idSucursal);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", filtro.idDeposito);
                    var cmd = @"select 
	                                auto as idToma,
                                    fechaEmision as fechaEmision,
                                    documento_toma_nro as docTomaNro,
                                    documento_solicitud_nro as docSolicitudNro, 
                                    cntItem_result as cntItemResult
                                from tomainv
                                where idSucursal=@idSucursal 
                                and idDeposito=@idDeposito
                                and estatusProcesado='1'
                                and estatusMovAjuste='0'";
                    var _sql = cmd;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Ficha>(_sql, p1, p2).ToList();
                    result.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.Resultado.Ficha> 
            TomaInv_GetToma_Resultado(string idToma)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.Resultado.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", idToma);
                    var cmd = @"select 
	                                detalle.idPrd as autoPrd,
                                    detalle.codPrd as codigoPrd,
                                    detalle.descPrd as nombrePrd,
                                    detalle.contEmpCompra as contEmp,
                                    result.costoMonDivisa as costoDivisa,
                                    result.costoMonLocal as costo,
                                    result.diferencia_und as cantidadAjustar,
                                    result.signo as signo,
                                    result.descripcion as descTipoAjuste,
                                    result.estatusDivisa as estatusDivisa,
                                    product.auto_departamento as autoDepart,
                                    product.auto_grupo  as autoGrupo,
                                    product.categoria as catPrd,
                                    tasa.auto as autoTasa,
                                    tasa.nombre as descTasa,
                                    tasa.tasa as valorTasa,
                                    conteo.cant as conteo 
                                from tomainv_result as result
                                join tomainv_conteo as conteo on conteo.auto_tomainv=result.auto_tomainv and conteo.idPrd=result.idPrd
                                join tomainv_detalle as detalle on detalle.auto_tomainv=result.auto_tomainv and detalle.idPrd=result.idPrd
                                join productos as product on product.auto=detalle.idPrd
                                join empresa_tasas as tasa on tasa.auto=product.auto_tasa
                                where result.auto_tomainv=@idToma";
                    var _sql = cmd;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.Resumen.Resultado.Ficha>(_sql, p1).ToList();
                    result.Lista = _lst;
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
            TomaInv_AnalizarToma_GetMotivo(DtoLibInventario.TomaInv.Analisis.Motivo.Obtener.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", ficha.idToma);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                    var cmd = @"select 
                                    motivo
                                from tomainv_conteo
                                where auto_tomainv=@idToma
                                and idPrd=@idPrd";
                    var _sql = cmd;
                    var _ent = cnn.Database.SqlQuery<string>(_sql, p1, p2).FirstOrDefault();
                    if (_ent == null) 
                    {
                        throw new Exception("ITEM NO ENCONTRADO");
                    }
                    result.Entidad = _ent;
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
            TomaInv_AnalizarToma_SetMotivo(DtoLibInventario.TomaInv.Analisis.Motivo.Cambiar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idToma", ficha.idToma);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", ficha.motivo);
                    var cmd = @"update tomainv_conteo
                                    set motivo=@motivo
                                where auto_tomainv=@idToma
                                and idPrd=@idPrd";
                    var _sql = cmd;
                    var _r = cnn.Database.ExecuteSqlCommand(_sql, p1, p2, p3);
                    if (_r == 0)
                    {
                        throw new Exception("ITEM NO ACTUALIZADO");
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
            TomaInv_AnalizarToma_NoHayExistencia(DtoLibInventario.TomaInv.Analisis.NoHayExistencia.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _vta = 0m;
                        var _comp = 0m;
                        var _inv = 0m;
                        var _porDesp = 0m;
                        //
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idTomaInv",ficha.idTomaInv);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd",ficha.idPrd);
                        var _sql = @"SELECT 
                                        sum(cantidad_und*signo) 
                                    FROM productos_kardex as kardex
                                    JOIN tomainv_detalle as tomaDet ON tomaDet.idPrd = kardex.auto_producto
                                    JOIN tomainv as toma ON toma.auto = tomaDet.auto_tomainv
                                    WHERE kardex.auto_producto = @idPrd
                                        AND toma.auto = @idTomainv
                                        AND kardex.auto_deposito = toma.idDeposito
                                        AND kardex.estatus_anulado = '0'
                                        AND kardex.modulo = 'Ventas'
                                        AND substring(kardex.auto_documento, 4) > tomaDet.ultDocVta";
                        decimal? _cntVta = cnn.Database.SqlQuery<decimal?>(_sql, p1, p2).FirstOrDefault();
                        if (_cntVta.HasValue) 
                        {
                            _vta = _cntVta.Value;
                        }

                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idTomaInv", ficha.idTomaInv);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                        _sql = @"SELECT 
                                    sum(cantidad_und*signo)
                                FROM productos_kardex as kardex 
                                JOIN tomainv_detalle as tomaDet ON tomaDet.idPrd = kardex.auto_producto
                                JOIN tomainv as toma ON toma.auto = tomaDet.auto_tomainv
                                WHERE kardex.auto_producto=@idPrd
                                    AND toma.auto = @idTomaInv
                                    and kardex.auto_deposito=toma.idDeposito
                                    and kardex.estatus_anulado='0'
                                    and kardex.modulo='Compras'
                                    and kardex.auto_documento>tomaDet.ultDocComp";
                        decimal? _cntCompra = cnn.Database.SqlQuery<decimal?>(_sql, p1, p2).FirstOrDefault();
                        if (_cntCompra.HasValue)
                        {
                            _comp = _cntCompra.Value;
                        }
                        
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idTomaInv", ficha.idTomaInv);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                        _sql = @"SELECT 
                                    sum(cantidad_und*signo) 
                                FROM productos_kardex as kardex 
                                JOIN tomainv_detalle as tomaDet ON tomaDet.idPrd = kardex.auto_producto
                                JOIN tomainv as toma ON toma.auto = tomaDet.auto_tomainv
                                WHERE kardex.auto_producto=@idPrd
                                    AND toma.auto = @idTomaInv
                                    and kardex.auto_deposito=toma.idDeposito
                                    and kardex.estatus_anulado='0'
                                    and kardex.modulo='Inventario'
                                    and kardex.auto_documento>tomaDet.ultDocInv";
                        decimal? _cntInv = cnn.Database.SqlQuery<decimal?>(_sql, p1, p2).FirstOrDefault();
                        if (_cntInv.HasValue)
                        {
                            _inv = _cntInv.Value;
                        }

                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                        _sql = @"SELECT 
                                    sum(vdetalle.cantidad_und*vdetalle.signo) as cnt
		                        FROM `p_verificador` as verificador
		                        join ventas_detalle as vdetalle on vdetalle.auto_documento=verificador.autoDocumento
		                        join ventas as ventas on ventas.auto=verificador.autoDocumento
		                        where verificador.fechaReg=curdate()
			                        and vdetalle.auto_producto=@idPrd
			                        and verificador.estatusVer='0'
			                        and ventas.estatus_anulado='0'";
                        decimal? _cntPorDesp = cnn.Database.SqlQuery<decimal?>(_sql, p1).FirstOrDefault();
                        if (_cntPorDesp.HasValue)
                        {
                            _porDesp = _cntPorDesp.Value;
                        }

                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idTomaInv", ficha.idTomaInv);
                        _sql = @"SELECT 
                                    exFisica
		                        FROM tomainv_detalle 
		                        where idPrd=@idPrd 
			                        and auto_tomainv=@idTomaInv";
                        decimal? _exFisica = cnn.Database.SqlQuery<decimal?>(_sql, p1, p2).FirstOrDefault();
                        if (!_exFisica.HasValue) 
                        {
                            throw new Exception("EXISTENCIA FISICA DEL PRODUCTO NO ENCONTRADA");
                        }
                        var _ex = _exFisica.Value;

                        var cnt = Math.Abs(_ex + _vta + _comp + _inv + _porDesp) * (-1);
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idTomaInv", ficha.idTomaInv);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", ficha.idPrd);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@cnt", cnt);
                        _sql = @"INSERT INTO tomainv_conteo (
                                    `idPrd` ,
                                    `cant` ,
                                    `fechaRegistro` ,
                                    `auto_tomainv` ,
                                    `cnVta_und` ,
                                    `cnComp_und` ,
                                    `cnInv_und` ,
                                    `cnDesp_und` ,
                                    `cnDeposito_und` ,
                                    `cierre_ftp` ,
                                    `motivo` ,
                                    `idTerminal`
                                )
                                VALUES (
                                    @idPrd, 
                                    @cnt,
                                    CURRENT_TIMESTAMP, 
                                    @idTomaInv, 
                                    0, 
                                    0,
                                    0,
                                    0, 
                                    0, 
                                    '', 
                                    '', 
                                    '0')";
                        var r1 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2, p3);
                        if (r1 == 0) 
                        {
                            throw new Exception("PROBLEMA AL REGISTRAR CONTEO SIN EXISTENCIA");
                        }
                        cnn.SaveChanges();
                        ts.Complete();
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


        //VERIFICA SI EXISTE UNA TOMA ACTIVA
        //USADO CUANDO SE QUIERE GENERAR UN NUEVO CONTEO
        public DtoLib.ResultadoEntidad<int> 
            TomaInv_VerificaSiHayUnaTomaActiva()
        {
            var result = new DtoLib.ResultadoEntidad<int>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var _sql = @"SELECT count(*) as cnt FROM tomainv WHERE estatusProcesado='0'";
                    var _ent = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                    result.Entidad = 0;
                    if (_ent.HasValue) 
                    {
                        result.Entidad = _ent.Value;
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


        //GENERA UN NUEVO CONTEO, USADO POR LAS SUCURSALES 
        public DtoLib.Resultado 
            TomaInv_GenerarConteo(DtoLibInventario.TomaInv.Solicitud.Generar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                var _prdLista = new StringBuilder();
                var _firts = true;
                foreach (var rg in ficha.ProductosTomarInv)
                {
                    if (!_firts)
                    {
                        _prdLista.Append(",");
                    }
                    _prdLista.Append("'" + rg.idPrd + "'");
                    _firts = false;
                }

                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var cmd = @"UPDATE sistema_contadores set 
                                            a_toma_inventario_solicitud=a_toma_inventario_solicitud+1, 
                                            a_toma_inventario_solicitud_numero=a_toma_inventario_solicitud_numero+1";
                        var _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql);
                        cnn.SaveChanges();

                        cmd = "select a_toma_inventario_solicitud from sistema_contadores";
                        _sql = cmd;
                        var autoDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (autoDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }

                        var _xlargo = 10 - ficha.codigoSucursal.Trim().Length;
                        var sAutoDoc = ficha.codigoSucursal.Trim()+autoDoc.ToString().Trim().PadLeft(_xlargo, '0');

                        cmd = "select a_toma_inventario_solicitud_numero from sistema_contadores";
                        _sql = cmd;
                        var nroDoc = cnn.Database.SqlQuery<int?>(_sql).FirstOrDefault();
                        if (nroDoc == null)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADOR DE TOMAS");
                        }
                        var sNroDoc = nroDoc.ToString().Trim().PadLeft(10, '0');

                        var t0 = new MySql.Data.MySqlClient.MySqlParameter("@auto", sAutoDoc);
                        var t1 = new MySql.Data.MySqlClient.MySqlParameter("@idSucursal", ficha.idSucursal);
                        var t2 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", ficha.idDeposito);
                        var t3 = new MySql.Data.MySqlClient.MySqlParameter("@codigoSucursal", ficha.codigoSucursal);
                        var t4 = new MySql.Data.MySqlClient.MySqlParameter("@descSucursal", ficha.descSucursal);
                        var t5 = new MySql.Data.MySqlClient.MySqlParameter("@codigoDeposito", ficha.codigoDeposito);
                        var t6 = new MySql.Data.MySqlClient.MySqlParameter("@descDeposito", ficha.descDeposito);
                        var t7 = new MySql.Data.MySqlClient.MySqlParameter("@autorizadoPor", ficha.autorizadoPor);
                        var t8 = new MySql.Data.MySqlClient.MySqlParameter("@motivo", ficha.motivo);
                        var t9 = new MySql.Data.MySqlClient.MySqlParameter("@fechaEmision", fechaSistema.Date);
                        var ta = new MySql.Data.MySqlClient.MySqlParameter("@documentoNro", sNroDoc);
                        var tb = new MySql.Data.MySqlClient.MySqlParameter("@cntItems", ficha.cantItems);
                        cmd = @"INSERT INTO tomainv_solicitud (
                                        `auto`, 
                                        `idSucursal`, 
                                        `idDeposito`, 
                                        `estatusAnulado`, 
                                        `codigoSucursal`, 
                                        `descSucursal`, 
                                        `codigoDeposito`, 
                                        `descDeposito`, 
                                        `autorizadoPor`, 
                                        `motivo`, 
                                        `fechaEmision`, 
                                        `documentoNro`, 
                                        `cntItems`,
                                        `estatusTransmitida`) 
                                    VALUES (
                                        @auto,
                                        @idSucursal, 
                                        @idDeposito,
                                        '0',
                                        @codigoSucursal, 
                                        @descSucursal, 
                                        @codigoDeposito, 
                                        @descDeposito, 
                                        @autorizadoPor, 
                                        @motivo, 
                                        @fechaEmision,
                                        @documentoNro,
                                        @cntItems,
                                        '0')";
                        _sql = cmd;
                        cnn.Database.ExecuteSqlCommand(_sql, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, ta, tb);
                        cnn.SaveChanges();

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoSolicitud", sAutoDoc);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDepositoTomaInv", ficha.idDeposito);
                        cmd = @"INSERT INTO tomainv_solicitud_detalle (
                                    `auto_solicitud`,
                                    `idPrd`, 
                                    `codPrd`, 
                                    `descPrd`, 
                                    `idEmpCompra`, 
                                    `descEmpCompra`, 
                                    `decimalEmpCompra`, 
                                    `contEmpCompra`, 
                                    `exFisica`, 
                                    `fecUltConteo`, 
                                    `ultDocVta`, 
                                    `ultDocComp`, 
                                    `ultDocInv`, 
                                    `idEmpInv`, 
                                    `descEmpInv`, 
                                    `decimalEmpInv`, 
                                    `contEmpInv`) 
                                select 
                                    @autoSolicitud,
                                    prod.auto idPrd, 
                                    prod.codigo as codPrd, 
                                    prod.nombre as descPrd, 
                                    prodMed.auto as idEmpCompra, 
                                    prodMed.nombre as descEmpCompra, 
                                    prodMed.decimales as decimalEmpCompra,
                                    prod.contenido_compras as contEmpCompra,
                                    prodDep.fisica as exFisica,
                                    prodDep.fecha_conteo as fecUltConteo,
                                    (CASE WHEN kardex.ultDocVta IS NULL THEN '' ELSE kardex.ultDocVta END),
                                    (CASE WHEN kardex.ultDocComp IS NULL THEN '' ELSE kardex.ultDocComp END),
                                    (CASE WHEN kardex.ultDocInv IS NULL THEN '' ELSE kardex.ultDocInv END),
                                    prodExt.auto_emp_inv_1 as idEmpInv,
                                    prodMedExt.nombre as descEmpInv,
                                    prodMedExt.decimales as decimalEmpInv,
                                    prodExt.cont_emp_inv_1 as contEmpInv

                                from productos as prod
                                join productos_medida as prodMed on prodMed.auto=prod.auto_empaque_compra
                                join productos_deposito as prodDep on prodDep.auto_producto=prod.auto and prodDep.auto_deposito=@idDepositoTomaInv
                                join productos_ext as prodExt on prodExt.auto_producto=prod.auto
                                join productos_medida  as prodMedExt on prodMedExt.auto=prodExt.auto_emp_inv_1

                                LEFT join (
                                            SELECT
                                                p.auto_producto,
                                                MAX(CASE WHEN p.modulo = 'Ventas' THEN substring(p.auto_documento,4) END) AS ultDocVta,
                                                MAX(CASE WHEN p.modulo = 'Compras' THEN p.auto_documento END) AS ultDocComp,
                                                MAX(CASE WHEN p.modulo = 'Inventario' THEN p.auto_documento END) AS ultDocInv
                                            FROM
                                                productos_kardex AS p
                                            WHERE
                                                p.auto_deposito = @idDepositoTomaInv
                                                AND p.modulo IN ('Ventas', 'Compras', 'Inventario')
                                                and p.estatus_anulado='0'
                                            GROUP BY
                                                p.auto_producto
                                        ) as kardex on kardex.auto_producto = prod.auto

                                WHERE prod.auto IN (" + _prdLista.ToString() + ")";
                        _sql = cmd;
                        var v1 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        cnn.SaveChanges();
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