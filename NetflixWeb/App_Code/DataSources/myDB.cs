using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for myDB
/// </summary>
public class myDB
{
		public static IList<MovieModel> GetMoviesByRottenTomatoCriticRating( int count = 10, int skip = 0)
        {
            try
            {
                MoviesDBDataContext db = new MoviesDBDataContext();
                var movies = (from m in db.RtMovies
                              where m.CriticsRating != ""
                             select m).OrderBy(m => m.CriticsRating).ThenByDescending(m => m.CriticsScore).Skip(skip).Take(count).ToList();

                List<MovieModel> results = new List<MovieModel>();
                foreach (var m in movies)
                {
                    MovieModel t = new MovieModel(m.NetflixMovy, m);
                    results.Add(t);
                }
                
                return results;
            }
            catch
            {
                return null;
            }
        }

        public static IList<MovieModel> GetMoviesByRottenTomatoAudienceRating(int count = 10, int skip = 0)
        {
            try
            {
                MoviesDBDataContext db = new MoviesDBDataContext();
                var movies = (from m in db.RtMovies
                              select m).OrderByDescending(m => m.AudienceScore).Skip(skip).Take(count).ToList();

                List<MovieModel> results = new List<MovieModel>();
                foreach (var m in movies)
                {
                    MovieModel t = new MovieModel(m.NetflixMovy, m);
                    results.Add(t);
                }

                return results;
            }
            catch
            {
                return null;
            }
        }
}