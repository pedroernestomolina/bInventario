using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    public interface ITomaInv
    {
        DtoLib.ResultadoLista<DtoLibInventario.TomaInv.ObtenerToma.Ficha>
            TomaInv_GetListaPrd(DtoLibInventario.TomaInv.ObtenerToma.Filtro filtro);
        DtoLib.Resultado
            TomaInv_GenerarToma(DtoLibInventario.TomaInv.GenerarToma.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha>
            TomaInv_AnalizarToma(int idToma);
        DtoLib.Resultado
            TomaInv_RechazarItemsToma(DtoLibInventario.TomaInv.RechazarItem.Ficha ficha);
        DtoLib.Resultado
            TomaInv_ProcesarToma(DtoLibInventario.TomaInv.Procesar.Ficha ficha);
    }
}