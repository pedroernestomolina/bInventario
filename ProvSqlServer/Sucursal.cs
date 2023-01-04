using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> 
            Sucursal_GetLista(DtoLibInventario.Sucursal.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen>();
            rt.Lista = new List<DtoLibInventario.Sucursal.Resumen>();
            return rt;
        }
    }
}