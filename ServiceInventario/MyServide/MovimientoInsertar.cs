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
        public DtoLib.ResultadoEntidad<string> 
            InsertarMovCargo(DtoLibInventario.MovimientoInsertar.Cargo.Ficha ficha)
        {
            return ServiceProv.insertarMovimientoCargo(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            InsertarMovDescargo(DtoLibInventario.MovimientoInsertar.Descargo.Ficha ficha)
        {
            return ServiceProv.insertarMovimientoDescargo(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            InsertarMovTraslado(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha)
        {
            return ServiceProv.insertarMovimientoTraslado(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            insertarMovimientoTrasladoPorDevolucion(DtoLibInventario.MovimientoInsertar.Traslado.Ficha ficha)
        {
            return ServiceProv.insertarMovimientoTrasladoPorDevolucion(ficha);
        }
    }
}