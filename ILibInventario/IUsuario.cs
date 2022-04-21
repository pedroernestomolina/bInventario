using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Principal();
        DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Buscar(DtoLibInventario.Usuario.Buscar.Ficha ficha);
        DtoLib.Resultado Usuario_ActualizarSesion(DtoLibInventario.Usuario.ActualizarSesion.Ficha ficha);
        DtoLib.ResultadoEntidad<string> Usuario_GetClave_ById(string idUsuario);

    }

}