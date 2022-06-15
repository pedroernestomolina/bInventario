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

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> 
            Reportes_MaestroProducto(DtoLibInventario.Reportes.MaestroProducto.Filtro filtro)
        {
            return ServiceProv.Reportes_MaestroProducto(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> 
            Reportes_MaestroInventario(DtoLibInventario.Reportes.MaestroInventario.Filtro filtro)
        {
            return ServiceProv.Reportes_MaestroInventario(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> 
            Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro)
        {
            return ServiceProv.Reportes_Top20(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha> 
            Reportes_MaestroExistencia(DtoLibInventario.Reportes.MaestroExistencia.Filtro filtro)
        {
            return ServiceProv.Reportes_MaestroExistencia(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha> 
            Reportes_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            return ServiceProv.Reportes_MaestroPrecio(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Kardex.Ficha> 
            Reportes_Kardex(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            return ServiceProv.Reportes_Kardex(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha> 
            Reportes_CompraVentaAlmacen(DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro filtro)
        {
            return ServiceProv.Reportes_CompraVentaAlmacen(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.DepositoResumen.Ficha> 
            Reportes_DepositoResumen()
        {
            return ServiceProv.Reportes_DepositoResumen();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha> 
            Reportes_NivelMinimo(DtoLibInventario.Reportes.MaestroNivelMinimo.Filtro filtro)
        {
            return ServiceProv.Reportes_NivelMinimo(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Valorizacion.Ficha> 
            Reportes_Valorizacion(DtoLibInventario.Reportes.Valorizacion.Filtro filtro)
        {
            return ServiceProv.Reportes_Valorizacion(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.KardexResumen.Ficha> 
            Reportes_KardexResumen(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            return ServiceProv.Reportes_KardexResumen(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistenciaInventario.Ficha> 
            Reportes_MaestroExistenciaInventario(DtoLibInventario.Reportes.MaestroExistenciaInventario.Filtro filtro)
        {
            return ServiceProv.Reportes_MaestroExistenciaInventario(filtro);
        }

    }

}