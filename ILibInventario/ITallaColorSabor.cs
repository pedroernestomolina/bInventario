using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface ITallaColorSabor
    {
        DtoLib.ResultadoEntidad<DtoLibInventario.TallaColorSabor.Existencia.Ficha>
            TallaColorSabor_ExDep(DtoLibInventario.TallaColorSabor.Existencia.Filtro filtro);
    }
}