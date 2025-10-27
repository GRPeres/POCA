window.userTracker = {
    startTracking: function (dotNetRef) {
        document.addEventListener('mousemove', function (e) {
            dotNetRef.invokeMethodAsync('TrackMouse', e.clientX, e.clientY);
        });
    }
};
