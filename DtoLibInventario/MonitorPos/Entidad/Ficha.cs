using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MonitorPos.Entidad
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public decimal cnt { get; set; }


        public Ficha() 
        {
            autoProducto = "";
            cnt = 0.0m;
        }

    }

}