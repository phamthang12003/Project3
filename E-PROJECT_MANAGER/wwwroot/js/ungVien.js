
//$(document).ready(function () {

//    $('#example').DataTable({
//        processing: true,
//        serverSide: true,
//        ajax: {
//            url: "/UngVien/Filter",
//            type: 'GET'
//        },
//        columns: [
//            { data: "id" },
//            { data: "gioiTinh" },
//            { data: "tuoi" },
//            { data: "tenUngVien" },
//            { data: "diaChi" },
//            { data: "viTriUngTuyen" },
//            { data: "kinhNghiemLamViec" },
//            {
//                data: null,
//                render: function (data, type, row) {
//                    var updateLink = '<a href="/UngVien/Update/' + row.id + '" class="btn btn-warning">Update</a>';
//                    var deleteLink = '<a href="/UngVien/Delete/' + row.id + '" class="btn btn-danger">Delete</a>';
//                    return updateLink + " | " + deleteLink;
//                },
//            }
//        ]
//    });

//});


var SP = {
    Init: function () {
        SP.LoadDataToDataTable();
        SP.RegisterEvent();
        //SP.Init();
    }, 
    RegisterEvent: function () {
        $('#btn-tao-moi').off('click').on('click', function () {
            SP.CreateOrUpdatePatialView(0);
        })
        $('.btn-update-san-pham').off('click').on('click', function () {
            var sp_id = $(this).attr('sp-id');
            SP.CreateOrUpdatePatialView(sp_id);
        })
        $('#btn-save-san-pham').off('click').on('click', function () {
            var gioiTinh = $('#gioi-tinh').val();
            var tuoi = $('#tuoi').val();
            var tenUngVien = $('#ten-ung-vien').val();
            var diaChi = $('#dia-chi').val();
            var viTriUngTuyen = $('#vi-tri-ung-tuyen').val();
            var kinhNghiemLamViec = $('#kinh-nghiem-lam-viec').val();
            var id = $(this).attr('sp-id');
            //Xay dung Oject San pham
            var sanPham = {
                GioiTinh: gioiTinh,
                Tuoi: tuoi,
                TenUngVien: tenUngVien,
                DiaChi: diaChi,
                ViTriUngTuyen: viTriUngTuyen,
                KinhNghiemLamViec: kinhNghiemLamViec,
                Id: id
            }
            SP.SaveSanPham(sanPham);
        })
        $('.btn-delete-san-pham').off('click').on('click', function () {
            var _id = $(this).attr('sp-id');
            SP.DeleteSanPham(_id);
        });
        $('#export-excel').off('click').on('click', function () {
            var exportActionURL = "/UngVien/ExportToExcel";
            $.ajax({
                url: exportActionURL,
                type: "POST",
                xhrFields: {
                    responseType: 'blob'
                },
                success: function (data) {
                    var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    var link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.download = "ungVienExcel.xlsx";
                    link.click();
                    window.URL.revokeObjectURL(link.href);
                }
            });
        })
    }, // Tat ca cac su kien dang ky vao day
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/UngVien/ResposeDataTables",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" },
                { "data": "gioiTinh", "name": "GioiTinh" },
                { "data": "tuoi", "name": "Tuoi" },
                { "data": "tenUngVien", "name": "TenUngVien"},
                {
                    "data": "diaChi", "name": "DiaChi"
                },
                {
                    "data": "viTriUngTuyen", "name": "ViTriUngTuyen"
                },
                {
                    "data": "kinhNghiemLamViec", "name": "KinhNghiemLamViec"
                },
                {
                    "data": "id", "render": function (data) {
                        return `
                            <div class="btn-group" role="group" aria-label="Basic example">
                                
                                <a href="javascript:void(0)" sp-id=${data} class="btn btn-warning btn-update-san-pham">Cập nhật</a>
                                <a href="javascript:void(0)" sp-id=${data} class="btn btn-danger btn-delete-san-pham">Xóa</a>
                            </div>
                        `;
                    }
                }
            ],
            "searching": true,
            'searchDelay': 500,
            'search': {
                'regex': false
            },
            "paging": true,
            "initComplete": function () {
                SP.RegisterEvent();
            },
            "drawCallback": function (settings) {
                SP.RegisterEvent();
            }
        });
        SP.RegisterEvent();
    },
    CreateOrUpdatePatialView: function (id) {
        $.get('/UngVien/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        })
    },
    SaveSanPham: function (ungvien) {
        $.post('/UngVien/Save', ungvien, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    DeleteSanPham: function (id) {
        alert(id)
        $.get('/UngVien/DeleteItem?id=' + id, function (response) {
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    ReloadDataTable: function () {
        var table = $('#datatable').DataTable();
        table.ajax.reload(null, false);
        SP.RegisterEvent();
    }


}


SP.Init();




