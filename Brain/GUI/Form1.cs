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
        #region All Buttons Defined
        Button NewUser;
        Button ExistingUser;
        Button Play;
        Button Statistics;
        Button PathFinding;
        Button PartialMatch;
        Button Nanogram;
        Button LowToHigh;
        Button Sort;
        Button ColorRead;
        Button WordHunt;
        Button Typing;
        #endregion
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Const.WindowWidth, Const.WindowHeight);
            BackColor = MConst.WindowColor();

            //create new username button
            NewUser = CreateButton("New User", MConst.NewUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            NewUser.Click += new EventHandler(OnNewUserClick);
            this.Controls.Add(NewUser);
            //create existing username button
            ExistingUser = CreateButton("Existing User", MConst.ExsistingUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            ExistingUser.Click += new EventHandler(OnExsitingUserClick);
            this.Controls.Add(ExistingUser);
            Play = CreateButton("Play", MConst.PlayButton(), Const.PlayButtonWidth, Const.PlayButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            Play.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(Play);
        }

        #region Buttons
        //function that creates a button given certian parameters
        public Button CreateButton(string buttonName, Point location, int width, int height, Color forecolor, Color backcolor, Font font)
        {
            Button button = new Button();
            button.Height = height;
            button.Width = width;
            button.ForeColor = forecolor;
            button.BackColor = backcolor;
            button.Location = location;
            button.Text = buttonName;
            button.Name = buttonName;
            button.Font = font;
            return button;
        }

        bool User = false;
        bool PlayClicked = false;
        public void OnNewUserClick(object sender, EventArgs args)
        {
            User = true;
            Invalidate();
        }
        public void OnExsitingUserClick(object sender, EventArgs args)
        {
            User = false;
            Invalidate();
        }
        public void OnPlayClick(object sender, EventArgs args)
        {
            this.Controls.Remove(NewUser);
            this.Controls.Remove(ExistingUser);
            this.Controls.Remove(Play);
            //create Statistics button
            Statistics = CreateButton("Statistics", MConst.Statistics(), Const.StatisticsWidth, Const.StatisticsHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.PlayFont());
            Statistics.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(Statistics);
            //create Path finding button
            PathFinding = CreateButton("Path Finding", MConst.Game1(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.MemoryColor(), MConst.PlayFont());
            PathFinding.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(PathFinding);
            //create Partial match button
            PartialMatch = CreateButton("Partial Match", MConst.Game2(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.MemoryColor(), MConst.PlayFont());
            PartialMatch.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(PartialMatch);
            //create Nanogram button
            Nanogram = CreateButton("Nanogram", MConst.Game3(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            Nanogram.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(Nanogram);
            //create Low to High button
            LowToHigh = CreateButton("Low To High", MConst.Game4(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            LowToHigh.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(LowToHigh);
            //create Sort button
            Sort = CreateButton("Sort", MConst.Game5(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.FocusColor(), MConst.PlayFont());
            Sort.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(Sort);
            //create Color read button
            ColorRead = CreateButton("Color Read", MConst.Game6(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.FocusColor(), MConst.PlayFont());
            ColorRead.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(ColorRead);
            //create Word hunt button
            WordHunt = CreateButton("Word Hunt", MConst.Game7(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.LanguageColor(), MConst.PlayFont());
            WordHunt.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(WordHunt);
            //create Typing button
            Typing = CreateButton("Typing", MConst.Game8(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.WindowColor(), MConst.LanguageColor(), MConst.PlayFont());
            Typing.Click += new EventHandler(OnPlayClick);
            this.Controls.Add(Typing);
            PlayClicked = true;
            Invalidate();
        }
        #endregion
        #region Overrides
        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;
            if(User && !PlayClicked)
            {
                DrawUsernameBox(args, g, "Create new username");
            }
            else if (!User && !PlayClicked)
            {
                DrawUsernameBox(args, g, "Enter Username");
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            CheckIfUsernameBoxClicked(e, MConst.UsernameBoxPos());
        }
        #endregion
        #region Text Box
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
