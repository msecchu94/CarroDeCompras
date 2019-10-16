using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
            "~/Content/bootstrap/js/bootstrap.min.js",
            "~/Content/plugins/morris/morris.min.js",
            "~/Content/plugins/sparkline/jquery.sparkline.min.js",
            "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
            "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
            "~/Content/plugins/knob/jquery.knob.js",
            "~/Content/plugins/daterangepicker/daterangepicker.js",
            "~/Content/plugins/datepicker/bootstrap-datepicker.js",
            "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
            "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
            "~/Content/plugins/fastclick/fastclick.js",
            "~/Content/dist/js/app.min.js",
            "~/Content/dist/js/pages/dashboard.js",
            "~/Content/dist/js/demo.js",
             "~/Content/bootstrap/js/Site.js",
             "~/Scripts/Components/Carro.js",
             "~/Scripts/Components/Producto.js",
              "~/Scripts/Components/Pedido.js"

             ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/bootstrap/css/bootstrap.min.css",
            "~/Content/dist/css/AdminLTE.min.css",
            "~/Content/dist/css/skins/_all-skins.min.css",
            "~/Content/plugins/iCheck/flat/blue.css",
            "~/Content/plugins/morris/morris.css",
            "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
            "~/Content/plugins/datepicker/datepicker3.css",
            "~/Content/plugins/daterangepicker/daterangepicker.css",
            "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
            "~/Content/SweetAlert2/animations.scss",
             "~/Content/SweetAlert2/mixins.scss",
             "~/Content/SweetAlert2/sweetalert2.scss",
              "~/Content/SweetAlert2/toasts.scss",
                "~/Content/SweetAlert2/variables.scss",
               "~/Content/css/General.css" ));


        }
    }
}
