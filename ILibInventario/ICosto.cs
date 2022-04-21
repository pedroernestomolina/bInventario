using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface ICosto
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Costo.Historico.Resumen> HistoricoCosto_GetLista(DtoLibInventario.Costo.Historico.Filtro filtro);
        DtoLib.Resultado CostoProducto_Actualizar(DtoLibInventario.Costo.Editar.Ficha ficha );

    }

}