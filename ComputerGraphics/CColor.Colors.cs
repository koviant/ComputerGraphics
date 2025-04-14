namespace ComputerGraphics;

public readonly partial struct CColor
{
    public static CColor Red => new() { R = byte.MaxValue };
    public static CColor Green => new() { G = byte.MaxValue };
    public static CColor Blue => new() { B = byte.MaxValue };
    public static CColor Yellow => new() { R = byte.MaxValue, G = byte.MaxValue };
    public static CColor White => new() { R = byte.MaxValue, G = byte.MaxValue, B = byte.MaxValue };
}