using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.CapturaMov
{
    
    public class Filtro
    {

        public string idDepOrigen { get; set; }
        public string idDepDestino { get; set; }
        public string idProducto { get; set; }


        public Filtro() 
        {
            idDepOrigen= "";
            idDepDestino= "";
            idProducto = "";
        }

    }

}