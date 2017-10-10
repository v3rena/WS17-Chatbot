var url = "/api/message/";

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var posting = $.post(url, { message: $("#message").val() });
    //add own message
    posting.always(function (data) {
        addMessage(data.Message, true);
    });
    //add response message
    posting.done(function (data) {
        addMessage(data.Message, false);
    });
    //error
    posting.fail(function (data) {
        alert("something went wrong!");
    });
});

function addMessage(message, me) {
    if (me) {
        $("#messageContainer").append("<span class=\"message me\">" + message + "</span>");
    } else {
        $("#messageContainer").append("<span class=\"message other\">" + message + "</span>");
    }
}