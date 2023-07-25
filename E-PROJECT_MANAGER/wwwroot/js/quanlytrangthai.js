var SP = {
    Init: function () {
        SP.LoadDataToDataTable();
        SP.RegisterEvent();
    },
    RegisterEvent: function () {
        $('#btn-tao-moi').off('click').on('click', function () {
            SP.CreateOrUpdatePatialView(0);
        });
        $('.btn-update-trang-thai').off('click').on('click', function () {
            var sp_id = $(this).attr('sp-id');
            SP.CreateOrUpdatePatialView(sp_id);
        });
        $('#btn-save-trang-thai').off('click').on('click', function () {
            var tenBang = $('#ten-bang').val();
            var tenTrangThai = $('#ten-trang-thai').val();
            var giaTri = $('#gia-tri').val();
            var cssClass = $('#css-class').val();
            var sapXep = $('#sap-xep').val();
            var id = $(this).attr('data-id');
            // Xây dựng Object QuanLyTrangThai
            var trangThai = {
                TenBaang: tenBang,
                TenTrangThai: tenTrangThai,
                GiaTri: giaTri,
                CSSClass: cssClass,
                SapXep: sapXep,
                Id: id
            };
            SP.SaveQuanLyTrangThai(trangThai);
        });
        $('.btn-delete-trang-thai').off('click').on('click', function () {
            var _id = $(this).attr('data-id');
            SP.DeleteQuanLyTrangThai(_id);
        });
    }, // Tất cả các sự kiện đăng ký vào đây
    LoadDataToDataTable: function () {
        $('#datatable').DataTable({
            "serverSide": true,
            "ajax": {
                "url": "/QuanLyTrangThai/Filter",
                "type": "POST"
            },
            "columns": [
                { "data": "id", "name": "Id" },
                { "data": "tenBang", "name": "TenBaang" },
                { "data": "tenTrangThai", "name": "TenTrangThai" },
                { "data": "giaTri", "name": "GiaTri" },
                { "data": "cssClass", "name": "CSSClass" },
                { "data": "sapXep", "name": "SapXep" },
                {
                    "data": "id", "render": function (data) {
                        return `
                            <div class="btn-group" role="group" aria-label="Basic example">
                                <a href="javascript:void(0)" tt-id=${data} class="btn btn-warning btn-update-trang-thai">Cập nhật</a>
                                <a href="javascript:void(0)" tt-id=${data} class="btn btn-danger btn-delete-trang-thai">Xóa</a>
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
        $.get('/QuanLyTrangThai/ViewCreateOrUpdate?id=' + id, function (response) {
            $('#container-create-or-update-modal').html('').html(response);
            $('#create-or-update-modal').modal('show');
            SP.RegisterEvent();
        });
    },
    SaveQuanLyTrangThai: function (quanlytrangthai) {
        $.post('/QuanLyTrangThai/Save', quanlytrangthai, function (response) {
            $('#create-or-update-modal').modal('hide');
            console.log(response);
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        });
    },
    DeleteQuanLyTrangThai: function (id) {
        alert(id);
        $.get('/QuanLyTrangThai/DeleteItem?id=' + id, function (response) {
            alert(response.message);
            SP.ReloadDataTable();
            SP.RegisterEvent();
        });
    },
    ReloadDataTable: function () {
        var table = $('#datatable').DataTable();
        table.ajax.reload(null, false);
        SP.RegisterEvent();
    }
};
SP.Init();



//var SP = {
//    Init: function () {
//        SP.LoadDataToDataTable();
//        SP.RegisterEvent();
//    },
//    RegisterEvent: function () {
//        $('#btn-tao-moi').off('click').on('click', function () {
//            SP.CreateOrUpdatePatialView(0);
//        })
//        $('.btn-update-trang-thai').off('click').on('click', function () {
//            var sp_id = $(this).attr('sp-id');
//            SP.CreateOrUpdatePatialView(sp_id);
//        })
//        $('#btn-save-trang-thai').off('click').on('click', function () {
//            var tenBang = $('#ten-bang').val();
//            var tenTrangThai = $('#ten-trang-thai').val();
//            var giaTri = $('#gia-tri').val();
//            var cssClass = $('#css-class').val();
//            var sapXep = $('#sap-xep').val();
//            var id = $(this).attr('data-id');
//            //Xay dung Oject San pham
//            var trangThai = {
//                TenBaang: tenBang,
//                TenTrangThai: tenTrangThai,
//                GiaTri: giaTri,
//                CSSClass: cssClass,
//                SapXep: sapXep,
//                Id: id
//            }
//            SP.SaveSanPham(trangThai);
//        })
//        $('.btn-delete-trang-thai').off('click').on('click', function () {
//            var _id = $(this).attr('data-id');
//            SP.DeleteSanPham(_id);
//        })
//    }, // Tat ca cac su kien dang ky vao day
//    LoadDataToDataTable: function () {
//        $('#datatable').DataTable({
//            "serverSide": true,
//            "ajax": {
//                "url": "/QuanLyTrangThai/Filter",
//                "type": "POST"
//            },

//            "columns": [
//                { "data": "id", "name": "Id" },
//                { "data": "tenBaang", "name": "TenBaang" },
//                { "data": "tenTrangThai", "name": "TenTrangThai" },
//                { "data": "giaTri", "name": "GiaTri" },
//                { "data": "cssClass", "name": "CSSClass" },
//                { "data": "sapXep", "name": "SapXep" },
//                {
//                    "data": "id", "render": function (data) {
//                        return `
//                            <div class="btn-group" role="group" aria-label="Basic example">
//                                <a href="javascript:void(0)" tt-id=${data} class="btn btn-warning btn-update-trang-thai">Cập nhật</a>
//                                <a href="javascript:void(0)" tt-id=${data} class="btn btn-danger btn-delete-trang-thai">Xóa</a>
//                            </div>
//                        `;
//                    }
//                }
//            ],
//            "searching": true,
//            'searchDelay': 500,
//            'search': {
//                'regex': false
//            },
//            "paging": true,
//            "initComplete": function () {
//                SP.RegisterEvent();
//            },
//            "drawCallback": function (settings) {
//                SP.RegisterEvent();
//            }
//        });
//        SP.RegisterEvent();
//    },
//    CreateOrUpdatePatialView: function (id) {
//        $.get('/QuanLyTrangThai/ViewCreateOrUpdate?id=' + id, function (response) {
//            $('#container-create-or-update-modal').html('').html(response);
//            $('#create-or-update-modal').modal('show');
//            SP.RegisterEvent();
//        })
//    },
//    SaveSanPham: function (quanlytrangthai) {
//        $.post('/QuanLyTrangThai/Save', quanlytrangthai, function (response) {
//            $('#create-or-update-modal').modal('hide');
//            console.log(response);
//            alert(response.message);
//            SP.ReloadDataTable();
//            SP.RegisterEvent();
//        })
//    },
//    DeleteSanPham: function (id) {
//        alert(id)
//        $.get('/QuanLyTrangThai/DeleteItem?id=' + id, function (response) {
//            alert(response.message);
//            SP.ReloadDataTable();
//            SP.RegisterEvent();
//        })
//    },
//    ReloadDataTable: function () {
//        var table = $('#datatable').DataTable();
//        table.ajax.reload(null, false);
//        SP.RegisterEvent();
//    }
//}
//SP.Init();







