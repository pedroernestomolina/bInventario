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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Auditoria.Entidad.Ficha> Auditoria_Documento_GetFichaBy(DtoLibInventario.Auditoria.Buscar.Ficha ficha)
        {
            return ServiceProv.Auditoria_Documento_GetFichaBy(ficha);
        }

    }

}