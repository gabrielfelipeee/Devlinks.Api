namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdateResult : UserBaseDto
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Slug { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
