using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.HistoricoPrecio.ModoAdm
{
    public class Ficha 
    {
        public string prdCodigo { get; set; }
        public string prdDescripcion { get; set; }
        public List<Data> data { get; set; }
    }
}