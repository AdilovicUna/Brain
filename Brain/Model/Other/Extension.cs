﻿using System;
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
        public static T[,] ListTo2DArray<T>(this List<T> l, int rows, int columns)
        {
            T[,] result = new T[rows, columns];
            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = l[index];
                    index++;
                }
            }
            return result;
        }
    }
}
