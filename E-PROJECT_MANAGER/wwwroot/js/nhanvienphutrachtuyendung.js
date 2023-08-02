
var SP = {
    
    Init: function () {
        SP.LoadDataToDataTable();
        SP.RegisterEvent();
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
            var nhanVienId = $('#NhanVienId').val();
            var ViTriTuyenDungId = $('#ViTriTuyenDungId').val();
            var id = $(this).attr('sp-id');
            //Xay dung Oject San pham
            var sanPham = {
                NhanVienId: nhanVienId,
                ViTriTuyenDungId: parseInt(ViTriTuyenDungId),
                Id: id
            }
            SP.SaveSanPham(sanPham);
        })
        $('.btn-delete-san-pham').off('click').on('click', function () {
            var _id = $(this).attr('sp-id');
            SP.DeleteSanPham(_id);
        });

    }, // Tat ca cac su kien dang ky vao day
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/NhanVienPhuTrachTuyenDung/ResposeDataTables",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" }, 
                { "data": "nhanVienId", "name": "NhanVienId" },
                { "data": "getViTriTuyenDung.tenViTriTuyenDung", "name": "ViTriTuyenDung" },
                
                
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
        $.get('/NhanVienPhuTrachTuyenDung/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        })
    },
    SaveSanPham: function (sanPham) {
        $.post('/NhanVienPhuTrachTuyenDung/Save', sanPham, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    DeleteSanPham: function (id) {
        alert(id)
        $.get('/NhanVienPhuTrachTuyenDung/DeleteItem?id=' + id, function (response) {
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