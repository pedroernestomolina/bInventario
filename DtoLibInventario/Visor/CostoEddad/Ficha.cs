using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.CostoEddad
{
    
    public class Ficha
    {

        public DateTime fechaServidor { get; set; }
        public List<FichaDetalle> detalles { get; set; }

    }

}