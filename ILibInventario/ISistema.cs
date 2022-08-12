using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface ISistema
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Sistema.TipoDocumento.Entidad.Ficha> 
            Sistema_TipoDocumento_GetFichaById(string id);
        DtoLib.ResultadoLista<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha>
            Sistema_TipoPreciosDefinidos_Lista();

    }

}