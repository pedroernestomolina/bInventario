using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Analisis
{
    public class Ficha
    {
        public string sucursal { get; set; }
        public string deposito { get; set; }
        public string solicitudNro { get; set; }
        public string tomaNro { get; set; }
        public List<Item> Items { get; set; }
    }
}