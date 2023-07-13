$(document).ready(function () {
    moment.locale('id');
    $('#employeeTable').DataTable({
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
                    '<h4>Export</h4> <hr />',
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
    });
});


//document.getElementById('updateAlert').addEventListener('click', function (event) {
//    event.preventDefault(); // Mencegah pengiriman formulir secara default
function updateAlert() {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You are about to update the employee record.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, update it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // Tampilkan SweetAlert sukses setelah konfirmasi
            Swal.fire({
                icon: 'success',
                title: 'Sukses',
                text: 'Data berhasil disimpan!',
                confirmButtonText: 'OK'
            }).then(() => {
                // Mereload halaman setelah menekan tombol "OK"
                window.location.href = "/Employees/Index";
            });
        }
    });
}

function showSuccessToast(message) {
    Swal.fire({
        icon: 'success',
        title: 'Success bro',
        text: message,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
}
