using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibInventario.Costo.Historico.Resumen> HistoricoCosto_GetLista(DtoLibInventario.Costo.Historico.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Costo.Historico.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var entprd = cnn.productos.Find(filtro.autoProducto);
                    if (entprd == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        return result;
                    }

                    var q = cnn.productos_costos.Where(f=>f.auto_producto==filtro.autoProducto && f.serie.Trim()!="").ToList();
                    var list = new List<DtoLibInventario.Costo.Historico.Data>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _modoCambio= DtoLibInventario.Costo.Enumerados.enumModoCambio.SinDefinir;
                                switch (s.serie.Trim().ToUpper()) 
                                {
                                    case "FAC":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PorCompra;
                                        break;
                                    case "ORD":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PorOrdenCompra;
                                        break;
                                    case "MAN":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.ManualPorInventario;
                                        break;
                                    case "LIS":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PoListaPrecioProveedor;
                                        break;
                                }
                                var r = new DtoLibInventario.Costo.Historico.Data()
                                {
                                    costo = s.costo,
                                    costoDivisa = s.costo_divisa,
                                    documento = s.documento,
                                    estacion = s.estacion,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    modoCambio = _modoCambio,
                                    nota = s.nota,
                                    serie = s.serie,
                                    tasaDivisa = s.divisa,
                                    usuario = s.usuario,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    var nr = new DtoLibInventario.Costo.Historico.Resumen();
                    nr.codigo = entprd.codigo;
                    nr.descripcion = entprd.nombre;
                    nr.data = list;
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

        public DtoLib.Resultado CostoProducto_Actualizar(DtoLibInventario.Costo.Editar.Ficha ficha)
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
                        entPrd.costo_proveedor =ficha.costoProveedor;
                        entPrd.costo_proveedor_und = ficha.costoProveedorUnd;
                        entPrd.costo_importacion = ficha.costoImportacion;
                        entPrd.costo_importacion_und = ficha.costoImportacionUnd;
                        entPrd.costo_varios = ficha.costoVario;
                        entPrd.costo_varios_und = ficha.costoVarioUnd;
                        entPrd.costo = ficha.costoFinal;
                        entPrd.costo_und = ficha.costoFinalUnd;
                        entPrd.costo_promedio = ficha.costoPromedio;
                        entPrd.costo_promedio_und = ficha.costoPromedioUnd;
                        entPrd.divisa = ficha.costoDivisa;
                        entPrd.fecha_ult_costo = fechaSistema.Date;
                        entPrd.fecha_cambio = fechaSistema.Date;
                        if (ficha.precio != null)
                        {
                            entPrd.precio_1 = ficha.precio.pn1;
                            entPrd.precio_2 = ficha.precio.pn2;
                            entPrd.precio_3 = ficha.precio.pn3;
                            entPrd.precio_4 = ficha.precio.pn4;
                            entPrd.precio_pto = ficha.precio.pn5;
                            entPrd.utilidad_1 = ficha.precio.ut1;
                            entPrd.utilidad_2 = ficha.precio.ut2;
                            entPrd.utilidad_3 = ficha.precio.ut3;
                            entPrd.utilidad_4 = ficha.precio.ut4;
                            entPrd.utilidad_pto = ficha.precio.ut5;
                        }
                        cnn.SaveChanges();

                        var entHist = new productos_costos()
                        {
                            auto_producto = ficha.autoProducto,
                            costo = ficha.historia.costo,
                            costo_divisa = ficha.historia.divisa,
                            divisa = ficha.historia.tasaCambio,
                            documento = ficha.historia.documento,
                            estacion = ficha.estacion,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            nota = ficha.historia.nota,
                            serie = ficha.historia.serie,
                            usuario = ficha.nombreUsuario,
                        };
                        cnn.productos_costos.Add(entHist);
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
                foreach (var eve in e.Entries )
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
    }
}