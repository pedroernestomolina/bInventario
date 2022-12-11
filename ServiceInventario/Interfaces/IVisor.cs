using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IVisor
    {
        DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha> 
            Visor_Existencia(DtoLibInventario.Visor.Existencia.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha>
            Visor_CostoEdad(DtoLibInventario.Visor.CostoEddad.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha> 
            Visor_Traslado(DtoLibInventario.Visor.Traslado.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha> 
            Visor_Ajuste(DtoLibInventario.Visor.Ajuste.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha> 
            Visor_CostoExistencia(DtoLibInventario.Visor.CostoExistencia.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.Ficha> 
            Visor_Precio(DtoLibInventario.Visor.Precio.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Visor.PrecioAjuste.Ficha>
            Visor_PrecioAjuste(DtoLibInventario.Visor.PrecioAjuste.Filtro filtro);
        //
        DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.SoloReporte.Ficha>
            Visor_Precio_Modo_SoloReporte(DtoLibInventario.Visor.Precio.SoloReporte.Filtro filtro);
    }

}