namespace ComputerGraphics;

public static class Coord
{
    public static (int X, int Y) Convert(int x, int y, int width, int height) =>
        (x + width / 2, height / 2 - y);
}