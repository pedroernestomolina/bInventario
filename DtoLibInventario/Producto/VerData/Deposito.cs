using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Deposito
    {

        public string autoId { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal exFisica { get; set; }
        public decimal exReserva { get; set; }
        public decimal exDisponible { get; set; }

    }

}
