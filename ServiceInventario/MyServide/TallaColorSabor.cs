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
        public DtoLib.ResultadoEntidad<DtoLibInventario.TallaColorSabor.Existencia.Ficha> 
            TallaColorSabor_ExDep(DtoLibInventario.TallaColorSabor.Existencia.Filtro filtro)
        {
            return ServiceProv.TallaColorSabor_ExDep(filtro);
        }
    }
}