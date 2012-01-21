<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link rel="stylesheet" href="/styles/style.css" />
    <script type="text/javascript" src="/scripts/jquery-1.4.1.min.js"></script>
     <%--<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.5.1.min.js"></script>--%>
     <script type="text/javascript" src="/scripts/jquery.raty.js"></script>
      <script type="text/javascript" src="/scripts/jquery.corners.min.js"></script>
      <script type="text/javascript" src="/scripts/jquery.cookie.js"></script>
      <script type="text/javascript" src="http://jsapi.netflix.com/us/api/js/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


<div style="width: 100%;text-align: center; margin-top: 40px;">
    <div style="display: inline-block;">
        <a href="/"><img src="/images/1315541351.jpg" alt="man watching netflix instant" style="width:600px; float:left;" /></a>
        <div class="Homepage-ButtonContainer">
            <div class="Homepage-TagLine">The Best Of Netflix Instant</div>

            <a href="/top/all"><div id="btnAll" class="button disabled" style="display:none" runat="server">All</div></a>
            <a href="/top/movies"><div id="btnMovies" class="button disabled" runat="server" >Movies</div></a>
            <a href="/top/tv"><div id="btnTv" class="button disabled" runat="server" >TV</div></a>
            <asp:Panel ID="pnlSort" runat="server" Visible="false">
                <span>Sort by</span> <asp:DropDownList ID="ddlSort" runat="server" ClientIDMode="Static"></asp:DropDownList>
            </asp:Panel>
        </div>
    </div>

   
</div>



<asp:Panel runat="server" ID="adPanel"  Visible="false">
    <div style="width: 100%;text-align: center; margin-top: 10px;">
    <script type="text/javascript"><!--
        google_ad_client = "ca-pub-6713985493253597";
        /* WhatsOnInstantTop */
        google_ad_slot = "6713771889";
        google_ad_width = 728;
        google_ad_height = 90;
    //-->
    </script>
    <script type="text/javascript"
    src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
    </script>

</asp:Panel>

     <div class="clear"></div>
    <div style="float:right;">
 <!-- Place this tag where you want the +1 button to render -->
                <g:plusone annotation="inline"></g:plusone>

                <!-- Place this render call where appropriate -->
                <script type="text/javascript">
                    (function () {
                        var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                        po.src = 'https://apis.google.com/js/plusone.js';
                        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                    })();
                </script>

</div>
    <div class="pagingContainer">
        <a runat="server" style="padding-right:20px;" class="paging" id="btnPrev">Prev</a><a id="btnNext" class="paging" runat="server">Next</a>
     </div>
    </div>

