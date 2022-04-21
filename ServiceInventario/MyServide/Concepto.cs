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

        public DtoLib.ResultadoLista<DtoLibInventario.Concepto.Resumen> Concepto_GetLista()
        {
            return ServiceProv.Concepto_GetLista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_GetFicha(string auto)
        {
            return ServiceProv.Concepto_GetFicha (auto);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_PorTraslado()
        {
            return ServiceProv.Concepto_PorTraslado();
        }

        public DtoLib.ResultadoAuto Concepto_Agregar(DtoLibInventario.Concepto.Agregar ficha)
        {
            return ServiceProv.Concepto_Agregar(ficha);
        }

        public DtoLib.Resultado Concepto_Editar(DtoLibInventario.Concepto.Editar ficha)
        {
            return ServiceProv.Concepto_Editar(ficha);
        }

        public DtoLib.Resultado Concepto_Eliminar(string auto)
        {
            return ServiceProv.Concepto_Eliminar(auto);
        }

    }

}