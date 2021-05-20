using System;
namespace IMDb.Domain.Models
{
    class ActorMovie
    {


        public ActorMovie(int actorId, int movieId)
        {
            ActorId = actorId;
            MovieId = movieId;
        }

        public ActorMovie(Actor actor, Movie movie)
        {
            Actor = actor;
            Movie = movie;
        }

        public int ActorId { get; protected set; }
        public int MovieId { get; protected set; }
        public Actor Actor { get; protected set; }
        public Movie Movie { get; protected set; }
    }
}
