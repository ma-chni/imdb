using System;
namespace IMDb.Domain.Models
{
    class Address
    {
        public Address(string street, string city, string postcode)
        {
            Street = street;
            City = city;
            Postcode = postcode;
        }

        public int Id { get; protected set; }
        public string Street { get; protected set; }
        public string City { get; protected set; }
        public string Postcode { get; protected set; }
        public int ActorId { get; protected set; }
        public Actor Actor { get; protected set; }
    }
}
