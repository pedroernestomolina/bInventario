using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IAuditoria
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Auditoria.Entidad.Ficha> 
            Auditoria_Documento_GetFichaBy(DtoLibInventario.Auditoria.Buscar.Ficha ficha);

    }

}
