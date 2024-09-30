var searchInput = $("#searchInput");
var table = $("table");
searchInput.on("keyup", function (event) {
    var searchValue = searchInput.val();
    $.ajax({
        url: `/${(this).data(model)}/Search`,
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