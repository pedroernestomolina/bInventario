using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrecioMargen
    {

        public decimal utilidad { get; set; }


        public FichaPrecioMargen() 
        {
            utilidad = 0m;
        }

    }

}