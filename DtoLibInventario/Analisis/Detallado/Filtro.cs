using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Analisis.Detallado
{
    
    public class Filtro
    {

        public int ultimosXDias { get; set; }
        public string autoDeposito { get; set; }
        public string autoProducto { get; set; }
        public string autoConcepto { get; set; }
        public Enumerados.EnumModulo modulo { get; set; }


        public Filtro()
        {
            ultimosXDias = 0;
            autoDeposito = "";
            autoConcepto = "";
            autoProducto = ""; 
            modulo = Enumerados.EnumModulo.SnDefinir;
        }

    }

}