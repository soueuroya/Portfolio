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
            { "data": "name", "width": "25%" },
            { "data": "author", "width": "25%" },
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
                    <a class='btn btn-success text-white' style='cursor:pointer; width:90px;'
                        onclick=Checkout("/api/Book?id=${data}&a=c")>
                        Checkout
                    </a>
                    <a class='btn btn-danger text-white' style='cursor:pointer; width:90px;'
                        onclick=Remove("/api/Book?id=${data}&a=r")>
                        Remove
                    </a></div>
                    `;
                    return options;
                }, "width": "30%"
            }
        ],
        "createdRow": (row, data, dataIndex) => {
            if (data.bought || !data.cart) {
                dataTable.rows($(row)).remove();
            }
        },
        "language": {
            "emptyTable": "No books available"
        },
        "width": "100%"
    });
}

function Checkout(url) {
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

function Remove(url) {
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