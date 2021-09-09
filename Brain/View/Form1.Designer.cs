using System;
using System.Drawing;
using System.Windows.Forms;
using ViewConstants;
using Brain.Model;

namespace Brain
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region All Buttons Defined
        Button NewUser;
        Button ExistingUser;
        Button Play;
        Button Statistics;
        Button PathFinding;
        Button PartialMatch;
        Button SumUp;
        Button ColorRead;
        Button Exit;
        #endregion

        #region General global variables
        public static readonly Color WindowColor = Color.Thistle;
        public static readonly Brush WindowBrush = Brushes.Thistle;
        public static readonly Pen p = new Pen(Color.Black, 2);
        public static readonly Font f1 = new Font("Times New Roman", 30);
        public static readonly Font f2 = new Font("Times New Roman", 20);
        public static readonly Font bigFont = new Font("Times New Roman", 100);
        public static readonly StringFormat format = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };

        public static readonly Random random = new Random();

        public static Point Center = new Point
        {
            X = Const.WindowWidth / 2,
            Y = Const.WindowHeight / 2
        };

        public static MyTimer timer = new MyTimer();


        int current = Const.FirstWindow;
        bool newUser = false;

        public readonly User user = new User();
        #endregion

        #region UsernameBox global variables
        readonly Point UsernameBoxPos = new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) - 200);
        TextBox editBox;
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}

