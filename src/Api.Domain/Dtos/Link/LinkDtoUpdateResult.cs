namespace Api.Domain.Dtos.Link
{
    public class LinkDtoUpdateResult : LinkBaseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
