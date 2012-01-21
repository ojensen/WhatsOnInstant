using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Helpers;

public partial class movieDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string title = "Not Set";
        string id = "";
        if (!String.IsNullOrEmpty(Page.RouteData.Values["title"] as string))
            title = Page.RouteData.Values["title"] as string;
        if (!String.IsNullOrEmpty(Page.RouteData.Values["id"] as string))
            id = Page.RouteData.Values["id"] as string;
        lblTitle.Text = title;

        if (id != string.Empty)
        {
            dynamic movie = Netflix.GetById(id);
            lblDescription.Text = movie[0].Synopsis;
            imgMovie.ImageUrl = movie[0].BoxArt.LargeUrl;
            imgMovie.AlternateText = title;
        }
    }
}