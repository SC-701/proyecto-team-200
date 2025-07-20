using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> PostAsync([FromBody] Modelos.Usuario usuario);
        Task<IActionResult> ObtenerUsuario([FromBody] Modelos.Usuario usuario);
    }
}
