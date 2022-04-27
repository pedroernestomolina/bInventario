using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoLista<DtoLibInventario.MovPend.Lista.Ficha> 
            MovPend_GetLista(DtoLibInventario.MovPend.Lista.Filtro filtro)
        {
            return ServiceProv.MovPend_GetLista(filtro);
        }
        public DtoLib.Resultado 
            MovPend_Anular(int idMov)
        {
            return ServiceProv.MovPend_Anular(idMov);
        }
    }

}