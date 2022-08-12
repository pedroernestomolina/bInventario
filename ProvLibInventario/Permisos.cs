using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL17");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMedio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL18");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMinimo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL19");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearProducto(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarProducto(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarPrecios(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301040000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarCostos(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0301050000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignarDepositos(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarDatosDelDeposito(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ActualizarEstatusDelProducto(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330030000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambiarImagenDelProducto(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330040000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Departamento(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0303000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearDepartamento(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0303010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarDepartamento(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0303020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarDepartamento(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0303030000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Grupo(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0304000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearGrupo(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0304010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarGrupo(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0304020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarGrupo(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0304030000");
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Marca(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0305000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearMarca(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0305010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarMarca(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0305020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarMarca(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0305030000");
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_UnidadEmpaque(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0306000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearUnidadEmpaque(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0306010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarUnidadEmpaque(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0306020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarUnidadEmpaque(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0306030000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ConceptoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0307000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CrearConceptoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0307010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ModificarConceptoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0307020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_EliminarConcepto(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0307030000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_ToolInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0310000000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoCargoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoDescargoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308030000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoPorDevolucionInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308050000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventarioCero(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308060000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoAjusteInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308040000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdministradorMovimientoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0309000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmAnularMovimientoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0309010000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmVisualizarMovimientoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0309020000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AdmReporteMovimientoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0309030000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_DefinirNivelMinimoMaximoInventario(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330050000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330060000");
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Reportes(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0399000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Visor(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330070000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Estadistica(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330080000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Configuracion_Sistema(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "1202000000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTraslado_Procesar(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0308070000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignacionMasivaProductosDeposito(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330090000");
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_CambioMovimientoMasivoPrecio(string autoGrupoUsuario)
        {
            return Helpers.Permiso_Modulo(autoGrupoUsuario, "0330100000");
        }

    }

}