$(document).ready(function () {
    moment.locale('id');
    $('#myTableEmployee').DataTable({
        ajax: {
            url: "https://localhost:7010/api/employees",
            dataType: "JSON",
            dataSrc: "data" //data source -> butuh array of object
        },
        dom: /*'Bfrtip',*/
            "<'row justfy-content-center'<'col-md-2'<'d-flex justify-content-start'l>><'col-md-6 text-center d-flex justify-content-end'B><'col-md-4'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-4'i><'col-md-8'p>>",
        
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
                extend: 'collection',
                text: 'Export',
                className: 'custom-html-collection',
                buttons: [
                    '<h4>Export</h4>',
                    {
                        extend: 'pdfHtml5',
                        title: 'PDF',
                        text: 'to PDF',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'excelHtml5',
                        title: 'Excel',
                        text: 'to Excel',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                ]
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
            {
                data: 'fullName',
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
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
function Insert() {
    var obj = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        birthDate: $("#birthDate").val(),
        gender: $("input[name='gender']:checked").attr("id") === "Female" ? 0 : 1,
        hiringDate: $("#hiringDate").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val()
    };

    $.ajax({
        url: "https://localhost:7010/api/employees", // Sesuaikan URL dengan endpoint API yang benar
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
        //dataType: "json"
    }).done(result => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika berhasil
        alert("Data inserted successfully!");
        location.reload(); // Mereset form
    }).fail( error => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika gagal
        alert("Failed to insert data.");
    });
}



