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

        public DtoLib.ResultadoLista<DtoLibInventario.Departamento.Resumen> Departamento_GetLista()
        {
            return ServiceProv.Departamento_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Departamento.Ficha> Departamento_GetFicha(string auto)
        {
            return ServiceProv.Departamento_GetFicha(auto);
        }

        public DtoLib.ResultadoAuto Departamento_Agregar(DtoLibInventario.Departamento.Agregar ficha)
        {
            return ServiceProv.Departamento_Agregar(ficha);
        }

        public DtoLib.Resultado Departamento_Editar(DtoLibInventario.Departamento.Editar ficha)
        {
            return ServiceProv.Departamento_Editar(ficha);
        }

        public DtoLib.Resultado Departamento_Eliminar(string auto)
        {
            return ServiceProv.Departamento_Eliminar(auto);
        }

    }

}