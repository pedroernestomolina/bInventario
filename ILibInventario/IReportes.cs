using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IReportes
    {

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> 
            Reportes_MaestroProducto ( DtoLibInventario.Reportes.MaestroProducto.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> 
            Reportes_MaestroInventario (DtoLibInventario.Reportes.MaestroInventario.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha> 
            Reportes_MaestroExistencia(DtoLibInventario.Reportes.MaestroExistencia.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha> 
            Reportes_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Kardex.Ficha> 
            Reportes_Kardex(DtoLibInventario.Reportes.Kardex.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.KardexResumen.Ficha> 
            Reportes_KardexResumen(DtoLibInventario.Reportes.Kardex.Filtro filtro);

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> 
            Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha> 
            Reportes_TopDepartUtilidad(DtoLibInventario.Reportes.TopDepartUtilidad.Filtro filtro);

        DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha> 
            Reportes_CompraVentaAlmacen(DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.DepositoResumen.Ficha> 
            Reportes_DepositoResumen();

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha> 
            Reportes_NivelMinimo(DtoLibInventario.Reportes.MaestroNivelMinimo.Filtro filtro);

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.Valorizacion.Ficha> 
            Reportes_Valorizacion(DtoLibInventario.Reportes.Valorizacion.Filtro filtro);

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistenciaInventario.Ficha>
            Reportes_MaestroExistenciaInventario(DtoLibInventario.Reportes.MaestroExistenciaInventario.Filtro filtro);

    }

}