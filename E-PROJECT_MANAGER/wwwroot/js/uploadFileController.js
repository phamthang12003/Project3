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
            var params = {
                fullName: $('#name').val(),
                phoneNumber: $('#phoneNumber').val(),
                email: $('#email').val(),
                jobSector: $('#jobSector').val(),
                urlFile: $('#result-upload').val(), // Điền giá trị tương ứng nếu có trường urlFile trong form
                commentMessage: $('#message').val(),
                age: $('#age').val(),
                sex: $('#sex').val(),
                address: $('#address').val(),
                
            };
            console.log(params);
            
            UL.AddFullDetail(params);
        });
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
                $('#result-upload').val(urlFile);
                UL.RegisterEvent();
            }
        });
    },
    AddFullDetail: function (formData) {
        $.ajax({
            url: "/UngVien/AddFullDetail",
            type: "POST",
            data: formData,


            success: function (response) {
                // Xử lý sau khi dữ liệu được lưu vào bảng UngVien
                alert('Data saved successfully!');
                // Reset form
                /*$('#your-form-id').reset();*/
            },
            error: function () {
                alert('Error occurred while saving data.');
            }
        })
    }

}
UL.Init();