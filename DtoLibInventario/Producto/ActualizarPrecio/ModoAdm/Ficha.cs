using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarPrecio.ModoAdm
{
    public class Ficha
    {
        public string autoPrd { get; set; }
        public string prdCodigo { get; set; }
        public string prdDesc { get; set; }
        public string estacion { get; set; }
        public string usuNombre { get; set; }
        public string usuCodigo { get; set; }
        public decimal factorCambio { get; set; }
        public string nota { get; set; }
        public List<Precio> precios { get; set; }
        public List<Historia> historia { get; set; }
    }
}