namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Slug { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
