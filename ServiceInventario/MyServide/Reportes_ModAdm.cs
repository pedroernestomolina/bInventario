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
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha> 
            Reportes_ModAdm_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            return ServiceProv.Reportes_ModAdm_MaestroPrecio(filtro);
        }
    }
}
