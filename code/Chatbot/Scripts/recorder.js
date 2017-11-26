var SDK;
var key = "31993c62e9f146bbaec9f49bf2cdb0b3";
var recognizer;
var recognitionMode = "Dictation", languageOptions = "de-DE", formatOptions = "Simple";
var startBtn, stopBtn;


function Setup() {
    if (recognizer != null) {
        RecognizerStop(SDK, recognizer);
    }
    recognizer = RecognizerSetup(SDK, recognitionMode, languageOptions, SDK.SpeechResultFormat[formatOptions], key);
}

document.addEventListener("DOMContentLoaded", function () {
    startBtn = document.getElementById("startRecord");
    stopBtn = document.getElementById("stopRecord");

    Initialize(function (speechSdk) {
        SDK = speechSdk;
        startBtn.disabled = false;
    });

    startBtn.addEventListener("click", function () {
        if (!recognizer) {
            previousSubscriptionKey = key.value;
            Setup();
        }
        //startBtn.prop('disabled', true);
        //stopBtn.prop('disabled', false);
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
