$(document).ready(function () {
    var searchInput = $("#searchInput");
    var table = $("table");

    searchInput.on("keyup", function () {
        var searchValue = searchInput.val();

        $.ajax({
            url: `/${controllerName}/Search`,
            type: "GET",
            data: { search: searchValue },
            success: function (response) {
                table.html(response);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});
