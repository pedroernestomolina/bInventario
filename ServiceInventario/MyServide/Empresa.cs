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

        public DtoLib.ResultadoLista<DtoLibInventario.Empresa.Grupo.Lista.Ficha>
            EmpresaGrupo_GetLista()
        {
            return ServiceProv.EmpresaGrupo_GetLista();
        }
        public DtoLib.ResultadoEntidad<string> 
            EmpresaGrupo_PrecioManejar_GetById(string idGrupo)
        {
            return ServiceProv.EmpresaGrupo_PrecioManejar_GetById(idGrupo);
        }

    }

}