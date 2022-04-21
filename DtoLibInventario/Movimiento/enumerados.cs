using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento
{

    public class enumerados
    {

        public enum EnumTipoDocumento { SinDefinir = -1, Cargo = 1, Descargo, Traslado, Ajuste };
        public enum EnumEstatus { SinDefinir = -1, Activo = 1, Anulado };

    }

}