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

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.General.Ficha> Producto_Analisis_General(DtoLibInventario.Analisis.General.Filtro filtro)
        {
            return ServiceProv.Producto_Analisis_General(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Detallado.Ficha> Producto_Analisis_Detallado(DtoLibInventario.Analisis.Detallado.Filtro filtro)
        {
            return ServiceProv.Producto_Analisis_Detallado(filtro);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Existencia.Ficha> Producto_Analisis_Existencia(DtoLibInventario.Analisis.Existencia.Filtro filtro)
        {
            return ServiceProv.Producto_Analisis_Existencia(filtro);
        }

    }

}