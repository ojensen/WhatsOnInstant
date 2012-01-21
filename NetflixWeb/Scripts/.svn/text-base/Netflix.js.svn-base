/*Netflix OData URIs*/
var MOVIE_LANGUAGES_URI = "http://odata.netflix.com/Catalog/Languages";
var MOVIE_GENRES_URI = "http://odata.netflix.com/Catalog/Genres";
//$filter=Instant/Available eq true
var MOVIE_LISTING_URI = "http://odata.netflix.com/Catalog/Titles?$filter=Type eq 'Movie' and Instant/Available eq true&$orderby=ReleaseYear desc,Name asc";
var SEASONS_LISTING_URI = "http://odata.netflix.com/Catalog/Titles?$filter=Type eq 'Season' and Instant/Available eq true&$orderby=ReleaseYear desc,Name asc";

var MOVIE_BY_LANGUAGES_URI = "http://odata.netflix.com/Catalog/Languages('*lang*')/Titles?$filter=Instant/Available eq true&$orderby=ReleaseYear desc,Name asc";
var MOVIE_BY_GENRES_URI = "http://odata.netflix.com/Catalog/Genres('*genre*')/Titles?$filter=Instant/Available eq true&$orderby=ReleaseYear desc,Name asc";
var MOVIE_BY_NAME_URI = "http://odata.netflix.com/Catalog/Titles?$filter=startswith(Name,'*name*') and Instant/Available eq true&$orderby=ReleaseYear desc,Name asc";

var MOVIE_BY_LANGUAGES_URI = "http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc";
var MOVIE_BY_LANGUAGES_URI_Movie = "http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and Type eq 'Movie' and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc";
var MOVIE_BY_LANGUAGES_URI_Tv = "http://odata.netflix.com/Catalog/Languages('English')/Titles?$filter=Instant/Available eq true and Type ne 'Movie' and AverageRating gt 3&$orderby=AverageRating desc,ReleaseYear desc,Name asc";


//=AverageRating gt 2 
/*End - Netflix OData URIs*/

/*OData Setup*/
OData.defaultHttpClient.enableJsonpCallback = true;

var genreCache = datajs.createDataCache({
                                        name: "genres",
                                        source: MOVIE_GENRES_URI,
                                        prefetchSize: -1
                                    });
                                    
var languageCache = datajs.createDataCache({
                                        name: "languages",
                                        source: MOVIE_LANGUAGES_URI,
                                        prefetchSize: -1
                                    });

var moviesCache = null;    
/*End - OData Setup*/

/*Globals*/
var movieCriteria = "";
var movieCriteriaValue = "";
var genreCount = 485; //need to change this to see how readrange can read upto max
var languageCount = 124; //need to change this to see how readrange can read upto max
var startPage = 0;
var pageSize = 20;
/*Globals End*/


 function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

//Main Function
function domReady() 
{
    $("#Loading").center(true);

    //$("#listingmessages").hide();

    FillGenres();

    FillLanguages();
}

$(document).ready(function() {
  // Handler for .ready() called.
      BrowseByLanguages();
});

//End - Main Function

//Page Functions
//Calls Genres Collection on odata.netflix.com
function FillGenres() 
{
    genreCache.readRange(0, genreCount).then(FillGenresCallBack, ErrorCallBack);
}

//Calls Languages Collection on odata.netflix.com
function FillLanguages() 
{
    languageCache.readRange(0,languageCount).then(FillLanguagesCallBack, ErrorCallBack);
}

//Browse Titles collection where Type = Movie on odata.netflix.com
function BrowseMovies() 
{
    HideListingCriteria();
    movieCriteria = "All"
    movieCriteriaValue = "Movies";
    BuildMoviesCache(MOVIE_LISTING_URI)
    startPage = 0;
    FetchTitles(startPage)
}

//Browse Titles collection where Type = Seasons on odata.netflix.com
function BrowseSeasons() 
{
    HideListingCriteria();
    movieCriteria = "All"
    movieCriteriaValue = "Seasons";
    BuildMoviesCache(SEASONS_LISTING_URI)
    startPage = 0;
    FetchTitles(startPage)
}

//Browse Titles collection by Language on odata.netflix.com
function BrowseByLanguages() 
{
    HideListingCriteria()
    var selectedLanguage = $("#Language option:selected").text();
    if (selectedLanguage == "Select Language")
        return;
    movieCriteria = "Language"
    movieCriteriaValue = selectedLanguage;
    $("#Genre").attr('selectedIndex', 0);
    var uri = MOVIE_BY_LANGUAGES_URI.replace("*lang*", selectedLanguage);
    var filter = getParameterByName("filter");
    if(filter == "movies")
    {
        uri = MOVIE_BY_LANGUAGES_URI_Movie.replace("*lang*", selectedLanguage);
        ('#btnMovies') 
        }
    else if(filter == "tv")
    {
        uri = MOVIE_BY_LANGUAGES_URI_Tv.replace("*lang*", selectedLanguage);
        }
    else
    {

    }


    BuildMoviesCache(uri)
    startPage = 0;
    FetchTitles(startPage)
}

