using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;

namespace WebPrinter.Controllers
{
    public class FinishingController : Controller
    {

        private PrinterStockEntities db = new PrinterStockEntities();

        // GET: Finishing
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ManageFinishing")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult LoadFinishing(datatable param)
        {
            param.ColumnsName = new List<string>() { "a.topic", "a.detail", "b.type_finishing", "c.unit" };
            param.SelectName = new List<string>() { "a.id", "a.topic", "a.detail", "b.type_finishing", "c.unit", "a.quantity1", "a.price1", "a.quantity2", "a.price2", "a.quantity3", "a.price3" };
            param.OrderColumn = new List<string>() { "int", "string", "string", "string", "string", "double", "double", "double", "double", "double", "double" };
            string select = WebPrinter.DataTable.select(param);
            string search = WebPrinter.DataTable.filter(param);
            
            string strSQL = "SELECT " + select + " FROM \"Finishing\" a inner join \"Type_Finishing\" b on a.type_id = b.id inner join \"Unit\" c on a.unit_id = c.id" + search + " order by id asc";
            var displayedCompanies = db.Database.SqlQuery<TempResult>(@strSQL);
            int count = displayedCompanies.Count();
            var tmp = displayedCompanies.Select((b, index) => new string[] 
            {
                (index+1).ToString(),
                b.topic,
                b.detail,
                b.type_finishing,
                b.unit,
                General.NumberFormat(b.quantity1),
                General.NumberFormat(b.price1),
                General.NumberFormat(b.quantity2),
                General.NumberFormat(b.price2),
                General.NumberFormat(b.quantity3),
                General.NumberFormat(b.price3),
                "<button type='button' class='btn btn-success' onclick=\"Finish.edit('"+b.id+"')\"><i class='glyphicon glyphicon-pencil'></i></button><span class='left-10'></span><button type='button' class='btn btn-danger' onclick=\"Finish.remove('"+b.id+"')\"><i class='glyphicon glyphicon-trash'></i></button>"
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
        public ActionResult Edit(int id)
        {
            var b = db.Finishings.Where(s => s.id == id).FirstOrDefault();
            return PartialView(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditFinishing model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    EditFinishing m = new EditFinishing();
                    Finishing n = m.EditFinishingModel(model);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            CreateFinishing o = new CreateFinishing();
            return PartialView(o);
        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CreateFinishing model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    CreateFinishing m = new CreateFinishing();
                    Finishing n = m.CreateFinishingModel(model);
                    db.Finishings.Add(n);
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return Json(new { complete = complete });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(int id = 0)
        {
            bool rs;
            Finishing o = db.Finishings.Where(s => s.id == id).FirstOrDefault<Finishing>();
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult CheckDuplicateTopic(string topic)
        {
            var rs = (Object)null;
            rs = db.Finishings.Where(s => s.topic == topic).FirstOrDefault();
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
    }
}