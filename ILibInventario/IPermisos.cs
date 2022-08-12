using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IPermisos
    {

        DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMaximo();
        DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMedio();
        DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMinimo();
        
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearProducto(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarProducto(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarPrecios(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarCostos(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignarDepositos(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarDatosDelDeposito(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ActualizarEstatusDelProducto(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarImagenDelProducto(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_ToolInventario (string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Departamento(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearDepartamento(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_ModificarDepartamento(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarDepartamento(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_Grupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearGrupo(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarGrupo (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarGrupo(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Marca(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_CrearMarca(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarMarca (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarMarca(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_UnidadEmpaque(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_CrearUnidadEmpaque(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_ModificarUnidadEmpaque (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarUnidadEmpaque(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_ConceptoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearConceptoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarConceptoInventario (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarConcepto(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoCargoInventario (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoDescargoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_MovimientoTrasladoPorDevolucionInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventarioCero(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTraslado_Procesar(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdministradorMovimientoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmAnularMovimientoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_AdmVisualizarMovimientoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmReporteMovimientoInventario(string autoGrupoUsuario);

        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_DefinirNivelMinimoMaximoInventario(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(string autoGrupoUsuario);


        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Reportes (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Visor (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Estadistica (string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Configuracion_Sistema(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_AsignacionMasivaProductosDeposito(string autoGrupoUsuario);
        DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>
            Permiso_CambioMovimientoMasivoPrecio(string autoGrupoUsuario);

    }

}