using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    public interface IGestionCosto
    {
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto.Ficha>
            Producto_GestionCosto_CapturarDataPrdEditarCosto(string idPrd);
    }
}