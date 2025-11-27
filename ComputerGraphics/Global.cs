using ComputerGraphics.Projections;

namespace ComputerGraphics;

public static class Global
{
    private static int _framesCounter;

    public static Point3D Origin
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            GlobalOriginChanged?.Invoke();
            DrawSpheresProjection();
        }
    } = new() { Z = -3 };

    public static SphereProjectionType SphereProjectionType
    {
        get;
        set
        {
            field = value;
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

    public static ICanvas Canvas { get; set; }

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
        Interlocked.Increment(ref _framesCounter);
        
        switch (SphereProjectionType)
        {
            case SphereProjectionType.Orthogonal:
                SphereOrthogonalProjection.Draw(Canvas, Origin, _scene);
                break;
            case SphereProjectionType.Perspective:
                SpherePerspectiveProjection.Draw(Canvas, Origin, _scene);
                break;
        }
    }

    public static async IAsyncEnumerable<int> FramesPerLastSecond()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await timer.WaitForNextTickAsync())
        {
            var frames = _framesCounter;
            Interlocked.Exchange(ref _framesCounter, 0);
            yield return frames;
        }
    }
}