using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar
{
    public abstract class BaseFicha
    {
        public Encabezado movEncabezado { get; set; }
        public List<Detalle> movDetalles { get; set; }
        public List<Kardex> movKardex { get; set; }
        public BaseFicha()
        {
            movEncabezado = new Encabezado();
            movDetalles = new List<Detalle>();
            movKardex = new List<Kardex>();
        }
    }
}