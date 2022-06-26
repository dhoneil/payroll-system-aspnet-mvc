using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class SystemHelper
    {
        public static User CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["user"] as User;
            }
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public static string BaseUrl()
        {
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/');
            return baseUrl;
        }
    }
}