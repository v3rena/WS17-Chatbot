var url = "/api/message/";

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var m = $("#message").val();
    $("#message").val("");
    var posting = $.post(url, { Content: m });
    var posting = $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ Content: m }),
        success: function (data) {
            addMessage(data, false);
        },
        error: function (data) {
            addMessage("something went wrong", true);
        }
    });
    //add own message
    addMessage(m, false, true);

});

function addMessage(message, error, me = null) {
    var poster;
    if (me != null) {
        if (me) {
            poster = "me";
        } else {
            poster = "other";
        }
    } else {
        poster = "";
    }
    var status;
    if (error) {
        status = "info error";
    } else {
        status = "";
    }

    $("#messageContainer").append("<span class=\"message " + poster + " " + status + "\">" + message + "</span>");
}