$(document).ready(function () {
    $("a").click(function (e) {
        if ($(this).attr("href").indexOf("#") === -1) {
            $("#loading").show();
        }
    }); 
    $(window).on('beforeunload', function () {
        $("#loading").show();
    });

    $(window).on('hashchange', function () {
    });
});
