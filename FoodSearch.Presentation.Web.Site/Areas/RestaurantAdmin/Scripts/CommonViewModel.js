//navigation

$(document).ready(function () {
    $("aside section.sidebar a[data-page]").click(function () {
        $("aside.right-side").hide();
        $(".sidebar-menu .active").toggleClass("active");
        var pageId = $(this).attr("data-page");
        $("#" + pageId).show();
        $(this).parent().toggleClass("active");
    });

    ko.applyBindings(new OpeningHoursViewModel(), document.getElementById("pageOpeningHours"));
});

