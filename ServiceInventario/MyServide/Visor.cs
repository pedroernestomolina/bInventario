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

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha> Visor_Existencia(DtoLibInventario.Visor.Existencia.Filtro filtro)
        {
            return ServiceProv.Visor_Existencia(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha> Visor_CostoEdad(DtoLibInventario.Visor.CostoEddad.Filtro filtro)
        {
            return ServiceProv.Visor_CostoEdad(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha> Visor_Traslado(DtoLibInventario.Visor.Traslado.Filtro filtro)
        {
            return ServiceProv.Visor_Traslado(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha> Visor_Ajuste(DtoLibInventario.Visor.Ajuste.Filtro filtro)
        {
            return ServiceProv.Visor_Ajuste(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha> Visor_CostoExistencia(DtoLibInventario.Visor.CostoExistencia.Filtro filtro)
        {
            return ServiceProv.Visor_CostoExistencia(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.Ficha> Visor_Precio(DtoLibInventario.Visor.Precio.Filtro filtro)
        {
            return ServiceProv.Visor_Precio(filtro);
        }

    }

}