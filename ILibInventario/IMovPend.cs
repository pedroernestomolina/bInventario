using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{


    public interface IMovPend
    {

        DtoLib.ResultadoLista<DtoLibInventario.MovPend.Lista.Ficha> 
            MovPend_GetLista(DtoLibInventario.MovPend.Lista.Filtro filtro);
        DtoLib.Resultado 
            MovPend_Anular(int idMov);

    }

}