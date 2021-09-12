using Brain.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ViewConstants;

namespace Brain
{
    public class LowToHighView
    {
        public LowToHigh<Dots> Dots;
        public LowToHigh<Number> Number;
        public LowToHigh<RomanNumeral> RomanNumeral;

        private List<string> values = new List<string>();

        public readonly int SquareSize = 150;
        readonly string[] types = new string[]{"Dots", "Number", "Roman Numeral" };
        public string type;

        public static int CorrectAnswers = 0;
        public static int WrongAnswers = 0;

        private List<Point> positions = new List<Point>();
        public int curClicked = -1;
        public int prevClicked = -1;
        private Point upperLeft;

        public bool correct = false;
        public bool clicked = false;
        private bool initialized = false;

        private readonly List<int> shade = new List<int>();


        public void OneRound(Graphics g)
        {
            type = types[Form1.random.Next(0, types.Length)];
            switch (type)
            {
                case "Dots":
                    Dots = new LowToHigh<Dots>();
                    foreach (Dots entry in Dots.values)
                    {
                        values.Add(entry.ToString());
                    }
                    break;
                case "Number":
                    Number = new LowToHigh<Number>();
                    foreach (Number entry in Number.values)
                    {
                        values.Add(entry.ToString());
                    }
                    break;
                case "Roman Numeral":
                    RomanNumeral = new LowToHigh<RomanNumeral>();
                    foreach (RomanNumeral entry in RomanNumeral.values)
                    {
                        values.Add(entry.ToString());
                    }
                    break;
            }
            DrawAll(g);
        }
        void DrawAll(Graphics g)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (positions.Count == values.Count)
                {
                    DrawOneField(g, values[i], PickBrush(i), positions[i]);
                }
                else
                {
                    GenerateNewUpperLeft();
                    while (InsidePreviouslyDrawn())
                    {
                        GenerateNewUpperLeft();
                    }
                    positions.Add(new Point(upperLeft.X,upperLeft.Y));
                    DrawOneField(g, values[i], PickBrush(i), upperLeft);
                }
  
            }
        }

        Brush PickBrush(int i)
        {
            if(shade.Contains(i))
            {
                return Brushes.LightSteelBlue;
            }
            return Brushes.MintCream;
        }
        void DrawOneField(Graphics g, string s, Brush b, Point p)
        {
            
            Rectangle rect = new Rectangle(p.X, p.Y, SquareSize, SquareSize);
            g.FillRectangle(b, rect);
            g.DrawRectangle(Form1.p, rect);
            g.DrawString(s, Form1.f2, Brushes.MediumVioletRed, p.X + SquareSize / 2, p.Y + SquareSize / 2, Form1.format);
        }
        private void GenerateNewUpperLeft()
        {
            upperLeft = new Point
            (
                Form1.random.Next(2 * SquareSize, Const.WindowWidth - 2 * SquareSize),
                Form1.random.Next(2 * SquareSize, Const.WindowHeight - 2 * SquareSize)
            );
        }
        public bool IsValidSquare(MouseEventArgs e)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                if (e.X > positions[i].X && e.X < positions[i].X + SquareSize &&
                    e.Y > positions[i].Y && e.Y < positions[i].Y + SquareSize)
                {
                    prevClicked = curClicked;
                    curClicked = i;
                    shade.Add(i);
                    return true;
                }
            }
            return false;
        }

        public bool InsidePreviouslyDrawn()
        {
            foreach(Point prevUpperLeft in positions)
            {
                if (Overlap(prevUpperLeft,upperLeft))
                {
                    return true;
                }
            }
            return false;
        }
        public bool Overlap(Point aTop, Point bTop)
        {
            Point aBottom = new Point(aTop.X + SquareSize, aTop.Y + SquareSize);
            Point bBottom = new Point(bTop.X + SquareSize, bTop.Y + SquareSize);
            if (aTop.X > bBottom.X || bTop.X > aBottom.X ||
                aBottom.Y < bTop.Y || bBottom.Y < aTop.Y)
            {
                return false;
            }
            return true;
        }
        public void OnPaint(Graphics g)
        {
            Form1.timer.DrawTimer(g);
            if (!initialized)
            {
                OneRound(g);
                initialized = true;
            }
            else if (clicked)
            {
                clicked = false;
                if (curClicked == positions.Count - 1)
                {
                    CorrectAnswers += 1;
                    Reset(CorrectAnswers, WrongAnswers);
                    OneRound(g);
                }
                else if (!correct)
                {
                    WrongAnswers += 1;
                    Reset(CorrectAnswers, WrongAnswers);
                    OneRound(g);
                }
                else if (correct)
                {
                    DrawAll(g);
                }
            }
            else
            {
                DrawAll(g);
            }

        }

        public void Reset(int c, int w)
        {
            values.Clear();
            positions.Clear();
            shade.Clear();
            CorrectAnswers = c;
            WrongAnswers = w;
            prevClicked = -1;
            curClicked = -1;
        }
    }
}
