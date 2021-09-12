using System;
using System.Collections.Generic;

namespace Brain.Model
{
    class LowToHigh<T> : Games where T: new()
    {
        public List<T> values;
        private readonly Random random = new Random();
        public LowToHigh()
        {
            values = new List<T>();
            int length = random.Next(4,9);
            while(values.Count != length)
            {
                T temp = new T();
                if (!values.Contains(temp))
                {
                    values.Add(temp);
                }
            }
            values.Sort();
        }

        public override void EvalScore() 
        {
            score = LowToHighView.CorrectAnswers * 100;
        }
    }
}
