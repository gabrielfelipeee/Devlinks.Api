using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Link
{
    public class LinkDtoCreate
    {
        [Required(ErrorMessage = "Plataforma é um campo obrigatório")]
        public string Platform { get; set; }

        [Required(ErrorMessage = "Link é um campo obrigatório")]
        public string Link { get; set; }
    }
}
