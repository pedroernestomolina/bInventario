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
                                    vtas.margen as margen
                                from productos as p 

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
                                where auto_departamento not IN("+_departExcluir.ToString()+@")
                                        and p.estatus!='Inactivo' 
                                        and p.categoria!='Bien de Servicio'";
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
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDepositoTomaInv", ficha.IdDepositoTomaInv);
                        var cmd = @"INSERT INTO tomainv_detalle (
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
                                    `descEmpaqueInv`, 
                                    `decimalEmpInv`, 
                                    `contEmpInv`) 
                                select 
                                    null, 
                                    1, 
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
                        var _sql = cmd;
                        var v1 = cnn.Database.ExecuteSqlCommand(_sql, p1);
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha> 
            TomaInv_AnalizarToma(int idToma)
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
                                    conteo.cant as conteo,

                                    (
                                        SELECT sum(cantidad_und*signo) as cnt 
                                        FROM `productos_kardex` 
                                        where auto_producto=det.idPrd
                                            and auto_deposito=toma.idDeposito
                                            and estatus_anulado='0'
                                            and modulo='Ventas'
                                            and substring(auto_documento,4)>det.ultDocVta
                                    ) as vtas,

                                    (
                                        SELECT sum(cantidad_und*signo) as cnt
                                        FROM `productos_kardex` 
                                        where auto_producto=det.idPrd
                                            and auto_deposito=toma.idDeposito
                                            and estatus_anulado='0'
                                            and modulo='Compras'
                                            and auto_documento>det.ultDocComp
                                    ) as comp,

                                    (
                                        SELECT sum(cantidad_und*signo) as cnt
                                        FROM `productos_kardex` 
                                        where auto_producto=det.idPrd
                                            and auto_deposito=toma.idDeposito
                                            and estatus_anulado='0'
                                            and modulo='Inventario'
                                            and auto_documento>det.ultDocInv
                                    ) as inv,

                                    deposito.fisica as fisicoDep

                                FROM `tomainv_detalle` det
                                join tomainv as toma on det.idTomaInv=toma.id
                                join productos_deposito as deposito on deposito.auto_producto=det.idPrd and deposito.auto_deposito=toma.idDeposito
                                left join tomainv_conteo as conteo on conteo.idPrd=det.idPrd and conteo.idToma=toma.id
                                where toma.id=@idToma
                                    and toma.estatusAnulado='0'
                                    and toma.estatusProcesado='0'";
                    var _sql = cmd;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.TomaInv.Analisis.Item>(_sql, p1).ToList();
                    result.Entidad = new DtoLibInventario.TomaInv.Analisis.Ficha()
                    {
                        Items = _lst,
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
                        var cmd1 = @"update tomainv_detalle set estatusToma='' where idTomaInv=@idToma and idPrd=@idPrd";
                        var cmd2 = @"delete from tomainv_conteo where idToma=@idToma and idPrd=@idPrd";
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
    }
}