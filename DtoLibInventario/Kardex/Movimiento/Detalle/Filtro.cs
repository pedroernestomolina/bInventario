using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex.Movimiento.Detalle
{
    
    public class Filtro: Movimiento.Filtro 
    {

        public string autoDeposito { get; set; }
        public string autoConcepto { get; set; }
        public string modulo { get; set; }

    }

}