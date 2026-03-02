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
        public DtoLib.ResultadoEntidad<DtoLibInventario.MovimientoRecuperar.Entidad.Ficha> 
            recuperarMovimientoFicha(string autoDoc)
        {
            return ServiceProv.recuperarMovimientoFicha(autoDoc);
        }
    }
}