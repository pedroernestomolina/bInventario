using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Capturar
{
    public class Filtro
    {
        public string tipoPrecio { get; set; }
        public string tipoEmpaque { get; set; }
        public List<string> listadPrd { get; set; }
    }
}