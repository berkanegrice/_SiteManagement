
$(document).ready(function () {
    $("#duesInformationDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "responsive": true,
        "ajax": {
            "type": "GET",
            "url": "/Due/GetDuesInformation",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "leaseHolder", "name": "LeaseHolder", "autoWidth": true },
            { "data": "debt", "name": "Debt", "autoWidth": true },
            { "data": "credit", "name": "Credit", "autoWidth": true },
            { "data": "balanceDebt", "name": "BalanceDebt", "autoWidth": true },
            { "data": "balanceCredit", "name": "BalanceCredit", "autoWidth": true },
            {
                "render": function (data, type, row, meta) { 
                    return "<a class='btn btn-info' style='position:relative; left:50px;' onclick=GetDuesDetail('" + row.id+ "');>Detay</a>";   
                }
            },
        ]
    });
});

var rowId = '';
$(document).on('shown.bs.modal', function () {
    showTable();
});

function GetDuesDetail(arg) {
    rowId = arg;
    $('#detailModal').modal('show');
}

function showTable() {
    $("#duesDetailInformationDatatable").DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "filter": true,
        "responsive": true,
        "ajax": {
            "type": "POST",
            "url": "/Dues/GetDuesDetailedInformation",
            "data" : {"rowId": rowId},
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "render": $.fn.dataTable.render.moment('YYYY-MM-DDTHH:mm:ss', 'DD/MM/YYYY')
            }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "accountCode", "name": "AccountCode", "autoWidth": true },
            { "data": "date", "name": "Date", "autoWidth": true },
            { "data": "detail", "name": "Detail", "autoWidth": true },
            { "data": "debt", "name": "Debt", "autoWidth": true },
            { "data": "credit", "name": "Credit", "autoWidth": true },
            { "data": "balanceDebt", "name": "BalanceDebt", "autoWidth": true },
            { "data": "balanceCredit", "name": "BalanceCredit", "autoWidth": true }
        ]
    });
}

