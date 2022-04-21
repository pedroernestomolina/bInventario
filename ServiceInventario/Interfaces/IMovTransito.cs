using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IMovTransito
    {

        DtoLib.ResultadoId Transito_Movimiento_Agregar(DtoLibInventario.Transito.Movimiento.Agregar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha>
            Transito_Movimiento_GetById(int idMov);
        DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha>
            Transito_Movimiento_GetLista(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro);
        DtoLib.Resultado Transito_Movimiento_AnularById(int idMov);
        DtoLib.ResultadoEntidad<int> Transito_Movimiento_GetCnt(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro);

    }

}