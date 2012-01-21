using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Home
/// </summary>
public class MovieModel
{

    private NetflixMovy _netflix;
    private RtMovy _rottenTomato;

    public NetflixMovy Netflix
    {
        get { return _netflix; }
    }
    public RtMovy RottenTomato
    {
        get {
            if (_rottenTomato != null)
                return _rottenTomato;
            else
                return new RtMovy();
        }
    }

    public MovieModel(NetflixMovy netflix, RtMovy rottenTomato)
    {
        _netflix = netflix;
        _rottenTomato = rottenTomato;
    }

    
    public MovieModel(dynamic movie)
    {
        NetflixMovy nm = new NetflixMovy();
        nm.NetflixID = movie.Id; nm.Title = movie.Name; nm.ReleaseYear = movie.ReleaseYear; nm.Rating = movie.Rating; nm.Synopsis = movie.Synopsis; nm.ShortSynopsis = movie.ShortSynopsis; nm.AverageRating = movie.AverageRating;
        nm.Type = movie.Type;
        
        if( movie.Runtime != null)
            nm.Runtime = movie.Runtime;
        
        if( movie.Instant != null)
        nm.ExpiresDate = movie.Instant.AvailableTo;

        if( movie.BoxArt != null)
            nm.LargeUrl = movie.BoxArt.LargeUrl;


        string[] apiParts = movie.NetflixApiId.Split('/');
        nm.ApiId = int.Parse(apiParts[apiParts.Count() - 1]);

        _netflix = nm;
        _rottenTomato = null;
    }

    //private string _name;
    //private int _releaseYear;
    //private string _rating;
    //private string _synopsis;
    //private string _shortSynopsis;
    //private double _averageRating;
    //private string _largeUrl;
    //private int _runtime;
    //private DateTime _expiresDate;
    //private int _ApiId;

    //private string _rottenTomatoAudienceScore;
    //private string _rottenTomatoCriticScore;
    //private string _rottenTomatoAudienceRating;
    //private string _rottenTomatoCriticRating;

    //private string _rottenTomatoeLink;
    //private string _id;

    //public string Id
    //{
    //    get { return _id; }
    //    set { _id = value; }
    //}
    //public string Name
    //{
    //    get { return _name; }
    //    set { _name = value; }
    //}
    //public int ReleaseYear
    //{
    //    get { return _releaseYear; }
    //    set { _releaseYear = value; }
    //}
    //public string Rating
    //{
    //    get { return _rating; }
    //    set { _rating = value; }
    //}
    //public string Synopsis
    //{
    //    get { return _synopsis; }
    //    set { _synopsis = value; }
    //}
    //public string ShortSynopsis
    //{
    //    get { return _shortSynopsis; }
    //    set { _shortSynopsis = value; }
    //}
    //public double AverageRating
    //{
    //    get { return _averageRating; }
    //    set { _averageRating = value; }
    //}
    //public string LargeUrl
    //{
    //    get { return _largeUrl; }
    //    set { _largeUrl = value; }
    //}
    //public int Runtime
    //{
    //    get { return _runtime / 60; }
    //    set { _runtime = value; }
    //}
    //public string RottenTomatoesLink
    //{
    //    get
    //    {
    //        return _rottenTomatoeLink;
    //    }
    //    set { _rottenTomatoeLink = value; }
    //}
    //public string RottenTomatoAudienceScore
    //{
    //    get { return _rottenTomatoAudienceScore; }
    //    set { _rottenTomatoAudienceScore = value; }
    //}
    //public string RottenTomatoCriticScore
    //{
    //    get { return _rottenTomatoCriticScore; }
    //    set { _rottenTomatoCriticScore = value; }
    //}
    //public string RottenTomatoAudienceRating
    //{
    //    get { return _rottenTomatoAudienceRating; }
    //    set { _rottenTomatoAudienceRating = value; }
    //}
    //public string RottenTomatoCriticRating
    //{
    //    get { return _rottenTomatoCriticRating; }
    //    set { _rottenTomatoCriticRating = value; }
    //}
    //public DateTime ExpiresDate
    //{
    //    get { return _expiresDate; }
    //    set { _expiresDate = value; }
    //}
    //public int ApiId
    //{
    //    get { return _ApiId; }
    //    set { _ApiId = value; }
    //}

    //public Home(string id, string name, int releaseYear, string rating, string sysnopsis, string shortSynopsis, double averageRating, string largeUrl, int runtime, string rottenTomatoAudienceScore, string rottenTomatoeLink, string rottenTomatoCriticScore, string rottenTomatoAudienceRating, string rottenTomatoCriticRating, DateTime expiresDate, int apiId)
    //{
    //    _id = id;
    //    _name = name;
    //    _releaseYear = releaseYear;
    //    _rating = rating;
    //    _synopsis = sysnopsis;
    //    _shortSynopsis = shortSynopsis;
    //    _averageRating = averageRating;
    //    _largeUrl = largeUrl;
    //    _runtime = runtime;
    //    _rottenTomatoAudienceScore = rottenTomatoAudienceScore;
    //    _rottenTomatoAudienceRating = rottenTomatoAudienceRating;
    //    _rottenTomatoeLink = rottenTomatoeLink;
    //    _rottenTomatoCriticScore = rottenTomatoCriticScore;
    //    _rottenTomatoCriticRating = rottenTomatoCriticRating;
    //    _expiresDate = expiresDate;
    //    _ApiId = apiId;
    //}
}