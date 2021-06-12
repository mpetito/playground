using AutoMapper;
using NodaTime;
using Playground.Domain;

namespace Playground.Features.Invoices
{
    public record Model(int Id, OffsetDateTime CreatedAt)
    {
        public class Mapping : Profile
        {
            public Mapping() => CreateMap<Invoice, Model>();
        }
    }
}
