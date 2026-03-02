using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface IMovimientoRecuperar
    {
        DtoLib.ResultadoEntidad<DtoLibInventario.MovimientoRecuperar.Entidad.Ficha> 
            recuperarMovimientoFicha(string autoDoc);
    }
}