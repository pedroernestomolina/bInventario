using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface ISucursal
    {
        DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> 
            Sucursal_GetLista(DtoLibInventario.Sucursal.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> 
            Sucursal_GetFicha(string auto);
    }
}