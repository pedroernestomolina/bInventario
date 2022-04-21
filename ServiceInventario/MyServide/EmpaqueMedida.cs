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

        public DtoLib.ResultadoLista<DtoLibInventario.EmpaqueMedida.Resumen> EmpaqueMedida_GetLista()
        {
            return ServiceProv.EmpaqueMedida_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.EmpaqueMedida.Ficha> EmpaqueMedida_GetFicha(string auto)
        {
            return ServiceProv.EmpaqueMedida_GetFicha(auto);
        }

        public DtoLib.ResultadoAuto EmpaqueMedida_Agregar(DtoLibInventario.EmpaqueMedida.Agregar ficha)
        {
            return ServiceProv.EmpaqueMedida_Agregar(ficha);
        }

        public DtoLib.Resultado EmpaqueMedida_Editar(DtoLibInventario.EmpaqueMedida.Editar ficha)
        {
            return ServiceProv.EmpaqueMedida_Editar(ficha);
        }

        public DtoLib.Resultado EmpaqueMedida_Eliminar(string auto)
        {
            return ServiceProv.EmpaqueMedida_Eliminar(auto);
        }

    }

}