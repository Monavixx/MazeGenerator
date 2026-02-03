using System.Collections.Immutable;

namespace MazeGenerator.Domain;

public class CarvingMazeGenerator(Random? random) : IMazeGenerator
{
    private Random Random { get; } = random ?? Random.Shared;
    public Maze? Maze { get; private set; }
    public Maze Generate(int width, int height)
    {
        Maze = new Maze(width, height);
        foreach (var _ in GetSteps()) {}
        return Maze;
    }

    public void StartGeneration(int width, int height)
    {
        Maze = new Maze(width, height);
        Maze.FinishCell = Maze.FinishCell.EraseRightWall();
    }

    public IEnumerable<GenerationStep> GetSteps()
    {
        if(Maze is null) throw new InvalidOperationException("You should call StartGeneration() first!");
        
        HashSet<(int x, int y)> visited = [Maze.EntryCellCoords];
        Stack<(int x, int y)> trace = [];
        trace.Push(Maze.EntryCellCoords);
        while (visited.Count != Maze.Height*Maze.Width)
        {
            var neighbor = NextNeighbor();
            (int, int)? updatedCell = null;
            if (neighbor is var (nx, ny, direction))
            {
                var (tx, ty) = trace.Peek();

                switch (direction)
                {
                    case Direction.Up:
                        updatedCell = (nx, ny);
                        Maze.EraseBottomWall(nx, ny);
                        break;
                    case Direction.Down:
                        updatedCell = (tx, ty);
                        Maze.EraseBottomWall(tx, ty);
                        break;
                    case Direction.Left:
                        updatedCell = (nx, ny);
                        Maze.EraseRightWall(nx, ny);
                        break;
                    case Direction.Right:
                        updatedCell = (tx, ty);
                        Maze.EraseRightWall(tx, ty);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                visited.Add((nx, ny));
                trace.Push((nx, ny));
                
            }
            else
            {
                trace.Pop();
            }

            var (resX, resY) = trace.Peek();
            yield return new GenerationStep(resX, resY, updatedCell);
        }


        (int x, int y, Direction direction)? NextNeighbor()
        {
            var (cx, cy) = trace.Peek();
            return Directions
                .Select((int x, int y, Direction direction) (d) => (d.x + cx, d.y + cy, GetDirection(d.x, d.y)))
                .Where(n => n.x >= 0 && n.x < Maze.Width
                                     && n.y >= 0 && n.y < Maze.Height
                                     && !visited.Contains((n.x, n.y)))
                .OrderBy(_ => Random.Next())
                .Select((int, int, Direction)? (n)=>n)
                .FirstOrDefault();
        }

        Direction GetDirection(int tox, int toy)
            => (tox, toy) switch
            {
                (0, >0) => Direction.Down,
                (0, <0) => Direction.Up,
                (>0, 0) => Direction.Right,
                (<0, 0) => Direction.Left,
                _ => throw new InvalidOperationException()
            };
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private static readonly ImmutableArray<(int x, int y)> Directions =
    [
         (0, -1), 
        (-1, 0), (1, 0),
         (0, 1)
    ];
}