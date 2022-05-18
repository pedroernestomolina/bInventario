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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Principal()
        {
            return ServiceProv.Usuario_Principal();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Buscar(DtoLibInventario.Usuario.Buscar.Ficha ficha)
        {
            return ServiceProv.Usuario_Buscar(ficha);
        }
        public DtoLib.Resultado Usuario_ActualizarSesion(DtoLibInventario.Usuario.ActualizarSesion.Ficha ficha)
        {
            return ServiceProv.Usuario_ActualizarSesion(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            Usuario_GetClave_ById(string idUsuario)
        {
            return ServiceProv.Usuario_GetClave_ById(idUsuario);
        }
        public DtoLib.ResultadoEntidad<string> 
            Usuario_GetId_ByClaveUsuGrupoAdm(string clave)
        {
            return ServiceProv.Usuario_GetId_ByClaveUsuGrupoAdm(clave);
        }

    }

}