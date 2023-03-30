using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface IProducto_ModoAdm
    {
        DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta>
            Producto_ModoAdm_GetEmpaqueVenta_By(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha>
            Producto_ModoAdm_GetPrecio_By(string autoPrd, string tipoPrecio);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha>
            Producto_ModoAdm_GetCosto_By(string autoPrd);
        DtoLib.Resultado
            Producto_ModoAdm_ActualizarPrecio(DtoLibInventario.Producto.ActualizarPrecio.ModoAdm.Ficha ficha);
    }
}