using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{

    public class Extra
    {

        public string lugar { get; set; }
        public Enumerados.EnumPesado esPesado { get; set; }
        public string plu { get;set; }
        public int diasEmpaque { get; set; }
        public List<string> codigosAlterno { get; set; }
        public byte[] imagen { get; set; }

    }

}