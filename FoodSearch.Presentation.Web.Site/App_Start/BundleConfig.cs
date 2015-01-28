using System.Web.Optimization;

namespace FoodSearch.Presentation.Web.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /******   FRONTEND   ******/

            //layout frontend
            bundles.Add(new StyleBundle("~/css/frontend/layout")
                .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Site.css", new CssRewriteUrlTransform())
                .Include("~/Content/Selectize/css/selectize.css", new CssRewriteUrlTransform()));
                

            bundles.Add(new ScriptBundle("~/js/frontend/layout").Include(
                "~/Scripts/jquery-2.1.3.min.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/Frontend/CookiePolicy.js",
                "~/Scripts/selectize.min.js",
                "~/Scripts/knockout-3.2.0.js"
                ));

            //homepage
            bundles.Add(new ScriptBundle("~/js/frontend/homepage").Include(
                "~/Scripts/Frontend/HomePage.js"
                ));

            //restaurantsList
            bundles.Add(new ScriptBundle("~/js/frontend/restaurantsList").Include(
                "~/Scripts/Frontend/SearchRestaurants.js"
                ));

            //restaurant menu
            bundles.Add(new ScriptBundle("~/js/frontend/restaurantMenu").Include(
                "~/Scripts/Frontend/Basket.js"
                ));

            //basket summary
            bundles.Add(new ScriptBundle("~/js/frontend/basketSummary").Include(
                "~/Scripts/Frontend/BasketSummary.js"
                ));

            //shipping page
            bundles.Add(new ScriptBundle("~/js/frontend/shipping").Include(
                "~/Scripts/Frontend/Delivery.js"
                ));

            //restaurant page
            bundles.Add(new ScriptBundle("~/js/frontend/restaurantPage").Include(
                "~/Scripts/starr.js",
                "~/Scripts/Frontend/RestaurantPage.js"
                ));

            //register
            bundles.Add(new ScriptBundle("~/js/frontend/register").Include(
                "~/Scripts/Frontend/Register.js"
                ));



            /******   USER PANEL   ******/
            bundles.Add(new ScriptBundle("~/js/user/panel").Include(
                "~/Scripts/Frontend/UserPanel.js"
                ));


            /*****   ADMIN COMMON   ******/
            bundles.Add(new StyleBundle("~/css/admin/common")
                .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/Admin/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Content/Admin/AdminLTE.css", new CssRewriteUrlTransform())
                .Include("~/Content/Selectize/css/selectize.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/js/admin/common").Include(
                "~/Scripts/jquery-2.1.3.min.js",
                "~/Content/Selectize/js/standalone/selectize.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/angular-filter.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/app.js"
                ));


            /******   RESTAURANT ADMIN   ******/
            bundles.Add(new StyleBundle("~/css/restaurantAdmin/panel")
                .Include("~/Content/bootstrap-timepicker.min.css", new CssRewriteUrlTransform())
                .Include("~/Areas/RestaurantAdmin/Content/Site.css", new CssRewriteUrlTransform())
                .Include("~/Content/Admin/ionicons.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/js/restaurantAdmin/employee").Include(
                "~/Scripts/bootstrap-notify.js",
                "~/Scripts/Admin/Common/directives.js",
                "~/Scripts/Admin/Common/services.js",
                "~/Scripts/Admin/RestaurantAdmin/app.js",
                "~/Scripts/Admin/RestaurantAdmin/service/services.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/controllersEmployee.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/cuisineController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/dishGroupController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/dishController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/newOrdersController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/pendingOrdersController.js"
                ));

            bundles.Add(new ScriptBundle("~/js/restaurantAdmin/admin").Include(
                "~/Scripts/Admin/RestaurantAdmin/controller/controllersAdmin.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/openingHoursController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/opinionsController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/restaurantController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/deliveryRangeController.js",
                "~/Scripts/Admin/RestaurantAdmin/controller/employeeController.js"
                ));


            /******   SITE ADMIN   ******/
            bundles.Add(new ScriptBundle("~/js/siteAdmin").Include(
                "~/Scripts/Admin/Common/directives.js",
                "~/Scripts/Admin/SiteAdmin/app.js",
                "~/Scripts/Admin/SiteAdmin/controller/controllers.js",
                "~/Scripts/Admin/SiteAdmin/controller/restaurantController.js",
                "~/Scripts/Admin/SiteAdmin/service/service.js"
                ));
        }
    }
}
