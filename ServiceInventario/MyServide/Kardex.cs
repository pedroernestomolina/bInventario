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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Resumen.Ficha> Producto_Kardex_Movimiento_Lista_Resumen(DtoLibInventario.Kardex.Movimiento.Resumen.Filtro filtro)
        {
            return ServiceProv.Producto_Kardex_Movimiento_Lista_Resumen(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Detalle.Ficha > Producto_Kardex_Movimiento_Lista_Detalle(DtoLibInventario.Kardex.Movimiento.Detalle.Filtro filtro)
        {
            return ServiceProv.Producto_Kardex_Movimiento_Lista_Detalle(filtro);
        }

    }

}