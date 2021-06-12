using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using Playground.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground
{
    public class Seed : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Faker<Address> Addresses { get; }

        public Faker<Customer> Customers { get; }

        public Faker<Invoice> Invoices { get; }

        public Seed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Addresses = new Faker<Address>()
                .CustomInstantiator(f => new()
                {
                    Line1 = f.Address.StreetAddress(),
                    City = f.Address.City(),
                    State = f.Address.StateAbbr(),
                    Zip = f.Address.ZipCode()
                });

            Customers = new Faker<Customer>()
                .CustomInstantiator(f => new Customer(f.Company.CompanyName()))
                .RuleFor(f => f.Phone, f => f.Phone.PhoneNumber("+1##########"))
                .RuleFor(f => f.Address, f => Addresses.Generate());

            Invoices = new Faker<Invoice>()
                .CustomInstantiator(f => new Invoice(OffsetDateTime.FromDateTimeOffset(f.Date.PastOffset())))
                .RuleFor(f => f.Total, f => f.Random.Decimal(min: 100, max: 10_000));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<PlaygroundDbContext>();
            if (!await dbContext.Database.EnsureCreatedAsync())
                return;

            await dbContext.Customers.AddRangeAsync(Customers.Generate(200));
            await dbContext.Invoices.AddRangeAsync(Invoices.Generate(1000));

            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
