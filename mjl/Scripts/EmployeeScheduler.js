var scheduleTypeArray = ["FIXED WEEKLY", "CHANGING WEEKLY", "CHANGING MONTHLY"];
var scheduleConstraintArray = ["TIMELY", "FREE"];
var maxNumberOfDaysPerWeek = 7;
//array structured based on EmployeeSchedule.ScheduleDays enumerable
var daysArray = ["Sunday","Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

function fnGenerateEmployeeSchedulerTreeBox(scheduleArray) {
    var scheduleCtr = 0;    
    for (s in scheduleArray) {
        var innerBox = "";
        if (scheduleTypeArray[scheduleArray[s].Header.ScheduleType] == "FIXED WEEKLY")
            innerBox = fnGenerateScheduleTable(scheduleArray[s], false);
        else if (scheduleTypeArray[scheduleArray[s].Header.ScheduleType] == "CHANGING WEEKLY")
            innerBox = fnGenerateScheduleTable(scheduleArray[s], true);
        else
            innerBox = fnGenerateScheduleCalendar(scheduleArray[s], scheduleCtr);
        $("#Schedule>.box").append(innerBox);
        scheduleCtr++;
    }
    if (scheduleCtr == 0) {
        //insert no schedule
        var noSchedule = "<div class='alert alert-warning'>" +
            "<h4><i class='icon fa fa-warning'></i> Warning!</h4>" +
            "Employee don't have a work schedule yet." +
        "</div>";
        $("#Schedule>.box").append(noSchedule);
    }
    $.AdminLTE.boxWidget.activate();
    $(".schedule-calendar").fullCalendar("render");
}

function fnGenerateScheduleTable(scheduleRow, isChanging) {
    var innerBox = $("<div class='box " + (scheduleRow.Header.ScheduleTitle == "Current Schedule" ? "" : "collapsed-box") + "'>");
    var boxHeader = $("<div class='box-header'>");
    boxHeader.append("<h3 class='box-title'>" + scheduleRow.Header.ScheduleTitle + " " + (scheduleRow.Header.ScheduleTitle == "Current Schedule" ? "<small>(Valid since " + scheduleRow.Header.ValidityStart + ")</small>" : "") + " - <small><i>" + scheduleRow.Header.ScheduleTypeStr + "</i></small></h3>");
    boxHeader.append("<div class='box-tools pull-right'>" +
                    "<button type='button' class='btn btn-box-tool' data-widget='collapse'><i class='fa fa-" + (scheduleRow.Header.ScheduleTitle == "Current Schedule" ? "minus" : "plus") + "'></i></button>" +
                "</div>");
    innerBox.append(boxHeader);
    var boxBody = $("<div class='box-body no-padding'>");
    var scheduleTable = $("<table class='table table-striped table-condensed'>");
    scheduleTable.append("<tbody>");
    scheduleTable.find("tbody").append("<tr>" +
                            "<th style='width:25%;'>Day</th>" +
                            "<th style='width:15%;'>Time In</th>" +
                            "<th style='width:15%;'>Time Out</th>" +
                            "<th style='width:20%;'>Added By</th>" +
                            "<th>Added On</th>" +
                        "</tr>");
    for (b in scheduleRow.Schedules) {
        scheduleTable.find("tbody").append(fnLoadRowSchedule(scheduleRow.Schedules[b],isChanging));
    }
    boxBody.append(scheduleTable);
    innerBox.append(boxBody);

    return innerBox;
}

function fnGenerateScheduleCalendar(scheduleRow, scheduleCtr) {
    var innerBox = $("<div class='box'>");
    var boxHeader = $("<div class='box-header'>");
    boxHeader.append("<h3 class='box-title'>" + scheduleRow.Header.ScheduleTitle + " " + (scheduleRow.Header.ScheduleTitle == "Current Schedule" ? "<small>(Valid since " + scheduleRow.Header.ValidityStart + ")</small>" : "") + " - <small><i>" + scheduleRow.Header.ScheduleTypeStr + "</i></small></h3>");
    boxHeader.append("<div class='box-tools pull-right'>" +
                    "<button type='button' class='btn btn-box-tool' data-widget='collapse'><i class='fa fa-minus'></i></button>" +
                "</div>");
    innerBox.append(boxHeader);
    var boxBody = $("<div class='box-body no-padding'>");
    var scheduleCalendar = $("<div id='calendar-" + scheduleCtr + "' class='schedule-calendar' style='width:100%;height:auto;'>");

    var events = new Array();
    var validityStart = new Date(scheduleRow.Header.ValidityStart);
    for (b in scheduleRow.Schedules) {
        events.push(fnGenerateCalendarDayData(scheduleRow.Schedules[b]));
    }    

    boxBody.append(scheduleCalendar);
    innerBox.append(boxBody);

    scheduleCalendar.fullCalendar({
        header: {
            left: '',
            center: 'title',
            right: ''
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },
        //Random default events
        events: events,
        editable: false,
        droppable: false
    });
    scheduleCalendar.fullCalendar("gotoDate", validityStart);
    return innerBox;
}

function fnGetTime(date, isMilitaryTime) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    var hours = dt.getHours();
    var minutes = dt.getMinutes();
    var AmPm = "";
    if (isMilitaryTime == true) {
        if ((hours == 12 && minutes > 0) || hours > 12)
            AmPm = "PM";
        else
            AmPm = "AM";
        if (hours > 12)
            hours = hours % 12;
    }
    return fnPadNumber(hours, 2) + ":" + fnPadNumber(minutes, 2) + " " + AmPm;
}

