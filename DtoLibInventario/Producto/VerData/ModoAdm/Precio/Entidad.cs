using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData.ModoAdm.Precio
{
    public class Entidad
    {
        public string auto { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal tasaIva { get; set; }
        public string estatusDivisa { get; set; }
    }
}