using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<Guid> CrearUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuario(Usuario usuario);
    }
}
