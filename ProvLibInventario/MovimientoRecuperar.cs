using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    public partial class Provider: ILibInventario.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibInventario.MovimientoRecuperar.Entidad.Ficha> 
            recuperarMovimientoFicha(string autoDoc)
        {
            var rst = new DtoLib.ResultadoEntidad<DtoLibInventario.MovimientoRecuperar.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var _sql_1 = @"select 
                                        enc.auto as movId, 
                                        enc.documento as movNumero,
                                        enc.fecha as movFecha,
                                        enc.tipo as docCodigo,
                                        enc.documento_nombre as docNombre,
                                        enc.concepto as conceptoDesc,
                                        enc.codigo_concepto as conceptoCodigo,
                                        enc.deposito as depositoOrigenDesc,
                                        enc.codigo_deposito as depositoOrigenCodigo,
                                        enc.destino as depositoDestinoDesc,
                                        enc.codigo_destino as depositoDestinoCodigo,
                                        enc.nota as movNotas,
                                        enc.hora as movHora,
                                        enc.estacion as movEstacionEquipo,
                                        enc.autorizado as movPersonaAutoriza,
                                        enc.total as movTotalMonedaLocal, 
                                        ext.monto_divisa as movTotalMonedaRef,
                                        ext.factor_cambio as movFactorCambio,
                                        enc.estatus_anulado as movEstatusAnulado, 
                                        enc.usuario as usuarioNombre ,
                                        enc.codigo_usuario as usuarioCodigo,
                                        '' as sucursalDesc,
                                        enc.codigo_sucursal as sucursalCodigo 
                                from productos_movimientos as enc 
                                left join productos_movimientos_extra as ext on ext.auto_movimiento=enc.auto
                                where enc.auto=@idMov";
                    var sql = _sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", autoDoc);
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.MovimientoRecuperar.Entidad.Encabezado>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("[ ID ] DOCUMENTO NO ENCONTRADO");
                    }
                    //
                    _sql_1 = @"select 
                                codigo as prdCodigo,
                                nombre as prdDesc,
                                cantidad as cantidadEmp,
                                cantidad_und as cantidadUnd,
                                empaque as empqDesc,
                                contenido_empaque as empqContenido,
                                signo as signoMov,
                                costo_und as costoUndMonedaLocal,
                                total as importeMonedaLocal,
                                0 as costoUndMonedaRef,
                                0 as importeMonedaRef,
                                decimales as cntDecimales 
                            from productos_movimientos_detalle
                            where auto_documento=@idMov";
                    sql = _sql_1;
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", autoDoc);
                    var det = cnn.Database.SqlQuery<DtoLibInventario.MovimientoRecuperar.Entidad.Detalle>(sql, p1).ToList();
                    //
                    rst.Entidad = new DtoLibInventario.MovimientoRecuperar.Entidad.Ficha()
                    {
                        encabezado = ent,
                        detalles = det,
                    };
                }
            }
            catch (Exception e)
            {
                rst.Mensaje = e.Message;
                rst.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rst;
        }
    }
}