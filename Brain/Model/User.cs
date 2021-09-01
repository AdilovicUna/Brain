using System;
using System.Windows.Forms;
using System.IO;

namespace Brain.Model
{
    class User
    {
        public string Name { get; set; }
        public string UserFilePath { get; set; }
        public void StoreScore(string name, int score)
        {
            using StreamWriter sw = File.AppendText(UserFilePath);
            sw.WriteLine(name + " " + score);
        }
    }
}
