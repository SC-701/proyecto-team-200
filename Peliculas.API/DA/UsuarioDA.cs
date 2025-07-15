using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;
using Helpers;

namespace DA
{
    public class UsuarioDA: IUsuarioDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _SqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _SqlConnection = repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> CrearUsuario(Usuario usuario)
        {
            var sql = @"[AgregarUsuario]";
            var resultado = await _SqlConnection.ExecuteScalarAsync<Guid>(sql, new { NombreUsuario = usuario.NombreUsuario, PasswordHash = usuario.PasswordHash, CorreoElectronico = usuario.CorreoElectronico });
            return resultado;
        }

        public async Task<IEnumerable<Perfiles>> ObtenerPerfilesUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerPerfilesxUsuario]";
            var consulta = await _SqlConnection.QueryAsync<Abstracciones.Entidades.Perfiles>(sql, new { CorreoElectronico = usuario.CorreoElectronico, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.ConvertirLista<Abstracciones.Entidades.Perfiles, Abstracciones.Modelos.Perfiles>(consulta);
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var consulta = await _SqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new
            {
                CorreoElectronico = usuario.CorreoElectronico
                ,
                NombreUsuario = usuario.NombreUsuario
            });
            return Convertidor.Convertir<Abstracciones.Entidades.Usuario, Abstracciones.Modelos.Usuario>(consulta.FirstOrDefault());
        }
    }
}
