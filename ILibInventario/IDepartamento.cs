using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IDepartamento
    {

        DtoLib.ResultadoLista<DtoLibInventario.Departamento.Resumen> Departamento_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Departamento.Ficha> Departamento_GetFicha(string auto);
        DtoLib.ResultadoAuto Departamento_Agregar(DtoLibInventario.Departamento.Agregar ficha);
        DtoLib.Resultado Departamento_Editar(DtoLibInventario.Departamento.Editar ficha);
        DtoLib.Resultado Departamento_Eliminar(string auto);

    }

}