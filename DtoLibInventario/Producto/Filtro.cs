using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto
{
    public class Filtro
    {
        public enum Existencia { SinDefinir = -1, MayorQueCero = 1, IgualCero, MenorQueCero };

        public string cadena { get; set; }
        public string autoProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoDeposito { get; set; }
        public string autoTasa { get; set; }
        public string autoProveedor { get; set; }
        public string autoMarca { get; set; }
        public Enumerados.EnumOrigen origen { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }
        public Enumerados.EnumAdministradorPorDivisa admPorDivisa { get; set; }
        public Enumerados.EnumPesado pesado { get; set; }
        public Enumerados.EnumCatalogo catalogo { get; set; }
        public Enumerados.EnumOferta oferta { get; set; }
        public Enumerados.EnumMetodoBusqueda MetodoBusqueda { get; set; }
        public bool activarBusquedaPorTrasalado { get; set; }
        public string autoDepOrigen { get; set; }
        public string autoDepDestino { get; set; }
        public string estatusTCS { get; set; }


        public Filtro()
        {
            cadena = "";
            autoProducto = "";
            autoDepartamento = "";
            autoDeposito = "";
            autoGrupo = "";
            autoTasa = "";
            autoProveedor = "";
            autoMarca = "";
            origen = Enumerados.EnumOrigen.SnDefinir;
            estatus = Enumerados.EnumEstatus.SnDefinir;
            categoria = Enumerados.EnumCategoria.SnDefinir;
            admPorDivisa = Enumerados.EnumAdministradorPorDivisa.SnDefinir;
            pesado = Enumerados.EnumPesado.SnDefinir;
            oferta = Enumerados.EnumOferta.SnDefinir;
            catalogo = Enumerados.EnumCatalogo.SnDefinir;
            activarBusquedaPorTrasalado = false;
            autoDepOrigen = "";
            autoDepDestino = "";
            estatusTCS = "";
        }
    }
}