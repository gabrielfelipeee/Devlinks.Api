using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreate
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(60, ErrorMessage = "Email deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório")]
        [MaxLength(40, ErrorMessage = "Senha deve ter no máximo {1} caracteres")]
        public string Password { get; set; }
    }
}
