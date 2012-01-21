using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Helpers;
using CherryTomato.Entities;

public partial class Home : System.Web.UI.Page
{

    protected enum pageMode
    {
        movies,
        tv,
        all
    }
    protected pageMode _DisplayMode;
    protected int _pageSize = 10;
    protected int _pageNumber = 1;
    protected string _sort = "Rotten%20Tomatos%3A%20Critcs";


    private void SetupPaging()
    {
        if( !String.IsNullOrEmpty( Request.QueryString["page"]) )
            _pageNumber = int.Parse(Request.QueryString["page"]);
        else if (!String.IsNullOrEmpty(Page.RouteData.Values["pageNum"] as string))
            _pageNumber = int.Parse(Page.RouteData.Values["pageNum"] as string);

        string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/top/" + _DisplayMode.ToString() + "/page/";

        btnNext.HRef = url + ( _pageNumber + 1).ToString();
        btnNext2.HRef = btnNext.HRef;
        if (_pageNumber != 1)
        {
            btnPrev.HRef = url + (_pageNumber - 1).ToString();
            adPanel.Visible = true;
        }
        btnPrev2.HRef = btnPrev.HRef;
    }
    private void SetupSort()
    {
        if (Request.Cookies["sort"] != null)
            _sort = Request.Cookies["sort"].Value;

        pnlSort.Visible = true;
        string[] sortOptions = { "Rotten Tomatos: Critcs", "Rotten Tomatos: Audience", "Netflix User Ratings" };
        ddlSort.DataSource = sortOptions;
        ddlSort.DataBind();
    }
    private void SetPageModeBasedOnQueryString()
    {
        string filter = string.Empty;
        if (!String.IsNullOrEmpty(Request.QueryString["filter"]))
        {
            filter = Request.QueryString["filter"];
        }
        else if (!String.IsNullOrEmpty(Page.RouteData.Values["category"] as string))
        {
            filter = Page.RouteData.Values["category"].ToString();
        }
        if (filter == "tv")
        {
            _DisplayMode = pageMode.tv;
        }
        else if (filter == "movies")
        {
            _DisplayMode = pageMode.movies;
        }
        else if (filter == "all")
        {
            _DisplayMode = pageMode.all;
        }
        else
        {
            _DisplayMode = pageMode.movies;
        }
    }
    
    
    private List<MovieModel> SetupRottenTomatoRatings(List<MovieModel> movies)
    {
        List<MovieModel> movieData = new List<MovieModel>();
        MoviesDBDataContext db = new MoviesDBDataContext();
        foreach (var m in movies)
        {
            RtMovy rtMovie = null;
            if (m.Netflix.Type == "Movie")//type = movie
            {
                int id = m.Netflix.MovieID;
                rtMovie = (from movie in db.RtMovies
                           where movie.MovieID == id
                           select movie).FirstOrDefault();
                if (rtMovie == null)
                {
                    Movie rt = RottenTomatoes.SearchMoviesByTitle(m.Netflix.Title, m.Netflix.ReleaseYear.ToString());
                    if (rt != null)
                    {
                        rtMovie = new RtMovy();
                        rtMovie.MovieID = m.Netflix.MovieID;

                        var a = rt.Ratings.Where(r => r.Type == "audience_rating").FirstOrDefault();
                        var b = rt.Ratings.Where(r => r.Type == "audience_score").FirstOrDefault();
                        var c = rt.Ratings.Where(r => r.Type == "critics_rating").FirstOrDefault();
                        var d = rt.Ratings.Where(r => r.Type == "critics_score").FirstOrDefault();
                        var e = rt.Links.Where(r => r.Type == "alternate").FirstOrDefault();

                        if (a != null)
                            rtMovie.AudienceRating = a.Score;
                        else
                            rtMovie.AudienceRating = "Unknown";
                        if (b != null)
                            rtMovie.AudienceScore = b.Score;
                        if (c != null)
                            rtMovie.CriticsRating = c.Score;
                        else
                            rtMovie.CriticsRating = "Unknown";
                        if (d != null)
                            rtMovie.CriticsScore = d.Score;
                        if (e != null)
                            rtMovie.WebsiteLink = e.Url;

                        if (rtMovie.CriticsRating != null)
                            rtMovie.CriticsRating = rtMovie.CriticsRating.Replace("\"", "").ToLower();
                        if (rtMovie.AudienceRating != null)
                            rtMovie.AudienceRating = rtMovie.AudienceRating.Replace("\"", "").ToLower();

                        if (rtMovie.CriticsScore == "-1")
                        {
                            rtMovie.CriticsScore = rtMovie.AudienceScore;
                            rtMovie.CriticsRating = "popcorn";
                        }

                        db.RtMovies.InsertOnSubmit(rtMovie);


                    }
                    else
                    {
                        rtMovie = new RtMovy();
                        rtMovie.WebsiteLink = string.Empty;
                        rtMovie.AudienceRating = "";
                        rtMovie.AudienceScore = "0";
                        rtMovie.CriticsScore = "0";
                        rtMovie.CriticsRating = "";
                        rtMovie.MovieID = m.Netflix.MovieID;
                        db.RtMovies.InsertOnSubmit(rtMovie);
                    }




                }

            }
            else
            {
                rtMovie = new RtMovy();
                rtMovie.WebsiteLink = string.Empty;
                rtMovie.AudienceRating = "";
                rtMovie.AudienceScore = "0";
                rtMovie.CriticsScore = "0";
                rtMovie.CriticsRating = "";
            }
            movieData.Add( new MovieModel( m.Netflix, rtMovie));
          //  movieData.Add(new MovieModel(m.NetflixID, m.Title, m.ReleaseYear, m.Rating, m.Synopsis, m.ShortSynopsis, m.AverageRating, m.LargeUrl, m.Runtime, rtMovie.AudienceScore, rtMovie.WebsiteLink, rtMovie.CriticsScore, rtMovie.AudienceRating, rtMovie.CriticsRating, m.ExpiresDate, m.ApiId.Value));
        }
        db.SubmitChanges();

        return movieData;
    }
    private List<MovieModel> SaveMoviesToDB(IList<dynamic> movies)
    {
        List<MovieModel> netflixMovies = new List<MovieModel>();
        if (movies != null)
        {

            MoviesDBDataContext db = new MoviesDBDataContext();
            foreach (var movie in movies)
            {
                string id = movie.Id;
                NetflixMovy movieFromDb = (from m in db.NetflixMovies
                                           where m.NetflixID == id
                                           select m).FirstOrDefault();
                if (movieFromDb == null)//If not already in db add it
                {
                    MovieModel nm = new MovieModel(movie);

                    db.NetflixMovies.InsertOnSubmit(nm.Netflix);
                    movieFromDb = nm.Netflix;
                }
                MovieModel mm = new MovieModel(movieFromDb, null);
                netflixMovies.Add(mm);
            }
            db.SubmitChanges();
        }
        return netflixMovies;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageModeBasedOnQueryString();
        SetupPaging();//Paging needs page mode to be set

        
        
        if (_DisplayMode == pageMode.all)
        {
            IList<dynamic> movies = Netflix.GetTopAllByRating(_pageSize, (_pageNumber - 1) * _pageSize);
            rptMovies.DataSource = SaveMoviesToDB(movies);
            btnAll.Attributes.Add("class", "button");
        }
        else if (_DisplayMode == pageMode.tv)
        {
            IList<dynamic> movies = Netflix.GetTopTvByRating(_pageSize, (_pageNumber - 1) * _pageSize);
            rptMovies.DataSource = SaveMoviesToDB(movies);
            btnTv.Attributes.Add("class", "button");
        }
        else
        {
            SetupSort();

           
            if (_sort == "Rotten%20Tomatos%3A%20Critcs")
            {
                IList<MovieModel> movies = myDB.GetMoviesByRottenTomatoCriticRating(_pageSize, (_pageNumber - 1) * _pageSize);
                rptMovies.DataSource = movies;
            }
            else if (_sort == "Rotten%20Tomatos%3A%20Audience")
            {
                IList<MovieModel> movies = myDB.GetMoviesByRottenTomatoAudienceRating(_pageSize, (_pageNumber - 1) * _pageSize);
                rptMovies.DataSource = movies;
            }
            else
            {//Netflix user ratings
                IList<dynamic> movies = Netflix.GetTopMoviesByRating(_pageSize, (_pageNumber - 1) * _pageSize);
                List<MovieModel> netflixMovies = SaveMoviesToDB(movies);
                rptMovies.DataSource = SetupRottenTomatoRatings(netflixMovies);
            }
           
                
            
            btnMovies.Attributes.Add("class", "button");

            
        }
       
      
       
       rptMovies.DataBind();


    }
}