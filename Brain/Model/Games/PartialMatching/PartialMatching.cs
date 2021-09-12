using System.Drawing;

namespace Brain.Model
{
    public class PartialMatching : Games
    {
        public (string shape, Brush brush) cur;
        public (string shape, Brush brush) prev;
        readonly string[] shapes = new string[] { "Circle", "Square", "Triangle" };
        readonly Brush[] brushes = new Brush[] { Brushes.CadetBlue, Brushes.DarkViolet };

        public void Update()
        {
            prev = cur;

            cur.shape = shapes[Form1.random.Next(0, shapes.Length)];
            cur.brush = brushes[Form1.random.Next(0, brushes.Length)];
        }
        public override void EvalScore()
        {
            score = PartialMatchingView.CorrectAnswers * 100 - PartialMatchingView.WrongAnswers * 20;
            if(score < 0)
            {
                score = 0;
            }
        }

        public bool Match() => prev == cur;
        public bool PartiallyMatch() => (prev.shape == cur.shape && prev.brush != cur.brush) || (prev.shape != cur.shape && prev.brush == cur.brush);
        public bool NoMatch() => prev.shape != cur.shape && prev.brush != cur.brush;
    }
}
