using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;

namespace WebPrinter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private PrinterStockEntities db = new PrinterStockEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddData()
        {
            ViewBag.job = new SelectList(db.Job_Type.OrderBy(x => x.id), "id", "name_job_type");
           
            return PartialView();
        }

        public JsonResult AddJob()
        {
            return Json("xxx");
        }

        public ActionResult LoadStockPaper()
        {
            Dictionary<string, object> arr = new Dictionary<string, object>();
            List<object> tmp = new List<object>();
            var papers = (from b in db.Stock_Paper.OrderBy(b => b.id)
                          select b).ToList();
            tmp.Add(papers);
            arr.Add("data", papers);
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [Route("PriceCalculate")]
        public ActionResult SearchResult()
        {
            Job_Type j = new Job_Type();
            ViewBag.job_type = j.JobTypeItems;
            
            Stock_Paper s = new Stock_Paper();
            ViewBag.paper = s.PaperItems;
            ViewBag.IdandSize = s.IdandSize;

            Machine_Printer m = new Machine_Printer();
            ViewBag.printer = m.PrinterItems;

            Finishing f = new Finishing();
            ViewBag.finishing = f.FinishingItems;
            var mapUnit = f.mapUnit;
            ViewBag.mapUnit = mapUnit;
            ViewBag.unit_first = mapUnit.First().Value.ToString();

            Transfer t = new Transfer();
            ViewBag.transfer = t.TransferItems;

            return View();
        }

        public ActionResult LoadDetail(datatable param)
        {
            param.ColumnsName = new List<string>() { "c.name_job_type", "d.type_paper", "e.name_printer", "f.fname", "f.lname" };
            param.SelectName = new List<string>() { "a.id", "c.name_job_type", "b.quantity", "d.type_paper", "e.name_printer", "a.grandtotalprice", "((a.grandtotalprice * b.vat / 100) + a.grandtotalprice) AS vatprice", "f.fname || ' ' || f.lname AS name" };
            param.OrderColumn = new List<string>() { "int", "string", "int", "string", "string", "double", "double", "string" };
            string select = WebPrinter.DataTable.select(param);
            string search = WebPrinter.DataTable.filter(param);
            
            string strSQL = "SELECT " + select + " FROM \"HeadDataPrinter\" a inner join \"DetailDataPrinter\" b on a.id = b.id_headdataprinter inner join \"Job_Type\" c on b.id_job_type = c.id inner join \"Stock_Paper\" d on b.id_paper = d.id inner join \"Machine_Printer\" e on b.id_printer = e.id  inner join \"Customer\" f on a.id_customer = f.id " + search  + " order by a.id asc";
            var displayedCompanies = db.Database.SqlQuery<TempResultSearch>(@strSQL);

            int count = displayedCompanies.Count();
            var tmp = displayedCompanies.Select((b, index) => new string[]
            {
                Convert.ToString(index+1),
                b.name_job_type,
                General.NumberFormat(b.quantity),
                b.type_paper,
                b.name_printer,
                General.NumberFormat(b.grandtotalprice),
                General.NumberFormat(b.vatprice),
                b.name,
                "</span><button type='button' class='btn btn-danger' onclick=\"Purchase.remove('"+b.id+"')\"><i class='glyphicon glyphicon-trash'></i></button>"
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Remove(int id = 0)
        {
            bool rs;
            HeadDataPrinter o = db.HeadDataPrinters.Where(s => s.id == id).FirstOrDefault<HeadDataPrinter>();
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