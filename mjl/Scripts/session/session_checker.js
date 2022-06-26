$(document).ready(function () {
    checkSession();
});
var interID;
function testSession() {
    $.ajax({
        url: "../Session/CheckSessions",
        type: "get",
        success: function (data) {
            if (data == 0) {
                clearInterval(interID);
                $.ajax({
                    url: "../Session/SessionLogin",
                    type: "get",
                    success: function (data) {
                        $("#sessionModal .modal-body").html(data);
                        $("#sessionModal").modal('show');

                    }
                })
            }
        }
    })
}
function checkSession() {
    var flag = 0;
    interID = setInterval("testSession()", 10000);

}