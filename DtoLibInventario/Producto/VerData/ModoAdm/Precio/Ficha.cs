using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData.ModoAdm.Precio
{
    public class Ficha
    {
        public Entidad entidad { get; set; }
        public List<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Precio> precios { get; set; }
    }
}