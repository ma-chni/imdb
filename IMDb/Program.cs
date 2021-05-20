using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IMDb.Data;
using IMDb.Domain.Models;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace IMDb
{
    class Program
    {
        static IMDbContext Context = new IMDbContext();
        static void Main()
        {
            bool shouldRun = true;
            while (shouldRun)
            {
                Clear();
                WriteLine("1. Lägg till skådespelare");
                WriteLine("2. Lista skådespelare");
                WriteLine("3. Lägg till filmbolag");
                WriteLine("4. Lägg till film");
                WriteLine("5. Lägg till skådespelare till film");
                WriteLine("6. Lista filmer");
                WriteLine("7. Avsluta");
                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        RegisterActor();
                        break;
                    case ConsoleKey.D2:
                        DisplayActor();
                        break;
                    case ConsoleKey.D3:
                        RegisterCompany();
                        break;
                    case ConsoleKey.D4:
                        RegisterMovie();
                        break;
                    case ConsoleKey.D5:
                        RegisterActorToMovie();
                        break;
                    case ConsoleKey.D6:
                        DisplayMovie();
                        break;
                    case ConsoleKey.D7:
                        shouldRun = false;
                        break;
                }
            }
        }

        private static void DisplayMovie()
        {
            List<Movie> movieList = Context.Movie
                .Include(movie => movie.Company)
                .Include(x => x.Actors)
                .ThenInclude(x => x.Actor).ToList();

                foreach (var movie in movieList)
                {
                WriteLine($"Titel: {movie.Title}");
                WriteLine($"Regissör: {movie.Company.Name}");
                WriteLine($"År: {movie.Year}");

                WriteLine("Skådespelare:");
                foreach (var actor in movie.Actors)
                {
                     WriteLine($"{actor.Actor.FirstName} {actor.Actor.LastName}");
                }
                WriteLine("---------------------------------");
            }

            ConsoleKeyInfo keyPressed;
            bool escapePressed = false;

            do
            {
                keyPressed = ReadKey(true);

                escapePressed |= keyPressed.Key == ConsoleKey.Escape;

            } while (!escapePressed);
        }

        private static void RegisterActorToMovie()
        {
            {
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Skådespelare (personnr):");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();
                    Actor actor = Context.Actor.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);

                    WriteLine("Film (titel):");
                    string title = ReadLine();
                    Movie movie = Context.Movie.FirstOrDefault(x => x.Title == title);
                    WriteLine();
                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                List<ActorMovie> actorMovies = Context.ActorMovie.ToList();

                                if (actorMovies != null)
                                {
                                    if (actor == null)
                                    {
                                        WriteLine("Skådespelare finns ej");
                                        Thread.Sleep(3000);
                                    }
                                    else if (movie == null)
                                    {
                                        WriteLine("Film finns ej");
                                        Thread.Sleep(3000);
                                    }
                                    else
                                    {
                                        ActorMovie actorMovie = new ActorMovie(actor, movie);
                                        Context.ActorMovie.Add(actorMovie);
                                        Context.SaveChanges();
                                        WriteLine("Skådespelare tillagd till film");
                                        Thread.Sleep(3000);
                                    }
                                }
                      
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void RegisterMovie()
        {
            {
                bool movieExists = true;
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Titel:");
                    string title = ReadLine();
                    WriteLine();

                    WriteLine("Beskrivning:");
                    string description = ReadLine();
                    WriteLine();

                    WriteLine("År:");
                    int year = int.Parse(ReadLine());
                    WriteLine();

                    WriteLine("Genre:");
                    string genre = ReadLine();
                    WriteLine();

                    WriteLine("Filmbolag:");
                    string companyName = ReadLine();
                    Company company = Context.Company.FirstOrDefault(x => x.Name == companyName);
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                if (company != null)
                                {

                                    List<Movie> movieList = Context.Movie.ToList();
                                    if (movieList.Count == 0)
                                    {
                                        Movie movie = new Movie(title, description, year, genre, company);
                                        SaveMovie(movie);
                                        WriteLine("Film tillagd");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    for (int i = 0; i < movieList.Count; i++)
                                    {
                                        if (movieList[i].Title == title)
                                        {
                                            WriteLine("Film finns redan");
                                            Thread.Sleep(3000);
                                            break;
                                        }
                                        if (movieExists)
                                        {
                                            Movie movie = new Movie(title, description, year, genre, company);
                                            SaveMovie(movie);
                                            WriteLine("Film tillagd");
                                            Thread.Sleep(3000);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    WriteLine("Ogiltigt filmbolag");
                                    Thread.Sleep(3000);
                                }
                     
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void SaveMovie(Movie movie)
        {
            Context.Movie.Add(movie);
            Context.SaveChanges();
        }

        private static void RegisterCompany()
        {
            {
                bool companyExists = true;
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Namn:");
                    string name = ReadLine();
                    WriteLine();


                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                List<Company> companyList = Context.Company.ToList();
                                if (companyList.Count == 0)
                                {
                                    Company company = new Company(name);
                                    SaveCompany(company);
                                    WriteLine("Filmbolag tillagd");
                                    Thread.Sleep(3000);
                                    break;
                                }
                                for (int i = 0; i < companyList.Count; i++)
                                {
                                    if (companyList[i].Name == name)
                                    {
                                        WriteLine("Filmbolag finns redan");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    if (companyExists)
                                    {
                                        Company company = new Company(name);
                                        SaveCompany(company);
                                        WriteLine("Filmbolag tillagd");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                }
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void SaveCompany(Company company)
        {
            Context.Company.Add(company);
            Context.SaveChanges();
        }

        private static void DisplayActor()
        {
            List<Actor> actorList = Context.Actor.Include(actor => actor.Address).ToList();
            WriteLine("Namn\t\t\t\t\tAdress");
            WriteLine("------------------------------------------------------------");
            foreach (var actor in actorList)
            {
                WriteLine($"{actor.FirstName} {actor.LastName}, {actor.SocialSecurityNumber}              {actor.Address.Street}, {actor.Address.Postcode} {actor.Address.City}");
            }

            ConsoleKeyInfo keyPressed;

            bool escapePressed = false;

            do
            {
                keyPressed = ReadKey(true);

                escapePressed |= keyPressed.Key == ConsoleKey.Escape;

            } while (!escapePressed);
        }

        private static void RegisterActor()
        {
            {
                bool actorExists = true;
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Förnamn:");
                    string firstName = ReadLine();
                    WriteLine();

                    WriteLine("Efternamn:");
                    string lastName = ReadLine();
                    WriteLine();

                    WriteLine("Personnummer:");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();

                    WriteLine("Gata:");
                    string street = ReadLine();
                    WriteLine();

                    WriteLine("Ort:");
                    string city = ReadLine();
                    WriteLine();

                    WriteLine("Postnummer:");
                    string postcode = ReadLine();
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                List<Actor> actorList = Context.Actor.ToList();
                                if (actorList.Count == 0)
                                {
                                    Address address = new Address(street, city, postcode);
                                    Actor actor = new Actor(firstName, lastName, socialSecurityNumber, address);
                                    SaveActor(actor);
                                    WriteLine("Skådespelare tillagd");
                                    Thread.Sleep(3000);
                                    break;
                                }
                                for (int i = 0; i < actorList.Count; i++)
                                {
                                    if (actorList[i].SocialSecurityNumber == socialSecurityNumber)
                                    {
                                        WriteLine("Skådespelare finns redan");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    if (actorExists)
                                    {
                                        Address address = new Address(street, city, postcode);
                                        Actor actor = new Actor(firstName, lastName, socialSecurityNumber, address);
                                        SaveActor(actor);
                                        WriteLine("Skådespelare tillagd");
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                }
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void SaveActor(Actor actor)
        {
            Context.Actor.Add(actor);
            Context.SaveChanges();

        }
    }
}
