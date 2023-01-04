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


        public Service(string instancia, string bd, string usu, string gestorDatos)
        {
            switch (gestorDatos.Trim().ToUpper())
            {
                case "SQLSERVER":
                    ServiceProv = new ProvSqlServer.Provider(instancia, bd);
                    break;
                default:
                    if (usu.Trim() == "")
                    { ServiceProv = new ProvLibInventario.Provider(instancia, bd); }
                    else
                    { ServiceProv = new ProvLibInventario.Provider(instancia, bd, usu); }
                    break;
            }
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

    }

}