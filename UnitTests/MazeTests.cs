using MazeGenerator.Domain;

namespace UnitTests;

public class MazeTests
{
    [Fact]
    public void TestEraseRightWall_WhenJustConstructed_ThatOneCellShouldBeWithErasedRightWall()
    {
        Maze maze = new Maze(10,10);
        maze.EraseRightWall(3,4);

        for (int x = 0; x < maze.Width; x++)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                if (x == 3 && y == 4)
                {
                    Assert.True(!maze[x, y].RightWall && maze[x, y].BottomWall);
                }
                else
                {
                    Assert.True(maze[x, y].RightWall && maze[x, y].BottomWall);
                }
            }
        }
    }
    [Fact]
    public void TestEraseBottomWall_WhenJustConstructed_ThatOneCellShouldBeWithErasedBottomWall()
    {
        Maze maze = new Maze(10,10);
        maze.EraseBottomWall(2, 7);

        for (int x = 0; x < maze.Width; x++)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                if (x == 2 && y == 7)
                {
                    Assert.True(maze[x, y].RightWall && !maze[x, y].BottomWall);
                }
                else
                {
                    Assert.True(maze[x, y].RightWall && maze[x, y].BottomWall);
                }
            }
        }
    }
}