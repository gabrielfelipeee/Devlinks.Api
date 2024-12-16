using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreate : UserBaseDto
    {
        public string Password { get; set; }
    }
}
