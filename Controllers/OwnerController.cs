using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using System.Web.Security;
using WebPrinter.Models;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;

namespace WebPrinter.Controllers
{
    public class OwnerController : Controller
    {
        private PrinterStockEntities db = new PrinterStockEntities();

        public OwnerController()
        {
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }

        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            LogOnModel o = new LogOnModel();
            return View("Login", o);
        }

        [HttpGet]
        public ActionResult Login()
        {
            LogOnModel o = new LogOnModel();
            return View(o);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel user, string ReturnUrl)
        {
            Dictionary<string, string> json = new Dictionary<string, string>();
            if (ModelState.IsValid)
            {
                if (IsValid(user.username, user.password))
                {
                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                    && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    {
                        json.Add("error", null);
                        json.Add("url", ReturnUrl);
                    }
                    else
                    {
                        //Url.RouteUrl("")
                        json.Add("error", null);
                        json.Add("url", "/Home/Index");
                    }
                }
                else
                {
                    //ModelState.AddModelError("Error", "Login not correct");
                    json.Add("error", "Username or Password not correct.");
                    json.Add("url", null);
                }
            }
            else 
            {
                string errors = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errors += error.ErrorMessage;
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            
            return Json(json);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Owner");
        }

        private bool IsValid(string username, string password)
        {
            bool userValid = false;
            
            /*NpgsqlParameter p_username = new NpgsqlParameter("@username", username);
            NpgsqlParameter p_password = new NpgsqlParameter("@password", password);
            var user = from a in db.Owners
                        where (a.username == username && a.password == password)
                        select a;
            NpgsqlParameter p_username = new NpgsqlParameter("@username", username);
            NpgsqlParameter p_password = new NpgsqlParameter("@password", password);
            object[] parameters = new object[] { p_username, p_password };
            var rs = db.Owners.SqlQuery("select * from \"Owner\" where username = '" + @username+ "' and password = '"+ @password +"'", parameters).FirstOrDefault();
            bool userValid = entities.Users.Any(user => user.username == username && user.password == password);
                
            var rs = (from a in db.Owners
                        where String.Compare(a.username, username, StringComparison.OrdinalIgnoreCase) == 0
                        && String.Compare(a.password, password, StringComparison.OrdinalIgnoreCase) == 0
                        select a).FirstOrDefault();*/

            userValid = db.Owners.Any(user => user.username == username && user.password == password);
            if (userValid)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                userValid = true;
            }
            return userValid;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            CreateUserModel o = new CreateUserModel();
            return PartialView(o);
        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CreateUserModel model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    CreateUserModel m = new CreateUserModel();
                    Owner n = m.CreateUserModelOwner(model);
                    db.Owners.Add(n);
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {
                    
                }
            }
            return Json(new { complete = complete });
        }

        [Route("ManageAccount")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ManageAccount()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult LoadAccount(datatable param)
        {
            param.ColumnsName = new List<string>() { "a.firstname", "a.lastname", "a.email", "a.roles" };
            //param.SelectName = new List<string>() { "a.id", "a.firstname || ' ' || a.lastname AS name", "a.email", "a.roles" };
            param.OrderColumn = new List<string>() { "int", "string", "string", "string" };
            string select = WebPrinter.DataTable.select(param);
            string search = WebPrinter.DataTable.filter(param);
            string strSQL = "SELECT " + select + " FROM \"Owner\" a " + search  + " order by a.id asc";
            var displayedCompanies = db.Owners.SqlQuery(strSQL).ToList();
            int count = displayedCompanies.Count();
            var tmp = displayedCompanies.Select((c, index) => new string[]
            {
                Convert.ToString(index+1),
                c.name,
                c.email,
                c.roles,
                "<button type='button' class='btn btn-success' onclick='Owner.edit("+Convert.ToString(c.id)+")'><i class='glyphicon glyphicon-pencil'></i></button><span class='left-10'></span><button type='button' class='btn btn-danger' onclick='Owner.remove("+Convert.ToString(c.id)+")'><i class='glyphicon glyphicon-trash'></i></button>"
            });
            tmp = WebPrinter.DataTable.order(param, tmp);
            var result = tmp.Skip(param.start).Take(param.length);
            return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = count,
                    iTotalDisplayRecords = count,
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            var b = db.Owners.Find(id);
            return PartialView(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditUserModel model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    EditUserModel m = new EditUserModel();
                    Owner n = m.EditUserModelOwner(model);
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {
                    
                }
            }
            return Json(new { complete = complete });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(int id = 0)
        {
            bool rs;
            Owner o = db.Owners.Where(s => s.id == id).FirstOrDefault<Owner>();
            try
            {
                db.Entry(o).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                rs = true;
            }
            catch (Exception ex) {
                rs = false;
            }
            return Json(new { result = rs });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult CheckDuplicateUser(string username)
        {
            var rs = db.Owners.Where(s => s.username == username).FirstOrDefault();
            return Json(rs == null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<Owner> GetDataFromDB()
        {
            List<Owner> objUserDetail = new List<Owner>();
            objUserDetail.Add(new Owner { username = "user1", password = "pass1" });
            objUserDetail.Add(new Owner { username = "user2", password = "pass2" });
            objUserDetail.Add(new Owner { username = "user3", password = "pass3" });
            return objUserDetail;
        }
    }
}