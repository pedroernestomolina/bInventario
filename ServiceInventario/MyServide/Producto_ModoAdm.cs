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
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta> 
            Producto_ModoAdm_GetEmpaqueVenta_By(string autoPrd)
        {
            return ServiceProv.Producto_ModoAdm_GetEmpaqueVenta_By(autoPrd);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha> 
            Producto_ModoAdm_GetPrecio_By(string autoPrd, string tipoPrecio)
        {
            return ServiceProv.Producto_ModoAdm_GetPrecio_By(autoPrd, tipoPrecio);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha> 
            Producto_ModoAdm_GetCosto_By(string autoPrd)
        {
            return ServiceProv.Producto_ModoAdm_GetCosto_By (autoPrd);
        }
        public DtoLib.Resultado 
            Producto_ModoAdm_ActualizarPrecio(DtoLibInventario.Producto.ActualizarPrecio.ModoAdm.Ficha ficha)
        {
            return ServiceProv.Producto_ModoAdm_ActualizarPrecio(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha> 
            Producto_ModoAdm_HistoricoPrecio_By(DtoLibInventario.Producto.HistoricoPrecio.Filtro filtro)
        {
            return ServiceProv.Producto_ModoAdm_HistoricoPrecio_By(filtro);
        }
        public DtoLib.Resultado 
            Producto_ModoAdm_ActualizarOferta(DtoLibInventario.Producto.ActualizarOferta.ModoAdm.Actualizar.Ficha ficha)
        {
            return ServiceProv.Producto_ModoAdm_ActualizarOferta(ficha);
        }
    }
}