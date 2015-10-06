using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;

namespace WebPrinter.Controllers
{
    public class StockPaperController : Controller
    {

        private PrinterStockEntities db = new PrinterStockEntities();

        // GET: StockPaper
        [Route("ManagePaper")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult LoadPaper(datatable param)
        {
            param.ColumnsName = new List<string>() { "a.type_paper" };
            param.SelectName = new List<string>() { "a.id", "a.type_paper", "a.gsm", "a.width", "a.length", "a.sheets_per_pack", "a.price_per_pack", "a.price_per_sheet" };
            param.OrderColumn = new List<string>() { "string", "string", "int", "double", "double", "double", "double", "double" };
            string select = WebPrinter.DataTable.select(param);
            string search = WebPrinter.DataTable.filter(param);
            string strSQL = "SELECT " + select + " FROM \"Stock_Paper\" a " + search  + " order by a.id asc";
            var displayedCompanies = db.Stock_Paper.SqlQuery(strSQL).ToList();
            int count = displayedCompanies.Count();
            var tmp = displayedCompanies.Select((c, index) => new string[]
            {
                c.id,
                c.type_paper,
                General.NumberFormat(c.gsm),
                General.NumberFormat(c.width),
                General.NumberFormat(c.length),
                General.NumberFormat(c.sheets_per_pack),
                General.NumberFormat(c.price_per_pack),
                General.NumberFormat(c.price_per_sheet),
                "<button type='button' class='btn btn-success' onclick=\"Paper.edit('"+c.id+"')\"><i class='glyphicon glyphicon-pencil'></i></button><span class='left-10'></span><button type='button' class='btn btn-danger' onclick=\"Paper.remove('"+c.id+"')\"><i class='glyphicon glyphicon-trash'></i></button>"
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
        public ActionResult Edit(string id = null)
        {
            var b = db.Stock_Paper.Where(s => s.id == id).FirstOrDefault();
            return PartialView(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditPaperModel model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    EditPaperModel m = new EditPaperModel();
                    Stock_Paper n = m.EditPaperModelStock(model);
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
            CreatePaperModel o = new CreatePaperModel();
            return PartialView(o);
        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Register(CreatePaperModel model)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    CreatePaperModel m = new CreatePaperModel();
                    Stock_Paper n = m.CreatePaperModelStock(model);
                    db.Stock_Paper.Add(n);
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    complete = false;
                }
            }
            return Json(new { complete = complete});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(string id = null)
        {
            bool rs;
            Stock_Paper o = db.Stock_Paper.Where(s => s.id == id).FirstOrDefault<Stock_Paper>();
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
        public JsonResult CheckDuplicateNo(string id)
        {
            var rs = db.Stock_Paper.Where(s => s.id == id).FirstOrDefault();
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