using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    public interface IReportes_ModoAdm
    {
        DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha>
            Reportes_ModAdm_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro);
    }
}