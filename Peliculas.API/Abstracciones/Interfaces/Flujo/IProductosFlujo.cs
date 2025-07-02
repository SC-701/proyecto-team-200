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
        Task<ProductosResponse> ObtenerPorId(Guid Id);

        Task<Guid> Agregar(ProductosRequest productos);

        Task<Guid> Editar(Guid Id, ProductosRequest productos);

        Task<Guid> Eliminar(Guid Id);
    }
}
