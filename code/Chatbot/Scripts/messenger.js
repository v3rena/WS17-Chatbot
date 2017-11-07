var url = "/api/message/";
var guid;

$(document).ready(ensureSession);

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var m = $("#message").val();
    $("#message").val("");

    var message = { Content: m, Guid: guid };

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
    $("#messageContainer").scrollTop = $("#messageContainer").scrollHeight;
}

function ensureSession() {
    if (typeof (Storage) !== "undefined") {
        var _guid = localStorage.getItem("guid");
        if (_guid === "undefined" || _guid == null) {
            $.ajax({
                type: "get",
                url: "api/session",
                datatype: "json",
                success: function (json, text) {
                    if (json.guid !== "undefined") {
                        guid = json.guid;
                        localStorage.setItem("guid", guid);
                    } else {
                        alert("Es konnte keine Session gestartet werden!");
                    }
                },
                error: function (request, status, error) {
                    alert("Es konnte keine Session gestartet werden!");
                }
            })
        } else {
            guid = _guid;
        }
    } else {
        alert("Sorry! No Web Storage support ...");
    }
}