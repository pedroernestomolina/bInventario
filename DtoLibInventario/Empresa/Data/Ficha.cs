using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Empresa.Data
{
    public class Ficha
    {
        public string Nombre { get; set; }
        public string CiRif { get; set; }
        public string DireccionFiscal { get; set; }
        public string Telefono { get; set; }
        public FichaExtra extra { get; set; }
        public byte[] logo { get; set; }
        public Ficha()
        {
            Nombre = "";
            CiRif = "";
            DireccionFiscal = "";
            Telefono = "";
            logo = new byte[] { };
        }
    }
}