using AppKit;
using ComputerGraphics.Debug;
using ComputerGraphics.Mac.Views;
using CoreGraphics;
using Foundation;

namespace ComputerGraphics.Mac;

[Register ("AppDelegate")]
public class AppDelegate : NSApplicationDelegate 
{
    private NSWindow _window;
    private DebugView _debugView;

    private const int DownKey = 125;
    private const int LeftKey = 123;
    private const int RightKey = 124;
    private const int UpKey = 126;

    public override void DidFinishLaunching(NSNotification notification)
    {
        _window = new NSWindow(
            new CGRect(0, 0, 720, 720),
            NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Resizable, NSBackingStore.Buffered,
        false);

        _window.Title = "Hello World App";
        var canvasView = new CanvasView(
            _window.ContentView.Frame,
            (int)_window.ContentView.Frame.Width,
            (int)_window.ContentView.Frame.Height);

        _window.ContentView.AddSubview(canvasView);

        _debugView = new DebugView(new CGRect(10, _window.ContentView.Frame.Height - 300, 300, 300));
        _debugView.OnSwitchStateChanged += DebugViewOnOnSwitchStateChanged;

        _debugView.Layer.BorderColor = new CGColor(255, 0, 0);
        _debugView.Layer.BorderWidth = 1;
        
        _window.ContentView.AddSubview(_debugView);

        _window.MakeKeyAndOrderFront(null);

        Global.GlobalOriginChanged += OnGlobalOriginChanged;
        Global.SetPlatformCanvas(canvasView);
        Global.DrawSpheresProjection();

        NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyDown, HandleKeyDown);

        OnGlobalOriginChanged();
    }

    private void DebugViewOnOnSwitchStateChanged()
    {
        Global.SphereProjectionType =
            _debugView.SwitchState ? SphereProjectionType.Orthogonal : SphereProjectionType.Perspective;
    }

    void OnGlobalOriginChanged() => _debugView.OriginText = $"Origin: {Global.GlobalOrigin.ToString()}";

    private NSEvent HandleKeyDown(NSEvent e)
    {
        Global.GlobalOrigin = e.KeyCode switch
        {
            UpKey => Global.GlobalOrigin with { Y = Global.GlobalOrigin.Y + 0.1f },
            DownKey => Global.GlobalOrigin with { Y = Global.GlobalOrigin.Y - 0.1f },
            LeftKey => Global.GlobalOrigin with { X = Global.GlobalOrigin.X - 0.1f },
            RightKey => Global.GlobalOrigin with { X = Global.GlobalOrigin.X + 0.1f },
            _ => Global.GlobalOrigin,
        };

        return null;
    }
}
