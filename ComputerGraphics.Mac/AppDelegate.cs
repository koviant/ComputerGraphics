using AppKit;
using ComputerGraphics.Mac.Views;
using CoreGraphics;
using Foundation;

namespace ComputerGraphics.Mac;

[Register ("AppDelegate")]
public class AppDelegate : NSApplicationDelegate 
{
    private NSWindow _window;
    private NSTextField _textField;

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

        _textField = new NSTextField(new CGRect(10, _window.ContentView.Frame.Height - 30, 300, 24))
        {
            Editable = false,
            Bezeled = false,
            DrawsBackground = false,
            Selectable = false,
            Font = NSFont.SystemFontOfSize(18),
            TextColor = NSColor.Black,
        };

        _window.ContentView.AddSubview(_textField);

        _window.MakeKeyAndOrderFront(null);

        // canvasView.Layer.BorderWidth = 1;
        // canvasView.Layer.BorderColor = new CGColor(255, 255, 255);


        Global.GlobalOriginChanged += OnGlobalOriginChanged;
        Global.SetPlatformCanvas(canvasView);
        // Global.DrawPixel(0, 0, CColor.Red);
        // Global.DrawXAxis(CColor.Red);
        // Global.DrawYAxis(CColor.Green);
        Global.DrawSpheresProjection();
        NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyDown, HandleKeyDown);
        
        OnGlobalOriginChanged();
    }

    void OnGlobalOriginChanged() => _textField.StringValue = $"Origin: {Global.GlobalOrigin.ToString()}";

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
