using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Precio.Editar
{
    public class Ficha
    {
        public string autoProducto { get; set; }
        public string estacion { get; set; }
        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public FichaPrecio precio_1 { get; set; }
        public FichaPrecio precio_2 { get; set; }
        public FichaPrecio precio_3 { get; set; }
        public FichaPrecio precio_4 { get; set; }
        public FichaPrecio precio_5 { get; set; }
        public FichaPrecio may_1 { get; set; }
        public FichaPrecio may_2 { get; set; }
        public FichaPrecio may_3 { get; set; }
        public FichaPrecio may_4 { get; set; }
        public FichaPrecio dsp_1 { get; set; }
        public FichaPrecio dsp_2 { get; set; }
        public FichaPrecio dsp_3 { get; set; }
        public FichaPrecio dsp_4 { get; set; }
        public List<FichaHistorica> historia { get; set; }
        public List<string> Presentaciones { get; set; }
        //
        public Ficha() 
        {
            autoProducto = "";
            estacion = "";
            autoUsuario = "";
            codigoUsuario = "";
            nombreUsuario = "";
            precio_1 = null;
            precio_2 = null;
            precio_3 = null;
            precio_4 = null;
            precio_5 = null;
            may_1 = null;
            may_2 = null;
            may_3 = null;
            may_4 = null;
            dsp_1 = null;
            dsp_2 = null;
            dsp_3 = null;
            dsp_4 = null;
            historia = null;
            Presentaciones = new List<string>();
        }
    }
}