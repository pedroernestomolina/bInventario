using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarOferta.ModoAdm.Actualizar
{
    public class Precio
    {
        public int idPrecioVta { get; set; }
        public string estatus { get; set; }
        public decimal portc { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
    }
}