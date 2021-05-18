using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Constants;
using System.IO;

namespace Brain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Const.WindowWidth, Const.WindowHeight);
            BackColor = MConst.WindowColor();

            //creare username button
            Button NewUsername = CreateButton("New User", MConst.UsernameButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor());
            NewUsername.Click += new EventHandler(OnNewUsernameClick);
            this.Controls.Add(NewUsername);
        }

        #region Buttons
        //function that creates a button given certian parameters
        public Button CreateButton(string buttonName, Point location, int width, int height, Color forecolor, Color backcolor)
        {
            Button button = new Button();
            button.Height = height;
            button.Width = width;
            button.ForeColor = forecolor;
            button.BackColor = backcolor;
            button.Location = location;
            button.Text = buttonName;
            button.Name = buttonName;
            button.Font = MConst.Font();
            return button;
        }

        bool NewUsername = false;
        public void OnNewUsernameClick(object sender, EventArgs args)
        {
            NewUsername = true;
            Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;
            if(NewUsername)
            {
                DrawUsernameBox(args, g, "Create new username");
            }
            else
            {
                DrawUsernameBox(args, g, "Enter Username");
            }
        }
        #region Text Box
        protected override void OnMouseDown(MouseEventArgs e)
        {
            CheckIfUsernameBoxClicked(e, MConst.UsernameBoxPos());
        }

        void CheckIfUsernameBoxClicked(MouseEventArgs e, Point p)
        {
            CloseUsernameBoX();
            int x = (e.X - p.X);
            int y = (e.Y - p.Y);
            if (x < Const.UsernameWidth && y < Const.UsernameHeight)
            {
                OpenUsernameBox();
            }
        }

        // open and close username box
        TextBox editBox;
        void OpenUsernameBox()
        {
            editBox = new TextBox();
            editBox.Font = MConst.Font();
            Point p = MConst.UsernameBoxPos();
            editBox.SetBounds(p.X, p.Y, Const.UsernameWidth, Const.UsernameHeight);
            editBox.Multiline = true;
            Controls.Add(editBox);
            editBox.Focus();
        }
        void CloseUsernameBoX()
        {
            if (editBox != null)
            {
                Controls.Remove(editBox);
                editBox = null;
                Invalidate();
            }
        }
        void DrawUsernameBox(PaintEventArgs args, Graphics g, string text)
        {
            Rectangle box = new Rectangle(MConst.UsernameBoxPos(), MConst.UsernameSize());
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.FillRectangle(MConst.UsernameBoxBrush(), box);
            g.DrawString(text, MConst.Font(), MConst.WindowBrush(), box, format);
        }
        #endregion
    }
}
