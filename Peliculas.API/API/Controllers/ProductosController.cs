using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductosController : Controller, IProductosController
    {
        private IProductosFlujo _productosFlujo;
        private ILogger<ProductosController> _logger;

        public ProductosController(IProductosFlujo productosFlujo, ILogger<ProductosController> logger)
        {
            _productosFlujo = productosFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProductosRequest productos)
        {
            var resultado = await _productosFlujo.Agregar(productos);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }


        public Task<IActionResult> Editar(Guid Id, ProductosRequest productos)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Eliminar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerPorId(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
