using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Brain.Model;

namespace Brain
{
    public class PathFindingView
    {
        public Game1 Game = new Game1(6);
        public readonly int OneSquareSize = 70;
        public readonly int MaxPuzzles = 10;

        public Point UpperLeft;
        public int DrawnGridSize;

        public bool AddSquare = false;
        public bool WallsHidden = false;
        public bool DrawHitWalls = false;

        public int NumOfPuzzlesPlayed = 0;

        // NOTE: x-axis corresponds to j and y-axis corresponds to i
        public void OnePuzzle(Graphics g)
        {
            Game.userPath = new List<MyPoint>();
            Game.wallsHit = new List<MyPoint>();
            NumOfPuzzlesPlayed += 1;

            // initialize game
            // first half of one sesion will be easier (smaller puzzles), and second half will be harder
            if (NumOfPuzzlesPlayed <= MaxPuzzles / 2)
            {
                Game = new Game1(Form1.random.Next(5, 8));
            }
            else
            {
               Game = new Game1(Form1.random.Next(7, 10));
            }
            Game.userPath.Add(Game.graph.start);

            DrawGrid(g);

            // draw walls
            DrawWalls(g, Brushes.Crimson);

            WallsHidden = false;

            // let the user memorize the placement of the walls
            Program.WaitSec(4);

            // delete walls by redrawing them with different color
            DrawWalls(g, Form1.WindowBrush);

            WallsHidden = true;

            DrawStartAndEnd(g);
        }
        public void OnPaint(Graphics g)
        {
            if (AddSquare || DrawHitWalls) // if we are continuing on current puzzle
            {
                DrawGrid(g);
                DrawStartAndEnd(g);
                DrawListOfPoints(g, Game.userPath, Brushes.Aquamarine);
                if (DrawHitWalls)
                {
                    DrawWalls(g, Brushes.Crimson);
                    DrawListOfPoints(g, Game.wallsHit, Brushes.LightSeaGreen);
                    DrawHitWalls = false;
                }
                AddSquare = false;
            }
            else // otherwise we generate a new one
            {
                OnePuzzle(g);
            }
        }
        public void DrawGrid(Graphics g)
        {
            // draw the outline of the square
            DrawnGridSize = OneSquareSize * Game.graph.gridSize;

            UpperLeft = new Point
            {
                X = Form1.Center.X - (DrawnGridSize / 2),
                Y = Form1.Center.Y - (DrawnGridSize / 2)
            };
            Rectangle rect = new Rectangle(UpperLeft.X, UpperLeft.Y, DrawnGridSize, DrawnGridSize);
            g.DrawRectangle(Form1.p, rect);

            // draw the grid inside
            for (int i = 0; i <= DrawnGridSize; i += OneSquareSize)
            {
                Point fromVertical = new Point(UpperLeft.X + i, UpperLeft.Y);
                Point toVertical = new Point(UpperLeft.X + i, UpperLeft.Y + DrawnGridSize);
                g.DrawLine(Form1.p, fromVertical, toVertical);

                Point fromHorizontal = new Point(UpperLeft.X, UpperLeft.Y + i);
                Point toHorizontal = new Point(UpperLeft.X + DrawnGridSize, UpperLeft.Y + i);
                g.DrawLine(Form1.p, fromHorizontal, toHorizontal);
            }
        }
        public void DrawWalls(Graphics g, Brush b)
        {
            for (int i = 0; i < Game.graph.gridSize; i++)
            {
                for (int j = 0; j < Game.graph.gridSize; j++)
                {
                    if (Game.graph.grid[i, j] == 1)
                    {
                        Rectangle rect = new Rectangle(UpperLeft.X + (OneSquareSize * j), UpperLeft.Y + (OneSquareSize * i), OneSquareSize, OneSquareSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(Form1.p, rect);
                    }
                }
            }
        }
        public void DrawStartAndEnd(Graphics g)
        {
            MyPoint start = Game.graph.start;
            MyPoint end = Game.graph.end;
            Rectangle rect = new Rectangle(UpperLeft.X + (OneSquareSize * start.j), UpperLeft.Y + (OneSquareSize * start.i), OneSquareSize, OneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(Form1.p, rect);
            g.DrawString("S", Form1.f1, Brushes.Plum, (UpperLeft.X + (OneSquareSize * start.j)) + OneSquareSize / 2, (UpperLeft.Y + (OneSquareSize * start.i)) + OneSquareSize / 2, Form1.format);

            rect = new Rectangle(UpperLeft.X + (OneSquareSize * end.j), UpperLeft.Y + (OneSquareSize * end.i), OneSquareSize, OneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(Form1.p, rect);
            g.DrawString("E", Form1.f1, Brushes.Plum, (UpperLeft.X + (OneSquareSize * end.j)) + OneSquareSize / 2, (UpperLeft.Y + (OneSquareSize * end.i)) + OneSquareSize / 2, Form1.format);
        }
        public void DrawListOfPoints(Graphics g, List<MyPoint> l, Brush b)
        {
            Rectangle rect;
            foreach (MyPoint entry in l)
            {
                if (!entry.Equals(Game.graph.start) && !entry.Equals(Game.graph.end))
                {
                    rect = new Rectangle(UpperLeft.X + (OneSquareSize * entry.j), UpperLeft.Y + (OneSquareSize * entry.i), OneSquareSize, OneSquareSize);
                    g.FillRectangle(b, rect);
                    g.DrawRectangle(Form1.p, rect);
                }
            }
        }
        public bool IsValidSquare(MouseEventArgs e, MyPoint square)
        {
            if (IsInsideGrid(e) && square.IsConnectedTo(Game.userPath[^1]))
            {
                return true;
            }
            return false;
        }
        public bool IsInsideGrid(MouseEventArgs e)
        {
            if (e.X > UpperLeft.X && e.X < UpperLeft.X + DrawnGridSize && e.Y > UpperLeft.Y && e.Y < UpperLeft.Y + DrawnGridSize)
            {
                return true;
            }
            return false;
        }
        public void Reset(int n)
        {
            AddSquare = false;
            WallsHidden = false;
            DrawHitWalls = false;

            Game.userPath.Clear();
            Game.wallsHit.Clear();

            NumOfPuzzlesPlayed = n;
        }
    }
}
