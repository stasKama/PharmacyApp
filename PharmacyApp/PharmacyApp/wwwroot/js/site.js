$(document).ready(function () {
    $("#submit-form").submit(function () {
        var name = $("#search-field").val().replace(/^\s+/, "");
        if (name == null || name == "") {
            $("#search-field").val("");
            return false;
        }
    });
});