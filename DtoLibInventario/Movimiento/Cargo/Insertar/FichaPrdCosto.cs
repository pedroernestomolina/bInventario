using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrdCosto
    {

        public string autoProducto { get; set; }
        public decimal costoFinal { get; set; }
        public decimal costoFinalUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal cantidadEntranteUnd { get; set; }
        public decimal importeEntradaUnd { get { return cantidadEntranteUnd * costoFinalUnd; } }


        public FichaPrdCosto() 
        {
            autoProducto = "";
            costoFinal = 0m;
            costoFinalUnd = 0m;
            costoDivisa = 0m;
            cantidadEntranteUnd = 0m;
        }

    }

}