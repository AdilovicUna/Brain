using System;
using System.Collections.Generic;

namespace Brain.Model
{
    public class SumUp : Games
    {
        public readonly int number;
        public readonly int[] sum;
        public SumUp(int n)
        {
            number = n;
            List<int> temp;
            temp = number.Split();
            while(temp.Count > 6) // we don't want too make the game too hard
            {
                temp = number.Split();
            }

            // now that we have a solution we want to add some arbitrary numbers as well;
            Random r = new Random();
            while(temp.Count < 12)
            {
                temp.Add(r.Next(1, number - number/4));
            }

            // our array is ready
            sum = temp.ToArray();
        }
        public override void EvalScore() { }
    }
}
