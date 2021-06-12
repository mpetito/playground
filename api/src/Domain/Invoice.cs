using NodaTime;

namespace Playground.Domain
{
    public class Invoice
    {
        public Invoice(OffsetDateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public int Id { get; init; }

        public OffsetDateTime CreatedAt { get; init; }

        public decimal Total { get; set; }
    }
}
