using System.Web;
using System.Web.Optimization;

namespace CreditPand.UI
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/JqueryJs/jquery.min.js", 
                        "~/Scripts/MainJs/slick.js", 
                        "~/Scripts/MainJs/owl.js",
                        "~/Scripts/MainJs/custom.js",
                        "~/Scripts/MainJs/accordions.js",
                        "~/Scripts/MainJs/jquery.singlePageNav.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/BootstrapJs/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/BootstrapCss/bootstrap.min.css",
                      "~/Content/MainCss/fontawesome.css",
                      "~/Content/MainCss/owl.css",
                      "~/Content/MainCss/templatemo-finance-business.css",
                      "~/Content/MainCss/flex-slider.css"));
        }
    }
}
