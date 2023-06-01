using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProvider: IProducto, IDeposito, IDepartamento, IGrupo, ITasaImpuesto, IProveedor, IMarca,
        ICosto, IPrecio, IKardex, IConcepto, ISucursal, IMovimiento, IUsuario, 
        ITool, IEmpaqueMedida, IConfiguracion, IVisor, IReportes, IPermisos, IAnalisis,
        IAuditoria, ISistema, IMonitorPos, IMovTransito, IMovPend , IEmpresa, ITallaColorSabor,
        IProducto_ModoAdm, IReportes_ModoAdm, ITomaInv
    {
        DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor();
        DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> 
            Empresa_Datos();
        DtoLib.ResultadoEntidad<string>
            Empresa_Sucursal_TipoPrecioManejar(string codEmpresa);
    }
}