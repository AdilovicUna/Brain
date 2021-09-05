using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Brain.Model;

namespace Brain
{
    public class SumUpView
    {
        public SumUp Game = new SumUp(1);
        public readonly int SquareSize = 120;
        public readonly int Margin = 30;
        public readonly List<Point> AllClicked = new List<Point>();
        public static int CorrectAnswers = 0;
        public int UserSum = 0;
        public Point NumUpperLeft;
        public Point SumUpperLeft;
        public Point Clicked;
        public Timer Timer = new Timer { Interval = 1000 };
        public int Seconds = 0;
        public int Duration = 60;
        public bool initialized = false;
    }
}
