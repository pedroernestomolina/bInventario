using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface ISucursal
    {

        DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> Sucursal_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> Sucursal_GetFicha(string auto);

    }

}