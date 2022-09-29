using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface ITasaImpuesto
    {

        DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetLista();
        DtoLib.ResultadoEntidad<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetById(string id);

    }

}