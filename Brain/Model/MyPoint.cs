using System;
using System.Windows.Forms;

namespace Brain.Model
{
    public class MyPoint // named like this because it overlapped with a class Point used in Form1.cs
    {
        public int i { get; set; }
        public int j { get; set; }
        public MyPoint(int x1, int y1)
        {
            i = x1;
            j = y1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MyPoint))
            {
                return false;
            }
            return (i == ((MyPoint)obj).i) && (j == ((MyPoint)obj).j);
        }
        public override int GetHashCode()
        {
            return i.GetHashCode() ^ j.GetHashCode();
        }
        public bool IsConnectedTo(MyPoint p)
        {
            if (Equals(new MyPoint(p.i - 1, p.j)) || Equals(new MyPoint(p.i + 1, p.j)) ||
                Equals(new MyPoint(p.i, p.j - 1)) || Equals(new MyPoint(p.i, p.j + 1)))
            {
                return true;
            }
            return false;
        }
    }
}
