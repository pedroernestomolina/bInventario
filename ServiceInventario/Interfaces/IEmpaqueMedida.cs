using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IEmpaqueMedida
    {

        DtoLib.ResultadoLista<DtoLibInventario.EmpaqueMedida.Resumen> 
            EmpaqueMedida_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.EmpaqueMedida.Ficha>
            EmpaqueMedida_GetFicha(string auto);
        DtoLib.ResultadoAuto 
            EmpaqueMedida_Agregar(DtoLibInventario.EmpaqueMedida.Agregar ficha);
        DtoLib.Resultado 
            EmpaqueMedida_Editar(DtoLibInventario.EmpaqueMedida.Editar ficha);
        DtoLib.Resultado 
            EmpaqueMedida_Eliminar(string auto);

    }

}