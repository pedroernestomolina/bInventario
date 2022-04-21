using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IMarca
    {

        DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen> Marca_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Marca.Ficha> Marca_GetFicha(string auto);
        DtoLib.ResultadoAuto Marca_Agregar(DtoLibInventario.Marca.Agregar ficha);
        DtoLib.Resultado Marca_Editar(DtoLibInventario.Marca.Editar ficha);
        DtoLib.Resultado Marca_Eliminar(string auto);

    }

}