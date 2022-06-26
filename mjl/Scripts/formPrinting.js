(function ($) {
    $.fn.print = function () {
        var printing_window_settings = "width=900,height=500,channelmode=yes,location=no,menubar=no,resizable=no,status=no,titlebar=no,toolbar=no,top=100,left=200,resizable=no,scrollbars=yes";
        var _this = $(this);

        //ScriptManager.RegisterStartupScript(this, typeof (string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.print('height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'');setTimeout(function () { window.close(); }, 100);", true);


        var printing_window = window.open("", "Custom Printing", printing_window_settings);

        //        printing_window.document.write("<link media='screen' href='/HRIS/Content/css/bootstrap.min.css' rel='stylesheet' type='text/css' />");
        //        printing_window.document.write("<link media='print' href='/HRIS/Content/css/bootstrap.min.css' rel='stylesheet' type='text/css' />");

        //printing_window.document.write("<link href='../Content/bootstrap-grid.css' rel='stylesheet' type='text/css' />");
        //printing_window.document.write("<link media='screen' href='../Content/bootstrap.css' rel='stylesheet' type='text/css' />");
        //printing_window.document.write("<link href='../Content/bootstrap.css' rel='stylesheet' type='text/css' />");
        printing_window.document.write("<link media='print' href='../Content/bootstrap4.css' rel='stylesheet' type='text/css' />");

        printing_window.document.write(_this.html());

        printing_window.document.close();
        printing_window.focus();
        setTimeout(function () { mywindow.print(); }, 1000);

        var print_window = function () {
            if (printing_window.document.readyState == "complete") {
                clearInterval(buffer);
                printing_window.print();
                printing_window.close();
                //setTimeout(function (_this) { window.close(); }, 100);
            }
        }
        var buffer = setInterval(print_window, 200);
    }
})(jQuery);

