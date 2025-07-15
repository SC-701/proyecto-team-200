using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<Usuario> ObtenerUsuario(Usuario usuario);
        Task<IEnumerable<Perfiles>> ObtenerPerfilesUsuario(Usuario usuario);
        Task<Guid> CrearUsuario(Usuario usuario);
    }
}
