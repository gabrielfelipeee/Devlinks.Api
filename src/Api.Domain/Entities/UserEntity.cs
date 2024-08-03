namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
    }
}
