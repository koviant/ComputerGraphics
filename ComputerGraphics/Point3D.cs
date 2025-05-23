namespace ComputerGraphics;

public record struct Point3D
{
    public float X { get; init; }
    public float Y { get; init; }
    public float Z { get; init; }
    
    public Point3D()
    {
    }
    
    public Point3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() => $"X={X:F2}, Y={Y:F2}, Z={Z:F2}";
}