using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibInventario.TallaColorSabor.Existencia.Ficha> 
            TallaColorSabor_ExDep(DtoLibInventario.TallaColorSabor.Existencia.Filtro filtro)
        {
            throw new NotImplementedException();
        }
    }
}