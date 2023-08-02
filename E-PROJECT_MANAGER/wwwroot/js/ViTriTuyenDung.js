﻿
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
            var tenViTriTuyenDung = $('#ten-vi-tri-tuyen-dung').val();
            var title = $('#title').val();
            var request = $('#request').val();
            var number = $('#number').val();
            var PhongBanID = $('#PhongBanID').val();
            var id = $(this).attr('sp-id');
            //Xay dung Oject San pham
            var sanPham = {
                TenViTriTuyenDung: tenViTriTuyenDung,
                Title: title,
                Request: request,
                Number: number,
                PhongBanID: parseInt(PhongBanID),
                Id: id
            }
            debugger
            SP.SaveSanPham(sanPham);
        })
        $('.btn-delete-san-pham').off('click').on('click', function () {
            var _id = $(this).attr('sp-id');
            SP.DeleteSanPham(_id);
        });
        //$('#export-excel').off('click').on('click', function () {
        //    var exportActionURL = "/UngVien/ExportToExcel";
        //    $.ajax({
        //        url: exportActionURL,
        //        type: "POST",
        //        xhrFields: {
        //            responseType: 'blob'
        //        },
        //        success: function (data) {
        //            var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        //            var link = document.createElement('a');
        //            link.href = window.URL.createObjectURL(blob);
        //            link.download = "ungVienExcel.xlsx";
        //            link.click();
        //            window.URL.revokeObjectURL(link.href);
        //        }
        //    });
        //})
    }, // Tat ca cac su kien dang ky vao day
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/ViTriTuyenDung/ResposeDataTables",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" },
                { "data": "tenViTriTuyenDung", "name": "TenViTriTuyenDung" },
                { "data": "title", "name": "Title" },
                
                { "data": "request", "name": "Request" },
                {
                  "data": "number", "name": "Number"
                },
                { "data": "getPhongBan.tenPhongBan", "name": "PhongBan" },
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
        $.get('/ViTriTuyenDung/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        })
    },
    SaveSanPham: function (sanPham) {
        $.post('/ViTriTuyenDung/Save', sanPham, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    DeleteSanPham: function (id) {
        alert(id)
        $.get('/ViTriTuyenDung/DeleteItem?id=' + id, function (response) {
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