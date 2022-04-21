using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Kardex
{

    
    public class Ficha
    {

        public List<Mov> movimientos { get; set; }
        public List<Existencia> exInicial { get; set; }


        public Ficha() 
        {
            movimientos = new List<Mov>();
            exInicial = new List<Existencia>();
        }

    }

}