//Browse Titles collection by Genres on odata.netflix.com
function BrowseByGenres() 
{
    HideListingCriteria();
    var selectedGenre = $("#Genre option:selected").text();
    if (selectedGenre == "Select Genre")
        return;
    movieCriteria = "Genre"
    movieCriteriaValue = selectedGenre;
    $("#Language").attr('selectedIndex', 0);
    var uri = MOVIE_BY_GENRES_URI.replace("*genre*", selectedGenre);
    BuildMoviesCache(uri)
    startPage = 0;
    FetchTitles(startPage)
}

//Browse Titles collection by Name on odata.netflix.com
function BrowseByName() 
{
    HideListingCriteria();
    $("#Language").attr('selectedIndex', 0);
    $("#Genre").attr('selectedIndex', 0);
    var movieNameToSearch = $("#txtName").val();
    movieCriteria = "Name"
    movieCriteriaValue = movieNameToSearch;
    var uri = MOVIE_BY_NAME_URI.replace("*name*", movieNameToSearch);
    BuildMoviesCache(uri)
    startPage = 0;
    FetchTitles(startPage)
}
//End - Page Functions

//Helpers
//Shows loading animation
function BuildMoviesCache(uri) 
{
    if(moviesCache != null)
        moviesCache.destroy();

    moviesCache = datajs.createDataCache({
                                                name: "movies",
                                                source: uri,
                                                pageSize: pageSize,
                                                prefetchSize: 10,
                                            });
}
function ShowLoading() 
{
    $("#movieListingContainer").fadeOut();
    $("#Loading").fadeIn();
}

//Hides loading animation
function HideLoading() 
{
    $("#Loading").fadeOut();
    $("#movieListingContainer").fadeIn();
}

//Generic Error handling
function ErrorCallBack(err) 
{
    alert("Error occurred " + err.message);
}

//Fills the dropdown with data passed
function FillDropDowns(cntrl, data) 
{
    var cntrlId = '#' + cntrl;
    $(cntrlId + ' option').remove()
    $(cntrlId).append('<option value=-1>Select ' + cntrl + '</option>');
    for (var i = 0; i < data.length; i++) {
        $(cntrlId).append('<option>' + data[i].Name + '</option>');
    }
}

//Callback for fill genres call
function FillGenresCallBack(data) 
{
    
    FillDropDowns("Genre", data);
}

//Callback for fill languages call
function FillLanguagesCallBack(data, request) {
    FillDropDowns("Language", data);
}

//Shows the movie search criteria div
function SetListingCriteria() 
{
   // $("#listingmessages").show();
    //$("#criteria").text(movieCriteria);
    //$("#criteriaValue").text(movieCriteriaValue);
}

//Hides the movie search criteria div
function HideListingCriteria() 
{
   // $("#listingmessages").hide();
    //$("#criteria").text(movieCriteria);
    //$("#criteriaValue").text(movieCriteriaValue);
}

//Fetches Titles based on the URI passed from odata.netflix.com
function FetchTitles(page) 
{
    ShowLoading();
    var startIndex = page * pageSize;
    moviesCache.readRange(startIndex, pageSize).then(FetchTitlesCallBack);
    $("#pagenumber").html("Page " + (startPage+1))
}

//Callback for fetch titles
function FetchTitlesCallBack(data) 
{
    $("#movieListingContainer").empty();
    HideLoading();
    var results = data;
    $.get('scripts/templates/moviedisplay.txt', function (template) 
    //$.get('moviedisplay.txt', function (template) 
            {
                $.tmpl(template, results, 
                {
                    runtime: function () 
                    {
                        var runTime = this.data["Runtime"];
                        if (runTime == null)
                            return "N/A";
                        else
                            return GetRunTimeInMinutes(runTime);
                    }
                }).appendTo('#movieListingContainer');
            }
        );
    SetListingCriteria();
    
    setTimeout(ApplyRating, 500)

    SetPager(startPage, results.length);
}

//Applies rating on the rating div 
function ApplyRating() 
{
    $("div[id^='movieratingdiv']").each(function () 
    {
        var avgRating = $(this).attr('avgrating');
        if(avgRating == null)
        {
            $(this).text("No Rating")
        }
        else
        {
            $(this).raty(
                            {
                                start: avgRating,
                                readOnly: true,
                                half: true
                            }
                        );
        }
    });

    $('div .moviesDiv').each(function () { $(this).corners() });
}

//Converts movie runtime to minutes
function GetRunTimeInMinutes(runtime) 
{
    var minutes = Math.round(runtime / 60);
    return minutes + " minutes";
}

function NextPage() {
    if (startPage < 0)
        startPage = 0;
    else
        startPage++;
    FetchTitles(startPage);
    
}

function PrevPage() {
    if (startPage < 0)
        startPage = 0;
    else
        startPage--;
    FetchTitles(startPage);
    
}

function SetPager(page, currentPageRecords)
{
    if((page+1) == 1)
    {
        $("#btnPrev").attr("disabled", true);
    }
    else
    {
        $("#btnPrev").attr("disabled", false);
    }
    if(currentPageRecords < pageSize)
    {
        $("#btnNext").attr("disabled", true);
    }
    else
    {
        $("#btnNext").attr("disabled", false);
    }
}
//End - Helpers