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
            TomaInv_AnalizarToma(string idToma);
        DtoLib.Resultado
            TomaInv_RechazarItemsToma(DtoLibInventario.TomaInv.RechazarItem.Ficha ficha);
        DtoLib.Resultado
            TomaInv_ProcesarToma(DtoLibInventario.TomaInv.Procesar.Ficha ficha);
        //
        DtoLib.Resultado
            TomaInv_GenerarSolicitud(DtoLibInventario.TomaInv.Solicitud.Generar.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            TomaInv_EncontrarSolicitudActiva(string codigoEmpSuc);
        DtoLib.Resultado
            TomaInv_ConvertirSolicitud_EnToma(DtoLibInventario.TomaInv.ConvertirSolicitud.Ficha ficha);
        DtoLib.ResultadoEntidad<string>
            TomaInv_Analizar_TomaDisponible();

        DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Ficha>
            TomaInv_GetLista_PorMovAjuste(DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.Resultado.Ficha>
            TomaInv_GetToma_Resultado(string idToma);
    }
}