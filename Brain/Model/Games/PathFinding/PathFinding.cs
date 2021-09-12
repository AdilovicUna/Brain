using System;
using System.Collections.Generic;

namespace Brain.Model
{
    public class PathFinding : Games
    {
        public Graph graph;
        public List<MyPoint> userPath = new List<MyPoint>();
        public List<MyPoint> wallsHit = new List<MyPoint>(); 

        public PathFinding(int size)
        {
            Random random = new Random();

            Graph g = ConstructGrid(size, random);

            // keep generating grids until one of them has at least 3 paths from the starting to ending points
            while (!ExistMinThreePaths(g))
            {
                g = ConstructGrid(size, random);
            }
            graph = g;
        }

        public Graph ConstructGrid(int size, Random r)
        {
            Graph g = new Graph
            (
                // 0 on the grid represent empty spots, 1 represents a mine, 2 represent start and end.
                // the path can be drawn from start to end or vice versa, so there is no reason to differentiate their numerical values on the grid
                new int[size,size],
                new MyPoint(r.Next(0, size), r.Next(0, size)),
                new MyPoint(r.Next(0, size), r.Next(0, size)),
                size
            );

            // make sure that there is at least 2 spots between start and end in any direction
            while (Math.Abs(g.start.i - g.end.i) < 3 && Math.Abs(g.start.j - g.end.j) < 3)
            {
                (g.end.i, g.end.j) = (r.Next(0, size), r.Next(0, size));
            }

            // place mines (every spot has 30% chance of being a mine)
            double value;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MyPoint temp = new MyPoint(i, j);
                    if (temp != g.start && temp != g.end) //make sure not to put a mine on start or end
                    {
                        value = r.NextDouble();
                        if (value > 0.7)
                        {
                            g.grid[i,j] = 1;
                        }
                    }
                }
            }
            //place start and end
            g.grid[g.start.i,g.start.j] = 2;
            g.grid[g.end.i,g.end.j] = 2;

            return g;
        }

        public override void EvalScore(){
            score = 0;
            if (wallsHit.Count == 0)
            {
                score += graph.gridSize * 100;
            }
            score += (userPath.Count - wallsHit.Count) * 25;
        }

        public bool ExistMinThreePaths(Graph g)
        {
            // run DFS and count how many paths are there
            int numberOfPaths = g.DFS(g.start, g.end, g, new List<MyPoint>(), new List<MyPoint>(), 0);
            if (numberOfPaths >= 3)
            {
                return true;
            }
            return false;
        }
    }
}