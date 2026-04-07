using System.ComponentModel.DataAnnotations;

namespace TravelWebApp.Infrastrcuture.api.dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name cannot be empty ")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email cannot be null ")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
