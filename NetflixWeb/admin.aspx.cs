using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MoviesDBDataContext db = new MoviesDBDataContext();
        var logs = from l in db.DevLogs
                   select l;
        string response = string.Empty;
        foreach (var log in logs)
            response += log.LogID + " " + log.Message + " " + log.Date + "<br/>";

        Response.Write(response);
    }
}