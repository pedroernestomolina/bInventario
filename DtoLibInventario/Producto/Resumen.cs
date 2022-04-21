using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto
{

    public class Resumen
    {

        private string xestatus { get; set; }
        private string estatus_divisa { get; set; }
        private string estatus_pesado { get; set; }
        private string estatus_catalogo { get; set; }
        private string estatus_oferta { get; set; }
        private string estatus_cambio { get; set; }
        private string xcategoria { get; set; }
        private string xorigen { get; set; }

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }

        public int contenido { get; set; }
        public string empaque { get; set; }
        public string decimales { get; set; }
        public decimal tasaIva { get; set; }
        public string tasaIvaDescripcion { get; set; }

        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaUltCambioCosto { get; set; }
        public DateTime? fechaUltActualizacion { get; set; }

        public decimal costoDivisa { get; set; }
        public decimal? existencia { get; set; }

        public decimal pDivisaFull_1 { get; set; }
        public decimal pDivisaFull_2 { get; set; }
        public decimal pDivisaFull_3 { get; set; }
        public decimal pDivisaFull_4 { get; set; }
        public decimal pDivisaFull_5 { get; set; }

        public decimal costo { get; set; }
        public decimal pNeto1 { get; set; }
        public decimal pNeto2 { get; set; }
        public decimal pNeto3 { get; set; }
        public decimal pNeto4 { get; set; }
        public decimal pNeto5 { get; set; }

        //
        public decimal pNetoMay1 { get; set; }
        public decimal pNetoMay2 { get; set; }
        public int contMay1 { get; set; }
        public int contMay2 { get; set; }
        public decimal pDivisaFullMay_1 { get; set; }
        public decimal pDivisaFullMay_2 { get; set; }


        public Enumerados.EnumCategoria categoria 
        {
            get 
            {
                var _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SnDefinir;
                switch (xcategoria.Trim().ToUpper())
                {
                    case "PRODUCTO TERMINADO":
                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.ProductoTerminado;
                        break;
                    case "BIEN DE SERVICIO":
                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.BienServicio;
                        break;
                    case "MATERIA PRIMA":
                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.MateriaPrima;
                        break;
                    case "USO INTERNO":
                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.UsoInterno;
                        break;
                    case "SUB PRODUCTO":
                        _categoria = DtoLibInventario.Producto.Enumerados.EnumCategoria.SubProducto;
                        break;
                }
                return _categoria;
            }
        }

        public Enumerados.EnumOrigen origen 
        {
            get 
            {
                var _origen = DtoLibInventario.Producto.Enumerados.EnumOrigen.SnDefinir;
                switch (xorigen.Trim().ToUpper())
                {
                    case "NACIONAL":
                        _origen = DtoLibInventario.Producto.Enumerados.EnumOrigen.Nacional;
                        break;
                    case "IMPORTADO":
                        _origen = DtoLibInventario.Producto.Enumerados.EnumOrigen.Importado;
                        break;
                }
                return _origen;
            }
        }

        public Enumerados.EnumEstatus estatus 
        {
            get 
            {
                var _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
                if (estatus_cambio.Trim().ToUpper() == "1")
                {
                    _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Suspendido;
                }
                else if (xestatus.Trim().ToUpper() != "ACTIVO")
                {
                    _estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Inactivo;
                }
                return _estatus;
            } 
        }

        public Enumerados.EnumAdministradorPorDivisa admPorDivisa 
        {
            get 
            {
                var _admDivisa = DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.No;
                if (estatus_divisa.Trim().ToUpper() == "1")
                    _admDivisa = DtoLibInventario.Producto.Enumerados.EnumAdministradorPorDivisa.Si;
                return _admDivisa;
            }
        }

        public Enumerados.EnumPesado esPesado 
        {
            get 
            {
                var _esPesado = DtoLibInventario.Producto.Enumerados.EnumPesado.No;
                if (estatus_pesado == "1")
                    _esPesado = DtoLibInventario.Producto.Enumerados.EnumPesado.Si;
                return _esPesado;
            }
        }

        public Enumerados.EnumCatalogo activarCatalogo 
        {
            get 
            {
                var _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.No;
                if (estatus_catalogo == "1")
                    _catalogo = DtoLibInventario.Producto.Enumerados.EnumCatalogo.Si;
                return _catalogo;
            }
        }

        public Enumerados.EnumOferta enOferta 
        {
            get 
            {
                var _enOferta = DtoLibInventario.Producto.Enumerados.EnumOferta.No;
                if (estatus_oferta == "1")
                    _enOferta = DtoLibInventario.Producto.Enumerados.EnumOferta.Si;
                return _enOferta;
            }
        }

    }

}