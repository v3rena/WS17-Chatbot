var url = "/api/message/";

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var m = $("#message").val();
    $("#message").val("");
    var posting = $.post(url, { Message: m });
    //add own message
    addMessage(m, false, true);

    //add response message
    posting.done(function (data) {
        addMessage(data.Message, false);
    });
    //add error message
    posting.fail(function (data) {
        addMessage("something went wrong", true);
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

    $("#messageContainer").append("<span class=\"message " + poster + " " + status + "\">" + message + "</span>");
}