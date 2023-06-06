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
            TomaInv_AnalizarToma(int idToma)
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
        public DtoLib.Resultado 
            TomaInv_ConvertirSolicitud_EnToma(string autoSolicitud)
        {
            return ServiceProv.TomaInv_ConvertirSolicitud_EnToma(autoSolicitud);
        }
    }
}