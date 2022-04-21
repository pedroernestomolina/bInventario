using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleInv
{


    class Program
    {

        static void Main(string[] args)
        {
            ILibInventario.IProvider invPrv = new ProvLibInventario.Provider("localhost", "pita");
//            var r01 = invPrv.Producto_GetFicha("0000000450");
        }

    }

}