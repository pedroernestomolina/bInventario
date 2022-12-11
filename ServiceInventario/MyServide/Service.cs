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

        public static ILibInventario.IProvider ServiceProv;


        public Service(string instancia, string bd, string usu="root")
        {
            ServiceProv = new ProvLibInventario.Provider(instancia, bd, usu);
        }


        public DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor()
        {
            return ServiceProv.FechaServidor();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> 
            Empresa_Datos()
        {
            return ServiceProv.Empresa_Datos();
        }
        public DtoLib.ResultadoEntidad<string> 
            Empresa_Sucursal_TipoPrecioManejar(string codEmpresa)
        {
            return ServiceProv.Empresa_Sucursal_TipoPrecioManejar(codEmpresa);
        }
        //public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD()
        //{
        //    throw new NotImplementedException();
        //    //return ServiceProv.InformacionBD();
        //}

    }

}