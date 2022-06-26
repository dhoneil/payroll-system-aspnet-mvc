$(document).on("click", ".btn-edit-dtr", function () {
    var _parent = $(this).parents(".covered-date");
    _parent.find(".allow_edit_true .input-box-holder").show();
    _parent.find(".allow_edit_true .display-text").hide();
    _parent.find(".btn-edit-dtr").hide();
    _parent.find(".btn-save-dtr").show();
});

$(document).on("click", ".btn-save-dtr", function () {
    var _this = $(this);
    var _parent = _this.parents(".covered-date");
    var RecordId = _this.attr("data-id");
    var TimeIn = _parent.find("input[name='TimeIn']").val();
    var TimeOut = _parent.find("input[name='TimeOut']").val();
    var Date = _parent.attr("rel");
    var EmployeeId = _parent.attr("data-id");

    $.ajax({
        url: action("DTR", "ModifyDTR"),
        type: "post",
        data: {
            "RecordID": RecordId,
            "TimeIn": TimeIn,
            "TimeOut": TimeOut,
            "Date": Date,
            "EmployeeId": EmployeeId
        },
        success: function (data) {
            _this.parents(".DTR-range").find(".btn-load-dtr").trigger("click");
            _parent.removeClass("text-danger");
            _parent.find(".btn-edit-dtr").show();
            _parent.find(".btn-save-dtr").hide();
        }
    });
});


function fnGetEmployeeDTR(employeeDataSelector, from, to) {
    showLoader("#DTR>.box");
    $.ajax({
        url: action("DTR", "GetEmployeeDTR"),
        type: "get",
        async: true,
        data: {
            employeeData: $(employeeDataSelector).val(),
            coveredFrom: from,
            coveredTo: to
        },
        success: function (data) {
            hideLoader("#DTR>.box");
            var recordDOM = null;
            var lateTotal = 0, underTimeTotal = 0, overTimeTotal = 0;
            var recordDOM = null;
            for (d in data.employeeDTRs) {
                var RecordId = data.employeeDTRs[d].EmployeeDTR ? data.employeeDTRs[d].EmployeeDTR.RecordID : 0;
                var logInEdit = data.employeeDTRs[d].EmployeeDTR && data.employeeDTRs[d].EmployeeDTR.TimeLogIn ? false : true;
                var logOutEdit = data.employeeDTRs[d].EmployeeDTR && data.employeeDTRs[d].EmployeeDTR.TimeLogOut ? false : true;
                var showEdit = Boolean(logInEdit) || Boolean(logOutEdit) ? true : false;

                var TimeIn = data.employeeDTRs[d].TimeIn ? data.employeeDTRs[d].TimeIn : "00:00";
                var TimeOut = data.employeeDTRs[d].TimeOut ? data.employeeDTRs[d].TimeOut : "00:00";

                recordDOM = $(".covered-date[rel='" + data.employeeDTRs[d].Date + "']").attr("data-id",data.employeeDTRs[d].EmployeeID);
                recordDOM.find(".status").html(data.employeeDTRs[d].Status);
                recordDOM.find(".time-in").html(
                        '<span class="display-text">' + TimeIn + '</span>' +
                        '<div class="input-box-holder input-group bootstrap-timepicker timepicker" style="display:none;">' +
                             '<input type="text" value="' + TimeIn + '" name="TimeIn" class="form-control input-sm input-box" />' +
                             '<div class="input-group-addon">' +
                                    '<i class="fa fa-clock-o"></i>' +
                             '</div>' +
                        '</div>'
                ).addClass("allow_edit_" + logInEdit);
                recordDOM.find(".time-out").html(
                    '<span class="display-text">' + TimeOut + '</span>' +
                    '<div class="input-box-holder input-group bootstrap-timepicker timepicker" style="display:none;" >' +
                        '<input type="text" value="' + TimeOut + '" name="TimeOut" class="form-control input-sm input-box" />' +
                        '<div class="input-group-addon">' +
                                '<i class="fa fa-clock-o"></i>' +
                            '</div>' +
                    '</div>'
               ).addClass("allow_edit_" + logOutEdit);
                var lates = parseFloat(data.employeeDTRs[d].LatesInMinutes);
                lateTotal += lates;
                var undertime = parseFloat(data.employeeDTRs[d].UnderTimeInMinutes);
                underTimeTotal += undertime;
                var ovetime = parseFloat(data.employeeDTRs[d].OverTimeInMinutes);
                overTimeTotal += ovetime;
                recordDOM.find(".lates-in-minutes").html(lates.toFixed(2));
                recordDOM.find(".undertime-in-minutes").html(undertime.toFixed(2));
                recordDOM.find(".overtime-in-minutes").html(ovetime.toFixed(2)).addClass("allow_" +showEdit);

                if (data.can_edit) {
                    if (showEdit) {
                        recordDOM.find(".edit-dtr").html(
                            "<button data-id='" + RecordId + "' class='btn btn-sm btn-edit-dtr' type='button'><i class='fa fa-edit'></i> Edit</button>" +
                            "<button  style='display:none;' data-id='" + RecordId + "' class='btn-primary btn btn-sm btn-save-dtr' type='button'><i class='fa fa-save'></i> Save</button>"
                        );
                    }
                }

                if (data.employeeDTRs[d].Status == "A" || data.employeeDTRs[d].Status == "NS") {
                    recordDOM.addClass("text-danger");
                }
                if (data.employeeDTRs[d].Status == "HLD" || data.employeeDTRs[d].Status == "RD") {
                    recordDOM.addClass("text-info");
                }
                if (data.employeeDTRs[d].Status == "N/A" || data.employeeDTRs[d].Status == "INC") {
                    recordDOM.addClass("text-warning");
                }
            }
            var totalsDOM = recordDOM.closest(".tbl-covered-date").find(".row-covered-totals");
            totalsDOM.find(".lates-in-minutes").html(lateTotal.toFixed(2));
            totalsDOM.find(".undertime-in-minutes").html(underTimeTotal.toFixed(2));
            totalsDOM.find(".overtime-in-minutes").html(overTimeTotal.toFixed(2));

            $('.input-box').timepicker();
        }
    });
}