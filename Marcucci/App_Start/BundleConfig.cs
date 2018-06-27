using System.Web;
using System.Web.Optimization;

namespace Marcucci
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
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/MyStyles").Include(
                      "~/Content/styles/font-awesome.css",
                      "~/Content/styles/bootstrap.css",
                      "~/Content/styles/jquery.smartmenus.bootstrap.css",
                      "~/Content/styles/jquery.simpleLens.css",
                      "~/Content/styles/slick.css",
                      "~/Content/styles/nouislider.css",
                      "~/Content/styles/theme-color/lite-blue-theme.css",
                      "~/Content/styles/sequence-theme.modern-slide-in.css",
                      "~/Content/styles/style.css"));

            bundles.Add(new ScriptBundle("~/Content/MyScripts").Include(
                   "~/Content/scripts/jquery.unobtrusive-ajax.min.js",
                   "~/Content/scripts/bootstrap.js",
                   "~/Content/scripts/jquery.smartmenus.js",
                   "~/Content/scripts/jquery.smartmenus.bootstrap.js",
                   "~/Content/scripts/sequence.js",
                   "~/Content/scripts/sequence-theme.modern-slide-in.js",
                   "~/Content/scripts/jquery.simpleGallery.js",
                   "~/Content/scripts/jquery.simpleLens.js",
                   "~/Content/scripts/slick.js",
                   "~/Content/scripts/nouislider.js",
                   "~/Content/scripts/custom.js"
                  ));
        }
    }
}
