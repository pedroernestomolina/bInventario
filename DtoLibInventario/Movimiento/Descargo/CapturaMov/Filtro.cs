using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.DesCargo.CapturaMov
{
    
    public class Filtro
    {

        public string idDeposito { get; set; }
        public string idProducto { get; set; }


        public Filtro() 
        {
            idDeposito = "";
            idProducto = "";
        }

    }

}