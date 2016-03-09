using System.Web;
using System.Web.Optimization;

namespace SISPTD
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/chosen.jquery.js",
                        "~/Scripts/Utils.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", 
                      "~/Content/sisptd.css",
                      "~/Content/chosen.css",
                      "~/Content/bootstrap-chosen.css",
                       "~/Content/bootstrap-chosen.lesss"
                      ));
        }
    }
}
