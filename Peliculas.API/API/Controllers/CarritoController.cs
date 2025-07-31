using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Carrito;


namespace API.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class CarritoController : ControllerBase, ICarritoController
	{
		private readonly ICarritoFlujo _carritoFlujo;
		private readonly ILogger<CarritoController> _logger;

		public CarritoController(ICarritoFlujo carritoFlujo, ILogger<CarritoController> logger)
		{
			_carritoFlujo = carritoFlujo;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Agregar([FromBody] CarritoBase carrito)
		{
			var resultado = await _carritoFlujo.Agregar(carrito);
			return CreatedAtAction(nameof(ObtenerPorID), new { CarritoId = resultado }, null);

		}



		[HttpPut("{CarritoId}")]
		public async Task<IActionResult> Editar([FromRoute] Guid CarritoId, [FromBody] CarritoBase carrito)
		{

			var resultado = await _carritoFlujo.Editar(CarritoId, carrito);
			return Ok(resultado);
		}




		[HttpDelete("{CarritoId}")]
		public async Task<IActionResult> Eliminar([FromRoute] Guid CarritoId)
		{

			var resultado = await _carritoFlujo.Eliminar(CarritoId);
			return NoContent();
		}


		[HttpGet("por-user/{UsuarioId}")]
		public async Task<IActionResult> ObtenerPorUsuario([FromRoute] Guid UsuarioId)
		{
			var resultado = await _carritoFlujo.ObtenerPorUsuario(UsuarioId);
			return Ok(resultado);
		}


		[HttpGet("por-id/{CarritoId}")]
		public async Task<IActionResult> ObtenerPorID([FromRoute] Guid CarritoId)
		{
			var resultado = await _carritoFlujo.ObtenerPorID(CarritoId);

			if (resultado == null)
				return NotFound();

			return Ok(resultado);
		}

	}
}
