namespace ComputerGraphics.Mac.Views;

public class CanvasView : NSView, ICanvas
{
    private readonly byte[] _pixelBuffer;

    public CanvasView(CGRect frame, int width, int height) : base(frame)
    {
        Width = width;
        Height = height;
        _pixelBuffer = new byte[width * height * 4];
        WantsLayer = true;
    }

    public override void DrawRect(CGRect dirtyRect)
    {
        base.DrawRect(dirtyRect);

        using var context = NSGraphicsContext.CurrentContext?.CGContext;
        
        if (context == null) return;

        using var provider = new CGDataProvider(_pixelBuffer);
        using var image = new CGImage(
            width: Width,
            height: Height,
            bitsPerComponent: 8,
            bitsPerPixel: 32,
            bytesPerRow: Width * 4,
            colorSpace: CGColorSpace.CreateDeviceRGB(),
            alphaInfo: CGImageAlphaInfo.Last,
            provider: provider,
            decode: null,
            shouldInterpolate: false,
            intent: CGColorRenderingIntent.Default);

        context.DrawImage(new CGRect(0, 0, Width, Height), image);
    }

    public int Width { get; }
    public int Height { get; }

    public void DrawPixel(int x, int y, CColor color)
    {
        SavePixel(x, y, color);

        this.InvokeOnMainThread(() => SetNeedsDisplayInRect(new CGRect(x, y, 1, 1)));
    }

    public void SavePixel(int x, int y, CColor color)
    {
        int index = (y * Width + x) * 4;
        
        if (index < 0 || index >= _pixelBuffer.Length)
        {
            return;
        }

        _pixelBuffer[index] = color.R; 
        _pixelBuffer[index + 1] = color.G;
        _pixelBuffer[index + 2] = color.B;
        _pixelBuffer[index + 3] = byte.MaxValue; // Alpha
    }

    public void DrawCurrentState()
    {
        InvokeOnMainThread(() => SetNeedsDisplayInRect(new CGRect(0, 0, Width, Height)));
    }
}