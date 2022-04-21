using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.Interfaces
{
    
    public interface IMovimiento
    {

        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Lista.Resumen> Producto_Movimiento_GetLista(DtoLibInventario.Movimiento.Lista.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Consultar.ProductoPorDebajoNivelMinimo> Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Consultar.Filtro filtro);
        DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Ver.Ficha> Producto_Movimiento_GetFicha(string autoDoc);
        DtoLib.ResultadoAuto Producto_Movimiento_Cargo_Insertar(DtoLibInventario.Movimiento.Cargo.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_DesCargo_Insertar(DtoLibInventario.Movimiento.DesCargo.Insertar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_Ajuste_Insertar(DtoLibInventario.Movimiento.Ajuste.Insertar.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Cargo_Anular (DtoLibInventario.Movimiento.Anular.Cargo.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Descargo_Anular(DtoLibInventario.Movimiento.Anular.Descargo.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Traslado_Anular(DtoLibInventario.Movimiento.Anular.Traslado.Ficha ficha);
        DtoLib.Resultado Producto_Movimiento_Ajuste_Anular(DtoLibInventario.Movimiento.Anular.Ajuste.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Movimiento_Traslado_Devolucion_Insertar(DtoLibInventario.Movimiento.Traslado.Insertar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.AjusteInvCero.Capture.Ficha> Producto_Movimiento_AjusteInventarioCero_Capture(DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro filtro);
        DtoLib.ResultadoAuto Producto_Movimiento_AjusteInvCero_Insertar(DtoLibInventario.Movimiento.AjusteInvCero.Insertar.Ficha ficha);
        //
        DtoLib.ResultadoLista<DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Ficha> Capturar_ProductosPorDebajoNivelMinimo(DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO DESCARGO
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.DesCargo.CapturaMov.Ficha>
            Producto_Movimiento_Descargo_Capture(DtoLibInventario.Movimiento.DesCargo.CapturaMov.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO CARGO
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Cargo.CapturaMov.Ficha>
            Producto_Movimiento_Cargo_Capture(DtoLibInventario.Movimiento.Cargo.CapturaMov.Filtro filtro);

        //CAPTURAR DATA PARA MOVIMIENTO TIPO TRASLADO ENTRE DEPOSITOS
        DtoLib.ResultadoEntidad<DtoLibInventario.Movimiento.Traslado.CapturaMov.Ficha>
            Producto_Movimiento_Traslado_Capture(DtoLibInventario.Movimiento.Traslado.CapturaMov.Filtro filtro);

    }

}