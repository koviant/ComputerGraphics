namespace ComputerGraphics;

public interface ICanvas
{
    int Width { get; }
    int Height { get; }
    void DrawPixel(int x, int y, CColor color);
    void SavePixel(int x, int y, CColor color);
    void DrawCurrentState();
}