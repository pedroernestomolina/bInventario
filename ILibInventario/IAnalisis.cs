using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IAnalisis
    {

        DtoLib.ResultadoLista<DtoLibInventario.Analisis.General.Ficha> Producto_Analisis_General(DtoLibInventario.Analisis.General.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Analisis.Detallado.Ficha> Producto_Analisis_Detallado(DtoLibInventario.Analisis.Detallado.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Analisis.Existencia.Ficha> Producto_Analisis_Existencia(DtoLibInventario.Analisis.Existencia.Filtro filtro);

    }

}