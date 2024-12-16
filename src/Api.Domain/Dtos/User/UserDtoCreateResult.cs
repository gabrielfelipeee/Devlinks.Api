namespace Api.Domain.Dtos.User
{
    public class UserDtoCreateResult : UserBaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set;}
    }
}
