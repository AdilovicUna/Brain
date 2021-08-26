using System;
using System.Collections.Generic;

public class MyPoint // named like this because it overlapped with a class Point used in Form1.cs
{
    public int X { get; set; }
    public int Y { get; set; }
    public MyPoint(int x1, int y1)
    {
        X = x1;
        Y = y1;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is MyPoint))
        {
            return false;
        }
        return (X == ((MyPoint)obj).X) && (Y == ((MyPoint)obj).Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}

public class Graph
{
    public int[][] grid;
    public MyPoint start;
    public MyPoint end;
}

public class Game1
{
    public Graph graph;
    public readonly int gridSize;

    public Game1(int size)
    {
        gridSize = size;

        Random r = new Random();

        Graph g = ConstructGrid(size, r);

        // keep generating grids until one of them has at least 3 paths from the starting to ending points
        while (!ExistMinThreePaths(g))
        {
            g = ConstructGrid(size, r);
        }
        this.graph = g;
    }

    public Graph ConstructGrid(int size, Random r)
    {
        Graph g = new Graph();

        // 0 on the grid represent empty spots, 1 represents a mine, 2 represent start and end.
        // the path can be drawn from start to end or vice versa, so there is no reason to differentiate their numerical values on the grid

        g.grid = new int[size][];
        g.start = new MyPoint(r.Next(0, size), r.Next(0, size));
        g.end = new MyPoint(r.Next(0, size), r.Next(0, size));

        // make sure that there is at least 1 spot between start and end in any direction
        while (Math.Abs(g.start.X - g.end.X) < 2 && Math.Abs(g.start.Y - g.end.Y) < 2)
        {
            (g.end.X, g.end.Y) = (r.Next(0, size), r.Next(0, size));
        }

        // place mines (every spot has 30% chance of being a mine)
        double value;
        for (int i = 0; i < size; i++)
        {
            g.grid[i] = new int[size];
            for (int j = 0; j < size; j++)
            {
                MyPoint temp = new MyPoint(i, j);
                if (temp != g.start && temp != g.end) //make sure not to put a mine on start or end
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
        g.grid[g.start.X][g.start.Y] = 2;
        g.grid[g.end.X][g.end.Y] = 2;

        return g;
    }

    public bool ExistMinThreePaths(Graph g)
    {
        // run DFS and count how many paths are there
        int numberOfPaths = DFS(g.start, g.end, g, new List<MyPoint>(), new List<MyPoint>(), 0);
        if (numberOfPaths >= 3)
        {
            return true;
        }
        return false;
    }

    public int DFS(MyPoint start, MyPoint end, Graph g, List<MyPoint> visited, List<MyPoint> path, int numberOfPaths)
    {
        // base case, if start and end are the same node we are done and we can count in one more path
        if (start.Equals(end))
        {
            numberOfPaths += 1;
            return numberOfPaths;
        }

        // loop through every neighbour of the current starting node
        List<MyPoint> n = FindNeighbours(start, g); 
        foreach (MyPoint entry in n)
        {
            // skip if this node is already on the path
            if (visited.Contains(entry))
            {
                continue;
            }
            //otherwise add entry to the path and to the list of visited nodes
            path.Add(entry);
            visited.Add(entry);
            // recursively call DSF on new starting node (entry) and update the number of paths 
            numberOfPaths = DFS(entry, end, g, visited, path, numberOfPaths); 
            // remove the last added node and proceed
            visited.Remove(entry);
            path.RemoveAt(path.Count - 1);
        }
        return numberOfPaths;
    }

    public bool IsWall(MyPoint p, Graph g)
    {
        // walls are marked with a number 1
        if (g.grid[p.X][p.Y] == 1)
        {
            return true;
        }
        return false;
    }

    public bool IsStart(MyPoint p, Graph g)
    {
        // Equals function is called instead of == 
        // otherwise it will return true if p and g.start have the same reference (which they don't)
        if (p.Equals(g.start))
        {
            return true;
        }
        return false;
    }
    public List<MyPoint> FindNeighbours(MyPoint p, Graph g)
    {
        // diagonal movement is not allowed
        int[] directionX = { -1, 1, 0, 0 };
        int[] directionY = { 0, 0, 1, -1 };

        List<MyPoint> neighbours = new List<MyPoint>();
        for (int i = 0; i < 4; i++)
        {
            MyPoint maybeNeighbour = new MyPoint(p.X + directionX[i], p.Y + directionY[i]);
            // check if there are any walls in the left, right, up and down direction from p
            // also check if p is a starting point, or if the maybeNeighbour is out of bounds of the grid
            if (maybeNeighbour.X < gridSize && maybeNeighbour.Y < gridSize &&
                maybeNeighbour.X >= 0 && maybeNeighbour.Y >= 0 &&
                !IsWall(maybeNeighbour, g) && !IsStart(maybeNeighbour, g))
            {
                neighbours.Add(maybeNeighbour);
            }
        }
        return neighbours;
    }
}