using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetLista();
        DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> Deposito_GetListaBySucursal(string codSuc);
        DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> Deposito_GetFicha(string autoDep);

    }

}