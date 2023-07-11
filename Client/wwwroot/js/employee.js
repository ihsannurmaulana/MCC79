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
                    return `
                    <div class="">
                        <button onclick="#" data-bs-toggle="modal" data-bs-target="#modalUpdate" class="btn btn-primary text-center"><i class="fas fa-fw fa-edit"></i></button>
                        <button onclick="Delete('${row.guid}')" data-bs-toggle="modal" data-bs-target="#" class="btn btn-danger text-center"><i class="fas fa-fw fa-trash"></i></button>
                    </div>`;
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
        Swal.fire(
            'Good job!',
            'Data has been successfuly inserted!',
            'success'
        ).then(() => {
            location.reload(); // Mereset form
        });

    }).fail(error => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika gagal
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data! Please try again.'
        })
    });
}

function Delete(deleteId) {
    Swal.fire({
        title: 'Are you sure ?',
        text: "You wan't to able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085D6',
        cancelButtonColor: '#D33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7010/api/employees?guid=" + deleteId, // Sesuaikan URL dengan endpoint API yang benar
                type: "DELETE",
            }).done(result => {
                Swal.fire(
                    'Deleted!',
                    'Your data has been deleted',
                    'succcess'
                ).then(() => {
                    location.reload();
                });
            }).fail(error => {
                // Tampilkan alert pemberitahuan jika gagal
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to delete data! Please try again.'
                }) 
            });
        }
    });
}