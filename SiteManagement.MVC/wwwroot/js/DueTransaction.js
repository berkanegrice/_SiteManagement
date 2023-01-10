function GetDueTransactions(userCode) {
    $('#dueTransactionModal').modal('show');
    $("#DueTransactionDatatable").DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "filter": true,
        "responsive": true,
        "ajax": {
            "type": "POST",
            "url": "/Due/GetDuesTransaction",
            "data" : {"userCode": userCode}
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "render": $.fn.dataTable.render.moment('YYYY-MM-DDTHH:mm:ss', 'DD/MM/YYYY')
            }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "date", "name": "Date", "autoWidth": true },
            { "data": "detail", "name": "Detail", "autoWidth": true },
            {
                "data": "debt", "name": "Debt", "autoWidth": true,
                "render": DataTable.render.number( null, null, 2, '₺' )
            },
            {
                "data": "credit", "name": "Credit", "autoWidth": true,
                "render": DataTable.render.number( null, null, 2, '₺' )
            },
            {
                "data": "balanceDebt", "name": "BalanceDebt", "autoWidth": true,
                "render": DataTable.render.number( null, null, 2, '₺' )
            },
            {
                "data": "balanceCredit", "name": "BalanceCredit", "autoWidth": true,
                "render": DataTable.render.number( null, null, 2, '₺' )
            }
        ]
    });
}