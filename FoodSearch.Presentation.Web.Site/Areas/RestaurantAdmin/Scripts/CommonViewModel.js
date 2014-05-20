//navigation

var vmOpeningHours;
var vmCuisines;
var vmDishGroups;
var vmDishes;

$(document).ready(function () {
    $("aside section.sidebar a[data-page]").click(function () {
        $("aside.right-side").hide();
        $(".sidebar-menu .active").toggleClass("active");
        var pageId = $(this).attr("data-page");
        $("#" + pageId).show();
        $(this).parent().toggleClass("active");
    });

    vmOpeningHours = new OpeningHoursViewModel();
    vmCuisines = new CuisinesViewModel();
    vmDishGroups = new DishGroupsViewModel();
    vmDishes = new DishesViewModel();
    ko.applyBindings(vmOpeningHours, document.getElementById("pageOpeningHours"));
    ko.applyBindings(vmCuisines, document.getElementById("pageCuisines"));
    ko.applyBindings(vmDishGroups, document.getElementById("pageDishGroups"));
    ko.applyBindings(vmDishes, document.getElementById("pageDishes"));
});

