using System.Drawing;
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
    }
}
