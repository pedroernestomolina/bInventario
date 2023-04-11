using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{
    public partial class Service: IService
    {
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearProducto(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearProducto(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarProducto(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarProducto(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarPrecios(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CambiarPrecios (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarCostos(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CambiarCostos (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignarDepositos(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AsignarDepositos (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarDatosDelDeposito(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CambiarDatosDelDeposito (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ActualizarEstatusDelProducto(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ActualizarEstatusDelProducto (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarImagenDelProducto(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CambiarImagenDelProducto (autoGrupoUsuario);
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Departamento(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Departamento(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearDepartamento(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearDepartamento (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarDepartamento(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarDepartamento (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarDepartamento(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_EliminarDepartamento(autoGrupoUsuario);
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Grupo(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Grupo(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearGrupo(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearGrupo (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarGrupo(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarGrupo (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarGrupo(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_EliminarGrupo(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Marca(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Marca(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearMarca(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearMarca (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarMarca(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarMarca (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarMarca(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_EliminarMarca(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_UnidadEmpaque(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_UnidadEmpaque(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearUnidadEmpaque(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearUnidadEmpaque (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarUnidadEmpaque(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarUnidadEmpaque (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarUnidadEmpaque(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_EliminarUnidadEmpaque(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ConceptoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ConceptoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearConceptoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CrearConceptoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarConceptoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ModificarConceptoInventario (autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarConceptoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_EliminarConcepto(autoGrupoUsuario);
        }
 
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ToolInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_ToolInventario (autoGrupoUsuario);
        }
        
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMaximo()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMaximo ();
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMedio()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMedio ();
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMinimo()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMinimo ();
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoCargoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoCargoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoDescargoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoDescargoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoTrasladoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoAjusteInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoPorDevolucionInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoTrasladoPorDevolucionInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventarioCero(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoAjusteInventarioCero(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdministradorMovimientoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AdministradorMovimientoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmAnularMovimientoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AdmAnularMovimientoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmVisualizarMovimientoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AdmVisualizarMovimientoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmReporteMovimientoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AdmReporteMovimientoInventario(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_DefinirNivelMinimoMaximoInventario(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_DefinirNivelMinimoMaximoInventario(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(autoGrupoUsuario);
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Reportes(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Reportes(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Visor(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Visor(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Estadistica(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Estadistica(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Configuracion_Sistema(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_Configuracion_Sistema(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTraslado_Procesar(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_MovimientoTraslado_Procesar(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignacionMasivaProductosDeposito(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AsignacionMasivaProductosDeposito(autoGrupoUsuario);
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambioMovimientoMasivoPrecio(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_CambioMovimientoMasivoPrecio(autoGrupoUsuario);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignarOfertaProducto(string autoGrupoUsuario)
        {
            return ServiceProv.Permiso_AsignarOfertaProducto(autoGrupoUsuario);
        }
    }
}