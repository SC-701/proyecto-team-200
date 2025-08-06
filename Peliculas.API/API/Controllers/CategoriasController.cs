using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Flujo;
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

        [HttpPost("padre")]
        public async Task<IActionResult> AgregarPadre([FromBody] CategoriasRequestPadre categorias)
		{
			var resultado = await _categoriasFlujo.AgregarPadre(categorias);
			return CreatedAtAction(nameof(ObtenerPorId), new { IdCategoria = resultado }, null);

		}

        [HttpPost("hija")]
        public async Task<IActionResult> AgregarHija([FromBody] CategoriasRequestHija categorias)
        {
            var resultado = await _categoriasFlujo.AgregarHija(categorias);
            return CreatedAtAction(nameof(ObtenerPorId), new { IdCategoria = resultado }, null);

        }



        [HttpPut("editar/{IdCategoria}")]
        public async Task<IActionResult> Editar([FromRoute] Guid IdCategoria, [FromBody] CategoriasRequestPadre categoria)
		{
			if (!await VerificarExistenciaCategoria(IdCategoria))
				return NotFound("la categoria no existe");
			var resultado = await _categoriasFlujo.Editar(IdCategoria, categoria);
			return Ok(resultado);
		}


        [HttpPut("desactivar/{IdCategoria}")]
        public async Task<IActionResult> Desactivar([FromRoute] Guid IdCategoria)
        {
            if (!await VerificarExistenciaCategoria(IdCategoria))
                return NotFound("la categoria no existe");
            var resultado = await _categoriasFlujo.Desactivar(IdCategoria);
            return Ok(resultado);
        }


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

        [HttpGet("hijas/{idPadre}")]
        public async Task<IActionResult> ObtenerHijas(Guid idPadre)
        {
            var resultado = await _categoriasFlujo.ObtenerHijas(idPadre);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("hijas-recursivo/{idPadre}")]
        public async Task<IActionResult> ObtenerHijasRecursivo(Guid idPadre)
        {
            var resultado = await _categoriasFlujo.ObtenerHijasRecursivo(idPadre);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
    }
}
