using System.Drawing;
using System.Windows.Forms;

namespace Constants
{
    public static class Const
    {
        public const int WindowWidth = 1200;
        public const int WindowHeight = 700;

        public const int UsernameWidth = 500;
        public const int UsernameHeight = 50;

        public const int UsernameButtonWidth = 500;
        public const int UsernameButtonHeight = 150;
    }

    public static class MConst
    {
        public static Color WindowColor() => Color.Thistle;
        public static Brush WindowBrush() => Brushes.Thistle;
        public static Point UsernameBoxPos() => new Point((Const.WindowWidth / 2) - (Const.UsernameWidth/2), (Const.WindowHeight / 2) - (Const.UsernameHeight/2) - 70);
        public static Size UsernameSize() => new Size(Const.UsernameWidth, Const.UsernameHeight);
        public static Brush UsernameBoxBrush() => Brushes.Lavender;
        public static Point UsernameButtonPos() => new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) + 70);
        public static Color UsernameButtonColor() => Color.PaleVioletRed;
        public static Font Font() => new Font("Times New Roman", 20);
        public static Pen Pen() => new Pen(Color.Black, 1);
    }
}