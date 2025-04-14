using System.Diagnostics;

namespace ComputerGraphics;

public readonly struct Vector3D
{
    public float X { get; init; }
    public float Y { get; init; }
    public float Z { get; init; }

    public Vector3D()
    {
    }
    
    public Vector3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3D(Point3D p1, Point3D p2)
    {
        X = p1.X - p2.X;
        Y = p1.Y - p2.Y;
        Z = p1.Z - p2.Z;
    }

    public float Dot(Vector3D other) => X * other.X + Y * other.Y + Z * other.Z;

    public static Vector3D operator *(int i, Vector3D vector) => new(i * vector.X, i * vector.Y, i * vector.Z);

    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    public override string ToString() => $"({X}, {Y}, {Z})";
}