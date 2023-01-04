using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen> 
            Producto_AdmDivisa_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen>();
            var list = new List<DtoLibInventario.Producto.AdmDivisa.Resumen>();
            var nr1 = new DtoLibInventario.Producto.AdmDivisa.Resumen() { Id = 1, Descripcion = "Si" };
            var nr2 = new DtoLibInventario.Producto.AdmDivisa.Resumen() { Id = 2, Descripcion = "No" };
            //list.Add(nr1);
            //list.Add(nr2);
            result.Lista = list;
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen>
            Producto_Categoria_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen>();
            var lst = new List<DtoLibInventario.Producto.Categoria.Resumen>();
            var nr1 = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 1, Descripcion = "Producto Terminado" };
            var nr2 = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 2, Descripcion = "Bien de Servicio" };
            var nr3 = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 3, Descripcion = "Materia Prima" };
            var nr4 = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 4, Descripcion = "Uso Interno" };
            var nr5 = new DtoLibInventario.Producto.Categoria.Resumen() { Id = 5, Descripcion = "Sub Producto" };
            lst.Add(nr1);
            lst.Add(nr2);
            lst.Add(nr3);
            lst.Add(nr4);
            lst.Add(nr5);
            result.Lista = lst;
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen>
            Producto_Origen_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen>();
            var lst = new List<DtoLibInventario.Producto.Origen.Resumen>();
            var nr1 = new DtoLibInventario.Producto.Origen.Resumen() { Id = 1, Descripcion = "Nacional" };
            var nr2 = new DtoLibInventario.Producto.Origen.Resumen() { Id = 2, Descripcion = "Importado" };
            lst.Add(nr1);
            lst.Add(nr2);
            result.Lista = lst;
            return result;
        }
    }
}