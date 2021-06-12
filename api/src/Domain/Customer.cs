using Playground.Features.Customers;

namespace Playground.Domain
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public Customer(Create.Form form)
        {
            Name = form.Name;
        }

        public int Id { get; init; }

        public string Name { get; set; }

        public string? Phone { get; set; }

        public Address Address { get; set; } = new Address();

        public void Update(Update.Form form)
        {
            Name = form.Name;
            Phone = form.Phone;
            Address = form.Address;
        }
    }
}