function fnPadNumber(number, size) {
    var s = number + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function fnGenerateCalendarDayData(rowData) {
    var dataEvent = {
        title: "",
        start: fnGetZeroBasedMomentDate(convert_to_js_date(rowData.ScheduleTimeIn),1),
        allDay: true,
        backgroundColor: "", 
        borderColor: ""
    };
    if (fnGetTime(rowData.ScheduleTimeIn, true) == fnGetTime(rowData.ScheduleTimeOut, true)) {
        dataEvent.backgroundColor = "#f56954";//Danger (red)
        dataEvent.borderColor = "#f56954";//Danger (red)
        dataEvent.title = "REST DAY";
    } else {        
        if (scheduleConstraintArray[parseInt(rowData.ScheduleConstraint)] == "FREE") {
            dataEvent.backgroundColor = "#f39c12";//Warning (orange)
            dataEvent.borderColor = "#f39c12";//Warning (orange)
            dataEvent.title = "Render " + rowData.ScheduleWorkHours + " hour(s)";
        } else {
            dataEvent.backgroundColor = "#00a65a";//Success (green)
            dataEvent.borderColor = "#00a65a";//Success (green)
            dataEvent.title = fnGetTime(rowData.ScheduleTimeIn, true) + " to " + fnGetTime(rowData.ScheduleTimeOut, true);
        }
    }
    return dataEvent;
}

function fnLoadRowSchedule(rowData,isChanging) {
    var result = "";
    var tempChangeDate = (isChanging == true ? convert_to_js_date(rowData.ScheduleTimeIn) : "");
    var scheduleDay = daysArray[rowData.ScheduleDay];
    if (isChanging == true)
        scheduleDay = tempChangeDate + " - " + "<small>" + daysArray[rowData.ScheduleDay] + "</small>";
    if (fnGetTime(rowData.ScheduleTimeIn, true) == fnGetTime(rowData.ScheduleTimeOut, true)) {
        result = "<tr>" +
            "<td>" + scheduleDay + "</td>" +
            "<td colspan='4'><span class='badge bg-red'>Rest Day</span></td>" +
        "</tr>";
    } else {
        result = "<tr>" +
            "<td>" + scheduleDay + "</td>" + 
            (scheduleConstraintArray[rowData.ScheduleConstraint]=="TIMELY"?
            "<td>" + fnGetTime(rowData.ScheduleTimeIn, true) + "</td>" + "<td>" + fnGetTime(rowData.ScheduleTimeOut, true) + "</td>" :
            "<td colspan='2'>Required to render " + rowData.ScheduleWorkHours + " hours</td>") +
            "<td>" + rowData.Preparer.FirstName + " " + rowData.Preparer.LastName + "</td>" +
            "<td>" + convert_to_js_date(rowData.AddedDate) + "</td>" +
        "</tr>";
    }
    return result;
}

function fnDateDifferenceInDays(date1, date2) {
    var tempDate1 = new Date(date1);
    var tempDate2 = new Date(date2);
    var timeDiff = (date2.getTime() - date1.getTime());
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays;
}

/*
    Start of Employee Scheduler 
*/

function fnInitializeScheduleCalendar() {
    var selectedScheduleType = parseInt($("[name='scheduleTypeOption']:checked").val());
    var selectedScheduleDate = fnGetZeroBasedMomentDate($("#txtValidOn").val(),1);
    var scheduleStart = null, scheduleEnd = null;    
    if (selectedScheduleType == 0 || selectedScheduleType == 1) {
        scheduleStart = selectedScheduleDate.clone().startOf("week");
        scheduleEnd = scheduleStart.clone().endOf("week");
    } else {
        scheduleStart = selectedScheduleDate.clone();
        scheduleEnd = scheduleStart.clone().endOf("month");
    }
    $("#hdrSelectedSchedule .box-title").find("span").html(scheduleTypeArray[selectedScheduleType]);
    $("#hdrSelectedSchedule .box-title").find("small").html("Valid on " + $("#txtValidOn").val());
    $("#lblDateCovered").html(scheduleStart.format("YYYY-MM-DD") + " to " + scheduleEnd.format("YYYY-MM-DD"))
        .attr("from", scheduleStart.format("YYYY-MM-DD"))
        .attr("to", scheduleEnd.format("YYYY-MM-DD"));
    var defaultView = selectedScheduleType == 0 || selectedScheduleType == 1 ? "basicWeek" : "month";
    $("#hdrScheduleCalendar").fullCalendar({
        header: {
            left: '',
            center: 'title',
            right: ''
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },
        defaultView: defaultView,
        editable: true,
        droppable: true,
        drop: function (date, allDay) {
            var dateMoment = fnGetZeroBasedMomentDate(date, 0);
            console.log(dateMoment.diff(scheduleStart, "day"));
            console.log(dateMoment.diff(scheduleEnd, "day"));
            if (dateMoment.diff(scheduleStart, "day") >= 0 && dateMoment.diff(scheduleEnd, "day") <= -1) {
                if (fnHasOverlappingEvent(date, date.clone().add(1, "days"), 0) == false) {
                    // get the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');

                    // we clone event object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.end = date.clone().add(1, "days");
                    copiedEventObject.allDay = true;
                    copiedEventObject.backgroundColor = $(this).css("background-color");
                    copiedEventObject.borderColor = $(this).css("border-color");
                    copiedEventObject.overlap = false;
                    copiedEventObject.className = [
                        "logIn-" + $(this).attr("log-in"),
                        "logOut-" + $(this).attr("log-out"),
                        "renderHours-" + $(this).attr("render-hours"),
                        "scheduleConstraint-" + $(this).attr("schedule-constraint")
                    ];
                    copiedEventObject.logIn = $(this).attr("log-in");
                    copiedEventObject.logOut = $(this).attr("log-out");
                    copiedEventObject.scheduleConstraint = $(this).attr("render-hours");
                    copiedEventObject.scheduleConstraint = $(this).attr("schedule-constraint");

                    $('#hdrScheduleCalendar').fullCalendar('renderEvent', copiedEventObject, true);
                } else {
                    alertDialog({ text: "Date already has a schedule!" });
                }
            } else {
                alertDialog({ text: "Date is outside the covered date range!" });
            }
        },
        eventRender: function (event, element) {
            element.find(".fc-content").append("<span class='closeon pull-right'><i class='fa fa-times'></i> &nbsp;</span>");
            element.find(".fc-title").addClass("pull-left");
            element.find(".closeon").click(function () {
                confirmDialog({
                    text: "Remove schedule?",
                    onOk: function () {
                        $('#hdrScheduleCalendar').fullCalendar('removeEvents', event._id);
                    }
                });
            });
        },
        eventConstraint: {
            start: scheduleStart,
            end: scheduleEnd.add(1, "days")
        },
        eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {
            if (fnHasOverlappingEvent(event.start.clone(), event.end.clone(), event._id) == true) {
                revertFunc();
            }
        }
    });
    $("#hdrScheduleCalendar").fullCalendar("gotoDate", scheduleStart);
    $("#mdlScheduleType").modal("hide");
}

function fnHasOverlappingEvent(start, end, eventID) {
    return $("#hdrScheduleCalendar").fullCalendar('clientEvents', function (event) {
        return ((start.isAfter(event.start) || start.isSame(event.start, 'minute')) &&
                (end.isBefore(event.end) || end.isSame(event.end, 'minute')) && event._id != eventID);
    }).length > 0;
}

function fnInitializeEvents(ele) {
    ele.each(function () {
        var eventObject = {
            title: $.trim($(this).text())
        };

        $(this).data('eventObject', eventObject);

        $(this).draggable({
            zIndex: 1070,
            revert: true,
            revertDuration: 0
        });
    });
}

function fnRemoveFromSelectedPool(employeeID) {
    confirmDialog({
        text: "Remove employee from selected pool?",
        onOk: function () {
            showLoader("#hdrSelectedEmployee");
            $("li.selected-for-schedule[employee-id='" + employeeID + "']").remove();
            if ($(".li.selected-for-schedule").length == 0) {
                noSelectedDOM.insertBefore($("#btnSelectEmployee"));
            }
            hideLoader("#hdrSelectedEmployee");
        }
    });
}

function fnAddPresetToPresetsPool() {
    if (parseInt($("#PresetID").val()) == 0) {
        //add
        var presetConstraint= parseInt($("#ScheduleConstraint").val());
        var presetHours= parseFloat($("#ScheduleWorkHours").val());
        var presetMinutes= parseFloat($("#ScheduleWorkMinutes").val());

        var draggableEvent = $("<div class='external-event ui-draggable ui-draggable-handle " + $("#btnSaveSchedulePreset").attr("rel") + "'>");
        draggableEvent.attr("schedule-id", newPresetCTR);
        draggableEvent.attr("log-in",$("#ScheduleTimeIn").val());
        draggableEvent.attr("log-out", $("#ScheduleTimeOut").val());
        draggableEvent.attr("schedule-constraint", presetConstraint);
        draggableEvent.attr("render-hours", presetHours + "." + presetMinutes);        
        draggableEvent.css("position","relative");        
        if(scheduleConstraintArray[presetConstraint]=="FREE"){
            draggableEvent.html("Render "+presetHours+" hour(s) "+(presetMinutes>0?presetMinutes+" minute(s)":""));
        }else{
            draggableEvent.html($("#ScheduleTimeIn").val() + " to " + $("#ScheduleTimeOut").val());
        }
        $("#external-events").append(draggableEvent);
        newPresetCTR++;
        fnInitializeEvents(draggableEvent);
    } else {
        //update
    }    
}

function fnRemoveSelectedPreset(presetID) {
    confirmDialog({
        text: "Remove schedule preset? You will no longer be able to re use this preset.",
        onOk: function () {
            showLoader("#hdrSchedulePresets");
            $.ajax({
                url:action("Schedule","DeactivateSchedulePreset"),
                type:"post",
                data: {
                    presetID: presetID
                },
                success: function (data) {
                    if (Boolean(data.Success) == true) {
                        $(".external-event[schedule-id='" + presetID + "']").remove();
                        hideLoader("#hdrSchedulePresets");                        
                    }
                }
            });
        }
    });
}
/*
    End of Employee Scheduler
*/