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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> Configuracion_PreferenciaBusqueda()
        {
            return ServiceProv.Configuracion_PreferenciaBusqueda();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> Configuracion_MetodoCalculoUtilidad()
        {
            return ServiceProv.Configuracion_MetodoCalculoUtilidad();
        }
        public DtoLib.ResultadoEntidad<string> Configuracion_TasaCambioActual()
        {
            return ServiceProv.Configuracion_TasaCambioActual();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta()
        {
            return ServiceProv.Configuracion_ForzarRedondeoPrecioVenta();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio()
        {
            return ServiceProv.Configuracion_PreferenciaRegistroPrecio();
        }
        public DtoLib.ResultadoEntidad<int> Configuracion_CostoEdadProducto()
        {
            return ServiceProv.Configuracion_CostoEdadProducto();
        }
        public DtoLib.ResultadoEntidad<string> Configuracion_VisualizarProductosInactivos()
        {
            return ServiceProv.Configuracion_VisualizarProductosInactivos();
        }
        public DtoLib.ResultadoEntidad<string> Configuracion_CantDocVisualizar()
        {
            return ServiceProv.Configuracion_CantDocVisualizar();
        }

        public DtoLib.Resultado Configuracion_SetCostoEdadProducto(DtoLibInventario.Configuracion.CostoEdad.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetCostoEdadProducto(ficha);
        }
        public DtoLib.Resultado Configuracion_SetRedondeoPrecioVenta(DtoLibInventario.Configuracion.RedondeoPrecio.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetRedondeoPrecioVenta(ficha);
        }
        public DtoLib.Resultado Configuracion_SetPreferenciaRegistroPrecio(DtoLibInventario.Configuracion.PreferenciaPrecio.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetPreferenciaRegistroPrecio(ficha);
        }
        public DtoLib.Resultado Configuracion_SetMetodoCalculoUtilidad(DtoLibInventario.Configuracion.MetodoCalculoUtilidad.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetMetodoCalculoUtilidad(ficha);
        }
        public DtoLib.Resultado Configuracion_SetBusquedaPredeterminada(DtoLibInventario.Configuracion.BusquedaPredeterminada.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetBusquedaPredeterminada(ficha);
        }
        public DtoLib.Resultado Configuracion_SetDepositosPreDeterminado(DtoLibInventario.Configuracion.DepositoPredeterminado.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetDepositosPreDeterminado(ficha);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData.Ficha> Configuracion_MetodoCalculoUtilidad_CapturarData()
        {
            return ServiceProv.Configuracion_MetodoCalculoUtilidad_CapturarData();
        }
        public DtoLib.ResultadoEntidad<string> Configuracion_HabilitarPrecio_5_ParaVentaMayorPos()
        {
            return ServiceProv.Configuracion_HabilitarPrecio_5_ParaVentaMayorPos();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.DepositoConceptoDev.Captura.Ficha> Configuracion_DepositoConceptoPreDeterminadoParaDevolucion()
        {
            return ServiceProv.Configuracion_DepositoConceptoPreDeterminadoParaDevolucion ();
        }
        public DtoLib.Resultado Configuracion_SetDepositoConceptoPreDeterminadoParaDevolucion(DtoLibInventario.Configuracion.DepositoConceptoDev.Editar.Ficha ficha)
        {
            return ServiceProv.Configuracion_SetDepositoConceptoPreDeterminadoParaDevolucion(ficha);
        }

    }

}