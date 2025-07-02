using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IProductosDA
    {
        Task<IEnumerable<ProductosResponse>> Obtener();
        Task<ProductosResponse> ObtenerPorId(Guid Id);

        Task<Guid> Agregar(ProductosRequest productos);

        Task<Guid> Editar(Guid Id, ProductosRequest productos);

        Task<Guid> Eliminar(Guid Id);
    }
}
