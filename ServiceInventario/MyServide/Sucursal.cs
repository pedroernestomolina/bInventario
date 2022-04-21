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

        public DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> Sucursal_GetLista()
        {
            return ServiceProv.Sucursal_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> Sucursal_GetFicha(string auto)
        {
            return  ServiceProv.Sucursal_GetFicha(auto);
        }

    }

}