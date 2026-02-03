namespace MazeGenerator.Domain;

public class Maze
{
    private Cell[][] _cells;

    public int Width => _cells.Length;
    public int Height => _cells[0].Length;

    public (int x, int y) EntryCellCoords => (0, 0);
    public (int x, int y) FinishCellCoords => (Width - 1, Height - 1);

    public Cell FinishCell
    {
        get => this[Width - 1, Height - 1];
        set => this[Width - 1, Height - 1] = value;
    }

    public Maze(int width, int height)
    {
        if (width < 1 || height < 1) throw new ArgumentException("Width and height must be greater than 0");
        
        _cells = new Cell[width][];
        for (int i = 0; i < width; i++)
        {
            _cells[i] = new Cell[height];
            for (int j = 0; j < height; j++)
            {
                _cells[i][j] = Cell.New;
            }
        }
    }

    public Cell this[int x, int y]
    {
        get => _cells[x][y];
        set => _cells[x][y] = value;
    }

    public void EraseRightWall(int x, int y) => this[x, y] = this[x, y].EraseRightWall();
    public void EraseBottomWall(int x, int y) => this[x, y] = this[x, y].EraseBottomWall();
}