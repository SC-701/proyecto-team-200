using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class ProductosFlujo : IProductosFlujo
    {
        private readonly IProductosDA _productosDA;
       

        public ProductosFlujo(IProductosDA productosDA)
        {
            _productosDA = productosDA;

        }
        public async Task<Guid> Agregar(ProductosRequest productos)
        {
            return await _productosDA.Agregar(productos);
        }

        public Task<Guid> Editar(Guid Id, ProductosRequest productos)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductosResponse>> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<ProductosResponse> ObtenerPorId(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
