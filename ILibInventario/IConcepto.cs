using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IConcepto
    {

        DtoLib.ResultadoLista<DtoLibInventario.Concepto.Resumen> Concepto_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_GetFicha(string auto);
        DtoLib.ResultadoAuto Concepto_Agregar(DtoLibInventario.Concepto.Agregar ficha);
        DtoLib.Resultado Concepto_Editar(DtoLibInventario.Concepto.Editar ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_PorTraslado();
        DtoLib.Resultado Concepto_Eliminar(string auto);

    }

}