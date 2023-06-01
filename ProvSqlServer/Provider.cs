using EntitySqlServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    
    public partial class Provider: ILibInventario.IProvider
    {

        public static EntityConnectionStringBuilder _cnInv;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;



        public Provider(string instancia, string bd)
        {
            _Usuario = "usuario";
            _Password = "112233-fs";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();
        }

        private void setConexion()
        {
            _cnInv = new EntityConnectionStringBuilder();
            _cnInv.Metadata = "res://*/FoxModel.csdl|res://*/FoxModel.ssdl|res://*/FoxModel.msl";
            _cnInv.Provider = "System.Data.SqlClient";
            _cnInv.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";MultipleActiveResultSets=True;";
        }


        public DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();
            try
            {
                using (var ctx = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select GETDATE()").FirstOrDefault();
                    result.Entidad = fechaSistema.Date;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }

     
        public DtoLib.ResultadoEntidad<string> Empresa_Sucursal_TipoPrecioManejar(string codEmpresa)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen> Producto_GetLista(DtoLibInventario.Producto.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha> Producto_GetFicha(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Identificacion> Producto_GetIdentificacion(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Existencia> Producto_GetExistencia(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Precio> Producto_GetPrecio(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Costo> Producto_GetCosto(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Imagen> Producto_GetImagen(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Proveedor.Ficha> Producto_GetProveedores(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Lista.Ficha> Producto_GetDepositos(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_AsignarRemoverDepositos(DtoLibInventario.Producto.Depositos.Asignar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Ver.Ficha> Producto_GetDeposito(DtoLibInventario.Producto.Depositos.Ver.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_EditarDeposito(DtoLibInventario.Producto.Depositos.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Plu.Lista.Resumen> Producto_Plu_Lista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Estatus.Lista.Resumen> Producto_Estatus_Lista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Pesado.Resumen> Producto_Pesado_Lista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Oferta.Resumen> Producto_Oferta_Lista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Clasificacion.Resumen> Producto_Clasificacion_Lista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Editar.Obtener.Ficha> Producto_Editar_GetFicha(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Editar_Actualizar(DtoLibInventario.Producto.Editar.Actualizar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Nuevo_Agregar(DtoLibInventario.Producto.Agregar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Estatus.Actual.Ficha> Producto_Estatus_GetFicha(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_CambiarEstatusA_Activo(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_CambiarEstatusA_Inactivo(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_CambiarEstatusA_Suspendido(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_EsBienServicio(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_HayDepositosAsignado(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_CodigoProductoYaRegistrado(string codigo, string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_CodigoPluProductoYaRegistrado(string codigo, string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_ExistenciaEnCero(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Verificar_QueExista_EstatusActivo_NoSeaBienServicio(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Verificar_DepositoRemover(string autoPrd, string autoDeposito)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Deposito_AsignacionMasiva(DtoLibInventario.Producto.Depositos.AsignacionMasiva.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Producto_GetId_ByCodigoBarra(string codBarra)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Precio.Ficha> Producto_Precio_GetById(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> Deposito_GetFicha(string autoDep)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Departamento.Ficha> Departamento_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Departamento_Agregar(DtoLibInventario.Departamento.Agregar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Departamento_Editar(DtoLibInventario.Departamento.Editar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Departamento_Eliminar(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetLista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Grupo.Ficha> Grupo_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Grupo_Agregar(DtoLibInventario.Grupo.Agregar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Grupo_Editar(DtoLibInventario.Grupo.Editar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Grupo_Eliminar(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Lista.Resumen> Proveedor_GetLista(DtoLibInventario.Proveedor.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Marca.Ficha> Marca_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Marca_Agregar(DtoLibInventario.Marca.Agregar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Marca_Editar(DtoLibInventario.Marca.Editar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Marca_Eliminar(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Costo.Historico.Resumen> HistoricoCosto_GetLista(DtoLibInventario.Costo.Historico.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado CostoProducto_Actualizar(DtoLibInventario.Costo.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen> HistoricoPrecio_GetLista(DtoLibInventario.Precio.Historico.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha> PrecioCosto_GetFicha(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado PrecioProducto_Actualizar(DtoLibInventario.Precio.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.PrecioCosto.Entidad.Ficha> PrecioCosto_GetData(string autoPrd)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Resumen.Ficha> Producto_Kardex_Movimiento_Lista_Resumen(DtoLibInventario.Kardex.Movimiento.Resumen.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Kardex.Movimiento.Detalle.Ficha> Producto_Kardex_Movimiento_Lista_Detalle(DtoLibInventario.Kardex.Movimiento.Detalle.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Concepto.Resumen> Concepto_GetLista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Concepto_Agregar(DtoLibInventario.Concepto.Agregar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Concepto_Editar(DtoLibInventario.Concepto.Editar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Concepto.Ficha> Concepto_PorTraslado()
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Concepto_Eliminar(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> Sucursal_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Devolucion_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto Producto_Movimiento_AjusteInvCero_Insertar(DtoLibInventario.Movimiento.AjusteInvCero.Insertar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen> Producto_Movimiento_GetLista(DtoLibInventario.Movimiento.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo> Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> Producto_Movimiento_GetFicha(string autoDoc)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Movimiento_Cargo_Anular(DtoLibInventario.Movimiento.Anular.Cargo.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Movimiento_Descargo_Anular(DtoLibInventario.Movimiento.Anular.Descargo.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Movimiento_Traslado_Anular(DtoLibInventario.Movimiento.Anular.Traslado.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Producto_Movimiento_Ajuste_Anular(DtoLibInventario.Movimiento.Anular.Ajuste.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_ExistenciaDisponible(List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha> lista)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_CostoEdad(DtoLibInventario.Movimiento.Verificar.CostoEdad.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_AnularDocumento(string autoDocumento)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_DepositoSucursalActivo(string idDepOrigen, string idDepDestino, string codSucursal)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha> Capturar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha> Producto_Movimiento_Cargo_Capture(DtoLibInventario.Movimiento.Cargo.CapturaMov.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha> Producto_Movimiento_Descargo_Capture(DtoLibInventario.Movimiento.DesCargo.CapturaMov.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha> Producto_Movimiento_Traslado_Capture(DtoLibInventario.Movimiento.Traslado.CapturaMov.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha> Producto_Movimiento_AjusteInventarioCero_Capture(DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> Usuario_Principal()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Usuario_GetClave_ById(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Usuario_GetId_ByClaveUsuGrupoAdm(string clave)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha> Tools_AjusteNivelMinimoMaximo_GetLista(DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Ficha> Tools_CambioMasivoPrecio_GetData(DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Tools_CambioMasivoPrecio_ActualizarData(DtoLibInventario.Tool.CambioMasivoPrecio.ActualizarData.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.EmpaqueMedida.Resumen> EmpaqueMedida_GetLista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.EmpaqueMedida.Ficha> EmpaqueMedida_GetFicha(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoAuto EmpaqueMedida_Agregar(DtoLibInventario.EmpaqueMedida.Agregar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado EmpaqueMedida_Editar(DtoLibInventario.EmpaqueMedida.Editar ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado EmpaqueMedida_Eliminar(string auto)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaBusqueda> Configuracion_PreferenciaBusqueda()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumMetodoCalculoUtilidad> Configuracion_MetodoCalculoUtilidad()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_TasaCambioActual()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumForzarRedondeoPrecioVenta> Configuracion_ForzarRedondeoPrecioVenta()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.Enumerados.EnumPreferenciaRegistroPrecio> Configuracion_PreferenciaRegistroPrecio()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<int> Configuracion_CostoEdadProducto()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_VisualizarProductosInactivos()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_CantDocVisualizar()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Configuracion.DepositoConceptoDev.Captura.Ficha> Configuracion_DepositoConceptoPreDeterminadoParaDevolucion()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_PermitirCambiarPrecioAlModificarCosto()
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetCostoEdadProducto(DtoLibInventario.Configuracion.CostoEdad.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetRedondeoPrecioVenta(DtoLibInventario.Configuracion.RedondeoPrecio.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetPreferenciaRegistroPrecio(DtoLibInventario.Configuracion.PreferenciaPrecio.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetMetodoCalculoUtilidad(DtoLibInventario.Configuracion.MetodoCalculoUtilidad.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetBusquedaPredeterminada(DtoLibInventario.Configuracion.BusquedaPredeterminada.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetDepositosPreDeterminado(DtoLibInventario.Configuracion.DepositoPredeterminado.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetDepositoConceptoPreDeterminadoParaDevolucion(DtoLibInventario.Configuracion.DepositoConceptoDev.Editar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Configuracion_SetPermitirCambiarPrecioAlModificarCosto(string conf)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData.Ficha> Configuracion_MetodoCalculoUtilidad_CapturarData()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_HabilitarPrecio_5_ParaVentaMayorPos()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Existencia.Ficha> Visor_Existencia(DtoLibInventario.Visor.Existencia.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.CostoEddad.Ficha> Visor_CostoEdad(DtoLibInventario.Visor.CostoEddad.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Traslado.Ficha> Visor_Traslado(DtoLibInventario.Visor.Traslado.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Visor.Ajuste.Ficha> Visor_Ajuste(DtoLibInventario.Visor.Ajuste.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.CostoExistencia.Ficha> Visor_CostoExistencia(DtoLibInventario.Visor.CostoExistencia.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.Ficha> Visor_Precio(DtoLibInventario.Visor.Precio.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.PrecioAjuste.Ficha> Visor_PrecioAjuste(DtoLibInventario.Visor.PrecioAjuste.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.Precio.SoloReporte.Ficha> Visor_Precio_Modo_SoloReporte(DtoLibInventario.Visor.Precio.SoloReporte.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Visor.EntradaxCompra.Ficha> Visor_EntradasxCompra(DtoLibInventario.Visor.EntradaxCompra.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroProducto.Ficha> Reportes_MaestroProducto(DtoLibInventario.Reportes.MaestroProducto.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroInventario.Ficha> Reportes_MaestroInventario(DtoLibInventario.Reportes.MaestroInventario.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistencia.Ficha> Reportes_MaestroExistencia(DtoLibInventario.Reportes.MaestroExistencia.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Kardex.Ficha> Reportes_Kardex(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.KardexResumen.Ficha> Reportes_KardexResumen(DtoLibInventario.Reportes.Kardex.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Top20.Ficha> Reportes_Top20(DtoLibInventario.Reportes.Top20.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.TopDepartUtilidad.Ficha> Reportes_TopDepartUtilidad(DtoLibInventario.Reportes.TopDepartUtilidad.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.CompraVentaAlmacen.Ficha> Reportes_CompraVentaAlmacen(DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.DepositoResumen.Ficha> Reportes_DepositoResumen()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroNivelMinimo.Ficha> Reportes_NivelMinimo(DtoLibInventario.Reportes.MaestroNivelMinimo.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.Valorizacion.Ficha> Reportes_Valorizacion(DtoLibInventario.Reportes.Valorizacion.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroExistenciaInventario.Ficha> Reportes_MaestroExistenciaInventario(DtoLibInventario.Reportes.MaestroExistenciaInventario.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.ResumenCostoInv.Ficha> Reportes_ResumenCostoInventario(DtoLibInventario.Reportes.ResumenCostoInv.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearProducto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarProducto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarPrecios(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarCostos(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AsignarDepositos(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarDatosDelDeposito(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ActualizarEstatusDelProducto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarImagenDelProducto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Departamento(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearDepartamento(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarDepartamento(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarDepartamento(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Grupo(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearGrupo(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarGrupo(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarGrupo(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Marca(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearMarca(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarMarca(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarMarca(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_UnidadEmpaque(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearUnidadEmpaque(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarUnidadEmpaque(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarUnidadEmpaque(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ConceptoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearConceptoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarConceptoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarConcepto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoCargoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoDescargoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoPorDevolucionInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoAjusteInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoAjusteInventarioCero(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTraslado_Procesar(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdministradorMovimientoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmAnularMovimientoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmVisualizarMovimientoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmReporteMovimientoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_DefinirNivelMinimoMaximoInventario(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Visor(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Estadistica(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Configuracion_Sistema(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AsignacionMasivaProductosDeposito(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambioMovimientoMasivoPrecio(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.General.Ficha> Producto_Analisis_General(DtoLibInventario.Analisis.General.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Detallado.Ficha> Producto_Analisis_Detallado(DtoLibInventario.Analisis.Detallado.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Analisis.Existencia.Ficha> Producto_Analisis_Existencia(DtoLibInventario.Analisis.Existencia.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Auditoria.Entidad.Ficha> Auditoria_Documento_GetFichaBy(DtoLibInventario.Auditoria.Buscar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.MonitorPos.Entidad.Ficha> MonitorPos_VentaResumen_GetLista(DtoLibInventario.MonitorPos.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoId Transito_Movimiento_Agregar(DtoLibInventario.Transito.Movimiento.Agregar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Transito.Movimiento.Entidad.Ficha> Transito_Movimiento_GetById(int idMov)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Transito.Movimiento.Lista.Ficha> Transito_Movimiento_GetLista(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado Transito_Movimiento_AnularById(int idMov)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<int> Transito_Movimiento_GetCnt(DtoLibInventario.Transito.Movimiento.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.MovPend.Lista.Ficha> MovPend_GetLista(DtoLibInventario.MovPend.Lista.Filtro filtro)
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado MovPend_Anular(int idMov)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Empresa.Grupo.Lista.Ficha> EmpresaGrupo_GetLista()
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<string> EmpresaGrupo_PrecioManejar_GetById(string idGrupo)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta> Producto_GetEmpaqueVenta_ModoAdm(string autoPrd)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.VerData.EmpaqueVenta> Producto_ModoAdm_GetEmpaqueVenta_By(string autoPrd)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Precio.Ficha> Producto_ModoAdm_GetPrecio_By(string autoPrd, string tipoPrecio)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.ModoAdm.Costo.Ficha> Producto_ModoAdm_GetCosto_By(string autoPrd)
        {
            throw new NotImplementedException();
        }
        public DtoLib.Resultado Producto_ModoAdm_ActualizarPrecio(DtoLibInventario.Producto.ActualizarPrecio.ModoAdm.Ficha ficha)
        {
            throw new NotImplementedException();
        }
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.HistoricoPrecio.ModoAdm.Ficha> ILibInventario.IProducto_ModoAdm.Producto_ModoAdm_HistoricoPrecio_By(DtoLibInventario.Producto.HistoricoPrecio.Filtro filtro)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha> Reportes_ModAdm_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            throw new NotImplementedException();
        }
        public DtoLib.Resultado Producto_ModoAdm_ActualizarOferta(DtoLibInventario.Producto.ActualizarOferta.ModoAdm.Actualizar.Ficha ficha)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AsignarOfertaProducto(string autoGrupoUsuario)
        {
            throw new NotImplementedException();
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Capturar.Ficha> Producto_ModoAdm_OfertaMasiva_Capturar(DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Capturar.Filtro filtro)
        {
            throw new NotImplementedException();
        }
        public DtoLib.Resultado Producto_ModoAdm_OfertaMasiva_Actualizar(DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Actualizar.Ficha ficha)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.TomaInv.ObtenerToma.Ficha> TomaInv_GetListaPrd(DtoLibInventario.TomaInv.ObtenerToma.Filtro filtro)
        {
            throw new NotImplementedException();
        }


        public DtoLib.Resultado TomaInv_GenerarToma(DtoLibInventario.TomaInv.GenerarToma.Ficha ficha)
        {
            throw new NotImplementedException();
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.TomaInv.Analisis.Ficha> TomaInv_AnalizarToma(int idToma)
        {
            throw new NotImplementedException();
        }


        public DtoLib.Resultado TomaInv_RechazarItemsToma(DtoLibInventario.TomaInv.RechazarItem.Ficha ficha)
        {
            throw new NotImplementedException();
        }
    }
}