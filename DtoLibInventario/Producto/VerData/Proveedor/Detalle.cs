using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData.Proveedor
{

    public class Detalle
    {

        public string idAuto { get; set; }
        public string codigo { get; set; }
        public string ciRif { get; set; }
        public string razonSocial { get; set; }
        public string direccionFiscal { get; set; }
        public string telefonos { get; set; }
        public string codigoRefPrd { get; set; }


        public Detalle()
        {
            idAuto = "";
            codigo = "";
            ciRif = "";
            razonSocial = "";
            direccionFiscal = "";
            telefonos = "";
            codigoRefPrd = "";
        }

    }

}