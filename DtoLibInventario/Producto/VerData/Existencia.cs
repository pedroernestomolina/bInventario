using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{

    public class Existencia
    {

        public List<Deposito> depositos { get; set; }
        public string decimales { get; set; }
        public string empaqueCompra { get; set; }
        public int empaqueCompraCont { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string descEmpInv { get; set; }
        public int contEmpInv { get; set; }


        public Existencia()
        {
            decimales = "";
            empaqueCompra = "";
            empaqueCompraCont=0;
            codigoPrd="";
            nombrePrd="";
            descEmpInv="";
            contEmpInv=0;
        }

    }

}