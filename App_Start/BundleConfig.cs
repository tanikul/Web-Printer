using System.Web;
using System.Web.Optimization;

namespace WebPrinter
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                       "~/Scripts/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/jquery.dataTables.responsive.js",
                        "~/Scripts/fnReloadAjax.js"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                        "~/Scripts/jquery.autocomplete.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/ajax-bootstrap-select.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                "~/Scripts/moment.js",        
                "~/Scripts/bootstrap-datetimepicker.js"
                        
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/bootstrap.min.css",
                      //"~/Content/bootstrap-responsive.min.css",
                      "~/Content/theme.css",
                      "~/Content/font-awesome.css",
                      "~/Content/sign-in.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/dataTable").Include(
                      "~/Content/dataTables.css",
                      "~/Content/dataTables.responsive.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                      "~/Content/jquery-ui.css"));
            
            bundles.Add(new StyleBundle("~/Content/autocomplete").Include(
                      "~/Content/jquery.autocomplete.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                      "~/Content/bootstrap-select.css",
                      "~/Content/ajax-bootstrap-select.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                      "~/Content/bootstrap-datetimepicker.css"));
        }
    }
}
