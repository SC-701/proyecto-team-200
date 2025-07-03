using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IProductosFlujo
    {

        Task<IEnumerable<ProductosResponse>> Obtener();
        Task<ProductosResponse> ObtenerPorId(Guid IdProducto);

        Task<Guid> Agregar(ProductosRequest productos);

        Task<Guid> Editar(Guid IdProducto, ProductosRequest productos);

        Task<Guid> Eliminar(Guid IdProducto);
    }
}
