using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace mjl.Models
{
    public class sysSession
    {
        private static HttpSessionState session { get { return HttpContext.Current.Session; } }

        public static string name
        {
            get { return session["name"] as string; }
            set { session["name"] = value; }
        }
        public static string UserID
        {
            get { return session["user_id"] as string; }
            set { session["user_id"] = value; }
        }
    }
}