using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen> Producto_GetLista(DtoLibInventario.Producto.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha> Producto_GetFicha(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Identificacion> Producto_GetIdentificacion(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Existencia> Producto_GetExistencia(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Precio> Producto_GetPrecio(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Costo> Producto_GetCosto(string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Imagen> Producto_GetImagen (string autoPrd);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Proveedor.Ficha> Producto_GetProveedores (string autoPrd);

        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Lista.Ficha> Producto_GetDepositos(string autoPrd);
        DtoLib.Resultado Producto_AsignarRemoverDepositos(DtoLibInventario.Producto.Depositos.Asignar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Ver.Ficha> Producto_GetDeposito(DtoLibInventario.Producto.Depositos.Ver.Filtro filtro);
        DtoLib.Resultado Producto_EditarDeposito(DtoLibInventario.Producto.Depositos.Editar.Ficha ficha);

        DtoLib.ResultadoLista<DtoLibInventario.Producto.Plu.Lista.Resumen> Producto_Plu_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Estatus.Lista.Resumen> Producto_Estatus_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen> Producto_Origen_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen> Producto_Categoria_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen> Producto_AdmDivisa_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Pesado.Resumen> Producto_Pesado_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Oferta.Resumen> Producto_Oferta_Lista();
        DtoLib.ResultadoLista<DtoLibInventario.Producto.Clasificacion.Resumen> Producto_Clasificacion_Lista();

        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Editar.Obtener.Ficha> Producto_Editar_GetFicha(string autoPrd);
        DtoLib.Resultado  Producto_Editar_Actualizar(DtoLibInventario.Producto.Editar.Actualizar.Ficha ficha);
        DtoLib.ResultadoAuto Producto_Nuevo_Agregar(DtoLibInventario.Producto.Agregar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Estatus.Actual.Ficha> Producto_Estatus_GetFicha(string autoPrd);

        DtoLib.Resultado Producto_CambiarEstatusA_Activo (string auto);
        DtoLib.Resultado Producto_CambiarEstatusA_Inactivo(string auto);
        DtoLib.Resultado Producto_CambiarEstatusA_Suspendido(string auto);

        DtoLib.ResultadoEntidad<bool> Producto_Verificar_EsBienServicio(string autoPrd);
        DtoLib.ResultadoEntidad<bool> Producto_Verificar_HayDepositosAsignado(string autoPrd);
        DtoLib.ResultadoEntidad<bool> Producto_Verificar_CodigoProductoYaRegistrado (string codigo, string autoPrd);
        DtoLib.ResultadoEntidad<bool> Producto_Verificar_CodigoPluProductoYaRegistrado(string codigo, string autoPrd);
        DtoLib.ResultadoEntidad<bool> Producto_Verificar_ExistenciaEnCero(string autoPrd);
        DtoLib.ResultadoEntidad<bool> Producto_Verificar_QueExista_EstatusActivo_NoSeaBienServicio(string autoPrd);
        DtoLib.Resultado Producto_Verificar_DepositoRemover(string autoPrd, string autoDeposito);

    }

}