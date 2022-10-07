using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes
{
    
    public class enumerados
    {

        public enum EnumAdministradorPorDivisa { SnDefinir = -1, Si = 1, No };
        public enum EnumOrigen { SnDefinir = -1, Nacional = 1, Importado };
        public enum EnumEstatus { SnDefinir = -1, Activo = 1, Inactivo };
        public enum EnumCategoria { SnDefinir = -1, ProductoTerminado = 1, BienServicio, MateriaPrima, UsoInterno, SubProducto };
        public enum EnumModulo { SnDefinir = -1, Ventas= 1, Compras , Inventario };
        public enum EnumPrecio { SnDefinir = -1, P1 = 1, P2, P3, P4, P5 };
        public enum EnumPesado { SnDefinir = -1, Si = 1, No };

    }

}