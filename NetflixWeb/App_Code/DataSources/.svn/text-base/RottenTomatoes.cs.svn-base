using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Helpers;

using CherryTomato;

public class RottenTomatoes
{


    public static CherryTomato.Entities.Movie SearchMoviesByTitle(string title, string year)
    {
        string apiKey = "x47y5zn3w973qx9jwpnhr9je";
        var tomato = new CherryTomato.Tomato(apiKey);
        var movies = tomato.FindMovieByQuery(title);
        string altTitle = title;
        if (title.StartsWith("The "))
            altTitle = title.Replace("The ", "");
        
        //string url = "http://api.rottentomatoes.com/api/public/v1.0/movies.json?q=" + HttpUtility.UrlEncode( title ) + "&page_limit=10&page=1&apikey=" + apiKey;
        int yearAsInt = int.Parse(year);
        //&& m.Year == yearAsInt
        CherryTomato.Entities.Movie result = null;
        if (movies.Count() == 1)
            result = movies.FirstOrDefault();
        else
        {
            foreach (var m in movies)
            {
                if (m.Title.ToLower().Trim() == title.ToLower().Trim() || m.Title.ToLower().Trim() == altTitle.ToLower().Trim())
                {
                    result = m;
                    break;
                }
            }
        }
        return result;
    }
}