using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;
using System.Data.Entity;

namespace WebPrinter.Controllers
{
    
    public class CustomerController : Controller
    {

        private PrinterStockEntities db = new PrinterStockEntities();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("AddCustomer")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCustomer()
        {        
            var result = (from b in db.Customers
                          from y in db.Provinces.Where(y => b.id_province == y.id).DefaultIfEmpty()
                          select new
                          {
                              id = b.id,
                              mobile = b.mobile,
                              name = b.fname + " " + b.lname,
                              province = y.province_name,
                              phone = b.phone,
                              postcode = b.postcode.ToString(),
                              fax = b.fax,
                              email = b.email,
                              company = b.company,
                              address = b.address,
                          }).ToList().Select((b, index) => new { 
                                id = Convert.ToString((index + 1)),
                                mobile = string.IsNullOrEmpty(b.mobile) ? "" : b.mobile,
                                name = string.IsNullOrEmpty(b.name) ? "" : b.name,
                                province = string.IsNullOrEmpty(b.province) ? "" : b.province,
                                phone = string.IsNullOrEmpty(b.phone) ? "" : b.phone,
                                postcode = string.IsNullOrEmpty(b.postcode) ? "" : b.postcode,
                                fax = string.IsNullOrEmpty(b.fax) ? "" : b.fax,
                                email = string.IsNullOrEmpty(b.email) ? "" : b.email,
                                company = string.IsNullOrEmpty(b.company) ? "" : b.company,
                                address = string.IsNullOrEmpty(b.address) ? "" : b.address,
                                edit = "<button type='button' class='btn btn-success' onclick=\"Customer.edit('" + b.id + "')\"><i class='glyphicon glyphicon-pencil'></i></button><span class='left-3'></span><button type='button' class='btn btn-danger' onclick=\"Customer.remove('" + b.id + "')\"><i class='glyphicon glyphicon-trash'></i></button>"
                          });

            return Json(new
            {
                data = result
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var b = db.Customers.Where(s => s.id == id).FirstOrDefault();
            return PartialView(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Customer model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {

                }
            }
            return Json(new { complete = complete });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            Customer c = new Customer();
            return PartialView(c);
        }

        [HttpPost]
        [AcceptVerbs("POST")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Customers.Add(cust);
                    db.SaveChanges();
                    return Json("success");
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
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
                    }
                }
                return Json(errors);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(int id = 0)
        {
            bool rs;
            Customer o = db.Customers.Where(s => s.id == id).FirstOrDefault<Customer>();
            try
            {
                db.Entry(o).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                rs = true;
            }
            catch (Exception ex)
            {
                rs = false;
            }
            return Json(new { result = rs });
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public JsonResult CheckDuplicateName(string topic)
        //{
        //    var rs = (Object)null;
        //    rs = db.Finishings.Where(s => s.topic == topic).FirstOrDefault();
        //    return Json(rs == null);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}