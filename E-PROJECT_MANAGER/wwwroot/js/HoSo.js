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
            var ungVienId = $('#ung-vien-id').val();
            var loaiHoSo = $('#loai-ho-so').val();
            var linkHoSo = $('#link-ho-so').val();
            var UngVienId = $('#UngVienId').val();
            var id = $(this).attr('sp-id');
            //Xay dung Oject San pham
            var hoso = {
                /*UngVienID: ungVienId,*/
                LoaiHoSo: loaiHoSo,
                LinkHoSo: linkHoSo,
                UngVienId: parseInt(UngVienId),
                Id: id
            }
            SP.SaveSanPham(hoso);
        })
        $('.btn-delete-san-pham').off('click').on('click', function () {
            var _id = $(this).attr('sp-id');
            SP.DeleteSanPham(_id);
        });
       
    }, 
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/HoSo/ResposeDataTables",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" },
                /*{ "data": "ungVienId", "name": "UngVienId" },*/
                { "data": "loaiHoSo", "name": "LoaiHoSo" },
                { "data": "linkHoSo", "name": "LinkHoSo" },
                { "data": "getUngVien.tenUngVien", "name": "UngVien" },
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
        $.get('/HoSo/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        })
    },
    SaveSanPham: function (hoso) {
        $.post('/HoSo/Save', hoso, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        })
    },
    DeleteSanPham: function (id) {
        alert(id)
        $.get('/HoSo/DeleteItem?id=' + id, function (response) {
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