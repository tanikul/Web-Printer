using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebPrinter.Models;

namespace WebPrinter
{
    public class General
    {
        private static PrinterStockEntities db = new PrinterStockEntities();

        public static string NumberFormat(int? number)
        {
            number = (number == null) ? 0 : number;
            return String.Format("{0:#,##0}", number);
        }

        public static string NumberFormat(double? number)
        {
            number = (number == null) ? 0 : number;
            return String.Format("{0:n}", number);
        }

        public static int NumberFormatToDB(int? number)
        {
            string str = String.Format("{0:#,##0}", number);
            return Convert.ToInt32(str.Replace(",",""));
        }

        public static double NumberFormatToDB(double? number)
        {
            number = (number == null) ? 0 : number;
            string str = String.Format("{0:n}", number);
            return Convert.ToDouble(str.Replace(",", ""));
        }

        public static double CalWidhHeight(Calaulate cal)
        {
            string[] s = cal.sum_size.Split('X');
            double width_db = Convert.ToDouble(s[0]);
            double height_db = Convert.ToDouble(s[1]);
            double w_1 = width_db / cal.width;
            double h_1 = height_db / cal.height;

            double w_2 = width_db / cal.height;
            double h_2 = height_db / cal.width;

            double h_1_r = Math.Floor(h_1);
            double w_1_r = Math.Floor(w_1);

            double h_2_r = Math.Floor(h_2);
            double w_2_r = Math.Floor(w_2);
            
            double sum_1 = h_1_r * w_1_r;
            double sum_2 = h_2_r * w_2_r;

            double rs;
            if (sum_1 > sum_2)
            {
                rs = sum_1;
            }
            else
            {
                rs = sum_2;
            }
            return rs;
        }

        public static double CalculateCostPaper(Calaulate cal)
        {
            double qty_layout = CalWidhHeight(cal);
            cal.qty_layout = qty_layout;
            double paper_page = cal.qty / qty_layout;
            cal.paper_page = paper_page;
            double? per_sheet = db.Stock_Paper.Where(p => p.id == cal.paper).Select(p => p.price_per_sheet).Sum();
            double cost_per_sheet = Convert.ToDouble(per_sheet);
            double sum = (paper_page + cal.lose) * cost_per_sheet;
            return sum;
        }

        public static double CalculateCostPrintColor(Calaulate cal)
        {
            var str = (from x in db.Machine_Printer
                        where x.id == cal.printer
                       select x).FirstOrDefault();
            double per_sheet = Convert.ToDouble(str.colormeter_price);
            double cost_printer = cal.round_print_all_color * per_sheet;

            double? prepair_printer = str.printercostprict / str.installment / str.workinghours / (str.printingspeed_color * 60) * cal.round_print_all_color;
            double prepair_printer_d = Convert.ToDouble(prepair_printer);

            double? electicity = str.electricity / (str.printingspeed_color * 60) * cal.round_print_all_color;
            double electicity_d = Convert.ToDouble(electicity);

            double? employee = str.@operator / str.workinghours / (str.printingspeed_color * 60) * cal.round_print_all_color;
            double employee_d = Convert.ToDouble(employee);
            double sum = cost_printer + prepair_printer_d + electicity_d + employee_d;
            return sum; 
        }

        public static double CalculateCostPrintBW(Calaulate cal)
        {
            var str = (from x in db.Machine_Printer
                       where x.id == cal.printer
                       select x).FirstOrDefault();

            double per_sheet = Convert.ToDouble(str.blackwhitemeter_price);
            double cost_printer = cal.round_print_all_bw * per_sheet;

            double? prepair_printer = str.printercostprict / str.installment / str.workinghours / (str.printingspeed_blackwhite * 60) * cal.round_print_all_bw;
            double prepair_printer_d = Convert.ToDouble(prepair_printer);

            double? electicity = str.electricity / (str.printingspeed_blackwhite * 60) * cal.round_print_all_bw;
            double electicity_d = Convert.ToDouble(electicity);

            double? employee = str.@operator / str.workinghours / (str.printingspeed_blackwhite * 60) * cal.round_print_all_bw;
            double employee_d = Convert.ToDouble(employee);
            double sum = cost_printer + prepair_printer_d + electicity_d + employee_d;
            return sum;
        }

        public static double CalculateFinishing(Calaulate cal)
        {
            double? value = 0;
            for(int i = 0; i<cal.finishing.Length; ++i){
                int j = Convert.ToInt32(cal.finishing[i]);
                var str = (from x in db.Finishings
                           where x.id == j
                       select x).FirstOrDefault();

                double? tmp = 0;
                int[] transfer = new int[3] { 34, 35, 36 };
                if (cal.qty <= str.quantity1)
                {
                    if (Array.IndexOf<int>(transfer, str.id) > 0) {
                        tmp = str.price1;
                    }
                    else
                    {
                        tmp = cal.qty * str.price1;
                    }
                }
                else if (cal.qty <= str.quantity2)
                {
                    if (Array.IndexOf<int>(transfer, str.id) > 0)
                    {
                        tmp = str.price2;
                    }
                    else
                    {
                        tmp = cal.qty * str.price2;
                    }
                }
                else if (cal.qty <= str.quantity3)
                {
                    if (Array.IndexOf<int>(transfer, str.id) > 0)
                    {
                        tmp = str.price2;
                    }
                    else 
                    {
                        tmp = cal.qty * str.price3;
                    }
                }
                value += tmp;
            }
            
            return Convert.ToDouble(value);
        }

        public static bool IsNumeric(string num)
        { 
            int n;
            if (int.TryParse(num, out n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    
        //public static void PrintSQL(System.Data.Objects.ObjectQuery result)
        //{
        //    //var result = from x in appEntities
        //    // where x.id = 32
        //    // select x;
        //    var sql = ((System.Data.Objects.ObjectQuery)result).ToTraceString();
        //    System.Diagnostics.Debug.WriteLine("strSQL " + sql);
        //}
    
}