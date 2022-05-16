using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Identificacion
    {

        public string auto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoMarca { get; set; }
        public string autoGrupo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string modelo { get; set; }
        public string referencia { get; set; }
        public int contenidoCompra { get; set; }
        public string empaqueCompra { get; set; }
        public string empInventario{ get; set; }
        public int contEmpInv { get; set; }
        public string decimales { get; set; }
        public Enumerados.EnumOrigen origen { get; set; }
        public Enumerados.EnumCategoria categoria { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public Enumerados.EnumAdministradorPorDivisa AdmPorDivisa { get; set; }
        public Enumerados.EnumCatalogo activarCatalogo { get; set; }
        public string departamento { get; set; }
        public string codigoDepartamento { get; set; }
        public string grupo { get; set; }
        public string codigoGrupo { get; set; }
        public string marca { get; set; }
        public decimal tasaIva { get; set; }
        public string nombreTasaIva { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaBaja { get; set; }
        public DateTime fechaUltActualizacion { get; set; }
        public string tipoABC { get; set; }
        public string comentarios { get; set; }
        public string advertencia { get; set; }
        public string presentacion { get; set; }
        public string estatusPesado { get; set; }
        public string plu { get; set; }
        public int diasEmpaque { get; set; }
        public List<CodAlterno> codAlterno { get; set; }


        public Identificacion()
        {
            auto = "";
            autoDepartamento = "";
            autoGrupo = "";
            autoMarca = "";

            codigo = "";
            nombre = "";
            descripcion = "";
            modelo = "";
            referencia = "";
            contenidoCompra = 1;
            empaqueCompra = "";
            empInventario = "";
            contEmpInv = 1;
            decimales = "0";
            origen = Enumerados.EnumOrigen.SnDefinir;
            categoria = Enumerados.EnumCategoria.SnDefinir;
            estatus = Enumerados.EnumEstatus.SnDefinir;
            AdmPorDivisa = Enumerados.EnumAdministradorPorDivisa.SnDefinir;
            departamento = "";
            codigoDepartamento = "";
            grupo = "";
            codigoGrupo = "";
            marca = "";
            tasaIva = 0.0m;
            nombreTasaIva = "";
            fechaAlta = DateTime.Now.Date;
            fechaBaja = DateTime.Now.Date;
            fechaUltActualizacion = DateTime.Now.Date;
            tipoABC = "";
            comentarios = "";
            advertencia = "";
            presentacion = "";
            activarCatalogo = Enumerados.EnumCatalogo.SnDefinir;
            estatusPesado = "";
            plu = "";
            diasEmpaque = 0;
            codAlterno = new List<CodAlterno>();
        }

    }

}