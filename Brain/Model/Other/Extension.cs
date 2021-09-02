using System;
using System.Collections.Generic;

namespace Brain.Model
{
    public static class Extension
    {
        public static List<int> Split(this int n)
        {
            Random r = new Random();
            List<int> result = new List<int>();
            int totalLeft = n;
            int subtract;
            while (true)
            {
                subtract = r.Next(1, totalLeft);
                if (totalLeft - subtract == 0)
                {
                    result.Add(subtract);
                    return result;
                }
                else if (totalLeft - subtract > 0)
                {
                    totalLeft -= subtract;
                    result.Add(subtract);
                }
            }
        }
    }
}
