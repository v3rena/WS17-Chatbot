var url = "/api/message/";

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var m = $("#message").val();
    $("#message").val("");

    var message = { Content : m };

    var posting = $.ajax({
        url: url,
        type: "POST",
        data: message,
        dataType: "json",
        success: function (data) {
            addMessage(data, false, false);
        },
        beforeSend: function() {
            addMessage(message, false, true);
        },
        error: function (data) {
            addMessage("something went wrong", true);
        }
    });
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

    $("#messageContainer").append("<span class=\"message " + poster + " " + status + "\">" + message.Content + "</span>");
}