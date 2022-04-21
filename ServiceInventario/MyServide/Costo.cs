using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{

    public partial class Service: IService
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Costo.Historico.Resumen> HistoricoCosto_GetLista(DtoLibInventario.Costo.Historico.Filtro filtro)
        {
            return ServiceProv.HistoricoCosto_GetLista(filtro);
        }

        public DtoLib.Resultado CostoProducto_Actualizar(DtoLibInventario.Costo.Editar.Ficha ficha)
        {
            return ServiceProv.CostoProducto_Actualizar(ficha);
        }

    }

}