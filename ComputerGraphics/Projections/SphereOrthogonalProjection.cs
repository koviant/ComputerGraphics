namespace ComputerGraphics;

public class SphereOrthogonalProjection
{
    private const float ViewportHeight = 10f;
    private const float ViewportWidth = 10f;

    public static void Draw(ICanvas canvas, Point3D viewportCenter, SpheresScene scene)
    {
        var chunks = Environment.ProcessorCount;
        var chunkWidth = canvas.Width / chunks;

        var firstChunkStart = -canvas.Width / 2;

        var directionVector = new Vector3D(0, 0, 1);

        Parallel.For(0, chunks, chunk =>
        {
            var xMin = firstChunkStart + chunkWidth * chunk;
            var xMax = xMin + chunkWidth;
            
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = -canvas.Height / 2 + 1; y < canvas.Height / 2; y++)
                {
                    var origin = new Point3D
                    {
                        X = x * ViewportWidth / canvas.Width + viewportCenter.X,
                        Y = y * ViewportHeight / canvas.Height + viewportCenter.Y,
                        Z = viewportCenter.Z,
                    };
                    var color = TraceRay(scene, origin, directionVector, origin.Z, float.PositiveInfinity);
                    var converted = Coord.Convert(x, y, canvas.Width, canvas.Height);
                    canvas.SavePixel(converted.X, converted.Y, color);
                }
            }
        });

        canvas.DrawCurrentState();
    }

    private static CColor TraceRay(SpheresScene scene, Point3D origin, Vector3D rayDirection, float minDistance, float maxDistance)
    {
        var closestPoint = float.PositiveInfinity;
        Sphere? closestSphere = null;
        foreach (var sphere in scene.Spheres)
        {
            var (t1, t2) = IntersectRaySphere(origin, rayDirection, sphere);
            
            if (t1 > minDistance && t1 < maxDistance && t1 < closestPoint)
            {
                closestPoint = t1;
                closestSphere = sphere;
            }

            if (t2 > minDistance && t2 < maxDistance && t2 < closestPoint)
            {
                closestPoint = t2;
                closestSphere = sphere;
            }
        }

        return closestSphere?.Color ?? CColor.White;
    }

    private static (float point1, float point2) IntersectRaySphere(Point3D origin, Vector3D rayDirection, Sphere sphere)
    {
        var r = sphere.Radius;
        var CO = new Vector3D(origin, sphere.Center);

        var a = Math.Pow(rayDirection.Length, 2);
        var b = 2 * CO.Dot(rayDirection);
        var c = Math.Pow(CO.Length, 2) - r * r;

        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return (float.PositiveInfinity, float.PositiveInfinity);
        }

        var t1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
        var t2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
        return (t1, t2);
    }
}