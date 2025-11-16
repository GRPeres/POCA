window.openGooglePopup = function (url) {
    const w = 500, h = 600;
    const left = (screen.width - w) / 2;
    const top = (screen.height - h) / 2;

    window.open(
        url,
        "_blank",
        `width=${w},height=${h},left=${left},top=${top}`
    );
};

// Handles messages sent from the popup callback page
window.addEventListener("message", (event) => {
    if (!event.data) return;

    // Calls the Blazor method
    DotNet.invokeMethodAsync("POCA.Web", "ReceiveGoogleAuth", event.data);
});
