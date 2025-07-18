using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ModuloInventario_Modo()
        {
            return new DtoLib.ResultadoEntidad<string>() { Entidad = "BASICO_FOX" };
        }
    }
}