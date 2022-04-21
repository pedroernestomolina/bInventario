using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex
{
    
    public class Enumerados
    {

        public enum EnumModulo { SinDefinir = -1, Inventario = 1, Compra, Venta };
        public enum EnumSiglas { SinDefinir = -1, Factura=1, NCredito, NDebito, NEntrega , Cargo, Descargo, Traslado, Ajuste };
        public enum EnumMovUltDias { SinDefinir = -1, Hoy=1, Ayer, _7Dias, _15Dias, _30Dias, _45Dias, _60Dias, _90Dias, _120Dias, Todo  };

    }

}
