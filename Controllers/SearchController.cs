using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;
using Microsoft.AspNet.Identity;

namespace WebPrinter.Controllers
{
    [Authorize]
    
    public class SearchController : Controller
    {
        private PrinterStockEntities db = new PrinterStockEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JobDesc()
        {
            ViewBag.type = "job";
            ViewBag.url = "/Search/JobDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public ActionResult JobDesc(datatable param)
        {
            param.ColumnsName = new List<string>() { "a.name_job_type" };
            param.SelectName = new List<string>() { "a.id", "a.name_job_type" };
            param.OrderColumn = new List<string>() { "int", "string" };
            string select = WebPrinter.DataTable.select(param);
            string search = WebPrinter.DataTable.filter(param);
            string strSQL = "SELECT " + select + " FROM \"Job_Type\" a " + search  + " order by a.id asc";
            var displayedCompanies = db.Job_Type.SqlQuery(strSQL).ToList();
            int count = displayedCompanies.Count();
            var tmp = displayedCompanies.Select((c, index) => new string[]
            {
                Convert.ToString(index+1),
                c.name_job_type 
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
        public ActionResult PaperDesc()
        {
            ViewBag.type = "paper";
            ViewBag.url = "/Search/PaperDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public JsonResult PaperDesc(string name)
        {
            string strSQL = "SELECT * FROM \"Stock_Paper\"";
            var displayedCompanies = db.Stock_Paper.SqlQuery(strSQL).ToList();
            var result = displayedCompanies.Select((c, index) => new 
            {
                id = c.id,
                type_paper = c.type_paper,
                gsm = General.NumberFormat(c.gsm),
                width = General.NumberFormat(c.width),
                length = General.NumberFormat(c.length),
                sheets_per_pack = General.NumberFormat(c.sheets_per_pack),
                price_per_pack = General.NumberFormat(c.price_per_pack),
                price_per_sheet = General.NumberFormat(c.price_per_sheet)
            });
            return Json(new
            {
                data = result
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PrinterDesc()
        {
            ViewBag.type = "printer";
            ViewBag.url = "/Search/PrinterDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public JsonResult PrinterDesc(string name)
        {
            string strSQL = "SELECT a.*, b.systemname, c.colorunit FROM \"Machine_Printer\" a inner join \"Sys_Print\" b on a.system_id = b.id inner join \"ColorUnit\" c on a.colorunit_id = c.id ORDER BY a.id ASC";
            var displayedCompanies = db.Database.SqlQuery<TempPrinterResult>(@strSQL);
            int count = db.Finishings.Count();
            var result = displayedCompanies.Select((b, index) => new
            {
                id = b.id,
                name_printer = b.name_printer,
                systemname = b.systemname,
                colorunit = b.colorunit,
                paper_width = General.NumberFormat(b.paper_width),
                paper_length = General.NumberFormat(b.paper_length),
                graeme_print = General.NumberFormat(b.graeme_print),
                paper_left = General.NumberFormat(b.paper_left),
                paper_right = General.NumberFormat(b.paper_right),
                paper_top = General.NumberFormat(b.paper_top),
                paper_under = General.NumberFormat(b.paper_under),
                autoduplex_gsm = General.NumberFormat(b.autoduplex_gsm),
                printingspeed_color = General.NumberFormat(b.printingspeed_color),
                printingspeed_blackwhite = General.NumberFormat(b.printingspeed_blackwhite),
                colormeter_price = General.NumberFormat(b.colormeter_price),
                blackwhitemeter_price = General.NumberFormat(b.blackwhitemeter_price),
                setprintpang1 = General.NumberFormat(b.setprintpang1),
                setprintpang2 = General.NumberFormat(b.setprintpang2),
                printercostprict = General.NumberFormat(b.printercostprict),
                installment = General.NumberFormat(b.installment),
                @operator = General.NumberFormat(b.@operator),
                workinghours = General.NumberFormat(b.workinghours)
            });
            return Json(new
            {
                data = result
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TransferDesc()
        {
            ViewBag.type = "transfer";
            ViewBag.url = "/Search/TransferDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public JsonResult TransferDesc(string name)
        {
            var result = db.Transfers.OrderBy(f => f.id).ToList().Select((b, index) => new
            {
                id = Convert.ToString((index + 1)),
                name_transfer = b.name_transfer
            });
            return Json(new
            {
                data = result
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FinishDesc()
        {
            ViewBag.type = "finish";
            ViewBag.url = "/Search/FinishDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public JsonResult FinishDesc(string name)
        {
            string strSQL = "SELECT a.*, b.type_finishing, c.unit FROM \"Finishing\" a inner join \"Type_Finishing\" b on a.type_id = b.id inner join \"Unit\" c on a.unit_id = c.id ORDER BY a.id ASC";
            var displayedCompanies = db.Database.SqlQuery<TempResult>(@strSQL);
            int count = db.Finishings.Count();
            var result = displayedCompanies.Select((b, index) => new
            {
                id = Convert.ToString((index+1)),
                topic = b.topic,
                detail = b.detail,
                type_finishing = b.type_finishing,
                unit = b.unit,
                quantity1 = General.NumberFormat(b.quantity1),
                price1 = General.NumberFormat(b.price1),
                quantity2 = General.NumberFormat(b.quantity2),
                price2 = General.NumberFormat(b.price2),
                quantity3 = General.NumberFormat(b.quantity3),
                price3 = General.NumberFormat(b.price3),
            });
            return Json(new
            {
                data = result
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CustomerDesc()
        {
            ViewBag.type = "customer";
            ViewBag.url = "/Search/CustomerDesc";
            return PartialView("Description");
        }

        [HttpPost]
        public JsonResult CustomerDesc(datatable param)
        {
            var x = db.Customers.Where(s => s.fname.Contains(param.search.value) || s.lname.Contains(param.search.value) || s.company.Contains(param.search.value)).OrderBy(s => s.id).ToList().Select((c, index) => new string[]
            {
                Convert.ToString(index+1),
                c.fname + ' ' + c.lname,
                c.company
            });
      
            param.OrderColumn = new List<string>() { "int", "string", "string" };
            int count = x.Count();
            x = WebPrinter.DataTable.order(param, x);
            var result = x.Skip(param.start).Take(param.length);
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
        public JsonResult Calculate(Calaulate cal)
        {
            double cost_paper = General.CalculateCostPaper(cal);
            double cost_color = General.CalculateCostPrintColor(cal);
            double cost_bw = General.CalculateCostPrintBW(cal);
            double cost_finish = General.CalculateFinishing(cal);
            Dictionary<string, string> arr = new Dictionary<string, string>();
            arr.Add("cost_paper", General.NumberFormat(cost_paper));
            double cost_printer = cost_color + cost_bw;
            arr.Add("cost_printer", General.NumberFormat(cost_printer));
            arr.Add("cost_finish", General.NumberFormat(cost_finish));
            double total = cost_paper + cost_color + cost_bw + cost_finish;
            arr.Add("total", General.NumberFormat(total));
            double sell = (total * cal.profit / 100) + total;
            double unit = sell / cal.qty;
            double vat = (sell * cal.vat / 100) + sell;
            double vat_unit = vat / cal.qty;
            arr.Add("sell", General.NumberFormat(sell));
            arr.Add("unit", General.NumberFormat(unit));
            arr.Add("vat", General.NumberFormat(vat));
            arr.Add("vat_unit", General.NumberFormat(vat_unit));
            arr.Add("qty", cal.qty.ToString());
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSearch(Calaulate cal)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                { 
                    MappingSearch m = new MappingSearch();
                    HeadDataPrinter h = m.MappingCalHead(cal);
                    db.HeadDataPrinters.Add(h);
                    db.SaveChanges();

                    DetailDataPrinter d = m.MappingCalDetail(cal);
                    d.id_headdataprinter = h.id;
                    db.DetailDataPrinters.Add(d);
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
        public JsonResult Customer(string q)
        {
            var str = (from c in db.Customers
                       where c.lname.Contains(q) || c.fname.Contains(q) || c.company.Contains(q)
                       select new {
                           id = c.id,
                           company = c.company,
                           name = c.fname + " " + c.lname
                       }).ToList();
            return Json(str, JsonRequestBehavior.AllowGet);
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