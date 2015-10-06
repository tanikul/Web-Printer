using System.Collections.Generic;
namespace WebPrinter.Models
{
    public class datatable
    {
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        public List<string> ColumnsName { get; set; }

        public List<string> SelectName { get; set; }
        public List<string> OrderColumn { get; set; }
        public List<columns> columns { get; set; }
        public List<order> order { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public search search { get; set; }
        public int start { get; set; }
    }

    public class columns
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool ordertable { get; set; }
        public search search { get; set; }
        public bool searchable { get; set; }
    }

    public class order
    {
        public int column { get; set; }
        public string dir { get; set; } 
    }
    
    public class search
    {
        public bool regex { get; set; }
        private string val;
        public string value 
        {
            get {
                return (string.IsNullOrEmpty(val)) ? "" : val;
            }
            set {
                this.val = value;
            }
        }
    }
}