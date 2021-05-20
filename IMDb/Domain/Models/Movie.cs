using System;
using System.Collections.Generic;

namespace IMDb.Domain.Models
{
    class Movie
    {
        public Movie(string title, string description, int year, string genre, Company company)
        {
            Title = title;
            Description = description;
            Year = year;
            Genre = genre;
            Company = company;
        }

        public Movie()
        {

        }

        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int Year { get; protected set; }
        public string Genre { get; protected set; }
        public Company Company { get; protected set; }
        public int CompanyId { get; protected set; }
        public List<ActorMovie> Actors { get; protected set; } = new List<ActorMovie>();
    }
}
