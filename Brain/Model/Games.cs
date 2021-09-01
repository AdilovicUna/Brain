using System;
using System.Windows.Forms;

namespace Brain.Model
{
    public abstract class Games
    {
        public static int score = 0;
        public abstract void EvalScore();
    }
}
