var UL = {
    Init: function () {
        UL.RegisterEvent();
    },
    RegisterEvent: function () {
        $('#validatedCustomFile').off('change').on('change', function () {
            var formData = new FormData();
            var file = $(this).get(0).files[0];
            formData.append(file.name, file);
            UL.UploadSingleFile(formData, $(this));
        });

        $('#contact_form_submit').off('click').on('click', function () {
            var formData = new FormData();
            formData.append('TenUngVien', $('#name').val());
            formData.append('phoneNumber', $('#phoneNumber').val());
            formData.append('Email', $('#email').val());
            formData.append('ViTriUngTuyen', $('#jobSector').val());
            formData.append('KinhNghiemLamViec', $('#message').val());

            UL.AddFullDetail(formData);
        })
    },
    UploadSingleFile: function (formData, element) {
        $.ajax({
            url: "/UploadFile/UploadSingleFile",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                var urlFile = "/upload/" + response;

                UL.RegisterEvent();
            }
        });
    },
    AddFullDetail: function (formData) {
        $.ajax({
            url: "UngVien/AddFullDetail",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                // Xử lý sau khi dữ liệu được lưu vào bảng UngVien
                aler('Data saved successfully!');
                // Reset form
                $('#your-form-id')[0].reset();
            },
            error: function () {
                alert('Error occurred while saving data.');
            }
        })
    }

}
UL.Init();