using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryString
/// </summary>
public class QueryString
{
	
	public static string AddQueryString( string url, string key, string value)
        {
            string result = url;
            string[] t = url.Split('?');
            string path = t[0];
            string[] querystring = new string[]{};
                
            if(t.Length > 1)
                querystring = t[1].Split('&');

            Dictionary<string, string> queryParams = new Dictionary<string,string>();

            foreach( string q in querystring ){
                if( q != string.Empty )
                    queryParams.Add( q.Split('=')[0], q.Split('=')[1]);
            }

           if( queryParams.ContainsKey(key) )
               queryParams[key] = value;
           else
               queryParams.Add( key, value);

            result = path;
            if( queryParams.Count() > 0)
            {
                result += "?";
                foreach( string k in queryParams.Keys )
                    result += k + "=" + queryParams[k] + "&";
                if (result.EndsWith("&"))
                    result = result.TrimEnd('&');
            }

            return result;
        }
    
}
