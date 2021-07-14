using System;

class Graph
{
    public int[][] grid;
    public (int i, int j) start;
    public (int i, int j) end;
}

class Game1
{
    Graph graph;

    public Game1(int size)
    {
        Random r = new Random();

        Graph g = constructGrid(size, r);

        while (!existMinThreePaths(g))
        {
            g = constructGrid(size,r);
        }

        this.graph = g;
    }

    public Graph constructGrid(int size, Random r)
    {
        Graph g = new Graph();

        // 0 on the grid represent empty spots, 1 represents a mine, 2 represent start and end.
        // the path can be drawn from start to end or vice versa, so there is no reason to differentiate their numerical values on the grid

        g.grid = new int[size][];
        g.start = (r.Next(0, size), r.Next(0, size));
        g.end = (r.Next(0, size), r.Next(0, size));

        // make sure that there is at least 1 spot between start and end in any direction
        while (Math.Abs(g.start.i - g.end.i) < 1 && Math.Abs(g.start.j - g.end.j) < 1)
        {
            g.end = (r.Next(0, size), r.Next(0, size));
        }

        // place mines (every spot has 30% chance of being a mine)
        double value;
        for (int i = 0; i < size; i++)
        {
            g.grid[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                if ((i, j) != g.start && (i, j) != g.end) //make sure not to put a mine on start or end
                {
                    value = r.NextDouble();
                    if (value > 0.7)
                    {
                        g.grid[i][j] = 1;
                    }
                }
            }
        }
        //place start and end
        g.grid[g.start.i][g.start.j] = 2;
        g.grid[g.end.i][g.end.j] = 2;

        return g;
    }

    public bool existMinThreePaths(Graph g)
    {

    }
}