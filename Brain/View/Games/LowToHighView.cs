using Brain.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ViewConstants;

namespace Brain
{
    class LowToHighView
    {
        public LowToHigh<Dots> Dots;
        public LowToHigh<Number> Number;
        public LowToHigh<RomanNumeral> RomanNumeral;

        public List<string> values = new List<string>();

        public readonly int SquareSize = 150;
        readonly string[] types = new string[]{"Dots", "Number", "Roman Numeral" };
        public string type;

        public static int CorrectAnswers = 0;

        public List<Point> positions = new List<Point>();
        public int curClicked = -1;
        public int prevClicked = -1;
        public Point upperLeft;

        public bool correct = false;
        public bool clicked = false;
        public bool initialized = false;

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
            foreach (string entry in values)
            {
               /* if(positions.Count == values.Count)
                {
                    DrawOneField(g, entry, Brushes.MintCream);
                }
                else
                {*/
                    GenerateNewUpperLeft();
                    while (InsidePreviouslyDrawn())
                    {
                        GenerateNewUpperLeft();
                    }
                    positions.Add(new Point(upperLeft.X,upperLeft.Y));
                    DrawOneField(g, entry, Brushes.MintCream,upperLeft);
                //}
            }
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
            
            if (clicked)
            {
                MessageBox.Show(curClicked.ToString());
                clicked = false;
                if (curClicked == positions.Count - 1)
                {
                    CorrectAnswers += 1;
                    Reset();
                    Program.WaitSec(1);
                    OneRound(g);
                }
                else if (!correct)
                {
                    DrawAll(g);
                    DrawOneField(g, values[curClicked], Brushes.Silver,upperLeft);
                    Program.WaitSec(1);
                    OneRound(g);
                }
                else if (correct)
                {
                    //positions.RemoveAt(curClicked);
                    DrawAll(g);
                }
            }
            else if (!initialized)
            {
                initialized = true;
                MessageBox.Show("2");
                OneRound(g);
                MessageBox.Show("3");
            }
            else if(!clicked)
            {
                DrawAll(g);
            }

        }

        public void Reset()
        {
            values.Clear();
            positions.Clear();
            CorrectAnswers = 0;
            prevClicked = 0;
            curClicked = 0;
            initialized = false;
        }
    }
}
