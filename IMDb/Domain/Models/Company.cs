using System;
using System.Collections.Generic;

namespace IMDb.Domain.Models
{
    class Company
    {
        public Company(string name)
        {
            Name = name;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public List<Movie> Movie { get; protected set; } = new List<Movie>();
    }
}
