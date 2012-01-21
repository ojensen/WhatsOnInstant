namespace Microsoft.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.Web.Helpers;

    public static class Netflix
    {

        public static IList<dynamic> GetTopAllByRating( int count = 10, int skip = 0)
        {
            try
            {
                string url = "http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc" + "&$top=" + count + "&$skip=" + skip;
                return OData.Get(url);
            }
            catch
            {
                return null;
            }
        }
        public static IList<dynamic> GetTopMoviesByRating( int count = 10, int skip = 0)
        {
            try
            {
                return OData.Get("http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and Type eq 'Movie' and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc" + "&$top=" + count + "&$skip=" + skip);
            }
            catch
            {
                return null;
            }
        }
        public static IList<dynamic> GetTopTvByRating(int count = 10, int skip = 0)
        {
            try
            {
                return OData.Get("http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and Type ne 'Movie' and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc" + "&$top=" + count + "&$skip=" + skip);

            }
            catch
            {
                return null;
            }
        }

        public static dynamic GetById(string Id)
        {
            try
            {
                return OData.Get("http://odata.netflix.com/Catalog/Titles('" + Id + "')");
            }
            catch
            {
                return null;
            }
        }

        public static IList<dynamic> GetTopMoviesByGenre(string genreId, int count = 10, int skip = 0)
        {
            return OData.Get("http://odata.netflix.com/Catalog/Genres('" + genreId + "')/Titles", "$filter=Type eq 'Movie'&$orderby=AverageRating desc" + "&$top=" + count + "&skip=" + skip);
        }

        public static IList<dynamic> GetMatchingTitles(string title)
        {
            return OData.Get("http://odata.netflix.com/Catalog/Titles?$filter=Name eq '" + title + "'");
        }

        public static IList<dynamic> GetMoviesContainingWord(string word)
        {
            return OData.Get("http://odata.netflix.com/Catalog/Titles", "$filter=Type eq 'Movie' and (substringof('" + word + "', Name) or substringof('" + word + "', Synopsis))");
        }

        public static string GetMovieImage(string movieId)
        {
            return "http://odata.netflix.com/Catalog/Titles('" + movieId + "')/$value";
        }

        public static IList<dynamic> GetTitlesByDirector(string directorName)
        {
            var people = OData.Get("http://odata.netflix.com/Catalog/People", "$filter=Name eq '" + directorName + "'");

            if (people.Count() > 0)
            {
                return OData.Get("http://odata.netflix.com/Catalog/People(" + people.First().Id + ")/TitlesDirected");
            }

            return new List<dynamic>();
        }
    }
}
