(function() {
    $(document).ready(function() {
        var cookieValue = $.cookie("cookiesPolicy");
        if (cookieValue == null) {
            $("#cookieAlertContainer").show();
            $("#closeCookieAlert").click(function() {
                $("#cookieAlertContainer").hide();
                $.cookie("cookiesPolicy", true, { expires: 30, path: '/' });
            });
        }
    });
})();