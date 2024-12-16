namespace Api.Domain.Dtos.User
{
    public class UserDto : UserBaseDto
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
