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

        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetLista()
        {
            return ServiceProv.Grupo_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Grupo.Ficha> Grupo_GetFicha(string auto)
        {
            return ServiceProv.Grupo_GetFicha (auto);
        }

        public DtoLib.ResultadoAuto Grupo_Agregar(DtoLibInventario.Grupo.Agregar ficha)
        {
            return ServiceProv.Grupo_Agregar (ficha);
        }

        public DtoLib.Resultado Grupo_Editar(DtoLibInventario.Grupo.Editar ficha)
        {
            return ServiceProv.Grupo_Editar(ficha);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetListaByDepartamento(string id)
        {
            return ServiceProv.Grupo_GetListaByDepartamento(id);
        }

        public DtoLib.Resultado Grupo_Eliminar(string auto)
        {
            return ServiceProv.Grupo_Eliminar(auto);
        }

    }

}