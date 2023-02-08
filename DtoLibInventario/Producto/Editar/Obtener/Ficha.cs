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
        public string autoEmpInv { get; set; }
        public int contEmpInv { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }
        public decimal alto { get; set; }
        public decimal largo { get; set; }
        public decimal ancho { get; set; }
        //
        public string autoEmpVentaTipo_1 { get; set; }
        public string autoEmpVentaTipo_2 { get; set; }
        public string autoEmpVentaTipo_3 { get; set; }
        public int contEmpVentaTipo_1 { get; set; }
        public int contEmpVentaTipo_2 { get; set; }
        public int contEmpVentaTipo_3 { get; set; }
        //
        public string estatusTallaColorSabor { get; set; }
        public List<TallaColorSabor> tallaColorSabor { get; set; }
    }
}