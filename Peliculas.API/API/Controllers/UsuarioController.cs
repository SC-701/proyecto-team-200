using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private IUsuarioFlujo _usuarioFlujo;

        public UsuarioController(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> PostAsync([FromBody] Usuario usuario)
        {
            return Ok(await _usuarioFlujo.CrearUsuario(usuario));
        }
        [Authorize(Roles = "2")]
        [HttpPost("ObtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario([FromBody] Usuario usuario)
        {
            return Ok(await _usuarioFlujo.ObtenerUsuario(usuario));
        }
        [Authorize(Roles = "2")]
        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> EditarUsuario([FromRoute]Guid idUsuario, [FromBody] UsuarioEditar usuario)
        {
            return Ok(await _usuarioFlujo.EditarUsuario(idUsuario,usuario));
        }
        [Authorize(Roles = "2")]
        [HttpGet("DetalleUsuario/{idUsuario}")]

        public async Task<IActionResult> DetalleUsuario(Guid idUsuario)
        {
            return Ok(await _usuarioFlujo.DetalleUsuario(idUsuario));
        }
    }
}
