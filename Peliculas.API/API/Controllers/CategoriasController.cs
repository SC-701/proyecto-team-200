using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Categorias;

namespace API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
	public class CategoriasController : ControllerBase, ICategoriasController
	{
		private readonly ICategoriasFlujo _categoriasFlujo;
		private readonly ILogger<CategoriasController> _logger;

		public CategoriasController(ICategoriasFlujo categoriasFlujo, ILogger<CategoriasController> logger)
		{
			_categoriasFlujo = categoriasFlujo;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Agregar([FromBody] CategoriasRequest categorias)
		{
			var resultado = await _categoriasFlujo.Agregar(categorias);
			return CreatedAtAction(nameof(ObtenerPorId), new { IdCategoria = resultado }, null);

		}



		[HttpPut("{IdCategoria}")]
		public async Task<IActionResult> Editar([FromRoute] Guid IdCategoria, [FromBody] CategoriasRequest categoria)
		{
			if (!await VerificarExistenciaCategoria(IdCategoria))
				return NotFound("el producto no existe");
			var resultado = await _categoriasFlujo.Editar(IdCategoria, categoria);
			return Ok(resultado);
		}


		/*
				[HttpDelete("{IdCategoria}")]
				public async Task<IActionResult> Eliminar([FromRoute] Guid IdCategoria)
				{
					if (!await VerificarExistenciaCategoria(IdCategoria))
						return NotFound("el producto no existe");
					var resultado = await _categoriasFlujo.Eliminar(IdCategoria);
					return NoContent();
				}
		*/
		[HttpGet]
		public async Task<IActionResult> Obtener()
		{
			var resultado = await _categoriasFlujo.Obtener();
			if (!resultado.Any())
				return NoContent();
			return Ok(resultado);
		}

		[HttpGet("{IdCategoria}")]
		public async Task<IActionResult> ObtenerPorId([FromRoute] Guid IdCategoria)
		{
			var resultado = await _categoriasFlujo.ObtenerPorId(IdCategoria);
			return Ok(resultado);
		}

		private async Task<bool> VerificarExistenciaCategoria(Guid Id)
		{
			var ResultadoValidacion = false;
			var resultadoCategoriaExiste = await _categoriasFlujo.ObtenerPorId(Id);
			if (resultadoCategoriaExiste != null)
				ResultadoValidacion = true;
			return ResultadoValidacion;
		}
	}
}
