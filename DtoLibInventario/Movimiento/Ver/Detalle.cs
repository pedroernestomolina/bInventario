using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Ver
{
    
    public class Detalle
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantidadUnd { get; set; }
        public string empaque { get; set; }
        public int contenido { get; set; }
        public int signo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal importe { get; set; }
        public bool esUnidad { get; set; }
        public string decimales { get; set; }

    }

}