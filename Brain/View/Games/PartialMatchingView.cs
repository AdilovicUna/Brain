using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ViewConstants;
using Brain.Model;


namespace Brain
{
    class PartialMatchingView
    {
        public PartialMatching Game = new PartialMatching();
        static readonly int BackgroundSquareSize = 250;
        static readonly Point UpperLeft = new Point
        (
            Form1.Center.X - BackgroundSquareSize/2,
            Form1.Center.Y - BackgroundSquareSize/2
        );

        static readonly int SquareSize = 120;
        static readonly Point SquareUpperLeft = new Point
        (
            Form1.Center.X - SquareSize/2,
            Form1.Center.Y - SquareSize/2
        );
        static readonly int dist = 100;
        readonly PointF[] trianglePos = new PointF[]
        {
            new Point(Form1.Center.X, Form1.Center.Y - dist),
            new Point(Form1.Center.X - dist, Form1.Center.Y + dist),
            new Point(Form1.Center.X + dist, Form1.Center.Y + dist)
        };

        readonly Pen greenPen = new Pen(Color.LimeGreen, 4);
        readonly Pen redPen = new Pen(Color.Red, 4);

        public bool correct;
        public bool initialized = false;
        public bool pressed = false;

        public static int CorrectAnswers = 0;
        public static int WrongAnswers = 0;

        Rectangle rect;
        public void OneRound(Graphics g)
        {
            DrawBackgroundRectangle(g, Brushes.MintCream);

            Game.Update();

            DrawShape(g);
        }

        public void DrawBackgroundRectangle(Graphics g, Brush b)
        {
            rect = new Rectangle(UpperLeft.X, UpperLeft.Y, BackgroundSquareSize, BackgroundSquareSize);
            g.FillRectangle(b, rect);
            g.DrawRectangle(Form1.p, rect);
        }

        public void DrawShape(Graphics g)
        {
            switch (Game.cur.shape)
            {
                case "Circle":
                    rect = new Rectangle(SquareUpperLeft.X, SquareUpperLeft.Y, SquareSize, SquareSize);
                    g.FillEllipse(Game.cur.brush, rect);
                    break;
                case "Square":
                    rect = new Rectangle(SquareUpperLeft.X, SquareUpperLeft.Y, SquareSize, SquareSize);
                    g.FillRectangle(Game.cur.brush, rect);
                    break;
                case "Triangle":
                    g.FillPolygon(Game.cur.brush, trianglePos);
                    break;
            }
        }

        public void DrawNumberOfAnswers(Graphics g)
        {

            rect = new Rectangle(UpperLeft.X + BackgroundSquareSize + SquareSize + 35,UpperLeft.Y + BackgroundSquareSize / 2 - 50 - SquareSize/4, SquareSize * 2, SquareSize / 2);
            g.DrawRectangle(greenPen, rect);
            g.DrawString("Correct: " + CorrectAnswers.ToString(), 
                Form1.f1, Brushes.MediumOrchid,
                UpperLeft.X + BackgroundSquareSize + SquareSize * 2, 
                UpperLeft.Y + BackgroundSquareSize / 2 - 50,
                Form1.format);

            rect = new Rectangle(UpperLeft.X + BackgroundSquareSize + SquareSize + 35, UpperLeft.Y + BackgroundSquareSize / 2 + 50 - SquareSize/4, SquareSize * 2, SquareSize / 2);
            g.DrawRectangle(redPen, rect);
            g.DrawString("Wrong: " + WrongAnswers.ToString(), Form1.f1, Brushes.MediumOrchid,
                UpperLeft.X + BackgroundSquareSize + SquareSize * 2,
                UpperLeft.Y + BackgroundSquareSize / 2 + 50,
                Form1.format);
        }
        public void OnPaint(Graphics g)
        {
            Form1.timer.DrawTimer(g);
            DrawNumberOfAnswers(g);

            if (!initialized)
            {
                initialized = true;
                OneRound(g);
                Program.WaitSec(1);
                OneRound(g);
            }
            else if (pressed)
            {
                pressed = false;
                if (correct)
                {
                    CorrectAnswers += 1;
                }
                else
                {
                    WrongAnswers += 1;
                }
                OneRound(g);
            }
            else
            {
                DrawBackgroundRectangle(g, Brushes.MintCream);
                DrawShape(g);
            }
        }
        public void Reset()
        {
            CorrectAnswers = 0;
            WrongAnswers = 0;
            initialized = false;
            correct = false;
            pressed = false;
        }
    }
}
