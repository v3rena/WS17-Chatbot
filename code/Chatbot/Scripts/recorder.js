var SDK;
var key = "31993c62e9f146bbaec9f49bf2cdb0b3";
var recognizer;
var recognitionMode = "Dictation", languageOptions = "de-DE", formatOptions = "Simple";
var startBtn, stopBtn, status, message;


function Setup() {
    if (recognizer != null) {
        RecognizerStop(SDK, recognizer);
    }
    recognizer = RecognizerSetup(SDK, recognitionMode, languageOptions, SDK.SpeechResultFormat[formatOptions], key);
}

document.addEventListener("DOMContentLoaded", function () {
    startBtn = document.getElementById("startRecord");
    stopBtn = document.getElementById("stopRecord");
    status = document.getElementById("status");
    message = document.getElementById("message");

    Initialize(function (speechSdk) {
        SDK = speechSdk;
        startBtn.disabled = false;
    });

    startBtn.addEventListener("click", function () {
        if (!recognizer) {
            previousSubscriptionKey = key.value;
            Setup();
        }
        RecognizerStart(SDK, recognizer);
        startBtn.disabled = true;
        stopBtn.disabled = false;
    });
});


function Initialize(onComplete) {
    require(["Speech.Browser.Sdk"], function (SDK) {
        onComplete(SDK);
    });
}


function RecognizerSetup(SDK, recognitionMode, language, format, subscriptionKey) {
    let recognizerConfig = new SDK.RecognizerConfig(
        new SDK.SpeechConfig(
            new SDK.Context(
                new SDK.OS(navigator.userAgent, "Browser", null),
                new SDK.Device("SpeechSample", "SpeechSample", "1.0.00000"))),
        recognitionMode, // SDK.RecognitionMode.Interactive  (Options - Interactive/Conversation/Dictation)
        language, // Supported laguages are specific to each recognition mode. Refer to docs.
        format); // SDK.SpeechResultFormat.Simple (Options - Simple/Detailed)

    // Alternatively use SDK.CognitiveTokenAuthentication(fetchCallback, fetchOnExpiryCallback) for token auth
    let authentication = new SDK.CognitiveSubscriptionKeyAuthentication(subscriptionKey);

    return SDK.CreateRecognizer(recognizerConfig, authentication);
}


// Start the recognition
function RecognizerStart(SDK, recognizer) {
    recognizer.Recognize((event) => {
        switch (event.Name) {
            case "RecognitionTriggeredEvent":
                UpdateStatus("Initializing");
                break;
            case "ListeningStartedEvent":
                UpdateStatus("Listening");
                break;
            case "RecognitionStartedEvent":
                UpdateStatus("Listening_Recognizing");
                break;
            case "SpeechStartDetectedEvent":
                UpdateStatus("Listening_DetectedSpeech_Recognizing");
                console.log(JSON.stringify(event.Result)); // check console for other information in result
                break;
            case "SpeechHypothesisEvent":
                UpdateRecognizedHypothesis(event.Result.Text, false);
                console.log(JSON.stringify(event.Result)); // check console for other information in result
                break;
            case "SpeechFragmentEvent":
                UpdateRecognizedHypothesis(event.Result.Text, true);
                console.log(JSON.stringify(event.Result)); // check console for other information in result
                break;
            case "SpeechEndDetectedEvent":
                OnSpeechEndDetected();
                UpdateStatus("Processing_Adding_Final_Touches");
                console.log(JSON.stringify(event.Result)); // check console for other information in result
                break;
            case "SpeechSimplePhraseEvent":
                UpdateRecognizedPhrase(JSON.stringify(event.Result, null, 3));
                break;
            case "SpeechDetailedPhraseEvent":
                UpdateRecognizedPhrase(JSON.stringify(event.Result, null, 3));
                break;
            case "RecognitionEndedEvent":
                OnComplete();
                UpdateStatus("Idle");
                console.log(JSON.stringify(event)); // Debug information
                break;
            default:
                console.log(JSON.stringify(event)); // Debug information
        }
    })
        .On(() => {
            // The request succeeded. Nothing to do here.
        },
        (error) => {
            console.error(error);
        });
}
// Stop the Recognition.
function RecognizerStop(SDK, recognizer) {
    // recognizer.AudioSource.Detach(audioNodeId) can be also used here. (audioNodeId is part of ListeningStartedEvent)
    recognizer.AudioSource.TurnOff();
}

function UpdateStatus(status) {
    message.value = status;
}

function UpdateRecognizedHypothesis(text, append) {
    if (append)
        message.value += text + " ";
    else
        message.value = text;
    var length = message.value.length;
    if (length > 403) {
        message.value = "..." + message.value.substr(length - 400, length);
    }
}

function OnSpeechEndDetected() {
    stopBtn.disabled = true;
}

function UpdateRecognizedPhrase(json) {
    message.value += json + "\n";
}

function OnComplete() {
    startBtn.disabled = false;
    stopBtn.disabled = true;
}