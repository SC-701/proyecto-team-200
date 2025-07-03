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

        public async Task<Guid> Editar(Guid IdProducto, ProductosRequest productos)
        {
            return await _productosDA.Editar(IdProducto, productos);
        }

        public async Task<Guid> Eliminar(Guid IdProducto)
        {
            return await _productosDA.Eliminar(IdProducto);
        }

        public async Task<IEnumerable<ProductosResponse>> Obtener()
        {
            return await _productosDA.Obtener();
        }

        public async Task<ProductosResponse> ObtenerPorId(Guid IdProducto)
        {
            return await _productosDA.ObtenerPorId(IdProducto);
        }
    }
}
