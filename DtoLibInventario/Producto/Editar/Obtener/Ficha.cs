using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Editar.Obtener
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoMarca { get; set; }
        public string autoTasaImpuesto { get; set; }
        public string autoEmpCompra { get; set; }

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public int contenidoCompra { get; set; }
        public byte[] imagen { get; set; }
        public Enumerados.EnumPesado esPesado { get; set; }
        public string plu { get; set; }
        public int diasEmpaque { get; set; }

        public Enumerados.EnumCatalogo  activarCatalogo { get; set; }

        public Enumerados.EnumOrigen origen { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }
        public Enumerados.EnumAdministradorPorDivisa AdmPorDivisa { get; set; }
        public Enumerados.EnumClasificacionABC Clasificacion { get; set; }
        public List<FichaAlterno> CodigosAlterno { get; set; }

    }

}