using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    public interface IMovimientoInsertar
    {
        DtoLib.ResultadoEntidad<string> 
            InsertarMovCargo(DtoLibInventario.MovimientoInsertar.Cargo.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            InsertarMovDescargo(DtoLibInventario.MovimientoInsertar.Descargo.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            InsertarMovTraslado(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            insertarMovimientoTrasladoPorDevolucion(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha);
    }
}