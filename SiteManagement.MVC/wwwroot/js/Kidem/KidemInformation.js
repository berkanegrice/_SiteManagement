
$("#KidemInformationDatatable").DataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "responsive": true,
    "ajax": {
        "type": "POST",
        "url": "/Kidem/GetKidemsInformation",
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
        }
    ],
    "columns": [
        { "data": "id", "name": "Id", "autoWidth": true },
        { "data": "accountCode", "name": "AccountCode", "autoWidth": true },
        { "data": "leaseHolder", "name": "LeaseHolder", "autoWidth": true },
        {
            "data": "debt", "name": "Debt", "autoWidth": true,
            "render": DataTable.render.number( '.', ',', 2, '₺' )
        },
        {
            "data": "credit", "name": "Credit", "autoWidth": true,
            "render": DataTable.render.number( '.', ',', 2, '₺')
        },
        {
            "data": "balanceDebt", "name": "BalanceDebt", "autoWidth": true,
            "render": DataTable.render.number( '.', ',', 2, '₺')
        },
        {
            "data": "balanceCredit", "name": "BalanceCredit", "autoWidth": true,
            "render": DataTable.render.number( '.', ',', 2, '₺')
        },
        {
            "render": function (data, type, row, meta) {
                return "<a class='btn btn-info' style='position:relative; left:50px;' onclick=GetKidemTransactions('" + row.accountCode+ "');>Detay</a>";
            }
        },
    ]
});