using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase, IProductosController
    {
        private readonly IProductosFlujo _productosFlujo;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductosFlujo productosFlujo, ILogger<ProductosController> logger)
        {
            _productosFlujo = productosFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProductosRequest productos)
        {
            var resultado = await _productosFlujo.Agregar(productos);
            return CreatedAtAction(nameof(ObtenerPorId), new { IdProducto = resultado }, null); 

        }

        [HttpPut("{IdProducto}")]
        public async Task<IActionResult> Editar([FromRoute] Guid IdProducto, [FromBody] ProductosRequest productos)
        {
            if (!await VerificarProductosExiste(IdProducto))
                return NotFound("el producto no existe");
            var resultado = await _productosFlujo.Editar(IdProducto, productos);
            return Ok(resultado);
        }

        [HttpDelete("{IdProducto}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdProducto)
        {
            if (!await VerificarProductosExiste(IdProducto))
                return NotFound("el producto no existe");
            var resultado = await _productosFlujo.Eliminar(IdProducto);
            return NoContent();
        }
        [HttpGet("ProductosPaginados/{pageIndex}/{pageSize}")]

        public async Task<IActionResult> ListarProductosPaginado([FromRoute] int pageIndex, [FromRoute]  int pageSize)
        {
            var resultado = await _productosFlujo.ListarProductosPaginado(pageIndex, pageSize);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _productosFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{IdProducto}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] Guid IdProducto)
        {
            var resultado = await _productosFlujo.ObtenerPorId(IdProducto);
            return Ok(resultado);
        }

        private async Task<bool> VerificarProductosExiste(Guid Id)
        {
            var ResultadoValidacion = false;
            var resultadoVehiculoExiste = await _productosFlujo.ObtenerPorId(Id);
            if (resultadoVehiculoExiste != null)
                ResultadoValidacion = true;
            return ResultadoValidacion;
        }
    }
}
