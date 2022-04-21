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

        public DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen> Marca_GetLista()
        {
            return ServiceProv.Marca_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Marca.Ficha> Marca_GetFicha(string auto)
        {
            return ServiceProv.Marca_GetFicha(auto);
        }

        public DtoLib.ResultadoAuto Marca_Agregar(DtoLibInventario.Marca.Agregar ficha)
        {
            return ServiceProv.Marca_Agregar(ficha);
        }

        public DtoLib.Resultado Marca_Editar(DtoLibInventario.Marca.Editar ficha)
        {
            return ServiceProv.Marca_Editar(ficha);
        }

        public DtoLib.Resultado Marca_Eliminar(string auto)
        {
            return ServiceProv.Marca_Eliminar(auto);
        }

    }

}