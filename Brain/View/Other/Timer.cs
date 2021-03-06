using System.Drawing;
using System.Windows.Forms;
using ViewConstants;

namespace Brain
{
    public class MyTimer
    {
        public Timer Timer = new Timer { Interval = 1000 };
        public int Seconds = 0;
        public static int Duration = 60;
        public Point position = new Point
        (
            Const.WindowWidth - 200, 
            100
        );

        public void DrawTimer(Graphics g)
        {
            int t = Duration - Seconds;
            g.DrawString($"Time left: {t}", Form1.f1, Brushes.Red, position.X, position.Y,  Form1.format);
        }
    }
}
