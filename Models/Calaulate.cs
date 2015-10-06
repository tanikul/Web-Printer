using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPrinter.Models
{
    public class Calaulate
    {
        public Calaulate()
        {
            qty = 0;
            lose = 0;
            qty_black = 0;
            qty_color = 0;
            width = 0;
            height = 0;
        }

        public int qty { get; set; }
        public int lose { get; set; }
        public string paper { get; set; }
        public string sum_size { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int qty_color { get; set; }
        public double paper_used { get; set; }
        public string printer { get; set; }
        public int qty_black { get; set; }
        public int[] finishing { get; set; }
        public double paper_page { get; set; }
        public double qty_layout { get; set; }
        public int customer { get; set; }
        public int transfer { get; set; }
        public double profit { get; set; }
        public double vat { get; set; }
        public double total { get; set; }
        public int job_type { get; set; }
        public double paper_cost { get; set; }
        public double finish_cost { get; set; }
        public double printer_cost { get; set; }
        public System.DateTime recieve_date { get; set; }

        public System.DateTime now 
        {
            get {
                return System.DateTime.Now;
            }
        }

        public double round_print_all_color 
        { 
            get {
                return ((qty / qty_layout) + lose) * qty_color;
            } 
        }

        public double round_print_all_bw
        {
            get
            {
                return ((qty / qty_layout) + lose) * qty_black;
            }
        }
    }

    public class MappingSearch
    {
        public int[] finishing { get; set; }
        public string ConvertFinishing
        {
            get {
                string str = "";
                for (int i = 0; i < finishing.Length; ++i) {
                    str += finishing[i].ToString() + ",";
                } 
                return str = str.Substring(0, str.Length - 1);
            }
        }

        public DetailDataPrinter MappingCalDetail(Calaulate cal)
        {
            finishing = cal.finishing;
            DetailDataPrinter d = new DetailDataPrinter();
            d.black_print = cal.qty_black;
            d.color_print = cal.qty_color;
            d.id_printer = cal.printer;
            d.id_paper = cal.paper;
            d.id_finishing = ConvertFinishing;
            d.id_job_type = cal.job_type;
            d.id_transfer = cal.transfer;
            d.recieve_date = cal.recieve_date;
            d.quantity = cal.qty;
            d.lose = cal.lose;
            d.width = cal.width;
            d.height = cal.height;
            d.vat = cal.vat;
            d.paper_cost = cal.paper_cost;
            d.printer_cost = cal.printer_cost;
            d.finishing_cost = cal.finish_cost;
            d.profit = cal.profit;
            return d;
        }

        public HeadDataPrinter MappingCalHead(Calaulate cal)
        {
            HeadDataPrinter h = new HeadDataPrinter();
            h.id_customer = cal.customer;
            h.quantity = cal.qty;
            h.totalprice = cal.total;
            h.createdate = cal.now;
            h.id_owner = SessionManager.Id;
            h.grandtotalprice = General.NumberFormatToDB((cal.total * cal.profit / 100) + cal.total);
            h.totalvat = General.NumberFormatToDB(h.grandtotalprice * cal.vat / 100);
            return h;
        }
    }

    public class TempResultSearch
    {
        public int id { get; set; }
        public string name_job_type { get; set; }
        public int quantity { get; set; }
        public string type_paper { get; set; }
        public string name_printer { get; set; }
        public double grandtotalprice { get; set; }
        public double vatprice { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string name { get; set; }
    }

    public class RemoveHeadDetail
    {
        public HeadDataPrinter head;
        public DetailDataPrinter detail;
    }
}