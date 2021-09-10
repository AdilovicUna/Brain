using System.Drawing;

namespace Brain
{
    class MatchingView
    {
        readonly string[] types = new string[] { "Circle", "Square", "Triangle" };
        readonly Brush[] brushes = new Brush[] { Brushes.CadetBlue, Brushes.Crimson, Brushes.SeaGreen, Brushes.DarkViolet };

        static readonly int BackgroundSquareSize = 150;
        static readonly Point UpperLeft = new Point
        (
            Form1.Center.X - BackgroundSquareSize,
            Form1.Center.Y - BackgroundSquareSize
        );

        readonly int SquareSize = 75;
        readonly PointF[] trianglePos = new PointF[]
        {
            new Point(UpperLeft.X, UpperLeft.Y - 50),
            new Point(UpperLeft.X - 50, UpperLeft.Y + 50),
            new Point(UpperLeft.X + 50, UpperLeft.Y + 50)
        };

        int correctAnswers = 0;

        (string type, Brush brush) cur;
        (string type, Brush brush) prev;
        public void OneRound(Graphics g)
        {
            Rectangle rect = new Rectangle(UpperLeft.X, UpperLeft.Y , BackgroundSquareSize, BackgroundSquareSize);
            g.FillRectangle(Brushes.MintCream, rect);
            g.DrawRectangle(Form1.p, rect);

            cur.type = types[Form1.random.Next(0, types.Length)];
            cur.brush = brushes[Form1.random.Next(0, brushes.Length)];
            switch (cur.type)
            {
                case "Circle":
                    rect = new Rectangle(UpperLeft.X, UpperLeft.Y, SquareSize, SquareSize);
                    g.FillEllipse(cur.brush, rect);
                    break;
                case "Square":
                    rect = new Rectangle(UpperLeft.X, UpperLeft.Y, SquareSize, SquareSize);
                    g.FillRectangle(cur.brush, rect);
                    break;
                case "Triangle":
                    g.FillPolygon(cur.brush, trianglePos);
                    break;
            }
        }

        public void OnPaint(Graphics g)
        {

        }
    }
}
