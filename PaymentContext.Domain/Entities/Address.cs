using Flunt.Validations;
using PaymentContext.Shared.Entities;
using System.Diagnostics.Contracts;

namespace PaymentContext.Domain.Entities
{
    public class Address : Entity
    {
        public Address(string street, string number, string complement, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Address>()
                .Requires());
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}