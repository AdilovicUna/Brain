using System;
class Game1
{
    public int[][] grid;
    (int i, int j) start;
    (int i, int j) end;

    public Game1(int size)
    {
        // 0 on the grid represent empty spots, 1 represents a mine, 2 represent start and end.
        // the path can be drawn from start to end or vice versa, so there is no reason to differentiate their numerical values on the grid

        grid = new int[size][];
        Random r = new Random();
        start = (r.Next(0, size), r.Next(0, size));
        end = (r.Next(0, size), r.Next(0, size));

        // make sure that there is at least 1 spot between start and end in any direction
        while (Math.Abs(start.i - end.i) < 1 && Math.Abs(start.j - end.j) < 1)
        {
            end = (r.Next(0, size), r.Next(0, size));
        }

        // place mines (every spot has 30% chance of being a mine)
        double value;
        for (int i = 0; i < size; i++)
        {
            grid[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                if ((i, j) != start && (i, j) != end) //make sure not to put a mine on start or end
                {
                    value = r.NextDouble();
                    if (value > 0.7)
                    {
                        grid[i][j] = 1;
                    }
                }
            }
        }
        //place start and end
        grid[start.i][start.j] = 2;
        grid[end.i][end.j] = 2;
    }
}