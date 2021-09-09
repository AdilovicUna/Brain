using System;
using System.Linq;
using System.Collections.Generic;


namespace Brain.Model
{
    public class SumUp : Games
    {
        public readonly int number;
        public readonly int[,] sum;
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
            Random random = new Random();
            while(temp.Count < 12)
            {
                temp.Add(random.Next(1, number - number/4));
            }

            // shuffle the elements
            temp.OrderBy(item => random.Next());

            // convert the temp list to an int[,]
            sum = temp.ListTo2DArray(4,3);

        }
        public override void EvalScore()
        {
            score = SumUpView.CorrectAnswers * 100;
        }
    }
}
