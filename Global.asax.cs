using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebPrinter.Models;

namespace WebPrinter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private int id { get; set; }
        private string username { get; set; }
        private string firstname { get; set; }
        private string lastname { get; set; }
        private string email { get; set; }
        private string role { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            SessionManager.FirstName = firstname;
            SessionManager.LastName = lastname;
            SessionManager.Id = id;
            SessionManager.Email = email;
            SessionManager.Role = role;
            SessionManager.UserName = username;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        using (PrinterStockEntities entities = new PrinterStockEntities())
                        {
                            Owner user = entities.Owners.SingleOrDefault(u => u.username == username);
                            this.username = user.username;
                            this.role = user.roles;
                            this.id = user.id;
                            this.email = user.email;
                            this.firstname = user.firstname;
                            this.lastname = user.lastname;
                        }

                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), this.role.Split(';'));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //somehting went wrong
                    }
                }
            }
        } 
    }
}
