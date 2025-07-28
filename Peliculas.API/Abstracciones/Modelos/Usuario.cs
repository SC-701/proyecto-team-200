using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Usuario
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public int IdEstado { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Apellido { get; set; }

    }
    public class UsuarioEditar
    {
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Apellido { get; set; }

    }
}
