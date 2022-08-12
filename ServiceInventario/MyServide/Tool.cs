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

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha> 
            Tools_AjusteNivelMinimoMaximo_GetLista(DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro filtro)
        {
            return ServiceProv.Tools_AjusteNivelMinimoMaximo_GetLista(filtro);
        }
        public DtoLib.Resultado 
            Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste)
        {
            return ServiceProv.Tools_AjusteNivelMinimoMaximo_Ajustar(listaAjuste);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Ficha> 
            Tools_CambioMasivoPrecio_GetData(DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Filtro filtro)
        {
            return ServiceProv.Tools_CambioMasivoPrecio_GetData(filtro);
        }
        public DtoLib.Resultado 
            Tools_CambioMasivoPrecio_ActualizarData(DtoLibInventario.Tool.CambioMasivoPrecio.ActualizarData.Ficha ficha)
        {
            return ServiceProv.Tools_CambioMasivoPrecio_ActualizarData(ficha);
        }

    }

}