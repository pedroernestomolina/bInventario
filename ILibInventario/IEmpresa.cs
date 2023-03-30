using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface IEmpresa
    {
        DtoLib.ResultadoLista<DtoLibInventario.Empresa.Grupo.Lista.Ficha>
            EmpresaGrupo_GetLista();
        DtoLib.ResultadoEntidad<string>
            EmpresaGrupo_PrecioManejar_GetById(string idGrupo);
    }
}