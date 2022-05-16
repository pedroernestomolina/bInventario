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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen> 
            HistoricoPrecio_GetLista(DtoLibInventario.Precio.Historico.Filtro filtro)
        {
            return ServiceProv.HistoricoPrecio_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha> 
            PrecioCosto_GetFicha(string autoPrd)
        {
            return ServiceProv.PrecioCosto_GetFicha(autoPrd);
        }
        public DtoLib.Resultado
            PrecioProducto_Actualizar(DtoLibInventario.Precio.Editar.Ficha ficha)
        {
            return ServiceProv.PrecioProducto_Actualizar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.PrecioCosto.Entidad.Ficha> 
            PrecioCosto_GetData(string autoPrd)
        {
            return ServiceProv.PrecioCosto_GetData(autoPrd);
        }

    }

}