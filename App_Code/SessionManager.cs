using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPrinter
{
    internal class SessionManager
    {
        internal static int Id
        {
            get
            {
                if (HttpContext.Current.Session["Id"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["Id"]);
                }
                else
                {
                    return -1;
                }
            }
            set { HttpContext.Current.Session["Id"] = value; }
        }

        internal static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["Username"] != null)
                {
                    return HttpContext.Current.Session["Username"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["Username"] = value; }
        }

        internal static string FirstName
        {
            get {
                if (HttpContext.Current.Session["FirstName"] != null)
                {
                    return HttpContext.Current.Session["FirstName"].ToString();
                }
                else 
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["FirstName"] = value; }
        }

        internal static string LastName
        {
            get
            {
                if (HttpContext.Current.Session["LastName"] != null)
                {
                    return HttpContext.Current.Session["LastName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["LastName"] = value; }
        }

        internal static string Email
        {
            get
            {
                if (HttpContext.Current.Session["Email"] != null)
                {
                    return HttpContext.Current.Session["Email"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["Email"] = value; }
        }

        internal static string Role
        {
            get
            {
                if (HttpContext.Current.Session["Role"] != null)
                {
                    return HttpContext.Current.Session["Role"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["Role"] = value; }
        }
    }
}