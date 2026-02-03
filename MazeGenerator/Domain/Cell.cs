namespace MazeGenerator.Domain;

public readonly record struct Cell(bool RightWall, bool BottomWall)
{
    public Cell EraseRightWall() => this with { RightWall = false };
    public Cell EraseBottomWall() => this with { BottomWall = false };
    public static Cell New => new Cell(true, true);
}