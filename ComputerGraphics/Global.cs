using ComputerGraphics.Debug;

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

    public static SphereProjectionType SphereProjectionType
    {
        get => _sphereProjectionType;
        set
        {
            _sphereProjectionType = value;
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

    private static SphereProjectionType _sphereProjectionType;

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

    public static void DrawSpheresProjection()
    {
        switch (_sphereProjectionType)
        {
            case SphereProjectionType.Orthogonal:
                SphereOrthogonalProjection.Draw(_canvas, GlobalOrigin, _scene);
                break;
            case SphereProjectionType.Perspective:
                SpherePerspectiveProjection.Draw(_canvas, GlobalOrigin, _scene);
                break;
        }
    }
}