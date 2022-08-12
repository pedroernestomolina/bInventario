using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Tool.CambioMasivoPrecio.ActualizarData
{
    
    public class Ficha
    {

        public List<CapturarData.Ficha> data { get; set; }
        public string codPrecioDestino { get; set; }


        public Ficha()
        {
            data = new List<CapturarData.Ficha>(); 
            codPrecioDestino = "";
        }

    }

}