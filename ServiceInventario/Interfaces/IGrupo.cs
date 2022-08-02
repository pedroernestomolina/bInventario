using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IGrupo
    {

        DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> 
            Grupo_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Grupo.Ficha> 
            Grupo_GetFicha(string auto);
        DtoLib.ResultadoAuto 
            Grupo_Agregar(DtoLibInventario.Grupo.Agregar ficha);
        DtoLib.Resultado 
            Grupo_Editar(DtoLibInventario.Grupo.Editar ficha);
        DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> 
            Grupo_GetListaByDepartamento(string id);
        DtoLib.Resultado 
            Grupo_Eliminar(string auto);

    }

}