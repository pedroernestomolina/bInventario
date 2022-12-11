using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IService: IDeposito, IConcepto, ISucursal, IMovimiento, IUsuario, 
        IReportes, IReporteDocumentos, ITool, IDepartamento, IGrupo, IMarca,
        IEmpaqueMedida, IProducto, ITasaImpuesto, IConfiguracion, IPrecio, ICosto, IKardex,
        IProveedor, IVisor, IPermisos, IAnalisis, IAuditoria, ISistema, IMovTransito,
        IMovPend, IEmpresa
    {
        DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor();
        DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> 
            Empresa_Datos();
        DtoLib.ResultadoEntidad<string>
            Empresa_Sucursal_TipoPrecioManejar(string codEmpresa);
        //DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD();
    }

}