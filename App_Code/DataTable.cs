using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPrinter.Models;

namespace WebPrinter
{
    public class DataTable
    {
        public static string filter(datatable param)
        { 
            string str = null;
            if (!string.IsNullOrEmpty(param.search.value))
            {
                for (int i = 0; i < param.ColumnsName.Count; ++i)
                {
                    str += param.ColumnsName[i] + " LIKE '%" + param.search.value + "%' OR ";
                }
                str = str.Substring(0, (str.Length-4));
                str = " WHERE (" + str + ")";
            }
            return str;
        }

        public static string limit(datatable param)
        {
            string str = null;
            if (param.start == 0)
            {
                str = " LIMIT " + param.length;
            }
            else
            {
                str = " LIMIT " + param.length + " OFFSET " + param.start;
            }
            return str;
        }

        public static IEnumerable<string[]> order(datatable param, IEnumerable<string[]> tmp)
        {
            if (param.order.Count > 0)
            {
                for (int i = 0; i < param.order.Count; ++i)
                {
                    int id = Convert.ToInt32(param.order[i].column);
                    if (id < param.OrderColumn.Count)
                    {
                        if (param.order[i].dir.Equals("asc"))
                        {
                            if (param.OrderColumn[id].Equals("int"))
                            {
                                tmp = tmp.OrderBy(a => int.Parse(a[id]));
                            }
                            else if (param.OrderColumn[id].Equals("string"))
                            {
                                tmp = tmp.OrderBy(a => a[id]);
                            }
                            else if (param.OrderColumn[id].Equals("double"))
                            {
                                tmp = tmp.OrderBy(a => double.Parse(a[id]));
                            }
                        }
                        else
                        {
                            if (param.OrderColumn[id].Equals("int"))
                            {
                                tmp = tmp.OrderByDescending(a => int.Parse(a[id]));
                            }
                            else if (param.OrderColumn[id].Equals("string"))
                            {
                                tmp = tmp.OrderByDescending(a => a[id]);
                            }
                            else if (param.OrderColumn[id].Equals("double"))
                            {
                                tmp = tmp.OrderBy(a => double.Parse(a[id]));
                            }
                        }
                    }
                }
            }
            return tmp;
        }

        public static IQueryable<string[]> order(datatable param, IQueryable<string[]> tmp)
        {
            if (param.order.Count > 0)
            {
                for (int i = 0; i < param.order.Count; ++i)
                {
                    int id = Convert.ToInt32(param.order[i].column);
                    if (id < param.OrderColumn.Count)
                    {
                        if (param.order[i].dir.Equals("asc"))
                        {
                            if (param.OrderColumn[id].Equals("int"))
                            {
                                tmp = tmp.OrderBy(a => int.Parse(a[id]));
                            }
                            else if (param.OrderColumn[id].Equals("string"))
                            {
                                tmp = tmp.OrderBy(a => a[id]);
                            }
                            else if (param.OrderColumn[id].Equals("double"))
                            {
                                tmp = tmp.OrderBy(a => double.Parse(a[id]));
                            }
                        }
                        else
                        {
                            if (param.OrderColumn[id].Equals("int"))
                            {
                                tmp = tmp.OrderByDescending(a => int.Parse(a[id]));
                            }
                            else if (param.OrderColumn[id].Equals("string"))
                            {
                                tmp = tmp.OrderByDescending(a => a[id]);
                            }
                            else if (param.OrderColumn[id].Equals("double"))
                            {
                                tmp = tmp.OrderBy(a => double.Parse(a[id]));
                            }
                        }
                    }
                }
            }
            return tmp;
        }

        public static string select(datatable param)
        {
            string str = null;
            if (param.SelectName != null)
            {
                for (int i = 0; i < param.SelectName.Count; ++i)
                {
                    str += " " + param.SelectName[i] + ",";
                }
                str = str.Substring(0, (str.Length - 1));
            }
            else
            {
                str = " * ";
            }
            return str;
        }
    }
}