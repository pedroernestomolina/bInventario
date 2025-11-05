using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface IMovimientoInsertar
    {
        DtoLib.ResultadoEntidad<string>
            insertarMovimientoCargo(DtoLibInventario.MovimientoInsertar.Cargo.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            insertarMovimientoDescargo(DtoLibInventario.MovimientoInsertar.Descargo.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            insertarMovimientoTraslado(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            insertarMovimientoTrasladoPorDevolucion(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha);
    }
}