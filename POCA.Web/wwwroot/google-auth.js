window.registerGoogleMessageHandler = function (dotnetHelper) {
    window.addEventListener("message", function (event) {
        if (!event.data) return;
        dotnetHelper.invokeMethodAsync("ReceiveGoogleAuth", event.data);
    });
};

window.openGooglePopup = function (url) {
    const width = 500, height = 600;
    const left = (window.innerWidth - width) / 2;
    const top = (window.innerHeight - height) / 2;

    window.open(
        url,
        "GoogleLogin",
        `width=${width},height=${height},top=${top},left=${left}`
    );
};
