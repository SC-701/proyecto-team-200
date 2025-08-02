using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase, IProveedorController
    {
        private readonly IProveedorFlujo _proveedorFlujo;
        private readonly ILogger<ProveedorController> _logger;
        public ProveedorController(IProveedorFlujo proveedorFlujo, ILogger<ProveedorController> logger)
        {
            _proveedorFlujo = proveedorFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] Proveedores proveedor)
        {
            var resultado = await _proveedorFlujo.Agregar(proveedor);
            return CreatedAtAction(nameof(ObtenerPorId), new { IdProveedor = resultado }, null);
        }
        [HttpGet]

        public async Task<IActionResult> Obtener()
        {
            var resultado = await _proveedorFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        [HttpGet("{IdProveedor}")]

        public async Task<IActionResult> ObtenerPorId([FromRoute] Guid IdProveedor)
        {
            var resultado = await _proveedorFlujo.ObtenerPorId(IdProveedor);
            return Ok(resultado);
        }
    }
}
