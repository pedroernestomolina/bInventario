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

        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetLista()
        {
            return ServiceProv.Deposito_GetLista();
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> Deposito_GetFicha(string autoDep)
        {
            return ServiceProv.Deposito_GetFicha(autoDep);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetListaBySucursal(string codSuc)
        {
            return ServiceProv.Deposito_GetListaBySucursal(codSuc);
        }

    }

}