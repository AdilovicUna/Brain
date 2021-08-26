using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ViewConstants;
using System.Threading;

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
        Point Center = new Point
        {
            X = Const.WindowWidth / 2,
            Y = Const.WindowHeight / 2
        };
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Const.WindowWidth, Const.WindowHeight);
            BackColor = MConst.WindowColor();

            //create new username button
            NewUser = CreateButton("New User", MConst.NewUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            NewUser.Click += new EventHandler(OnNewUserClick);
            Controls.Add(NewUser);
            //create existing username button
            ExistingUser = CreateButton("Existing User", MConst.ExsistingUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            ExistingUser.Click += new EventHandler(OnExsitingUserClick);
            Controls.Add(ExistingUser);
            Play = CreateButton("Play", MConst.PlayButton(), Const.PlayButtonWidth, Const.PlayButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            Play.Click += new EventHandler(OnPlayClick);
            Controls.Add(Play);
        }

        #region Buttons
        public Button CreateButton(string buttonName, Point location, int width, int height, Color forecolor, Color backcolor, Font font)
        {
            //function that creates a button given certian parameters
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
        bool PathFindingClicked = false;
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
            Controls.Remove(NewUser);
            Controls.Remove(ExistingUser);
            Controls.Remove(Play);
            //create Statistics button
            Statistics = CreateButton("Statistics", MConst.Statistics(), Const.StatisticsWidth, Const.StatisticsHeight, MConst.WindowColor(), MConst.StatisticsColor(), MConst.PlayFont());
            Statistics.Click += new EventHandler(OnPlayClick);
            Controls.Add(Statistics);
            //create Path finding button
            PathFinding = CreateButton("Path Finding", MConst.Game1(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.MemoryColor(), MConst.PlayFont());
            PathFinding.Click += new EventHandler(OnPathFindingClick);
            Controls.Add(PathFinding);
            //create Partial match button
            PartialMatch = CreateButton("Partial Match", MConst.Game2(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.MemoryColor(), MConst.PlayFont());
            PartialMatch.Click += new EventHandler(OnPlayClick);
            Controls.Add(PartialMatch);
            //create Nanogram button
            Nanogram = CreateButton("Nanogram", MConst.Game3(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            Nanogram.Click += new EventHandler(OnPlayClick);
            Controls.Add(Nanogram);
            //create Low to High button
            LowToHigh = CreateButton("Low To High", MConst.Game4(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            LowToHigh.Click += new EventHandler(OnPlayClick);
            Controls.Add(LowToHigh);
            //create Sort button
            Sort = CreateButton("Sort", MConst.Game5(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.FocusColor(), MConst.PlayFont());
            Sort.Click += new EventHandler(OnPlayClick);
            Controls.Add(Sort);
            //create Color read button
            ColorRead = CreateButton("Color Read", MConst.Game6(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.FocusColor(), MConst.PlayFont());
            ColorRead.Click += new EventHandler(OnPlayClick);
            Controls.Add(ColorRead);
            //create Word hunt button
            WordHunt = CreateButton("Word Hunt", MConst.Game7(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.LanguageColor(), MConst.PlayFont());
            WordHunt.Click += new EventHandler(OnPlayClick);
            Controls.Add(WordHunt);
            //create Typing button
            Typing = CreateButton("Typing", MConst.Game8(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.LanguageColor(), MConst.PlayFont());
            Typing.Click += new EventHandler(OnPlayClick);
            Controls.Add(Typing);
            PlayClicked = true;
            CloseUsernameBoX();
            Invalidate();
        }

        public void OnPathFindingClick(object sender, EventArgs args)
        {
            RemoveOnPlayClickButtons();
            PathFindingClicked = true;
            Invalidate();
        }

        public void RemoveOnPlayClickButtons()
        {
            Controls.Remove(Statistics);
            Controls.Remove(PathFinding);
            Controls.Remove(PartialMatch);
            Controls.Remove(Nanogram);
            Controls.Remove(LowToHigh);
            Controls.Remove(Sort);
            Controls.Remove(ColorRead);
            Controls.Remove(WordHunt);
            Controls.Remove(Typing);
        }
        #endregion
        #region Overrides
        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;
            if(User && !PlayClicked)
            {
                DrawUsernameBox(g, "Create new username");
            }
            else if (!User && !PlayClicked)
            {
                DrawUsernameBox(g, "Enter Username");
            }
            else if (PathFindingClicked)
            {
                PathFindingView(g);
                PathFindingClicked = false;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            CheckIfUsernameBoxClicked(e, MConst.UsernameBoxPos());
        }
        #endregion
        #region Username TextBox
        // open and close username box
        TextBox editBox;
        void OpenUsernameBox()
        {
            editBox = new TextBox
            { 
                Font = MConst.Font()
            };
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
        void DrawUsernameBox(Graphics g, string text)
        {
            Rectangle box = new Rectangle(MConst.UsernameBoxPos(), MConst.UsernameSize());

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            g.FillRectangle(MConst.UsernameBoxBrush(), box);
            g.DrawString(text, MConst.Font(), MConst.WindowBrush(), box, format);
        }
        #endregion
        #region PathFinding
        void PathFindingView(Graphics g)
        {
            Random r = new Random();
            int[][] grid;
            MyPoint start, end;
            for (int _ = 0; _ < 1; _++)
            {
                // initialize game
                Game1 game = new Game1(r.Next(6, 11));
                grid = game.graph.grid;
                start = game.graph.start;
                end = game.graph.end;

                // draw the outline of the square
                int oneSquareSize = 70;
                int drawnGridSize = oneSquareSize * game.gridSize;

                Pen pen = new Pen(Color.Black, 2);
                Point upperLeft = new Point
                {
                    X = Center.X - (drawnGridSize / 2),
                    Y = Center.Y - (drawnGridSize / 2)
                };
                Rectangle rect = new Rectangle(upperLeft.X, upperLeft.Y, drawnGridSize, drawnGridSize);
                g.DrawRectangle(pen, rect);

                // draw the grid inside
                for(int i = 0; i <= drawnGridSize; i += oneSquareSize)
                {
                    Point fromVertical = new Point(upperLeft.X + i, upperLeft.Y);
                    Point toVertical = new Point(upperLeft.X + i, upperLeft.Y + drawnGridSize);
                    g.DrawLine(pen, fromVertical, toVertical);

                    Point fromHorizontal = new Point(upperLeft.X, upperLeft.Y + i);
                    Point toHorizontal = new Point(upperLeft.X + drawnGridSize, upperLeft.Y + i);
                    g.DrawLine(pen, fromHorizontal, toHorizontal);
                }

                // draw walls 
                Rectangle wall;
                for (int i = 0; i < game.gridSize; i++)
                {
                    for (int j = 0; j < game.gridSize; j++)
                    {
                        if(grid[i][j] == 1)
                        {
                            wall = new Rectangle(upperLeft.X + (oneSquareSize * j), upperLeft.Y + (oneSquareSize * i), oneSquareSize, oneSquareSize);
                            g.FillRectangle(Brushes.Crimson, wall);
                            g.DrawRectangle(pen, wall);
                        }
                    }
                }

                // let the user memorize the placement of the walls
                Thread.Sleep(4000);

                // delete walls 
                for (int i = 0; i < game.gridSize; i++)
                {
                    for (int j = 0; j < game.gridSize; j++)
                    {
                        if (grid[i][j] == 1)
                        {
                            wall = new Rectangle(upperLeft.X + (oneSquareSize * j), upperLeft.Y + (oneSquareSize * i), oneSquareSize, oneSquareSize);
                            g.FillRectangle(Brushes.Thistle, wall);
                            g.DrawRectangle(pen, wall);
                        }
                    }
                }

                // draw start and end
                Rectangle rStart = new Rectangle(upperLeft.X + (oneSquareSize * start.Y), upperLeft.Y + (oneSquareSize * start.X), oneSquareSize, oneSquareSize);
                g.FillRectangle(Brushes.PapayaWhip, rStart);
                g.DrawRectangle(pen, rStart);

                Rectangle rEnd = new Rectangle(upperLeft.X + (oneSquareSize * end.Y), upperLeft.Y + (oneSquareSize * end.X), oneSquareSize, oneSquareSize);
                g.FillRectangle(Brushes.PapayaWhip, rEnd);
                g.DrawRectangle(pen, rEnd);
            }
        }
        #endregion
    }
}
