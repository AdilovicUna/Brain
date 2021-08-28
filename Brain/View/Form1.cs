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

        #region General global variables
        Point Center = new Point
        {
            X = Const.WindowWidth / 2,
            Y = Const.WindowHeight / 2
        };
        int currentGame = Const.FirstWindow;
        bool newUser = false;
        #endregion

        #region Path Finding global variables
        Game1 game = new Game1(6);
        Point pfUpperLeft;
        int drawnGridSize;
        readonly int oneSquareSize = 70;
        MyPoint square;
        bool addSquare = false;
        bool wallsHidden = false;
        List<MyPoint> userPath;
        List<MyPoint> wallsHit;
        int numOfPuzzlesPlayed = 0;
        #endregion
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Const.WindowWidth, Const.WindowHeight);
            BackColor = MConst.WindowColor();

            //create new username button
            NewUser = CreateButton("New newUser", MConst.NewUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
            NewUser.Click += new EventHandler(OnNewUserClick);
            Controls.Add(NewUser);
            //create existing username button
            ExistingUser = CreateButton("Existing newUser", MConst.ExsistingUserButtonPos(), Const.UsernameButtonWidth, Const.UsernameButtonHeight, MConst.WindowColor(), MConst.UsernameButtonColor(), MConst.Font());
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
        public void OnNewUserClick(object sender, EventArgs args)
        {
            newUser = true;
            Invalidate();
        }
        public void OnExsitingUserClick(object sender, EventArgs args)
        {
            newUser = false;
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
            currentGame = Const.MainMenu;
            CloseUsernameBoX();
            Invalidate();
        }

        public void OnPathFindingClick(object sender, EventArgs args)
        {
            RemoveOnPlayClickButtons();
            currentGame = Const.PathFinding;
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
            if(newUser && currentGame == Const.FirstWindow)
            {
                DrawUsernameBox(g, "Create new username");
            }
            else if (!newUser && currentGame == Const.FirstWindow)
            {
                DrawUsernameBox(g, "Enter Username");
            }
            else if (currentGame == Const.PathFinding)
            {
                MessageBox.Show("INVALIDATE 1");
                if (addSquare)
                {
                    MessageBox.Show("invalidate 2");
                    DrawGrid(g);
                    DrawStartAndEnd(g);
                    DrawSquare(g);
                    addSquare = false;
                }
                else
                {
                    PathFindingOnePuzzle(g);
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (currentGame == Const.PathFinding && wallsHidden) 
            {
                MessageBox.Show("if (currentGame == Const.PathFinding && wallsHidden) passed");
                square = new MyPoint
                (
                    (e.X - pfUpperLeft.X) / oneSquareSize,
                    (e.Y - pfUpperLeft.Y) / oneSquareSize
                );
                MessageBox.Show(square.X.ToString() + " " + square.Y.ToString());
                if (IsValidSquare(e))
                {
                    MessageBox.Show("isValidSquare passed");
                    addSquare = true;
                    userPath.Add(square);
                    Invalidate();
                }
                // draw the square
                // if the square is the neighbour of the end node
                // then call a function then draw the hit walls
                // evaluate the score
                // if numOfPuzzlesPlayed < 10
                // call Path Finding view again
                // else show final score and put an exit button set main menu as current game;
                // else add to path and to maybe to wallsHit
            }
            else
            {
                CheckIfUsernameBoxClicked(e, MConst.UsernameBoxPos());
            }
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
        #region Path Finding View
        void PathFindingOnePuzzle(Graphics g)
        {
            Random r = new Random();
            userPath = new List<MyPoint>();
            wallsHit = new List<MyPoint>();
            numOfPuzzlesPlayed += 1;

            // initialize game
            game = new Game1(r.Next(6, 11));

            DrawGrid(g);

            // draw walls
            DrawWalls(g, Brushes.Crimson);

            wallsHidden = false;

            // let the user memorize the placement of the walls
            Thread.Sleep(4000);

            // delete walls by redrawing them with different color
            DrawWalls(g, Brushes.Thistle);

            wallsHidden = true;

            DrawStartAndEnd(g);

            userPath.Add(game.graph.start);
            userPath.Add(game.graph.end);
        }

        void DrawGrid(Graphics g)
        {
            // draw the outline of the square
            drawnGridSize = oneSquareSize * game.gridSize;

            pfUpperLeft = new Point
            {
                X = Center.X - (drawnGridSize / 2),
                Y = Center.Y - (drawnGridSize / 2)
            };
            Rectangle rect = new Rectangle(pfUpperLeft.X, pfUpperLeft.Y, drawnGridSize, drawnGridSize);
            g.DrawRectangle(MConst.Pen2(), rect);

            // draw the grid inside
            for (int i = 0; i <= drawnGridSize; i += oneSquareSize)
            {
                Point fromVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y);
                Point toVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y + drawnGridSize);
                g.DrawLine(MConst.Pen2(), fromVertical, toVertical);

                Point fromHorizontal = new Point(pfUpperLeft.X, pfUpperLeft.Y + i);
                Point toHorizontal = new Point(pfUpperLeft.X + drawnGridSize, pfUpperLeft.Y + i);
                g.DrawLine(MConst.Pen2(), fromHorizontal, toHorizontal);
            }
        }
        void DrawWalls(Graphics g, Brush b)
        {
            for (int i = 0; i < game.gridSize; i++)
            {
                for (int j = 0; j < game.gridSize; j++)
                {
                    if (game.graph.grid[i][j] == 1)
                    {
                        Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * j), pfUpperLeft.Y + (oneSquareSize * i), oneSquareSize, oneSquareSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(MConst.Pen2(), rect);
                    }
                }
            }
        }
        void DrawStartAndEnd(Graphics g)
        {
            MyPoint start = game.graph.start;
            MyPoint end = game.graph.end;
            Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * start.Y), pfUpperLeft.Y + (oneSquareSize * start.X), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(MConst.Pen2(), rect);
            g.DrawString("S", MConst.Font(), Brushes.Black, pfUpperLeft.X + (oneSquareSize * start.Y), pfUpperLeft.Y + (oneSquareSize * start.X));

            rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * end.Y), pfUpperLeft.Y + (oneSquareSize * end.X), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(MConst.Pen2(), rect);
            g.DrawString("E", MConst.Font(), Brushes.Black, pfUpperLeft.X + (oneSquareSize * end.Y), pfUpperLeft.Y + (oneSquareSize * end.X));
        }
        void DrawSquare(Graphics g)
        {
            Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * square.Y), pfUpperLeft.Y + (oneSquareSize * square.X), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.Aquamarine, rect);
            g.DrawRectangle(MConst.Pen2(), rect);
        }
        bool IsValidSquare(MouseEventArgs e)
        {
            if (IsInsideGrid(e) && IsConnectedToPath())
            {
                return true;
            }
            return false;
        }
        bool IsInsideGrid(MouseEventArgs e)
        {
            if(e.X > pfUpperLeft.X && e.X < pfUpperLeft.X + drawnGridSize && e.Y > pfUpperLeft.Y && e.Y < pfUpperLeft.Y + drawnGridSize)
            {
                return true;
            }
            return false;
        } 
        bool IsConnectedToPath()
        {
            foreach(MyPoint entry in userPath)
            {
                if (entry.Equals(new MyPoint(square.X - 1, square.Y)) || entry.Equals(new MyPoint(square.X + 1, square.Y)) ||
                    entry.Equals(new MyPoint(square.X, square.Y - 1)) || entry.Equals(new MyPoint(square.X, square.Y + 1)))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
