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

        public DtoLib.ResultadoId 
            Transito_Movimiento_Agregar(DtoLibInventario.Transito.Movimiento.Agregar.Ficha ficha)
        {
            return ServiceProv.Transito_Movimiento_Agregar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha> 
            Transito_Movimiento_GetById(int idMov)
        {
            return ServiceProv.Transito_Movimiento_GetById(idMov);
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha> 
            Transito_Movimiento_GetLista(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            return ServiceProv.Transito_Movimiento_GetLista(filtro);
        }
        public DtoLib.Resultado 
            Transito_Movimiento_AnularById(int idMov)
        {
            return ServiceProv.Transito_Movimiento_AnularById(idMov);
        }
        public DtoLib.ResultadoEntidad<int> 
            Transito_Movimiento_GetCnt(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            return ServiceProv.Transito_Movimiento_GetCnt(filtro);
        }

    }

}