var url = "/api/message/";
var key;

$(document).ready(ensureSession);

$(function () {
    var href = $("base").attr("href");
    if (href !== "/" && href !== "") {
        url = href + url.substr(1);
    }
});

$("#messageForm").submit(function (event) {
    event.preventDefault();

    var m = $("#message").val();
    $("#message").val("");

    var message = { Content: m, SessionKey: key };

    var posting = $.ajax({
        url: url,
        type: "POST",
        data: message,
        dataType: "json",
        success: function (data) {
            addMessage(data, false, false);
        },
        beforeSend: function () {
            addMessage(message, false, true);
        },
        error: function (data) {
            addErrorMessage("something went wrong", true);
        }
    });
});

function addMessage(message, error, me = null) {
    var poster;
    if (me !== null) {
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

function addErrorMessage(errorMessage) {
    addMessage({ Content: errorMessage }, true);
}

function ensureSession() {
    if (typeof (Storage) !== "undefined") {
        var _sessionKey = localStorage.getItem("sessionKey");
        if (_sessionKey === "undefined" || _sessionKey == null) {
            $.ajax({
                type: "GET",
                url: "api/session/",
                datatype: "json",
                success: function (data) {
                    if (data !== "undefined" && data != null) {
                        key = JSON.stringify(data);
                        localStorage.setItem("sessionKey", key);
                    } else {
                        addErrorMessage("Unable to start session", true);
                    }
                },
                error: function (request, status, error) {
                    addErrorMessage("Unable to start session", true);
                }
            })
        } else {
            key = _sessionKey;
        }
    } else {
        addErrorMessage("Sorry! No Web Storage support.", true);
    }
}



navigator.mediaDevices.getUserMedia({ audio: true })
    .then(stream => {
        var options = {
            mimeType: 'audio/ogg'
        }
        rec = new MediaRecorder(stream, options);
        //startRecord();
        rec.ondataavailable = e => {
            audioChunks.push(e.data);
            if (rec.state == "inactive") {
                let blob = new Blob(audioChunks, { type: 'audio/ogg; codecs=opus' });
                postAudio(blob);
                recordedAudio.src = URL.createObjectURL(blob);
                recordedAudio.controls = true;
                recordedAudio.autoplay = true;
                audioDownload.href = recordedAudio.src;
                audioDownload.download = 'mp3';
                audioDownload.innerHTML = 'download';
            }
        }
    })
    .catch(e => console.log(e));

/*$("#startRecord").click(function () {

})*/


function postAudio(blob) {

    var fd = new FormData();
    fd.append('data', blob);

    console.log(fd);
    console.log(blob);

    $.ajax({
        url: 'https://speech.platform.bing.com/speech/recognition/dictation/cognitiveservices/v1?language=de-DE&format=detailed',
        type: 'post',
        data: blob,
        headers: {
            "Ocp-Apim-Subscription-Key": '31993c62e9f146bbaec9f49bf2cdb0b3',
        },
        contentType: 'audio/ogg; codecs=opus',
        processData: false,
        success: function (data) {
            console.info(data);
        }
    });
}


/*function startRecord()*/startRecord.onclick = e => {
    startRecord.disabled = true;
    stopRecord.disabled = false;
    audioChunks = [];
    rec.start();
}

stopRecord.onclick = e => {
    startRecord.disabled = false;
    stopRecord.disabled = true;
    rec.stop();
}

