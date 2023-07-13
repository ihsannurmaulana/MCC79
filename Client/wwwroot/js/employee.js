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
                        <button onclick="UpdateGet('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdate" class="btn btn-primary text-center"><i class="fas fa-fw fa-edit"></i></button>
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
        gender: ($('#gender').val() === "Female") ? 0 : 1,
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

 // Update
function UpdateGet(data) {
    $.ajax({
        url: "https://localhost:7010/api/employees/" + data, // Sesuaikan URL dengan endpoint API yang benar
        type: "GET",
        dataType: "json",
    }).done(res => {
        $('#UGuid').val(res.data.guid);
        $('#UNik').val(res.data.nik);
        $('#UpdatefirstName').val(res.data.firstName);
        $('#UpdatelastName').val(res.data.lastName);
        $('#UpdatebirthDate').val(moment(res.data.birthDate).format('YYYY-MM-DD'));
        $('#UpdateGender').val(res.data.gender === 0 ? "Female" : "Male");
        $('#Updateemail').val(res.data.email);
        $('#UpdatephoneNumber').val(res.data.phoneNumber);
        $('#UpdatehiringDate').val(moment(res.data.hiringDate).format('YYYY-MM-DD'));
    }).fail(error => {
        alert("Inseert failed")
    });
}

function UpdateEmployee() {
    var obj = {
        guid: $('#UGuid').val(),
        nik: $('#UNik').val(),
        firstName: $("#UpdatefirstName").val(),
        lastName: $("#UpdatelastName").val(),
        birthDate: $("#UpdatebirthDate").val(),
        gender: ($('#UpdateGender').val() === "Female") ? 0 : 1,
        hiringDate: $("#UpdatehiringDate").val(),
        email: $("#Updateemail").val(),
        phoneNumber: $("#UpdatephoneNumber").val()
    };

    $.ajax({
        url: "https://localhost:7010/api/employees", // Sesuaikan URL dengan endpoint API yang benar
        type: "PUT",
        data: JSON.stringify(obj),
        contentType: "application/json",
        //dataType: "json"
    }).done(result => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika berhasil
        Swal.fire(
            'Good job!',
            'Data has been successfuly updated!',
            'success'
        ).then(() => {
            location.reload(); // Mereset form
        });

    }).fail(error => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika gagal
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data! Please try again.'
        })
    });
}

// Delete 
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

// Mengambil data employee dari API menggunakan metode GET
function showChart() {
    // Mendapatkan data dari API untuk kedua jenis chart
    $.ajax({
        url: "https://localhost:7010/api/employees/get-all-master-employee", // Sesuaikan URL sesuai dengan endpoint API Anda
        type: "GET",
        dataType: "json"
    }).done(res => {
        // Menghitung jumlah jenis kelamin
        let femaleCount = 0;
        let maleCount = 0;
        for (let i = 0; i < res.data.length; i++) {
            if (res.data[i].gender === 0) {
                femaleCount++;
            } else if (res.data[i].gender === 1) {
                maleCount++;
            }
        }

        // Menghitung total data
        let totalCount = femaleCount + maleCount;

        // Menghitung persentase jenis kelamin
        let femalePercentage = (femaleCount / totalCount) * 100;
        let malePercentage = (maleCount / totalCount) * 100;

        // Membuat grafik jenis kelamin menggunakan Chart.js
        let genderCtx = document.getElementById('genderChart').getContext('2d');
        let genderChart = new Chart(genderCtx, {
            type: 'pie',
            data: {
                labels: ['Female', 'Male'],
                datasets: [{
                    data: [femalePercentage, malePercentage],
                    backgroundColor: ['#FF6384', '#36A2EB'],
                    hoverBackgroundColor: ['#FF6384', '#36A2EB']
                }]
            },
            options: {
                responsive: true,
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            let label = data.labels[tooltipItem.index];
                            let value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return label + ': ' + value.toFixed(2) + '% (' + Math.round(value * totalCount / 100) + ')';
                        }
                    }
                }
            }
        });

        // Menghitung jumlah universitas
        let universities = {};
        for (let i = 0; i < res.data.length; i++) {
            const universityName = res.data[i].universityName;
            if (universityName in universities) {
                universities[universityName]++;
            } else {
                universities[universityName] = 1;
            }
        }

        // Mengumpulkan data untuk grafik universitas
        const universityNames = Object.keys(universities);
        const universityCounts = Object.values(universities);

        // Membuat grafik universitas menggunakan Chart.js
        let universityCtx = document.getElementById('universityChart').getContext('2d');
        let universityChart = new Chart(universityCtx, {
            type: 'doughnut',
            data: {
                labels: universityNames,
                datasets: [{
                    data: universityCounts,
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#8d6e63', '#66bb6a', '#ba68c8'],
                    hoverBackgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#8d6e63', '#66bb6a', '#ba68c8']
                }]
            },
            options: {
                responsive: true,
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            let label = data.labels[tooltipItem.index];
                            let value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return label + ': ' + value;
                        }
                    }
                }
            }
        });

        // Membuka modal setelah grafik selesai dibuat
        $('#modalChart').modal('show');
    }).fail(error => {
        alert("Failed to fetch data from API.");
    });
}


