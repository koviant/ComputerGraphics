namespace ComputerGraphics;

public static class Global
{
    private static ICanvas? _canvas;
    private static Point3D _globalOrigin = new() { Z = -3 };

    public static Point3D GlobalOrigin
    {
        get => _globalOrigin;
        set
        {
            if (_globalOrigin == value)
            {
                return;
            }

            _globalOrigin = value;
            GlobalOriginChanged?.Invoke();
            DrawSpheresProjection();
        }
    }

    public static event Action? GlobalOriginChanged;

    private static SpheresScene _scene = new()
    {
        Spheres =
        [
            new Sphere
            {
                Center = new Point3D(0, -1, 3),
                Radius = 1,
                Color = CColor.Red,
            },
            new Sphere
            {
                Center = new Point3D(2, 0, 4),
                Radius = 1,
                Color = CColor.Blue,
            },
            new Sphere
            {
                Center = new Point3D(-2, 0, 4),
                Radius = 1,
                Color = CColor.Green,
            },
        ]
    };

    public static void SetPlatformCanvas(ICanvas canvas)
    {
        _canvas = canvas;
    }

    public static void TestDraw(ICanvas canvas)
    {
        canvas.DrawPixel(100, 100, CColor.Red);
        canvas.DrawPixel(101, 101, CColor.Red);
        canvas.DrawPixel(102, 102, CColor.Red);
        canvas.DrawPixel(103, 103, CColor.Yellow);
        canvas.DrawPixel(104, 104, CColor.Green);
        canvas.DrawPixel(105, 105, CColor.Green);
    }        

    public static void DrawPixel(int x, int y, CColor color)
    {
        var converted = Coord.Convert(x, y, _canvas.Width, _canvas.Height);
        _canvas.DrawPixel(converted.X, converted.Y, color);
    }

    public static void DrawXAxis(CColor color)
    {
        var y = _canvas.Height / 2;
        for (int i = 0; i < _canvas.Width; i++)
        {
            _canvas.DrawPixel(i, y, color);
        }
    }
    
    public static void DrawYAxis(CColor color)
    {
        var x = _canvas.Width / 2;
        for (int i = 0; i < _canvas.Height; i++)
        {
            _canvas.DrawPixel(x, i, color);
        }
    }

    public static void DrawSpheresProjection()
    {
        // SpherePerspectiveProjection.Draw(_canvas, GlobalOrigin, _scene);
        SphereOrthogonalProjection.Draw(_canvas, GlobalOrigin, _scene);
    }
}