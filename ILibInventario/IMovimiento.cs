using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    public interface IMovimiento
    {
        //INSERTAR MOVIMIENTOS
        DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Devolucion_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_AjusteInvCero_Insertar(DtoLibInventario.Movimiento.AjusteInvCero.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_AjustePorToma_Insertar(DtoLibInventario.Movimiento.AjustePorToma.Insertar.Ficha ficha);


        //GET
        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen> Producto_Movimiento_GetLista(DtoLibInventario.Movimiento.Lista.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo> Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> Producto_Movimiento_GetFicha(string autoDoc);


        //ANULAR MOVIMIENTOS
        DtoLib.Resultado Producto_Movimiento_Cargo_Anular(DtoLibInventario.Movimiento.Anular.Cargo.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Descargo_Anular(DtoLibInventario.Movimiento.Anular.Descargo.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Traslado_Anular(DtoLibInventario.Movimiento.Anular.Traslado.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Ajuste_Anular(DtoLibInventario.Movimiento.Anular.Ajuste.Ficha ficha);


        //VERIFICAR 
        DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_ExistenciaDisponible(List<DtoLibInventario.Movimiento.Verificar.ExistenciaDisponible.Ficha> lista);
        DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_CostoEdad(DtoLibInventario.Movimiento.Verificar.CostoEdad.Ficha ficha);
        DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_AnularDocumento(string autoDocumento);
        DtoLib.ResultadoEntidad<bool> Producto_Movimiento_Verificar_DepositoSucursalActivo(string idDepOrigen, string idDepDestino, string codSucursal);


        //CAPTURAR DATA PARA MOVIMIENTOS
        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha> 
            Capturar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO CARGO
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha>
            Producto_Movimiento_Cargo_Capture(DtoLibInventario.Movimiento.Cargo.CapturaMov.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO DESCARGO
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha>
            Producto_Movimiento_Descargo_Capture(DtoLibInventario.Movimiento.DesCargo.CapturaMov.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO TRASLADO ENTRE DEPOSITO
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha>
            Producto_Movimiento_Traslado_Capture(DtoLibInventario.Movimiento.Traslado.CapturaMov.Filtro filtro);

        //CAPTURAR DATA PARA AJUSTAR INVENTARIO A CERO(0)
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha> 
            Producto_Movimiento_AjusteInventarioCero_Capture(DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro filtro);
    }
}