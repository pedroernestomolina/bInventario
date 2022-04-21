using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IMonitorPos
    {

        DtoLib.ResultadoLista<DtoLibInventario.MonitorPos.Entidad.Ficha> MonitorPos_VentaResumen_GetLista(DtoLibInventario.MonitorPos.Lista.Filtro filtro);

    }

}