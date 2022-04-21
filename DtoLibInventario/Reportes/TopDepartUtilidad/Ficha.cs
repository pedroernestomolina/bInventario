using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.TopDepartUtilidad
{
    public class Ficha
    {

        public string Departamento { get; set; }
        public decimal cntMov { get; set; }
        public decimal costo { get; set; }
        public decimal venta { get; set; }

        public decimal utilidad { get { return venta - costo; } }


    }

}