<asp:Repeater ID="rptMovies" runat="server">
<ItemTemplate>
        <div class='moviesDiv'> 
            <div class='movieTitleCard'>
                <div class="btnContainer">
                    <div class="btnWatch LinkButton" data-id='<%# ((MovieModel)Container.DataItem).Netflix.ApiId %>' >Watch Now</div>  
                    <div class="clear"></div>
                    <div class="btnAddToQueue LinkButton" data-id='<%# ((MovieModel)Container.DataItem).Netflix.ApiId %>' >Add To Queue</div>  
                </div>

                <div class='movieName'><%# ((MovieModel)Container.DataItem).Netflix.Title %></div>
                <span class='movieReleaseYear'><%# ((MovieModel)Container.DataItem).Netflix.ReleaseYear %></span>
                <span class='movieRating'><%# ((MovieModel)Container.DataItem).Netflix.Rating %></span>
                <span class='movieRuntime'><%# ((MovieModel)Container.DataItem).Netflix.Runtime / 60 %> minutes</span>

            </div>
            
	        <div style="padding: 2px">
		        <table>
			        <tr>
				        <td><img src='<%# ((MovieModel)Container.DataItem).Netflix.LargeUrl %>' alt='<%# ((MovieModel)Container.DataItem).Netflix.Title.Replace("'","") + " Cover" %>' /></td>
				        <td valign="top" style="padding:10px;">
					        <div class="movieSynopsis"><%# ((MovieModel)Container.DataItem).Netflix.ShortSynopsis %>  <a href="/Movie/<%# ((MovieModel)Container.DataItem).Netflix.NetflixID %>/<%# ((MovieModel)Container.DataItem).Netflix.Title.ToString().Replace(" ","-").Replace(":","")%>">Read More</a></div> 
					        <br/><br /><br /><br /><br /><br />
					       
                           Expires <%# ((MovieModel)Container.DataItem).Netflix.ExpiresDate.ToShortDateString()%>
                            
                            
				        </td>
                        <td>
                        <div style="float:right;" id="pnlRottenTomatos" runat="server"  >
                            <span id="spnCriticRating"   runat="server" visible='<%# this._sort == "Rotten%20Tomatos%3A%20Critcs" && ((MovieModel)Container.DataItem).RottenTomato.CriticsScore != null && ((MovieModel)Container.DataItem).RottenTomato.CriticsScore != "0" %>' class='<%# "meter " + ((MovieModel)Container.DataItem).RottenTomato.CriticsRating + " numeric" %>' > <%# ((MovieModel)Container.DataItem).RottenTomato.CriticsScore%> % </span> 
                            <span id="spnAudienceRating" runat="server" visible='<%# this._sort == "Rotten%20Tomatos%3A%20Audience" %>' class='<%# "meter " + ((MovieModel)Container.DataItem).RottenTomato.AudienceRating + " numeric" %>' > <%# ((MovieModel)Container.DataItem).RottenTomato.AudienceScore%> % </span> 
                            <span id="spnNetflixRating"  runat="server" visible='<%# this._sort == "Netflix%20User%20Ratings" || this._DisplayMode != pageMode.movies %>' class='netflixRating'>
                                <div>Netflix Rating</div>
                                <div id="movieratingdiv${Id}" class="rating"  avgrating="<%# ((MovieModel)Container.DataItem).Netflix.AverageRating %>"></div>
                                <div class="movieAverageRating"><%# ((MovieModel)Container.DataItem).Netflix.AverageRating %></div>
                                <span >Member Average</span>
                            </span>
                            <div style="clear:both"></div>
                            <span runat="server" visible='<%# this._sort != "Netflix%20User%20Ratings" && this._DisplayMode == pageMode.movies %>' ><a style="float:right; padding: 10px;" target="_blank" href='<%# ((MovieModel)Container.DataItem).RottenTomato.WebsiteLink %>'> On Rotten Tomatoes</a></span>
                        </div>
                        </td>
			        </tr>
		        </table>
                  
	        </div>
                   
                   
         
        </div>
        
        <br/>
</ItemTemplate>

</asp:Repeater>

<div class="pagingContainer">
<a runat="server" style="padding-right:20px;" class="paging" id="btnPrev2">Prev</a><a id="btnNext2" class="paging" runat="server">Next</a>
</div>

<a id="fdbk_tab" style="background-color: rgb(34, 34, 34);" > Feedback</a>
<div id="fdbk_form">
    <table>
    <tr><td></td><td> <h3>Feedback - How are we doing?</h3></td></tr>
    <tr><td>Comments</td><td><textarea></textarea></td></tr>
    <tr><td>Email<br />(optional)</td><td>  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
   <tr><td>Name<br />(optional)</td><td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td></tr>
  <tr><td></td><td>    <asp:Button runat="server" Text="Submit" /></td></tr>
 
    

    </table>
</div>

<script type="text/javascript">
    var apiKey = 'x47y5zn3w973qx9jwpnhr9je';
    $('.btnWatch').click(function () {
        var id =  $(this).attr('data-id');
        nflx.openPlayer(id, 0, 0, apiKey);
    });
    $('.btnAddToQueue').click(function () {
        var id = $(this).attr('data-id');
        var url = 'http://api.netflix.com/catalog/titles/movies/';
        nflx.addToQueue(url + id, 10, 20, apiKey, 'instant', 'addMe');
        alert('done adding');
    });

    var ddlSortSelectedValue = "Rotten%20Tomatos%3A%20Critcs"; 
    if(jQuery.cookie("sort") != null)
        ddlSortSelectedValue = jQuery.cookie("sort");

    $('#ddlSort').val(ddlSortSelectedValue);
    $('#ddlSort').change(function () {
        ShowLoading();
        var sort = $(this).val();
        $.cookie("sort", sort, { path: '/' });
        window.location = '/';
    });
    $('#fdbk_tab').click(function () {
        $('#fdbk_form').toggle();
        $('.loading').toggle();
    });
    


    function ApplyRating() {
        $("div[id^='movieratingdiv']").each(function () {
            var avgRating = $(this).attr('avgrating');
            if (avgRating == null) {
                $(this).text("No Rating")
            }
            else {
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

    ApplyRating();

</script>

</asp:Content>

