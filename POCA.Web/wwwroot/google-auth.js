window.registerGoogleMessageHandler = function (dotnetHelper) {
    if (window._googleHandlerRegistered) return;

    window.addEventListener("message", function (event) {
        if (!event.data) return;

        try {
            const data = typeof event.data === "string" ? JSON.parse(event.data) : event.data;
            console.info("[GoogleAuth] Message received:", data);

            dotnetHelper.invokeMethodAsync("ReceiveGoogleAuth", data)
                .then(() => console.info("[GoogleAuth] Blazor method invoked"))
                .catch(err => console.error("[GoogleAuth] Blazor invoke error:", err));
        } catch (err) {
            console.error("[GoogleAuth] JSON parse error:", err);
        }
    });

    window._googleHandlerRegistered = true;
};

// Open Google login popup
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
