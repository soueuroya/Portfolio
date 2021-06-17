var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#DT_load").DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "releaseYear", "width": "20%" },
            {
                "data": "format",
                "render": function (data, type, row) {
                    if (data != null) {
                        return `<img src="/Images/${row.id}${data}" style="display: block; width: 100%;"/>`;
                    }
                    else {
                        return ``;
                    }
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    var options = `<div class="text-left">
                    <a href="/BookList/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                        Edit
                    </a>
                    <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                        onclick=Delete("/api/Book?id=${data}")>
                        Delete
                    </a>
                    `;
                    if (!row.cart) {
                        options += `<a class='btn btn-info text-white' style='cursor:pointer; width:70px;'
                            onclick=Buy("/api/Book?id=${data}&a=b") >
                                Buy
                    </a ></div>`
                    }
                    else {
                        options += "</div>";
                    }
                    return options;
                }, "width": "30%"
            }
        ],
        "createdRow": (row, data, dataIndex) => {
            if (data.bought) {
                $(row).addClass("greenClass");
            }

            if (data.cart) {
                $(row).addClass("yellowClass");
            }
        },
        "language": {
            "emptyTable": "No books available"
        },
        "width": "100%"
    });
}

function Delete(url) {
    $.ajax({
        type: "DELETE",
        url: url,
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
            }
            else {
                alert(data.message);
            }
        }
    });
}

function Buy(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
            }
            else {
                alert(data.message);
            }
        }
    });
}