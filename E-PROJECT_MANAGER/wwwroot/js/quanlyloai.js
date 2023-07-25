// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var SP = {
    Init: function () {
        SP.LoadDataToDataTable();
        SP.RegisterEvent();
    },
    RegisterEvent: function () {
        $('#btn-tao-moi').off('click').on('click', function () {
            SP.CreateOrUpdatePatialView(0);
        })
        $('.btn-update-loai').off('click').on('click', function () {
            var sp_id = $(this).attr('sp-id');
            SP.CreateOrUpdatePatialView(sp_id);
        })
        $('#btn-save-loai').off('click').on('click', function () {
            var tenLoai = $('#ten-loai').val();
            var giaTri = $('#gia-tri').val();
            var csscLass = $('#css-class').val();
            var sapXep = $('#sap-xep').val();

            var id = $(this).attr('loai-id');
            //Xay dung Oject San pham
            var loai = {
                TenLoai: tenLoai,
                GiaTri: giaTri,
                CSSClass: csscLass,
                SapXep: sapXep,
                Id: id
            }
            SP.SaveLoai(loai);
        })
        $('.btn-delete-loai').off('click').on('click', function () {
            var _id = $(this).attr('loai-id');
            SP.DeleteLoai(_id);
        })
    }, // Tat ca cac su kien dang ky vao day
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/QuanLyLoai/Filter",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" },
                { "data": "tenLoai", "name": "TenLoai" },
                { "data": "giaTri", "name": "GiaTri" },
                { "data": "csscLass", "name": "CSSClass" },
                { "data": "sapXep", "name": "SapXep" },

                
                {
                    "data": "id", "render": function (data) {
                        return `
                            <div class="btn-group" role="group" aria-label="Basic example">
                                
                                <a href="javascript:void(0)" sp-id=${data} class="btn btn-warning btn-update-loai">Cập nhật</a>
                                <a href="javascript:void(0)" sp-id=${data} class="btn btn-danger btn-delete-loai">Xóa</a>
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
        $.get('/QuanLyLoai/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        })
    },
    SaveLoai: function (QuanLyLoai) {
        $.post('/QuanLyLoai/Save', QuanLyLoai, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    DeleteLoai: function (id) {
        alert(id)
        $.get('/QuanLyLoai/DeleteItem?id=' + id, function (response) {
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