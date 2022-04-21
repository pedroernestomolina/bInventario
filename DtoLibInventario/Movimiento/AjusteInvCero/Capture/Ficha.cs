using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.AjusteInvCero.Capture
{
    
    public class Ficha
    {

        public List<Data> data { get; set; }


        public Ficha()
        {
            data = new List<Data>();
        }

    }

}