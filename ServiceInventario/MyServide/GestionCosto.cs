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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto.Ficha> 
            Producto_GestionCosto_CapturarDataPrdEditarCosto(string idPrd)
        {
            return ServiceProv.Producto_GestionCosto_CapturarDataPrdEditarCosto(idPrd);
        }
    }
}