using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrdCostoHistorico
    {

        public string autoProducto { get; set; }
        public decimal costo { get; set; }
        public decimal divisa { get; set; }
        public decimal tasaCambio { get; set; }
        public string serie { get; set; }
        public string nota { get; set; }


        public FichaPrdCostoHistorico() 
        {
            autoProducto = "";
            costo = 0m;
            divisa = 0m;
            tasaCambio = 0m;
            serie = "";
            nota = "";
        }

    }

}