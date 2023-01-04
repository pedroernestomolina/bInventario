using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{

    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ToolInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Reportes(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301000000");
        }
    }

}