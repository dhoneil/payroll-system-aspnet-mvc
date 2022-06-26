$(document).ready(function () {
    $.ajaxSetup({
        error: function (HTTPRequest, txtStatus, errorThrown) {            
            if (parseInt(HTTPRequest.status) == 401) {                
                //reset pre-loaded actions
                hideLoader(".box");
                //load login pop up
                loadLoginPopUp(errorThrown);
            }
        }
    });
});

function loadLoginPopUp(message) {
    var modal = $("<div class='modal' role='dialog' data-backdrop='static'>");
    var modal_dialog = $("<div class='modal-dialog'>");
    var modal_content = $("<div class='modal-content'>");

    var header = $("<div class='modal-header'>");    
    header.append("<h4 class='modal-title'>Sign in to start your session.</h4>");
    modal_content.append(header);

    var body = $("<div class='modal-body'>");
    body.append("<p>" + message + "</p>");
    var form = "<form id='fmrPopUpLogin' class='form-horizontal box box-solid' method='post' role='form' novalidate='novalidate' action='" + action("Account", "Login") + "' style='border-color:#fff;box-shadow:none;'>" +
            "<div class='validation-summary-errors text-danger'><ul></ul></div>" +
            "<input type='hidden' id='redirect' name='redirect' value='false'/>" +
            "<div class='form-group has-feedback' style='margin-right:0px;margin-left:0px;'>" +                
                "<label class='control-label' for='Email'>Username :</label>" +
                "<input class='form-control' data-val='true' data-val-required='The Username field is required.' id='Email' name='Email' placeholder='Username / Email' value='' type='text'>" +
                "<span class='field-validation-valid text-danger' data-valmsg-for='Email' data-valmsg-replace='true'></span>" +
            "</div>" +
            "<div class='form-group has-feedback' style='margin-right:0px;margin-left:0px;'>" +
                "<label class='control-label' for='Password'>Password :</label>" +
                "<input class='form-control' data-val='true' data-val-required='The Password field is required.' id='Password' name='Password' placeholder='Password' style='margin:0px;' type='password'>" +
                "<span class='field-validation-valid text-danger' data-valmsg-for='Password' data-valmsg-replace='true'></span>" +
            "</div>" +
            "<div class='form-group'>" +
                "<div class='checkbox' style='padding-left:35px;'>" +
                    "<input id='RememberMe' name='RememberMe' type='checkbox' value='true'/>" +
                    "<label for='RememberMe' style='margin-left:5px;' class=''>Remember me</label>" +
                "</div>" +
            "</div>" +
            "<div class='form-group text-right' style='padding:0px 15px;'>" +
                "<button class='btn btn-danger btnCloseModal' style='margin-right:5px;' type='button'><i class='fa fa-times'></i> Cancel</button>" +
                "<button class='btn btn-default btnOkModal'  type='button'><i class='fa fa-user'></i> Login</button>" +
            "</div>" +
        "</form>";
    body.append(form);
    modal_content.append(body);

    modal_dialog.append(modal_content);
    modal.append(modal_dialog);

    modal.modal({
        show: true
    });

    $(modal).on('hidden.bs.modal', function () {
        $(modal).remove();
    });

    body.find("input[type='text'], input[type='password']").bind("keypress", function (e) {
        if (e.which == 13) {
            body.find(".btnOkModal").trigger("click");
        }
    });

    body.find(".btnOkModal").bind("click", function () {
        var hasError = false;
        if (body.find("#Email").val() == ""){
            hasError = true;
            body.find(".field-validation-valid[data-valmsg-for='Email']").html("Username Field is required.");
        }else
            body.find(".field-validation-valid[data-valmsg-for='Email']").html("");
        if (body.find("#Password").val() == "") {
            hasError = true;
            body.find(".field-validation-valid[data-valmsg-for='Password']").html("Password Field is required.");
        }else
            body.find(".field-validation-valid[data-valmsg-for='Password']").html("");

        if (hasError == false) {
            showLoader("#fmrPopUpLogin");            

            var $form = body.find("#fmrPopUpLogin"),
                url = $form.attr('action');

            $.ajax({
                url: url,
                type: "post",
                data: $form.serialize(),
                success: function (response) {
                    if (Boolean(response.Success) == true) {
                        $(modal).remove();
                        window.location.reload();
                    } else {
                        body.find(".validation-summary-errors ul li").remove();
                        body.find(".validation-summary-errors ul").append("<li>" + response .Message+ "</li>");
                    }
                    hideLoader("#fmrPopUpLogin");
                },
                error: function () {
                    hideLoader("#fmrPopUpLogin");
                }
            });           
        }
    });

    body.find(".btnCloseModal").bind("click", function () {
        window.location.reload();
    });
}

