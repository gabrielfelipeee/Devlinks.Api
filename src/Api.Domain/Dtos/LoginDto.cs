using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        [MaxLength(60, ErrorMessage = "Senha deve ter no máximo {1} caracteres")]
        public string Password { get; set; }
    }
}