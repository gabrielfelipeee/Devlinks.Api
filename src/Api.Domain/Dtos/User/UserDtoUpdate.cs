using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdate : UserBaseDto
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Slug { get; set; }
    }
}
