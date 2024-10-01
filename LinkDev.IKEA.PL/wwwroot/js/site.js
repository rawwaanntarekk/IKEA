var searchInput = $("#searchInput");
var table = $("table");
searchInput.on("keyup", function (event, name = "Employee") {
    var searchValue = searchInput.val();
    $.ajax({
        url: `/${name}/Search`,
        type: "GET",
        data: { search: searchValue },
        success: function (response) {
            table.html(response);
        },
        error: function (error) {
            console.log(error);
        }
    });

})