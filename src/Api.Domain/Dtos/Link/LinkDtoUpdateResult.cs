namespace Api.Domain.Dtos.Link
{
    public class LinkDtoUpdateResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Platform { get; set; }
        public string Link { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}