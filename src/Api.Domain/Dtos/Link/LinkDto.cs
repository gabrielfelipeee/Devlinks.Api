namespace Api.Domain.Dtos.Link
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Platform { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
