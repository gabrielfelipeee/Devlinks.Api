using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Link
{
    public class LinkDtoUpdate : LinkBaseDto
    {
        public Guid Id { get; set; }
    }
}
