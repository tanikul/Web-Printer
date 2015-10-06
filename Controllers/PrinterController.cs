using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrinter.Models;

namespace WebPrinter.Controllers
{
    public class PrinterController : Controller
    {

        private PrinterStockEntities db = new PrinterStockEntities();

        // GET: Printer
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ManagePrinter")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult LoadPrinter()
        {
            string strSQL = "SELECT a.*, b.systemname, c.colorunit FROM \"Machine_Printer\" a inner join \"Sys_Print\" b on a.system_id = b.id inner join \"ColorUnit\" c on a.colorunit_id = c.id ORDER BY a.id ASC";
            var displayedCompanies = db.Database.SqlQuery<TempPrinterResult>(@strSQL);
            int count = displayedCompanies.Count();
            var result = displayedCompanies.Select((b, index) => new 
            {
                id = b.id,
                name_printer = b.name_printer,
                systemname = b.systemname,
                colorunit = b.colorunit,
                paper_width = General.NumberFormat(b.paper_width),
                paper_length = General.NumberFormat(b.paper_length),
                graeme_print = General.NumberFormat(b.graeme_print),
                edit = "<button type='button' class='btn btn-success' onclick=\"Printer.edit('"+b.id+"')\"><i class='glyphicon glyphicon-pencil'></i></button><span class='left-3'></span><button type='button' class='btn btn-danger' onclick=\"Printer.remove('"+b.id+"')\"><i class='glyphicon glyphicon-trash'></i></button>",
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            var b = db.Machine_Printer.Where(s => s.id == id).FirstOrDefault();
            return PartialView(b);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditCreatePrinter model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    EditCreatePrinter m = new EditCreatePrinter();
                    Machine_Printer n = m.EditCreatePrinterModel(model);
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();
                    complete = true;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.Dispose();
                }
            }
            return Json(new { complete = complete });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            EditCreatePrinter o = new EditCreatePrinter();
            return PartialView(o);
        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(EditCreatePrinter model)
        {
            bool complete = false;
            if (ModelState.IsValid)
            {
                try
                {
                    EditCreatePrinter m = new EditCreatePrinter();
                    Machine_Printer n = m.EditCreatePrinterModel(model);
                    db.Machine_Printer.Add(n);
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
        public JsonResult Remove(string id = null)
        {
            bool rs;
            Machine_Printer o = db.Machine_Printer.Where(s => s.id == id).FirstOrDefault<Machine_Printer>();
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
        public JsonResult CheckDuplicateID(string id)
        {
            var rs = (Object)null;
            rs = db.Machine_Printer.Where(s => s.id == id).FirstOrDefault();
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