function action(controller, action) {
    return BASE_URL + controller + "/" + action;
}

function alertDialog(options) {
    var settings = $.extend({
        text: "",
        okText: "Close",
        title: "System Message",
        onOk: function () { }
    }, options);

    var modal = $("<div class='modal' role='dialog' data-backdrop='static'>");
    var modal_dialog = $("<div class='modal-dialog'>");
    var modal_content = $("<div class='modal-content'>");

    var header = $("<div class='modal-header'>");
    header.append("<button type='button' class='close' data-dismiss='modal' aria-label='Close'>&times;</button>");
    header.append("<h4 class='modal-title'>" + settings.title + "</h4>");
    modal_content.append(header);

    var body = $("<div class='modal-body'>");
    body.append("<p>" + settings.text + "</p>");
    modal_content.append(body);

    var footer = $("<div class='modal-footer'>");
    footer.append("<button class='btn btn-danger btnOkModal btn-sm' data-dismiss='modal'><i class='fa fa-times'></i>" + settings.okText + "</button>");
    modal_content.append(footer);

    modal_dialog.append(modal_content);
    modal.append(modal_dialog);

    modal.modal({
        show: true
    });

    $(modal).on('hidden.bs.modal', function () {
        $(modal).remove();
    });

    footer.find(".btnOkModal").bind("click", function () {
        if (settings.onOk) {
            settings.onOk();
        }
    });
}

function confirmDialog(options) {

    var settings = $.extend({
        text: "",
        okText: "Yes",
        cancelText: "Close",
        title: "System Message",
        onOk: function () { },
        onCancel: function () { }
    }, options);

    var modal = $("<div class='modal' role='dialog' data-backdrop='static'>");
    var modal_dialog = $("<div class='modal-dialog'>");
    var modal_content = $("<div class='modal-content'>");

    var header = $("<div class='modal-header'>");
    header.append("<button type='button' class='close' data-dismiss='modal' aria-label='Close'>&times;</button>");
    header.append("<h4 class='modal-title'>" + settings.title + "</h4>");
    modal_content.append(header);

    var body = $("<div class='modal-body'>");
    body.append("<p>" + settings.text + "</p>");
    modal_content.append(body);

    var footer = $("<div class='modal-footer'>");
    footer.append("<button class='btn btn-default btnOkModal' data-dismiss='modal'>" + settings.okText + "</button>");
    footer.append("<button class='btn btn-danger btnCloseModal' data-dismiss='modal'>" + settings.cancelText + "</button>");
    modal_content.append(footer);

    modal_dialog.append(modal_content);
    modal.append(modal_dialog);

    modal.modal({
        show: true
    });

    $(modal).on('hidden.bs.modal', function () {
        $(modal).remove();
    });

    footer.find(".btnOkModal").bind("click", function () {
        if (settings.onOk) {
            settings.onOk();
        }
    });

    footer.find(".btnCloseModal").bind("click", function () {
        if (settings.onCancel) {
            settings.onCancel();
        }
    });
}

function showLoader(boxSelector) {
    var loader = $("<div class='overlay'><i class='fa fa-refresh fa-spin'></i></div>");
    $(boxSelector).append(loader);
}

function hideLoader(boxSelector) {
    $(boxSelector + ">.overlay").remove();
}

/*
    Start Of Date Functions
*/
function fnGetZeroBasedMomentDate(date, addDay) {
    var tempDate = new Date(date);
    var zeroBasedDate = new Date(tempDate.getFullYear(), tempDate.getMonth(), tempDate.getDate() + addDay, 0, 0, 0);
    return moment(zeroBasedDate);
}
/*
    End Of Date Functions
*/