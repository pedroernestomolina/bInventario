using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.PrecioCosto.Entidad
{
    
    public class Ficha
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal tasaIva { get; set; }
        public decimal costoMonedaDivisa { get; set; }
        public decimal costoMonedaLocal { get; set; }
        public string empCompraDesc { get; set; }
        public int contEmpCompra { get; set; }
        public string estatusDivisa { get; set; }

        public string autoEmp_1 { get; set; }
        public string autoEmp_2 { get; set; }
        public string autoEmp_3 { get; set; }
        public string autoEmp_4 { get; set; }
        public string autoEmp_5 { get; set; }
        public int cont_1 { get; set; }
        public int cont_2 { get; set; }
        public int cont_3 { get; set; }
        public int cont_4 { get; set; }
        public int cont_5 { get; set; }
        public decimal utilidad_1 { get; set; }
        public decimal utilidad_2 { get; set; }
        public decimal utilidad_3 { get; set; }
        public decimal utilidad_4 { get; set; }
        public decimal utilidad_5 { get; set; }
        public decimal pNeto_1 { get; set; }
        public decimal pNeto_2 { get; set; }
        public decimal pNeto_3 { get; set; }
        public decimal pNeto_4 { get; set; }
        public decimal pNeto_5 { get; set; }
        public decimal pfd_1 { get; set; }
        public decimal pfd_2 { get; set; }
        public decimal pfd_3 { get; set; }
        public decimal pfd_4 { get; set; }
        public decimal pfd_5 { get; set; }

        public string autoEmp_M1 { get; set; }
        public string autoEmp_M2 { get; set; }
        public string autoEmp_M3 { get; set; }
        public string autoEmp_M4 { get; set; }
        public int cont_M1 { get; set; }
        public int cont_M2 { get; set; }
        public int cont_M3 { get; set; }
        public int cont_M4 { get; set; }
        public decimal utilidad_M1 { get; set; }
        public decimal utilidad_M2 { get; set; }
        public decimal utilidad_M3 { get; set; }
        public decimal utilidad_M4 { get; set; }
        public decimal pNeto_M1 { get; set; }
        public decimal pNeto_M2 { get; set; }
        public decimal pNeto_M3 { get; set; }
        public decimal pNeto_M4 { get; set; }
        public decimal pfd_M1 { get; set; }
        public decimal pfd_M2 { get; set; }
        public decimal pfd_M3 { get; set; }
        public decimal pfd_M4 { get; set; }


        public string autoEmp_D1 { get; set; }
        public string autoEmp_D2 { get; set; }
        public string autoEmp_D3 { get; set; }
        public string autoEmp_D4 { get; set; }
        public int cont_D1 { get; set; }
        public int cont_D2 { get; set; }
        public int cont_D3 { get; set; }
        public int cont_D4 { get; set; }
        public decimal utilidad_D1 { get; set; }
        public decimal utilidad_D2 { get; set; }
        public decimal utilidad_D3 { get; set; }
        public decimal utilidad_D4 { get; set; }
        public decimal pNeto_D1 { get; set; }
        public decimal pNeto_D2 { get; set; }
        public decimal pNeto_D3 { get; set; }
        public decimal pNeto_D4 { get; set; }
        public decimal pfd_D1 { get; set; }
        public decimal pfd_D2 { get; set; }
        public decimal pfd_D3 { get; set; }
        public decimal pfd_D4 { get; set; }


        public Ficha()
        {
            auto = "";
            codigo = "";
            descripcion = "";
            tasaIva = 0;
            costoMonedaDivisa = 0m;
            costoMonedaLocal = 0m;
            empCompraDesc = "";
            contEmpCompra = 0;
            estatusDivisa = "";

            autoEmp_1 = "";
            autoEmp_2 = "";
            autoEmp_3 = "";
            autoEmp_4 = "";
            autoEmp_5 = "";
            cont_1 = 0;
            cont_2 = 0;
            cont_3 = 0;
            cont_4 = 0;
            cont_5 = 0;
            utilidad_1 = 0m;
            utilidad_2 = 0m;
            utilidad_3 = 0m;
            utilidad_4 = 0m;
            utilidad_5 = 0m;
            pNeto_1 = 0m;
            pNeto_2 = 0m;
            pNeto_3 = 0m;
            pNeto_4 = 0m;
            pNeto_5 = 0m;
            pfd_1 = 0m;
            pfd_2 = 0m;
            pfd_3 = 0m;
            pfd_4 = 0m;
            pfd_5 = 0m;

            autoEmp_M1 = "";
            autoEmp_M2 = "";
            autoEmp_M3 = "";
            autoEmp_M4 = "";
            cont_M1 = 0;
            cont_M2 = 0;
            cont_M3 = 0;
            cont_M4 = 0;
            utilidad_M1 = 0m;
            utilidad_M2 = 0m;
            utilidad_M3 = 0m;
            utilidad_M4 = 0m;
            pNeto_M1 = 0m;
            pNeto_M2 = 0m;
            pNeto_M3 = 0m;
            pNeto_M4 = 0m;
            pfd_M1 = 0m;
            pfd_M2 = 0m;
            pfd_M3 = 0m;
            pfd_M4 = 0m;

            autoEmp_D1 = "";
            autoEmp_D2 = "";
            autoEmp_D3 = "";
            autoEmp_D4 = "";
            cont_D1 = 0;
            cont_D2 = 0;
            cont_D3 = 0;
            cont_D4 = 0;
            utilidad_D1 = 0m;
            utilidad_D2 = 0m;
            utilidad_D3 = 0m;
            utilidad_D4 = 0m;
            pNeto_D1 = 0m;
            pNeto_D2 = 0m;
            pNeto_D3 = 0m;
            pNeto_D4 = 0m;
            pfd_D1 = 0m;
            pfd_D2 = 0m;
            pfd_D3 = 0m;
            pfd_D4 = 0m;
        }

    }

}