using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Editar.Actualizar
{
    public class TallaColorSabor
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Enumerados.EnumAccionTallaColorSabor  Accion { get; set; }
    }
}