using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.ObtenerToma.Ficha> 
            TomaInv_GetListaPrd(DtoLibInventario.TomaInv.ObtenerToma.Filtro filtro)
        {
            return ServiceProv.TomaInv_GetListaPrd(filtro);
        }
        public DtoLib.Resultado 
            TomaInv_GenerarToma(DtoLibInventario.TomaInv.GenerarToma.Ficha ficha)
        {
            return ServiceProv.TomaInv_GenerarToma(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha> 
            TomaInv_AnalizarToma(string idToma)
        {
            return ServiceProv.TomaInv_AnalizarToma(idToma);
        }
        public DtoLib.Resultado 
            TomaInv_RechazarItemsToma(DtoLibInventario.TomaInv.RechazarItem.Ficha ficha)
        {
            return ServiceProv.TomaInv_RechazarItemsToma(ficha);
        }
        public DtoLib.Resultado 
            TomaInv_ProcesarToma(DtoLibInventario.TomaInv.Procesar.Ficha ficha)
        {
            return ServiceProv.TomaInv_ProcesarToma(ficha);
        }
        //
        public DtoLib.Resultado 
            TomaInv_GenerarSolicitud(DtoLibInventario.TomaInv.Solicitud.Generar.Ficha ficha)
        {
            return ServiceProv.TomaInv_GenerarSolicitud(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            TomaInv_EncontrarSolicitudActiva(string codigoEmpSuc)
        {
            return ServiceProv.TomaInv_EncontrarSolicitudActiva(codigoEmpSuc);
        }
        public DtoLib.Resultado
            TomaInv_ConvertirSolicitud_EnToma(DtoLibInventario.TomaInv.ConvertirSolicitud.Ficha ficha)
        {
            return ServiceProv.TomaInv_ConvertirSolicitud_EnToma(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            TomaInv_Analizar_TomaDisponible()
        {
            return ServiceProv.TomaInv_Analizar_TomaDisponible();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Ficha> 
            TomaInv_GetLista_PorMovAjuste(DtoLibInventario.TomaInv.Resumen.PorMovAjuste.Filtro filtro)
        {
            return ServiceProv.TomaInv_GetLista_PorMovAjuste(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.Resumen.Resultado.Ficha> 
            TomaInv_GetToma_Resultado(string idToma)
        {
            return ServiceProv.TomaInv_GetToma_Resultado(idToma);
        }


        public DtoLib.ResultadoEntidad<string> 
            TomaInv_AnalizarToma_GetMotivo(DtoLibInventario.TomaInv.Analisis.Motivo.Obtener.Ficha ficha)
        {
            return ServiceProv.TomaInv_AnalizarToma_GetMotivo(ficha);
        }
        public DtoLib.Resultado 
            TomaInv_AnalizarToma_SetMotivo(DtoLibInventario.TomaInv.Analisis.Motivo.Cambiar.Ficha ficha)
        {
            return ServiceProv.TomaInv_AnalizarToma_SetMotivo(ficha);
        }
    }
}