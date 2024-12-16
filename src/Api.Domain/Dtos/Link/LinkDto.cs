namespace Api.Domain.Dtos.Link
{
    public class LinkDto : LinkBaseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
