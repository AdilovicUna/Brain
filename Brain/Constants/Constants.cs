using System.Drawing;
using System.Windows.Forms;

namespace Constants
{
    public static class Const
    {
        public const int WindowWidth = 1500;
        public const int WindowHeight = 900;

        public const int UsernameWidth = 600;
        public const int UsernameHeight = 80;

        public const int UsernameButtonWidth = 600;
        public const int UsernameButtonHeight = 130;

        public const int PlayButtonWidth = 250;
        public const int PlayButtonHeight = 100;

        public const int GameSquareWidth = 250;
        public const int GameSquareHeight = 200;

        public const int StatisticsWidth = 300;
        public const int StatisticsHeight = 150;
    }

    public static class MConst
    {
        public static Color WindowColor() => Color.Thistle;
        public static Color MemoryColor() => Color.PaleGoldenrod;
        public static Color ProblemSolvingColor() => Color.PaleGreen;
        public static Color FocusColor() => Color.PowderBlue;
        public static Color LanguageColor() => Color.NavajoWhite;
        public static Color StatisticsColor() => Color.PaleVioletRed;
        public static Color MainMenuColor() => Color.Plum;
        public static Brush WindowBrush() => Brushes.Thistle;
        public static Point UsernameBoxPos() => new Point((Const.WindowWidth / 2) - (Const.UsernameWidth/2), (Const.WindowHeight / 2) - (Const.UsernameHeight/2) - 130);
        public static Size UsernameSize() => new Size(Const.UsernameWidth, Const.UsernameHeight);
        public static Brush UsernameBoxBrush() => Brushes.Lavender;
        public static Color UsernameButtonColor() => Color.PaleVioletRed;
        public static Point NewUserButtonPos() => new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2));
        public static Point ExsistingUserButtonPos() => new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) + 150);
        public static Point PlayButton() => new Point(1150, 700);
        public static Point Game1() => new Point(100, 150);
        public static Point Game2() => new Point(100, 400);
        public static Point Game3() => new Point(450, 150);
        public static Point Game4() => new Point(450, 400);
        public static Point Game5() => new Point(800, 150);
        public static Point Game6() => new Point(800, 400);
        public static Point Game7() => new Point(1150, 150);
        public static Point Game8() => new Point(1150, 400);
        public static Point Statistics() => new Point(600, 700);
        public static Font PlayFont() => new Font("Times New Roman", 20);
        public static Font Font() => new Font("Times New Roman", 30);
        public static Pen Pen() => new Pen(Color.Black, 1);
    }
}