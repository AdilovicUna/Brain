using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
class Program{
    [STAThread]
    static void Main(){
        Form Form = new MyForm();
        Form.Text = "Untitled";
        Application.Run(Form);
    }
}