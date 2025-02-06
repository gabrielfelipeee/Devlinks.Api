namespace Api.Domain.Entities
{
    public class LinkEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? Platform { get; set; }
        public string Link { get; set; }
    }
}
