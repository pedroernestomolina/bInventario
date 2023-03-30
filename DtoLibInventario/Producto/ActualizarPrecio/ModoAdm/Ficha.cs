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
        public string estacion { get; set; }
        public string nombreUsuario { get; set; }
        public List<Precio> precios { get; set; }
        public List<Historia> historia { get; set; }
    }
}