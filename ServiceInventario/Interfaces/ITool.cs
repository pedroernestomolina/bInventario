using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface ITool
    {

        DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha> Tools_AjusteNivelMinimoMaximo_GetLista(DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro filtro);
        DtoLib.Resultado Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste);

    }

}