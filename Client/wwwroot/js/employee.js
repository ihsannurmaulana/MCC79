$(document).ready(function () {
    moment.locale('id');
    $('#myTableEmployee').DataTable({
        ajax: {
            url: "https://localhost:7010/api/employees",
            dataType: "JSON",
            dataSrc: "data" //data source -> butuh array of object
        },
        dom: /*'Bfrtip',*/
            "<'row justfy-content-center'<'col-md-2'<'d-flex justify-content-start'l>><'col-md-7 text-center d-flex justify-content-end'B><'col-md-3'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7'p>>",
        
        buttons: [ 
            {
                extend: 'colvis',
                collectionLayout: 'fixed three-column',
                postfixButtons: ['colvisRestore']
            },
            {
                extend: 'print',
                exportOptions: {
                     columns: ':visible'
                }
                
            },
            {
                extend: 'excelHtml5',
                title: 'Excel',
                text: 'Export to Excel',
                //Columns to export
                exportOptions: {
                     columns: [0, 1, 2, 3,4,5]
                }

            },
            {
                extend: 'pdfHtml5',
                title: 'PDF',
                text: 'Export to PDF',
                exportOptions: {
                     columns: [0, 1, 2, 3, 4, 5]
                }
            },
            {
                extend: 'copy',
                
            },
        ],
        autoWidth: false,
        columns: [
            {
                data: 'no',
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            { data: 'fullName', render: function (data, type, row) { return row.firstName + ' ' + row.lastName; } },
            {
                "data": function (row) {
                    return moment(row.birthdate).format("D MMMM YYYY");
                }
            },
            { "data": function (row) {
                    if (row.gender == "0") {
                        return "Female"
                    }
                    else {
                        return "Male"
                    }
                }
            },
            {
                "data": function (row) {
                    return moment(row.hiringDate).format("D MMMM YYYY");
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="detail('${data.url}')" data-bs-toggle="modal" data-bs-target="#" class="btn btn-primary">Detail</button>`;
                }
            },
        ]
    });
});



