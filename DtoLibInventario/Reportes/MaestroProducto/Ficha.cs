using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroProducto
{
    
    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string referenciaPrd { get; set; }
        public string modeloPrd { get; set; }
        private string estatusPrd { get; set; }
        private string estatusDivisaPrd { get; set; }
        private string estatusCambioPrd { get; set; }
        public int contenidoPrd { get; set; }
        private string origenPrd { get; set; }
        private string categoriaPrd { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public string empaque { get; set; }
        public decimal tasaIva { get; set; }


        public enumerados.EnumAdministradorPorDivisa admDivisa 
        {
            get 
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No;
                if (estatusDivisaPrd.Trim().ToUpper() == "1")
                    rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.Si;
                return rt;
            } 
        }

        public enumerados.EnumEstatus estatus 
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Activo;
                if (estatusPrd.Trim().ToUpper() != "ACTIVO")
                {
                    rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo;
                }
                return rt;
            }
        }

        public enumerados.EnumCategoria categoria 
        {
            get 
            {
                var rt= DtoLibInventario.Reportes.enumerados.EnumCategoria.ProductoTerminado;
                switch (categoriaPrd.Trim().ToUpper())
                {
                    case "BIEN DE SERVICIO":
                        rt = DtoLibInventario.Reportes.enumerados.EnumCategoria.BienServicio;
                        break;
                    case "MATERIA PRIMA":
                        rt= DtoLibInventario.Reportes.enumerados.EnumCategoria.MateriaPrima;
                        break;
                    case "USO INTERNO":
                        rt = DtoLibInventario.Reportes.enumerados.EnumCategoria.UsoInterno;
                        break;
                    case "SUB PRODUCTO":
                        rt= DtoLibInventario.Reportes.enumerados.EnumCategoria.SubProducto;
                        break;
                }
                return rt;
            }
        }

        public enumerados.EnumOrigen origen 
        {
            get 
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumOrigen.Nacional;
                if (origenPrd.Trim().ToUpper() != "NACIONAL")
                    rt= DtoLibInventario.Reportes.enumerados.EnumOrigen.Importado;
                return rt;
            }
        }

    }

}