using DtoLibInventario.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Precio.PrecioCosto
{
    
    public class Ficha
    {

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string nombreTasaIva { get; set; }
        public decimal tasaIva { get; set; }
        public string admDivisa { get; set; }

        public string etiqueta1 { get; set; }
        public string etiqueta2 { get; set; }
        public string etiqueta3 { get; set; }
        public string etiqueta4 { get; set; }
        public string etiqueta5 { get; set; }

        public decimal precioNeto1 { get; set; }
        public decimal precioNeto2 { get; set; }
        public decimal precioNeto3 { get; set; }
        public decimal precioNeto4 { get; set; }
        public decimal precioNeto5 { get; set; }

        public string autoEmp1 { get; set; }
        public string autoEmp2 { get; set; }
        public string autoEmp3 { get; set; }
        public string autoEmp4 { get; set; }
        public string autoEmp5 { get; set; }

        public int contenido1 { get; set; }
        public int contenido2 { get; set; }
        public int contenido3 { get; set; }
        public int contenido4 { get; set; }
        public int contenido5 { get; set; }

        public decimal utilidad1 { get; set; }
        public decimal utilidad2 { get; set; }
        public decimal utilidad3 { get; set; }
        public decimal utilidad4 { get; set; }
        public decimal utilidad5 { get; set; }

        public decimal precioFullDivisa1 { get; set; }
        public decimal precioFullDivisa2 { get; set; }
        public decimal precioFullDivisa3 { get; set; }
        public decimal precioFullDivisa4 { get; set; }
        public decimal precioFullDivisa5 { get; set; }

        public string fechaUltActualizacion { get; set; }
        public string empCompra { get; set; }
        public int contempCompra { get; set; }
        public decimal costo { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costoUnd { get; set; }


        // MAYOR
        public string autoEmpMay1 { get; set; }
        public string autoEmpMay2 { get; set; }
        public string autoEmpMay3 { get; set; }
        public int contenidoMay1 { get; set; }
        public int contenidoMay2 { get; set; }
        public int contenidoMay3 { get; set; }
        public decimal utilidadMay1 { get; set; }
        public decimal utilidadMay2 { get; set; }
        public decimal utilidadMay3 { get; set; }
        public decimal precioNetoMay1 { get; set; }
        public decimal precioNetoMay2 { get; set; }
        public decimal precioNetoMay3 { get; set; }
        public decimal precioFullDivisaMay1 { get; set; }
        public decimal precioFullDivisaMay2 { get; set; }
        public decimal precioFullDivisaMay3 { get; set; }


        public Ficha()
        {
            codigo = "";
            nombre = "";
            descripcion = "";
            nombreTasaIva = "";
            tasaIva = 0.0m;
            admDivisa = "";
            fechaUltActualizacion = "";
            empCompra = "";
            contempCompra = 0;
            costo = 0.0m;
            costoDivisa = 0.0m;
            costoUnd = 0.0m;

            autoEmp1 = "";
            autoEmp2 = "";
            autoEmp3 = "";
            autoEmp4 = "";
            autoEmp5 = "";
            contenido1 = 0;
            contenido2 = 0;
            contenido3 = 0;
            contenido4 = 0;
            contenido5 = 0;
            utilidad1 = 0.0m;
            utilidad2 = 0.0m;
            utilidad3 = 0.0m;
            utilidad4 = 0.0m;
            utilidad5 = 0.0m;
            precioNeto1 = 0.0m;
            precioNeto2 = 0.0m;
            precioNeto3 = 0.0m;
            precioNeto4 = 0.0m;
            precioNeto5 = 0.0m;
            precioFullDivisa1 = 0.0m;
            precioFullDivisa2 = 0.0m;
            precioFullDivisa3 = 0.0m;
            precioFullDivisa4 = 0.0m;
            precioFullDivisa5 = 0.0m;
            etiqueta1 = "";
            etiqueta2 = "";
            etiqueta3 = "";
            etiqueta4 = "";
            etiqueta5 = "";

            // MAYOR
            autoEmpMay1 = "";
            autoEmpMay2 = "";
            autoEmpMay3 = "";
            contenidoMay1 = 0;
            contenidoMay2 = 0;
            contenidoMay3 = 0;
            utilidadMay1 = 0.0m;
            utilidadMay2 = 0.0m;
            utilidadMay3 = 0.0m;
            precioFullDivisaMay1 = 0.0m;
            precioFullDivisaMay2 = 0.0m;
            precioFullDivisaMay3 = 0.0m;
            precioNetoMay1 = 0.0m;
            precioNetoMay2 = 0.0m;
            precioNetoMay3 = 0.0m;
        }

    }

}