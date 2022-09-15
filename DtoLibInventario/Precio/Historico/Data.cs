using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Precio.Historico
{
    
    public class Data
    {

        public string nota { get; set; }
        public DateTime fecha { get; set; }
        public string estacion { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public decimal precio { get; set; }
        public string empaque { get; set; }
        public int contenido { get; set; }
        public decimal factor_cambio { get; set; }
        public string precio_id { get; set; }

    }

}