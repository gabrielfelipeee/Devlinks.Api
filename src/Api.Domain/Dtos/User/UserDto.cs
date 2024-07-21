namespace Api.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set;}
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}