namespace ComputerGraphics;

public readonly partial struct CColor
{
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }

    public static CColor operator +(CColor lhs, CColor rhs) =>
        new()
        {
            R = (byte)Math.Max(lhs.R + rhs.R, byte.MaxValue),
            G = (byte)Math.Max(lhs.G + rhs.G, byte.MaxValue),
            B = (byte)Math.Max(lhs.B + rhs.B, byte.MaxValue),
        };
}