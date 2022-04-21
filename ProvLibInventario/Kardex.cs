using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Resumen.Ficha> Producto_Kardex_Movimiento_Lista_Resumen(DtoLibInventario.Kardex.Movimiento.Resumen.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Resumen.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var cmd = "SELECT count(*) as cntMovimiento, modulo, auto_deposito as autoDeposito, auto_concepto as autoConcepto, " +
                        "SUM(cantidad_und*signo) as cntInventario, nombre_deposito as nombreDeposito, codigo_deposito as codigoDeposito, " +
                        "codigo_concepto as codigoConcepto, nombre_concepto as nombreConcepto, siglas " +
                        "FROM `productos_kardex` as kardex " +
                        "WHERE auto_producto=@autoPrd and estatus_anulado='0' and fecha>@desde " +
                        "group by modulo,auto_deposito,auto_concepto,codigo_deposito,nombre_deposito,codigo_concepto,nombre_concepto,siglas";

                    var fechaServidor = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    DateTime? desde = fechaServidor.Date;
                    if (filtro.ultDias != DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.SinDefinir)
                    {
                        switch (filtro.ultDias) 
                        {
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Hoy:
                                desde= desde.Value.AddDays(-1);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Ayer:
                                desde = desde.Value.AddDays(-2);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._7Dias:
                                desde = desde.Value.AddDays(-7);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._15Dias:
                                desde = desde.Value.AddDays(-15);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._30Dias:
                                desde = desde.Value.AddDays(-30);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._45Dias:
                                desde = desde.Value.AddDays(-45);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._60Dias:
                                desde = desde.Value.AddDays(-60);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._90Dias:
                                desde = desde.Value.AddDays(-90);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._120Dias:
                                desde = desde.Value.AddDays(-120);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Todo:
                                desde = desde.Value.AddDays(-365);
                                break;
                        }
                    }

                    var entPrd= cnn.productos.Find(filtro.autoProducto);
                    if (entPrd==null)
                    {
                        result.Mensaje="[ ID ] PRODUCTO NO ENCOTRADO";
                        result.Result= DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    
                    var _empaqueCompra="";
                    var _existencia=0.0m;
                    var entPrdEx= cnn.productos_deposito.Where(w=>w.auto_producto==filtro.autoProducto).ToList();
                    var entPrdEmp=cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    if (entPrdEx.Count>0){_existencia=entPrdEx.Sum(s=>s.fisica);}
                    if (entPrdEmp!=null){_empaqueCompra=entPrdEmp.nombre;}

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", filtro.autoProducto);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@desde", desde);
                    var lst= cnn.Database.SqlQuery<DtoLibInventario.Kardex.Movimiento.Resumen.Data>(cmd, p1, p2).ToList();

                    var ex = 0.0m;
                    cmd = "select sum(cantidad_und*signo) as cnt from productos_kardex where auto_producto=@autoPrd " +
                        "and estatus_anulado='0' and fecha<=@desde";
                    var objEx = cnn.Database.SqlQuery<decimal?>(cmd, p1, p2).FirstOrDefault();
                    if (objEx != null) { ex = objEx.Value; }

                    var rt = new DtoLibInventario.Kardex.Movimiento.Resumen.Ficha()
                    {
                        codigoProducto = entPrd.codigo,
                        contenidoEmp = entPrd.contenido_compras,
                        empaqueCompra = _empaqueCompra,
                        decimales= entPrdEmp.decimales,
                        existenciaActual = _existencia,
                        existencaFecha=ex,
                        fecha=desde.Value.ToShortDateString(),
                        nombreProducto = entPrd.nombre,
                        referenciaProducto = entPrd.referencia,

                        Data = lst,
                    };
                    result.Entidad = rt;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Detalle.Ficha> Producto_Kardex_Movimiento_Lista_Detalle(DtoLibInventario.Kardex.Movimiento.Detalle.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Detalle.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaServidor = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    DateTime? desde = fechaServidor.Date;
                    if (filtro.ultDias != DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.SinDefinir)
                    {
                        switch (filtro.ultDias)
                        {
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Hoy:
                                desde = desde.Value.AddDays(-1);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Ayer:
                                desde = desde.Value.AddDays(-2);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._7Dias:
                                desde = desde.Value.AddDays(-7);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._15Dias:
                                desde = desde.Value.AddDays(-15);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._30Dias:
                                desde = desde.Value.AddDays(-30);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._45Dias:
                                desde = desde.Value.AddDays(-45);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._60Dias:
                                desde = desde.Value.AddDays(-60);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._90Dias:
                                desde = desde.Value.AddDays(-90);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._120Dias:
                                desde = desde.Value.AddDays(-120);
                                break;
                            case DtoLibInventario.Kardex.Enumerados.EnumMovUltDias.Todo:
                                desde = desde.Value.AddDays(-365);
                                break;
                        }
                    }

                    var entPrd = cnn.productos.Find(filtro.autoProducto);
                    if (entPrd == null)
                    {
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCOTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _empaqueCompra = "";
                    var _decimales = "0";
                    var _existencia = 0.0m;
                    var entPrdEx = cnn.productos_deposito.Where(w => w.auto_producto == filtro.autoProducto && w.auto_deposito== filtro.autoDeposito).ToList();
                    var entPrdEmp = cnn.productos_medida.Find(entPrd.auto_empaque_compra);
                    if (entPrdEx.Count > 0) { _existencia = entPrdEx.Sum(s => s.fisica); }
                    if (entPrdEmp != null) { _empaqueCompra = entPrdEmp.nombre; _decimales = entPrdEmp.decimales; }

                    var q = cnn.productos_kardex.Where(f => 
                        f.auto_producto == filtro.autoProducto && 
                        f.auto_deposito==filtro.autoDeposito &&
                        f.auto_concepto==filtro.autoConcepto &&
                        f.modulo==filtro.modulo &&
                        f.fecha > desde
                        ).ToList();

                    var list = new List<DtoLibInventario.Kardex.Movimiento.Detalle.Data>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _isAnulado = s.estatus_anulado.Trim().ToUpper() == "1" ? true : false;
                                var _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.SinDefinir;
                                switch (s.modulo.Trim().ToUpper())
                                {
                                    case "INVENTARIO":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Inventario;
                                        break;
                                    case "COMPRAS":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Compra;
                                        break;
                                    case "VENTAS":
                                        _modulo = DtoLibInventario.Kardex.Enumerados.EnumModulo.Venta;
                                        break;
                                }
                                var _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.SinDefinir;
                                switch (s.siglas.Trim().ToUpper())
                                {
                                    case "FAC":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Factura;
                                        break;
                                    case "NCR":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NCredito;
                                        break;
                                    case "NDB":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NDebito;
                                        break;
                                    case "NEN":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.NEntrega;
                                        break;
                                    case "CAR":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Cargo;
                                        break;
                                    case "DES":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Descargo;
                                        break;
                                    case "TRA":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Traslado;
                                        break;
                                    case "AJU":
                                        _siglas = DtoLibInventario.Kardex.Enumerados.EnumSiglas.Ajuste;
                                        break;
                                }

                                var r = new DtoLibInventario.Kardex.Movimiento.Detalle.Data()
                                {
                                    autoConcepto = s.auto_concepto,
                                    autoDeposito = s.auto_deposito,
                                    autoDocumento = s.auto_documento,
                                    cantidad = s.cantidad,
                                    cantidadBono = s.cantidad_bono,
                                    cantidadUnd = s.cantidad_und,
                                    codigoDoc = s.codigo,
                                    codigoSucursal = s.codigo_sucursal,
                                    costoUnd = s.costo_und,
                                    documento = s.documento,
                                    entidad = s.entidad,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    isAnulado = _isAnulado,
                                    Modulo = _modulo,
                                    moduloDoc = s.modulo,
                                    nota = s.nota,
                                    precioUnd = s.precio_und,
                                    Siglas = _siglas,
                                    siglasDoc = s.siglas,
                                    signoDoc = s.signo,
                                    total = s.total,
                                };
                                return r;
                            }).ToList();

                            var rt = new DtoLibInventario.Kardex.Movimiento.Detalle.Ficha()
                            {
                                codigoProducto = entPrd.codigo,
                                contenidoEmp = entPrd.contenido_compras,
                                EmpaqueCompra = _empaqueCompra,
                                existencia = _existencia,
                                nombreProducto = entPrd.nombre,
                                referenciaProducto = entPrd.referencia,
                                decimales=_decimales,
                                Data = list,
                            };
                            result.Entidad = rt;
                        }
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

    }

}