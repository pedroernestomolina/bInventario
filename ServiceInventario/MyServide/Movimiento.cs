using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{

    public partial class Service: IService
    {


        //INSERTAR
        public DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen, 
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            var rt1 = ServiceProv.Configuracion_CostoEdadProducto();
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rtx = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rt1.Mensaje,
                    Result = rt1.Result,
                };
                return rtx;
            }
            var costoEdad = rt1.Entidad;

            var lista = new List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha>();
            foreach (var rg in ficha.movDeposito.Where(w => w.cantidadUnd < 0).ToList())
            {
                lista.Add(new DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha()
                {
                    autoProducto = rg.autoProducto,
                    autoDeposito = rg.autoDeposito,
                    cantidadUnd = Math.Abs(rg.cantidadUnd),
                });
            }
            var rt = ServiceProv.Producto_Movimiento_Verificar_ExistenciaDisponible(lista);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError || rt.Entidad == false)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            if (costoEdad > 0)
            {
                var listaCostoEdad = new List<DtoLibInventario.Movimiento.Verificar.CostoEdad.FichaDetalle>();
                foreach (var rg in ficha.movDeposito.Where(w => w.cantidadUnd < 0).ToList())
                {
                    listaCostoEdad.Add(new DtoLibInventario.Movimiento.Verificar.CostoEdad.FichaDetalle()
                    {
                        autoProducto = rg.autoProducto,
                    });
                }
                var fichaCostoEdad = new DtoLibInventario.Movimiento.Verificar.CostoEdad.Ficha()
                {
                    detalles = listaCostoEdad,
                    dias = costoEdad,
                };

                if (listaCostoEdad.Count > 0)
                {
                    var rt2 = ServiceProv.Producto_Movimiento_Verificar_CostoEdad(fichaCostoEdad);
                    if (rt2.Result == DtoLib.Enumerados.EnumResult.isError || rt2.Entidad == false)
                    {
                        var rte = new DtoLib.ResultadoAuto()
                        {
                            Auto = "",
                            Mensaje = rt2.Mensaje,
                            Result = DtoLib.Enumerados.EnumResult.isError,
                        };
                        return rte;
                    }
                }
            }

            return ServiceProv.Producto_Movimiento_Ajuste_Insertar(ficha);
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen,
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            var lista = new List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha>();
            foreach (var rg in ficha.prdDeposito)
            {
                lista.Add(new DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha()
                {
                    autoProducto = rg.autoProducto,
                    autoDeposito = rg.autoDeposito,
                    cantidadUnd = rg.cantidadUnd,
                });
            }
            var rt = ServiceProv.Producto_Movimiento_Verificar_ExistenciaDisponible(lista);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError || rt.Entidad == false)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }
            return ServiceProv.Producto_Movimiento_Traslado_Insertar(ficha);
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Devolucion_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen,
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            var lista = new List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha>();
            foreach (var rg in ficha.prdDeposito)
            {
                lista.Add(new DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha()
                {
                    autoProducto = rg.autoProducto,
                    autoDeposito = rg.autoDeposito,
                    cantidadUnd = rg.cantidadUnd,
                });
            }
            var rt = ServiceProv.Producto_Movimiento_Verificar_ExistenciaDisponible(lista);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError || rt.Entidad == false)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }
            return ServiceProv.Producto_Movimiento_Traslado_Devolucion_Insertar(ficha);
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen,
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            var lista = new List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha>();
            foreach (var rg in ficha.movDeposito)
            {
                lista.Add(new DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha()
                {
                    autoProducto = rg.autoProducto,
                    autoDeposito = rg.autoDeposito,
                    cantidadUnd = rg.cantidadUnd,
                });
            }
            var rt = ServiceProv.Producto_Movimiento_Verificar_ExistenciaDisponible(lista);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError || rt.Entidad == false)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }
            return ServiceProv.Producto_Movimiento_DesCargo_Insertar(ficha);
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen,
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            return ServiceProv.Producto_Movimiento_Cargo_Insertar(ficha);
        }
        public DtoLib.ResultadoAuto Producto_Movimiento_AjusteInvCero_Insertar(DtoLibInventario.Movimiento.AjusteInvCero.Insertar.Ficha ficha)
        {
            var rv1 = ServiceProv.Producto_Movimiento_Verificar_DepositoSucursalActivo(
                ficha.mov.autoDepositoOrigen,
                ficha.mov.autoDepositoDestino,
                ficha.mov.codigoSucursal);
            if (rv1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rv1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            var rt1 = ServiceProv.Configuracion_CostoEdadProducto();
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rtx = new DtoLib.ResultadoAuto()
                {
                    Mensaje = rt1.Mensaje,
                    Result = rt1.Result,
                };
                return rtx;
            }
            var costoEdad = rt1.Entidad;

            var lista = new List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha>();
            foreach (var rg in ficha.movDeposito.Where(w => w.cantidadUnd < 0).ToList())
            {
                lista.Add(new DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha()
                {
                    autoProducto = rg.autoProducto,
                    autoDeposito = rg.autoDeposito,
                    cantidadUnd = Math.Abs(rg.cantidadUnd),
                });
            }
            var rt = ServiceProv.Producto_Movimiento_Verificar_ExistenciaDisponible(lista);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError || rt.Entidad == false)
            {
                var rte = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rte;
            }

            if (costoEdad > 0)
            {
                var listaCostoEdad = new List<DtoLibInventario.Movimiento.Verificar.CostoEdad.FichaDetalle>();
                foreach (var rg in ficha.movDeposito.Where(w => w.cantidadUnd < 0).ToList())
                {
                    listaCostoEdad.Add(new DtoLibInventario.Movimiento.Verificar.CostoEdad.FichaDetalle()
                    {
                        autoProducto = rg.autoProducto,
                    });
                }
                var fichaCostoEdad = new DtoLibInventario.Movimiento.Verificar.CostoEdad.Ficha()
                {
                    detalles = listaCostoEdad,
                    dias = costoEdad,
                };

                if (listaCostoEdad.Count > 0)
                {
                    var rt2 = ServiceProv.Producto_Movimiento_Verificar_CostoEdad(fichaCostoEdad);
                    if (rt2.Result == DtoLib.Enumerados.EnumResult.isError || rt2.Entidad == false)
                    {
                        var rte = new DtoLib.ResultadoAuto()
                        {
                            Auto = "",
                            Mensaje = rt2.Mensaje,
                            Result = DtoLib.Enumerados.EnumResult.isError,
                        };
                        return rte;
                    }
                }
            }

            return ServiceProv.Producto_Movimiento_AjusteInvCero_Insertar(ficha);
        }


        //GET
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> Producto_Movimiento_GetFicha(string autoDoc)
        {
            return ServiceProv.Producto_Movimiento_GetFicha(autoDoc);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen> Producto_Movimiento_GetLista(DtoLibInventario.Movimiento.Lista.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha> Producto_Movimiento_AjusteInventarioCero_Capture(DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_AjusteInventarioCero_Capture(filtro);
        }


        //ANULAR
        public DtoLib.Resultado Producto_Movimiento_Cargo_Anular(DtoLibInventario.Movimiento.Anular.Cargo.Ficha ficha)
        {
            var rt1 = ServiceProv.Producto_Movimiento_Verificar_AnularDocumento(ficha.autoDocumento);
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Mensaje = rt1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rt;
            }

            return ServiceProv.Producto_Movimiento_Cargo_Anular(ficha);
        }
        public DtoLib.Resultado Producto_Movimiento_Descargo_Anular(DtoLibInventario.Movimiento.Anular.Descargo.Ficha ficha)
        {
            var rt1 = ServiceProv.Producto_Movimiento_Verificar_AnularDocumento(ficha.autoDocumento);
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Mensaje = rt1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rt;
            }

            return ServiceProv.Producto_Movimiento_Descargo_Anular(ficha);
        }
        public DtoLib.Resultado Producto_Movimiento_Traslado_Anular(DtoLibInventario.Movimiento.Anular.Traslado.Ficha ficha)
        {
            var rt1 = ServiceProv.Producto_Movimiento_Verificar_AnularDocumento (ficha.autoDocumento);
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError) 
            {
                var rt = new DtoLib.Resultado()
                {
                    Mensaje = rt1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rt;
            }

            return ServiceProv.Producto_Movimiento_Traslado_Anular(ficha);
        }
        public DtoLib.Resultado Producto_Movimiento_Ajuste_Anular(DtoLibInventario.Movimiento.Anular.Ajuste.Ficha ficha)
        {
            var rt1 = ServiceProv.Producto_Movimiento_Verificar_AnularDocumento(ficha.autoDocumento);
            if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.Resultado()
                {
                    Mensaje = rt1.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rt;
            }

            return ServiceProv.Producto_Movimiento_Ajuste_Anular(ficha);
        }


        //
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha> Capturar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro filtro)
        {
            return ServiceProv.Capturar_ProductosPorDebajoNivelMinimo(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo> Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(filtro);
        }


        //CAPTURAR DATA PARA MOVIMIENTO TIPO DESCARGO
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha> 
            Producto_Movimiento_Descargo_Capture(DtoLibInventario.Movimiento.DesCargo.CapturaMov.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_Descargo_Capture(filtro);
        }


        //CAPTURAR DATA PARA MOVIMIENTO TIPO DESCARGO
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha> Producto_Movimiento_Cargo_Capture(DtoLibInventario.Movimiento.Cargo.CapturaMov.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_Cargo_Capture(filtro);
        }

        //CAPTURAR DATA PARA MOVIMIENTO TIPO TRASLADO
        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha> Producto_Movimiento_Traslado_Capture(DtoLibInventario.Movimiento.Traslado.CapturaMov.Filtro filtro)
        {
            return ServiceProv.Producto_Movimiento_Traslado_Capture(filtro);
        }

    }

}