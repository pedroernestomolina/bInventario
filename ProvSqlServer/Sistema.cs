using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha> 
            Sistema_TipoPreciosDefinidos_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha>();
            var lst = new List<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha>();
            var nr1 = new DtoLibInventario.Sistema.HndPrecios.Lista.Ficha() { id = 1, descripcion = "Precio 1", codigo = "" };
            var nr2 = new DtoLibInventario.Sistema.HndPrecios.Lista.Ficha() { id = 2, descripcion = "Precio 2", codigo = "" };
            lst.Add(nr1);
            lst.Add(nr2);
            result.Lista = lst;
            return result;
        }
    }
}