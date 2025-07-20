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

    }
}
