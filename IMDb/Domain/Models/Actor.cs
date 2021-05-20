using System;
using System.Collections.Generic;

namespace IMDb.Domain.Models
{
    class Actor
    {
        public Actor(string firstName, string lastName, string socialSecurityNumber, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            Address = address;
        }

        public Actor()
        {

        }

        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }
        public Address Address { get; protected set; }
        public List<ActorMovie> Movies { get; protected set; } = new List<ActorMovie>();
    }
}
