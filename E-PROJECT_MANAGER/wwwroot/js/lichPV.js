var LPV = {
    Init: function () {
        LPV.DatePicker = null;
        LPV.DateRangePicker = null;
        
        LPV.RegisterEvent();
        LPV.CreateCalendar();
        
    },
    RegisterEvent: function () {
        $('.save-lich').off('click').on('click', function () {
            var tenLich = $('#ten-lich').val();
            console.log(LPV.DatePicker);
            var ngayPV = LPV.DatePicker.startDate.format('YYYY-MM-DD');
            console.log(LPV.DateRangePicker);
            var startDate = LPV.DateRangePicker.startDate.format('YYYY-MM-DD hh:mm:ss');
            var endDate = LPV.DateRangePicker.endDate.format('YYYY-MM-DD hh:mm:ss');
            var viTriTD = parseInt($('#vi-tri-td').val());
            var ungVienId = 1;
            var params = {
                tenLich: tenLich,
                Id: $('.modal-body').attr('data-id'),
                NgayPhongVan: ngayPV,
                ThoiGianBatDau: startDate,
                ThoiGianKetThuc: endDate,
                ViTriTuyenDungId: viTriTD,
                UngVienId: ungVienId
            }
            LPV.Save(params, function () {
                $('.modal').modal('hide');
            });
        })
    },
    CreateCalendar: function () {

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                selectable: true,
                headerToolbar: {
                    left: 'prev,next',
                    center: 'title',
                    right: 'dayGridWeek,dayGridDay,dayGridMonth,dayGridYear' // user can switch between the two
                },
                events: '/LichPhongVans/FindAllEvent',
                editable: true, // Cho phép kéo và thả sự kiện
                eventDrop: function (info) {
                    var event = info.event;
                    var start = event.start;
                    var end = event.end;
                    console.log('Sự kiện đã được kéo và thả:', event);
                    console.log('Thời gian bắt đầu mới:', start);
                    console.log('Thời gian kết thúc mới:', end);

                    // Gửi yêu cầu AJAX để cập nhật cơ sở dữ liệu
                    $.ajax({
                        url: '/LichPhongVans/UpdateEvent',
                        type: 'POST',
                        data: {
                            id: event.id,
                            start: moment(start).format("YYYY-MM-DD"),
                            end: moment(end).format("YYYY-MM-DD")
                        },
                        success: function () {
                            // Thực hiện các thay đổi hoặc xử lý dữ liệu theo ý muốn

                            console.log('Cập nhật thành công');
                        },
                        error: function () {
                            console.log('Có lỗi xảy ra khi cập nhật');
                        }
                    });
                },
                eventDidMount: function (info) {
                    // Xử lý dữ liệu sự kiện nhận được từ server và hiển thị lên lịch
                    var event = info.event;
                    var start = event.start;
                    var end = event.end;

                    // Thực hiện các thay đổi hoặc hiển thị dữ liệu theo ý muốn
                    console.log('Sự kiện:', event);
                    console.log('Thời gian bắt đầu:', start);
                    console.log('Thời gian kết thúc:', end);
                },
                eventClick: function (info) {
                    var id = info.event.id;
                    var startDate = info.event.start;
                    var tenLich = info.event.title.split('\n')[0].replace("Lịch phỏng vấn ", ""); // Lấy tên lịch phỏng vấn từ title
                    var thoiGianTuyenDung = info.event.extendedProps.thoiGianTuyenDung; // Lấy giá trị trường 'thoiGianTuyenDung'
                    var viTriTuyenDungId = info.event.extendedProps.viTriTuyenDungId; // Lấy giá trị trường 'ViTriTuyenDungId'
                    var ungVienId = info.event.extendedProps.ungVienId; // Lấy giá trị trường 'UngVienId'

                    // Lấy thông tin ứng viên dựa trên ID của sự kiện
                    $.get('/LichPhongVans/CreateOrUpdateView?id=' + id, function (candidateInfo) {
                        // Điền thông tin vào modal
                        LPV.OpenModalSave(startDate, id, candidateInfo, tenLich, viTriTuyenDungId, ungVienId, thoiGianTuyenDung);
                    });
                    LPV.OpenModalSave(startDate, id, candidateInfo, tenLich, viTriTuyenDungId, ungVienId, thoiGianTuyenDung);

                },
                select: function (info) {
                    
                    LPV.OpenModalSave(info.start,0);
                    var start = moment(info.start);
                    //console.log(start);
                    
                    //LPV.CreateDateRangePicker(info.start);
                    LPV.RegisterEvent();
                }

            });
            calendar.render();
        });
    },
    CreateDatePicker: function (startDate, df_ngay, df_startDate, df_endDate) {
        $('#ngay-pv').datepicker('destroy');
        LPV.DatePicker = $('#ngay-pv').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            startDate: df_ngay == "" ? startDate : moment(df_ngay),
            minYear: 1901,
            maxYear: parseInt(moment().format('YYYY'), 10)
        }, function (startChoose) {
            LPV.CreateDateRangePicker(startChoose.format('YYYY-MM-DD'));
        }).data('daterangepicker');
        LPV.CreateDateRangePicker(startDate, df_startDate, df_endDate);
        LPV.RegisterEvent();
    },
    CreateDateRangePicker: function (ngayPv, df_startDate, df_endDate) {
        //range-pv
        $('#range-pv').datepicker('destroy');
        LPV.DateRangePicker = $('#range-pv').daterangepicker({
            timePicker: true,
            startDate: df_startDate == "" ? moment(ngayPv).add(8, 'hour') : moment(df_startDate),
            endDate: df_endDate == "" ? moment(ngayPv).add(10, 'hour') : moment(df_endDate),
            locale: {
                format: 'hh:mm A'
            }
        }).data('daterangepicker');
        LPV.RegisterEvent();
    },
    Save: function (params) {
        $.post('/LichPhongVans/Save', params, function (response) {
            alert(response);
            LPV.RegisterEvent();
        })
    },
    OpenModalSave: function (startDate, id, candidateInfo, tenLich, viTriTuyenDungId, ungVienId, thoiGianTuyenDung) {
        $.get('/LichPhongVans/CreateOrUpdateView?id=' + id, function (response) {
            $('.modal').html('').html(response);
            $('.modal').modal('show');
            // Gán giá trị cho các trường dữ liệu
            var lichPhongVan = JSON.parse(response); // Phân tích phản hồi thành đối tượng JSON (nếu cần)
            $('#ten-lich').val(lichPhongVan.tenLich); // Gán giá trị cho trường 'ten-lich'
            $('#vi-tri-td').val(lichPhongVan.ViTriTuyenDungId);
            $('#thoi-gian-tuyen-dung').val(thoiGianTuyenDung); // Gán giá trị cho trường 'thoi-gian-tuyen-dung'
            // Gán thông tin ứng viên vào các trường modal
            //$('#candidate-name').val(candidateInfo.name);
            //$('#candidate-position').val(candidateInfo.position);

            //var ngayPV = moment(startDate).format('YYYY-MM-DD');
            //$('#ngay-pv').val(ngayPV);

            var df_startDate = $('.modal-body').attr('data-start');
            var df_endDate = $('.modal-body').attr('data-end');
            var df_ngay = $('.modal-body').attr('data-ngay');
            LPV.CreateDatePicker(startDate,df_ngay, df_startDate, df_endDate);
            LPV.RegisterEvent();
        })
    }
}
LPV.Init();