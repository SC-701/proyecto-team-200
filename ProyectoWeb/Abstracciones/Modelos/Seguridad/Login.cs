using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
    public class LoginBase
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "Solo se permiten letras y espacios")]
        public string? NombreUsuario { get; set; }

        public string? PasswordHash { get; set; }
        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "Debe ser un correo electronico valido")]
        public string CorreoElectronico { get; set; }
    }
    public class Login : LoginBase
    {
        [Required]
        public Guid Id { get; set; }
    }
    public class LoginRequest : LoginBase
    {
        [Required]
        public string Password { get; set; }
    }
}
