using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ViewConstants;
using Brain.Model;

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
        Button LowToHigh;
        Button ColorRead;
        Button Exit;
        #endregion

        #region General global variables
        readonly Color WindowColor = Color.Thistle;
        readonly Brush WindowBrush = Brushes.Thistle;
        readonly Pen p = new Pen(Color.Black, 2);
        readonly Font f1 = new Font("Times New Roman", 30);
        readonly Font f2 = new Font("Times New Roman", 20);
        readonly StringFormat format = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };
        Point Center = new Point
        {
            X = Const.WindowWidth / 2,
            Y = Const.WindowHeight / 2
        };

        int current = Const.FirstWindow;
        bool newUser = false;

        User user = new User();
        #endregion

        #region UsernameBox global variables
        readonly Point UsernameBoxPos = new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) - 200);
        TextBox editBox;
        #endregion

        #region Path Finding global variables
        Game1 pfGame = new Game1(6);
        readonly int oneSquareSize = 70;

        Point pfUpperLeft;
        int drawnGridSize;

        bool addSquare = false;
        bool wallsHidden = false;
        bool drawHitWalls = false;

        int numOfPuzzlesPlayed = 0;
        #endregion
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Const.WindowWidth, Const.WindowHeight);
            BackColor = WindowColor;

            Color UsernameButtonColor = Color.PaleVioletRed;

            Point NewUserButton = new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) - 80);
            Point ExsistingUserButton = new Point((Const.WindowWidth / 2) - (Const.UsernameWidth / 2), (Const.WindowHeight / 2) - (Const.UsernameHeight / 2) + 75);
            Point PlayButton = new Point(1150, 700);

            //create new username button
            NewUser = CreateButton("New User", NewUserButton, Const.UsernameButtonWidth, Const.UsernameButtonHeight, WindowColor, UsernameButtonColor, f1);
            NewUser.Click += new EventHandler(OnNewUserClick);
            Controls.Add(NewUser);
            //create existing username button
            ExistingUser = CreateButton("Existing User", ExsistingUserButton, Const.UsernameButtonWidth, Const.UsernameButtonHeight, WindowColor, UsernameButtonColor, f1);
            ExistingUser.Click += new EventHandler(OnExsitingUserClick);
            Controls.Add(ExistingUser);
            //create play button
            Play = CreateButton("Play", PlayButton, Const.PlayButtonWidth, Const.PlayButtonHeight, WindowColor, UsernameButtonColor, f1);
            Play.Click += new EventHandler(MainMenu);
            Controls.Add(Play);
        }

        #region Buttons
        public Button CreateButton(string buttonName, Point location, int width, int height, Color forecolor, Color backcolor, Font font)
        {
            //function that creates a button given certian parameters
            Button button = new Button
            {
                Height = height,
                Width = width,
                ForeColor = forecolor,
                BackColor = backcolor,
                Location = location,
                Text = buttonName,
                Name = buttonName,
                Font = font
            };
            return button;
        }
        public void ExitButton()
        {
            current = Const.MainMenu;
            Point ExitPos = new Point(600, 650);
            Exit = CreateButton("Exit", ExitPos, Const.ExitWidth, Const.ExitHeight, Color.LavenderBlush, Color.LightGreen, f1);
            Exit.Click += new EventHandler(MainMenu);
            Controls.Add(Exit);
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

        public void MainMenu(object sender, EventArgs args)
        {
            CloseUsernameBoX();
            if (current == Const.FirstWindow && user.Name != null)
            {
                user.UserFilePath = @"user_" + user.Name + ".txt";
                if (newUser == false)
                {
                    if (!File.Exists(user.UserFilePath))
                    {
                        MessageBox.Show(@"ERROR: Username " + user.Name + " doesn't exist." +
                            "Click New User or enter a different username");
                    }
                    else
                    {
                        LoadMainMenu();
                    }
                }
                else
                {
                    // not working without the full path
                    File.Create(user.UserFilePath);
                    LoadMainMenu();
                }
            }
            else if (user.Name != null) // user was already loaded
            {
                LoadMainMenu();
            }
            Invalidate();
        }
        void LoadMainMenu()
        {
            Controls.Remove(Exit);
            Controls.Remove(NewUser);
            Controls.Remove(ExistingUser);
            Controls.Remove(Play);

            Point Game1Pos = new Point(100, 250);
            Point Game2Pos = new Point(450, 250);
            Point Game3Pos = new Point(800, 250);
            Point Game4Pos = new Point(1150, 250);
            Point StatisticsPos = new Point(600, 650);

            Color forecolor = Color.LavenderBlush;
            Color backcolor = Color.LightSteelBlue;

            //create Statistics button
            Statistics = CreateButton("Statistics", StatisticsPos, Const.StatisticsWidth, Const.StatisticsHeight, WindowColor, Color.LavenderBlush, f1);
            Statistics.Click += new EventHandler(OnStatisticsClick);
            Controls.Add(Statistics);
            //create Path finding button
            PathFinding = CreateButton("Path Finding", Game1Pos, Const.GameSquareWidth, Const.GameSquareHeight, forecolor, backcolor, f1);
            PathFinding.Click += new EventHandler(OnPathFindingClick);
            Controls.Add(PathFinding);
            //create Low to High button
            LowToHigh = CreateButton("Low To High", Game2Pos, Const.GameSquareWidth, Const.GameSquareHeight, forecolor, backcolor, f1);
            LowToHigh.Click += new EventHandler(MainMenu);
            Controls.Add(LowToHigh);
            //create Color read button
            ColorRead = CreateButton("Color Read", Game3Pos, Const.GameSquareWidth, Const.GameSquareHeight, forecolor, backcolor, f1);
            ColorRead.Click += new EventHandler(MainMenu);
            Controls.Add(ColorRead);
            //create Partial match button
            PartialMatch = CreateButton("Partial Match", Game4Pos, Const.GameSquareWidth, Const.GameSquareHeight, forecolor, backcolor, f1);
            PartialMatch.Click += new EventHandler(MainMenu);
            Controls.Add(PartialMatch);

            current = Const.MainMenu;
        }
        public void OnPathFindingClick(object sender, EventArgs args)
        {
            RemoveOnPlayClickButtons();
            current = Const.PathFinding;
            Invalidate();
        }
        public void OnStatisticsClick(object sender, EventArgs args)
        {
            RemoveOnPlayClickButtons();
            current = Const.Statistics;
            Invalidate();
        }
        public void RemoveOnPlayClickButtons()
        {
            Controls.Remove(Statistics);
            Controls.Remove(PathFinding);
            Controls.Remove(LowToHigh);
            Controls.Remove(ColorRead);
            Controls.Remove(PartialMatch);
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;
            if (newUser && current == Const.FirstWindow)
            {
                DrawUsernameBox(g, "Create new username");
            }
            else if (!newUser && current == Const.FirstWindow)
            {
                DrawUsernameBox(g, "Enter Username");
            }
            else if (current == Const.Statistics)
            {
                DrawStatistics(g);
            }
            else if (current == Const.Score)
            {
                DrawScore(g);
                Games.score = 0;
                ExitButton();
            }
            else if (current == Const.PathFinding)
            {
                if (addSquare || drawHitWalls)
                {
                    DrawGrid(g);
                    DrawStartAndEnd(g);
                    DrawListOfPoints(g, pfGame.userPath, Brushes.Aquamarine);
                    if (drawHitWalls)
                    {
                        DrawWalls(g, Brushes.Crimson);
                        DrawListOfPoints(g, pfGame.wallsHit, Brushes.LightSeaGreen);
                        drawHitWalls = false;
                    }
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
            if (current == Const.PathFinding && wallsHidden) // no need to click anything if walls arent hidden
            {
                PfGameControls(e);
            }
            else
            {
                CheckIfUsernameBoxClicked(e);
            }
        }
        #endregion

        #region Username TextBox
        // open and close username box
        void OpenUsernameBox()
        {
            editBox = new TextBox
            {
                Font = f1
            };
            editBox.SetBounds(UsernameBoxPos.X, UsernameBoxPos.Y, Const.UsernameWidth, Const.UsernameHeight);
            Controls.Add(editBox);
            editBox.Focus();
        }
        void CloseUsernameBoX()
        {
            if (editBox != null)
            {
                user.Name = editBox.Text;
                Controls.Remove(editBox);
                editBox = null;
                Invalidate();
            }
        }
        void CheckIfUsernameBoxClicked(MouseEventArgs e)
        {
            CloseUsernameBoX();

            int x = (e.X - UsernameBoxPos.X);
            int y = (e.Y - UsernameBoxPos.Y);
            if (x < Const.UsernameWidth && y < Const.UsernameHeight)
            {
                OpenUsernameBox();
            }
        }
        void DrawUsernameBox(Graphics g, string text)
        {
            Rectangle box = new Rectangle(UsernameBoxPos, new Size(Const.UsernameWidth, Const.UsernameHeight));
            Brush b = Brushes.Lavender;
            g.FillRectangle(b, box);
            g.DrawString(text, f1, WindowBrush, box, format);
        }
        #endregion

        #region Score
        void DrawScore(Graphics g)
        {
            Point UpperLeft = new Point
            {
                X = Center.X - (500 / 2) - 100,
                Y = Center.Y - (500 / 2) + 100
            };
            Font font = new Font("Times New Roman", 100);
            g.DrawString("Score: " + Games.score.ToString(), font, Brushes.Lavender, UpperLeft.X, UpperLeft.Y);
        }


        #endregion

        // GAME REQUIREMENTS:
        // Each game has to have 5 main things: model (in a separate file), controls, score eval function, general drawing and a reset function
        // If a new game is added, NumberOfGames from the ViewConstants.cs file should be updated.
        #region Statistics
        void DrawStatistics(Graphics g)
        {
            ExitButton();
            Dictionary<string, List<int>> data = new Dictionary<string, List<int>>();
            using StreamReader sr = new StreamReader("TestFile.txt");
            string line;
            string[] splitLine;
            while ((line = sr.ReadLine()) != null)
            {
                splitLine = line.Split();
                if (!data.ContainsKey(splitLine[0]))
                {
                    data.Add(splitLine[0], new List<int>());
                }
                data[splitLine[0]].Add(Int32.Parse(splitLine[1]));
            }
        }
        #endregion
        #region Path Finding View
        // NOTE: x-axis corresponds to j and y-axis corresponds to i
        void PfGameControls(MouseEventArgs e)
        {
            MyPoint square = new MyPoint // derermine which square was clicked
            (
                (e.Y - pfUpperLeft.Y) / oneSquareSize,
                (e.X - pfUpperLeft.X) / oneSquareSize
            );


            if (IsValidSquare(e, square))
            {
                addSquare = true;
                pfGame.userPath.Add(square);

                if (pfGame.graph.grid[square.i][square.j] == 1) // if square is a wall
                {
                    pfGame.wallsHit.Add(square);
                }

                if (square.IsConnectedTo(pfGame.graph.end)) // our starting and ending points are connected with a path
                {
                    drawHitWalls = true;
                    pfGame.EvalScore();
                    Invalidate();
                    Program.WaitSec(2); // aesthetics
                    if (numOfPuzzlesPlayed < 1) // repeat if 10 puzzles weren't played
                    {
                        ResetPF(numOfPuzzlesPlayed);
                    }
                    else // if they were, show the score, reset and exit
                    {
                        user.StoreScore("Path Finding", Games.score);
                        current = Const.Score;
                        ResetPF(0);
                    }
                }
                Invalidate();
            }
        }
        void PathFindingOnePuzzle(Graphics g)
        {
            Random r = new Random();
            pfGame.userPath = new List<MyPoint>();
            pfGame.wallsHit = new List<MyPoint>();
            numOfPuzzlesPlayed += 1;

            // initialize game
            pfGame = new Game1(r.Next(6, 9));
            pfGame.userPath.Add(pfGame.graph.start);

            DrawGrid(g);

            // draw walls
            DrawWalls(g, Brushes.Crimson);

            wallsHidden = false;

            // let the user memorize the placement of the walls
            Program.WaitSec(4);

            // delete walls by redrawing them with different color
            DrawWalls(g, WindowBrush);

            wallsHidden = true;

            DrawStartAndEnd(g);
        }
        void DrawGrid(Graphics g)
        {
            // draw the outline of the square
            drawnGridSize = oneSquareSize * pfGame.graph.gridSize;

            pfUpperLeft = new Point
            {
                X = Center.X - (drawnGridSize / 2),
                Y = Center.Y - (drawnGridSize / 2)
            };
            Rectangle rect = new Rectangle(pfUpperLeft.X, pfUpperLeft.Y, drawnGridSize, drawnGridSize);
            g.DrawRectangle(p, rect);

            // draw the grid inside
            for (int i = 0; i <= drawnGridSize; i += oneSquareSize)
            {
                Point fromVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y);
                Point toVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y + drawnGridSize);
                g.DrawLine(p, fromVertical, toVertical);

                Point fromHorizontal = new Point(pfUpperLeft.X, pfUpperLeft.Y + i);
                Point toHorizontal = new Point(pfUpperLeft.X + drawnGridSize, pfUpperLeft.Y + i);
                g.DrawLine(p, fromHorizontal, toHorizontal);
            }
        }
        void DrawWalls(Graphics g, Brush b)
        {
            for (int i = 0; i < pfGame.graph.gridSize; i++)
            {
                for (int j = 0; j < pfGame.graph.gridSize; j++)
                {
                    if (pfGame.graph.grid[i][j] == 1)
                    {
                        Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * j), pfUpperLeft.Y + (oneSquareSize * i), oneSquareSize, oneSquareSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p, rect);
                    }
                }
            }
        }
        void DrawStartAndEnd(Graphics g)
        {
            MyPoint start = pfGame.graph.start;
            MyPoint end = pfGame.graph.end;
            Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * start.j), pfUpperLeft.Y + (oneSquareSize * start.i), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("S", f1, Brushes.Plum, (pfUpperLeft.X + (oneSquareSize * start.j)) + oneSquareSize / 2, (pfUpperLeft.Y + (oneSquareSize * start.i)) + oneSquareSize / 2, format);

            rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * end.j), pfUpperLeft.Y + (oneSquareSize * end.i), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("E", f1, Brushes.Plum, (pfUpperLeft.X + (oneSquareSize * end.j)) + oneSquareSize / 2, (pfUpperLeft.Y + (oneSquareSize * end.i)) + oneSquareSize / 2, format);
        }
        void DrawListOfPoints(Graphics g, List<MyPoint> l, Brush b)
        {
            Rectangle rect;
            foreach (MyPoint entry in l)
            {
                if (!entry.Equals(pfGame.graph.start) && !entry.Equals(pfGame.graph.end))
                {
                    rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * entry.j), pfUpperLeft.Y + (oneSquareSize * entry.i), oneSquareSize, oneSquareSize);
                    g.FillRectangle(b, rect);
                    g.DrawRectangle(p, rect);
                }
            }
        }
        bool IsValidSquare(MouseEventArgs e, MyPoint square)
        {
            if (IsInsideGrid(e) && square.IsConnectedTo(pfGame.userPath[^1]))
            {
                return true;
            }
            return false;
        }
        bool IsInsideGrid(MouseEventArgs e)
        {
            if (e.X > pfUpperLeft.X && e.X < pfUpperLeft.X + drawnGridSize && e.Y > pfUpperLeft.Y && e.Y < pfUpperLeft.Y + drawnGridSize)
            {
                return true;
            }
            return false;
        }
        void ResetPF(int n)
        {
            addSquare = false;
            wallsHidden = false;
            drawHitWalls = false;

            pfGame.userPath.Clear();
            pfGame.wallsHit.Clear();

            numOfPuzzlesPlayed = n;
        }
        #endregion
    }
}
