using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Brain.Model;
using System.Linq;

namespace Brain
{
    public class Statistics
    {
        readonly static int width = 500;
        readonly static int height = 250;
        readonly Point[] UpperLefts = new Point[]
        {
            new Point(100,100),
            new Point(900,100),
            new Point(100,550),
            new Point(900,550)
        };
        readonly Point[] LowerLefts = new Point[]
        {
            new Point(100,100 + height),
            new Point(900,100 + height),
            new Point(100,550 + height),
            new Point(900,550 + height)
        };
        readonly Point[] LowerRights = new Point[]
        {
            new Point(100 + width,100 + height),
            new Point(900 + width,100 + height),
            new Point(100 + width,550 + height),
            new Point(900 + width,550 + height)
        };

        PointF[] curve;
        float highLow;
        float forward;
        float x;
        float y;

        public void DrawStatistics(Graphics g, User user)
        {
            user.GetData();
            int index = 0;
            if(user.data.Count > UpperLefts.Length)
            {
                MessageBox.Show("ERROR: game added, update arrays");
                return;
            }
            foreach(KeyValuePair<string, List<int>> entry in user.data)
            {
                int highest = entry.Value.Max();
                g.DrawString("0", Form1.f2, Brushes.Black, new Point(LowerLefts[index].X - 30, LowerLefts[index].Y));
                g.DrawString(highest.ToString(), Form1.f2, Brushes.Black, new Point(UpperLefts[index].X - 80, UpperLefts[index].Y));
                g.DrawString(entry.Key, Form1.f2, Brushes.Black, new Point(LowerLefts[index].X + width/3, LowerLefts[index].Y + 25));
                g.DrawLine(Form1.p, UpperLefts[index], LowerLefts[index]);
                g.DrawLine(Form1.p, LowerLefts[index], LowerRights[index]);
                curve = GetPoints(entry.Value, highest, index);
                if (entry.Value.Count > 2)
                {
                    g.DrawCurve(new Pen(Brushes.Blue, 2), curve);
                }
                else
                {
                    g.DrawLine(Form1.p, curve[0], curve[1]);
                }
                index++;
            }
        }

        public PointF[] GetPoints(List<int> l, float highest, int index)
        {
            List<PointF> temp = new List<PointF>();
            forward = width / (l.Count()-1);
            highLow = height / highest;
            for(int i = 0; i < l.Count; i++)
            {
                x = i * forward;
                y = (l[i] * highLow);
                temp.Add(new PointF(LowerLefts[index].X + x, LowerLefts[index].Y - y));
            }
            return temp.ToArray();
        }
    }
}
