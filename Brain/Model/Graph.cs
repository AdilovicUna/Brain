using System.Collections.Generic;

namespace Brain.Model
{
    public class Graph
    {
        public int[][] grid;
        public MyPoint start;
        public MyPoint end;

        public int gridSize;

        public Graph(int[][] g, MyPoint s, MyPoint e, int size)
        {
            grid = g;
            start = s;
            end = e;
            gridSize = size;
        }

        public int DFS(MyPoint start, MyPoint end, Graph g, List<MyPoint> visited, List<MyPoint> path, int numberOfPaths)
        {
            if (numberOfPaths >= 3) // no reason to calculate everything, we need at least 3
            {
                return numberOfPaths;
            }
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
            if (g.grid[p.i][p.j] == 1)
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
                MyPoint maybeNeighbour = new MyPoint(p.i + directionX[i], p.j + directionY[i]);
                // check if there are any walls in the left, right, up and down direction from p
                // also check if p is a starting point, or if the maybeNeighbour is out of bounds of the grid
                if (maybeNeighbour.i < gridSize && maybeNeighbour.j < gridSize &&
                    maybeNeighbour.i >= 0 && maybeNeighbour.j >= 0 &&
                    !IsWall(maybeNeighbour, g) && !IsStart(maybeNeighbour, g))
                {
                    neighbours.Add(maybeNeighbour);
                }
            }
            return neighbours;
        }
    }
}
