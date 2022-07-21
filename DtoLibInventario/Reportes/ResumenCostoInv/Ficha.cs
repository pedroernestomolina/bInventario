using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.ResumenCostoInv
{
    
    public class Ficha
    {


        public List<PorInventario> enInventario { get; set; }
        public List<PorMovInventario> enMovInv { get; set; }
        public List<PorCompras> enCompras { get; set; }
        public List<PorVentas> enVentas{ get; set; }


        public Ficha() 
        {
            enInventario = new List<PorInventario>();
            enMovInv = new List<PorMovInventario>();
            enCompras = new List<PorCompras>();
            enVentas = new List<PorVentas>();
        }

    }

}