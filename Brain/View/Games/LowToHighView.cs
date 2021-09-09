using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Brain.Model;
using ViewConstants;

namespace Brain
{
    class LowToHighView
    {
        public LowToHigh<Dots> Dots;
        public LowToHigh<Number> Number;
        public LowToHigh<RomanNumeral> RomanNumeral;

        string[] types = new string[]{ "Dots", "Number", "Roman Numeral" };
        string type;
        public readonly int SquareSize = 120;
        public List<Point> positions = new List<Point>();
        public Point upperLeft;
        public static int CorrectAnswers = 0;
        public Point Clicked;


        public void OneRound(Graphics g)
        {
            type = types[Form1.random.Next(0, types.Length)];
            switch (type)
            {
                case "Dots":
                    Dots = new LowToHigh<Dots>();
                    foreach (Dots entry in Dots.values)
                    {
                        DrawOneField(g,entry.ToString());
                    }
                    break;
                case "Number":
                    Number = new LowToHigh<Number>();
                    foreach (Number entry in Number.values)
                    {
                        DrawOneField(g, entry.ToString());
                    }
                    break;
                case "Roman Numeral":
                    RomanNumeral = new LowToHigh<RomanNumeral>();
                    foreach (RomanNumeral entry in RomanNumeral.values)
                    {
                        DrawOneField(g, entry.ToString());
                    }
                    break;
            }
        }
        void DrawOneField(Graphics g, string s)
        {
            GenerateNewUpperLeft();
            while (InsidePreviouslyDrawn())
            {
                GenerateNewUpperLeft();
            }
            Rectangle rect = new Rectangle(upperLeft.X, upperLeft.Y, SquareSize, SquareSize);
            g.FillRectangle(Brushes.PeachPuff, rect);
            g.DrawRectangle(Form1.p, rect);
            g.DrawString(s, Form1.f1, Brushes.MediumVioletRed, upperLeft.X + SquareSize / 2, upperLeft.Y + SquareSize / 2, Form1.format);
        }
        private void GenerateNewUpperLeft()
        {
            upperLeft = new Point
            (
                Form1.random.Next(2 * SquareSize, Const.WindowWidth - 2 * SquareSize),
                Form1.random.Next(2 * SquareSize, Const.WindowHeight - 2 * SquareSize)
            );
        }
        public bool InsidePreviouslyDrawn()
        {
            foreach(Point entry in positions)
            {
                if((upperLeft.X >= entry.X && upperLeft.X + SquareSize <= entry.X) ||
                   (upperLeft.Y >= entry.Y && upperLeft.Y + SquareSize <= entry.Y))
                {
                    return true;
                }
            }
            return false;
        }
        public void OnPaint(Graphics g)
        {
            Form1.timer.DrawTimer(g);
            OneRound(g);
        }
    }
}
