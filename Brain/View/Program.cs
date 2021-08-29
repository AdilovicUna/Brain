using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brain
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1
            {
                Text = "Brain"
            };
            Application.Run(f);
        }
        public static void WaitSec(int secs)
        {
            // code found at: https://www.reddit.com/r/csharp/comments/bmxb0e/less_harsh_alternative_to_threadsleep/
            // this was the best alternative to Thread.Sleep() method I could find
            DateTime Tthen = DateTime.Now;
            do
            {
                Application.DoEvents();
            } while (Tthen.AddSeconds(secs) > DateTime.Now);
        }
    }
}
