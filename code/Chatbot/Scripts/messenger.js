var url = "/api/message/";
var guid;

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

$(document).ready(ensureSession);

function ensureSession() {
    console.log("hello");
    if (typeof (Storage) !== "undefined") {
        var _guid = localStorage.getItem("guid");
        if (_guid === "undefined" || _guid == null) {
            $.getJSON("api/session", function (json) {
                console.log("s");
                if (json.guid !== "undefined") {
                    guid = json.guid;
                    localStorage.setItem("guid", guid);
                    console.log(guid);
                } else {
                    alert("Es konnte keine Session gestartet werden!");
                }
            });
        } else {
            guid = _guid;
            console.log(guid);
        }
    } else {
        alert("Sorry! No Web Storage support ...");
    }
}