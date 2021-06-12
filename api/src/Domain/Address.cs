using Microsoft.EntityFrameworkCore;

namespace Playground.Domain
{
    [Owned]
    public record Address
    {
        public string? Line1 { get; init; }

        public string? Line2 { get; init; }

        public string? City { get; init; }

        public string? State { get; init; }

        public string? Zip { get; init; }
    }
}
