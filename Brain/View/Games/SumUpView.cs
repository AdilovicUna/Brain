using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Brain.Model;
using ViewConstants;

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
        public bool initialized = false;


        public void OneRound(Graphics g)
        {
            Game = new SumUp(Form1.random.Next(15, 35));
            DrawNumber(g);
            DrawSum(g);
        }

        public void OnPaint(Graphics g)
        {
            Form1.timer.DrawTimer(g);
            if (initialized && UserSum < Game.number)
            {
                DrawNumber(g);
                DrawSum(g);
            }
            else if (!initialized)
            {
                initialized = true;
                OneRound(g);
            }
        }
        public Point SumUpperLeftInitialValue()
        {
            return new Point
            (
                Form1.Center.X - (SquareSize * 4 + Margin * 3) / 2,
                NumUpperLeft.Y + SquareSize + 100
            );
        }
        public void DrawNumber(Graphics g)
        {
            NumUpperLeft = new Point
           (
               Form1.Center.X - SquareSize / 2,
               Form1.Center.Y - Const.WindowHeight / 4 - SquareSize / 2 - 50 // quater of the screen and -50 to make it look nicer
           );
            g.DrawString(Game.number.ToString(), Form1.bigFont, Brushes.Navy, NumUpperLeft.X + SquareSize / 2, NumUpperLeft.Y + SquareSize / 2, Form1.format);
        }
        public void DrawSum(Graphics g)
        {
            SumUpperLeft = SumUpperLeftInitialValue();
            // colors at random
            Brush[] brushes = new Brush[] { Brushes.CadetBlue, Brushes.Crimson, Brushes.SeaGreen, Brushes.DarkViolet };

            // draw the sum in 4 rows and 3 columns
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (AllClicked.Contains(new Point(i, j))) // shade already selected squares
                    {
                        DrawOneSquare(g, SumUpperLeft, Game.sum[i, j], Brushes.LightSteelBlue);

                    }
                    else
                    {
                        DrawOneSquare(g, SumUpperLeft, Game.sum[i, j], brushes[i]);
                    }
                    SumUpperLeft.Y += SquareSize + Margin;
                }
                SumUpperLeft.X += SquareSize + Margin;
                SumUpperLeft.Y -= (SquareSize + Margin) * 3;
            }
            SumUpperLeft = SumUpperLeftInitialValue();
        }
        public void DrawOneSquare(Graphics g, Point upperLeft, int i, Brush b)
        {
            Rectangle rect = new Rectangle(upperLeft.X, upperLeft.Y, SquareSize, SquareSize);
            g.FillRectangle(b, rect);
            g.DrawRectangle(Form1.p, rect);
            g.DrawString(i.ToString(), Form1.f1, Brushes.Plum, upperLeft.X + SquareSize / 2, upperLeft.Y + SquareSize / 2, Form1.format);
        }
        public bool IsValidSquare(MouseEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Clicked = new Point(i, j);
                    if (e.X > SumUpperLeft.X && e.X < SumUpperLeft.X + SquareSize && e.Y > SumUpperLeft.Y && e.Y < SumUpperLeft.Y + SquareSize
                        && !AllClicked.Contains(Clicked))
                    {
                        SumUpperLeft = SumUpperLeftInitialValue();
                        return true;
                    }
                    SumUpperLeft.Y += 150;
                }
                SumUpperLeft.X += 150;
                SumUpperLeft.Y -= 150 * 3;
            }
            SumUpperLeft = SumUpperLeftInitialValue();
            return false;
        }
        public void Reset(int n)
        {
            UserSum = 0;
            CorrectAnswers = n;
            initialized = false;
            AllClicked.Clear();
        }
    }
}
