var HolidayTypes = ["REGULAR", "SPECIAL", "OTHER"];

function fnGetHolidays() {
    var currentMonth = $("#hdrHolidayCalendar").fullCalendar('getDate').format('MM');
    var from = $("#hdrHolidayCalendar").fullCalendar('getView').intervalStart;
    var to = $("#hdrHolidayCalendar").fullCalendar('getView').intervalEnd.add(-1, "days");
    $("#hdrHolidayCalendar").fullCalendar('removeEvents');
    showLoader("#hdrHolidays");
    $.ajax({
        url: action("Schedule", "GetHolidays"),
        type: "get",
        async: true,
        data: {
            from: from.format("YYYY-MM-DD"),
            to: to.format("YYYY-MM-DD"),
            holidayType: $("#sltHolidayType").val(),
            includeInActive: $("#chkShowInactive").is(":checked")
        },
        success: function (data) {
            var holidays = new Array();            
            for (d in data.holidays) {
                var startDate = fnGetZeroBasedMomentDate(convert_to_js_date(data.holidays[d].HolidayDate), 1);
                var holiday = {
                    title: data.holidays[d].HolidayName,
                    start: startDate,                    
                    allDay: true,
                    backgroundColor: "",
                    borderColor: "",
                    ParamName: data.holidays[d].HolidayName,
                    ParamDate: startDate.clone().format("YYYY-MM-DD"),
                    ParamType: data.holidays[d].HolidayType,
                    ParamMultiplier: data.holidays[d].CompensationRateMultiplier,
                    ParamDataGet: data.holidays[d].HolidayIDAsEncryptedURLData,
                    ParamIsActive: data.holidays[d].IsActive
                };
                if (new Boolean(data.holidays[d].IsActive) == false) {
                    holiday.backgroundColor = "#000000";//Black
                    holiday.borderColor = "#000000";//Black
                } else {
                    switch (parseInt(data.holidays[d].HolidayType)) {
                        case 0:
                            holiday.backgroundColor = "#f56954";//Danger (red)
                            holiday.borderColor = "#f56954";//Danger (red)
                            break;
                        case 1:
                            holiday.backgroundColor = "#f39c12";//Warning (orange)
                            holiday.borderColor = "#f39c12";//Warning (orange)
                            break;
                        default:
                            holiday.backgroundColor = "#00a65a";//Success (green)
                            holiday.borderColor = "#00a65a";//Success (green)
                            break;
                    }
                }                               
                holidays.push(holiday);
            }
            $("#hdrHolidayCalendar").fullCalendar('addEventSource', holidays);            
            hideLoader("#hdrHolidays");
        }
    });
}

function fnNewHoliday() {
    $("#mdlHolidayForm #data").val("");
    $("#mdlHolidayForm #HolidayName").val("");
    $("#mdlHolidayForm #CompensationRateMultiplier").val(0);
    $("#mdlHolidayForm").modal("show");
}

function fnInitializeHolidayCalendar() {
    $("#hdrHolidayCalendar").fullCalendar({
        defaultView: 'month',
        editable: true,
        eventRender: function (event, element) {
            if (Boolean(event.ParamIsActive) == true) {
                element.find(".fc-content").append("<span class='closeon pull-right'><i class='fa fa-times'></i> &nbsp;</span>");
                element.find(".fc-content").append("<span class='editon pull-right'><i class='fa fa-pencil'></i> &nbsp;</span>");
                element.find(".fc-title").addClass("pull-left");
                element.find(".closeon").click(function () {
                    confirmDialog({
                        text: "Remove schedule?",
                        onOk: function () {
                            showLoader("#hdrHolidays");
                            $.ajax({
                                url: action("Schedule", "DeactivateHoliday"),
                                type: "post",
                                data: { data: event.ParamDataGet },
                                success: function (data) {
                                    if (Boolean(data.Success) == true) {
                                        hideLoader("#hdrHolidays");
                                        fnGetHolidays();
                                    }
                                }
                            });
                        }
                    });
                });
                element.find(".editon").click(function () {
                    $("#mdlHolidayForm #data").val(event.ParamDataGet);
                    $("#mdlHolidayForm #HolidayDate").val(event.ParamDate);
                    $("#mdlHolidayForm #HolidayName").val(event.ParamName);
                    $("#mdlHolidayForm #HolidayType").val(event.ParamType);
                    $("#mdlHolidayForm #CompensationRateMultiplier").val(event.ParamMultiplier);
                    fnCalculateHolidayRate();
                    $("#mdlHolidayForm").modal("show");
                });
            }            
        }
    });
}

function fnCalculateHolidayRate() {
    var sampleBS = 5000;
    var multiplierPercentage = parseFloat($("#CompensationRateMultiplier").val());
    var multiplier = multiplierPercentage / 100;
    var holidayRate = sampleBS * multiplier;
    $("#lblMultiplier").html(multiplier.toFixed(2));
    $("#lblHolidayRate").html(holidayRate.toFixed(2));
}