using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }

    }

}