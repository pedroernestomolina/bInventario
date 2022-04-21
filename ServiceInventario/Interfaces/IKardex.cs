using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IKardex
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Resumen.Ficha> Producto_Kardex_Movimiento_Lista_Resumen(DtoLibInventario.Kardex.Movimiento.Resumen.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Detalle.Ficha> Producto_Kardex_Movimiento_Lista_Detalle(DtoLibInventario.Kardex.Movimiento.Detalle.Filtro filtro);

    }

}