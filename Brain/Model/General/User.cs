using System.Collections.Generic;
using System.IO;

namespace Brain.Model
{
    public class User
    {
        public string Name { get; set; }
        public string GameName { get; set; }
        public string UserFilePath { get; set; }

        public Dictionary<string, List<int>> data;
        public void StoreData(int score)
        {
            using StreamWriter sw = File.AppendText(UserFilePath);
            sw.WriteLine(GameName + " " + score);
        }
        public void GetData()
        {
            using StreamReader sr = new StreamReader(UserFilePath);
            string line;
            string[] splitLine;
            while ((line = sr.ReadLine()) != null)
            {
                splitLine = line.Split();
                if (!data.ContainsKey(splitLine[0]))
                {
                    data.Add(splitLine[0], new List<int>());
                }
                data[splitLine[0]].Add(int.Parse(splitLine[1]));
            }
        }    
    }
}
