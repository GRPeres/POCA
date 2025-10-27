using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

public class UserTrackingService : IAsyncDisposable
{
    private readonly NavigationManager _navigation;
    private readonly IJSRuntime _js;
    private DotNetObjectReference<UserTrackingService>? _dotNetRef;

    private DateTime _lastPageChange;
    private readonly List<PageVisit> _pageVisits = new();
    private readonly List<MousePosition> _mousePositions = new();

    public IReadOnlyList<PageVisit> PageVisits => _pageVisits;
    public IReadOnlyList<MousePosition> MousePositions => _mousePositions;

    public UserTrackingService(NavigationManager navigation, IJSRuntime js)
    {
        _navigation = navigation;
        _js = js;
        _lastPageChange = DateTime.Now;
        _navigation.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        var now = DateTime.Now;
        var timeSpent = now - _lastPageChange;
        _lastPageChange = now;

        _pageVisits.Add(new PageVisit
        {
            Url = e.Location,
            TimeSpent = timeSpent,
            Timestamp = now
        });
    }

    public async Task InitializeMouseTrackingAsync()
    {
        _dotNetRef = DotNetObjectReference.Create(this);
        await _js.InvokeVoidAsync("userTracker.startTracking", _dotNetRef);
    }

    [JSInvokable]
    public void TrackMouse(int x, int y)
    {
        _mousePositions.Add(new MousePosition
        {
            X = x,
            Y = y,
            Timestamp = DateTime.Now
        });
    }

    public void Dispose()
    {
        _navigation.LocationChanged -= OnLocationChanged;
        _dotNetRef?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        Dispose();
        await Task.CompletedTask;
    }

    public class PageVisit
    {
        public string Url { get; set; } = string.Empty;
        public TimeSpan TimeSpent { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MousePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
