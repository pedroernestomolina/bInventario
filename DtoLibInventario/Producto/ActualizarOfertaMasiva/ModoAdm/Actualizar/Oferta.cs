using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Actualizar
{
    public class Oferta
    {
        public string autoPrd { get; set; }
        public string estatusOferta { get; set; }
        public int idPrecioVta { get; set; }
        public string estatus { get; set; }
        public decimal portc { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
    }
}