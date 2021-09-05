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
        readonly PathFindingView pf = new PathFindingView();
        readonly SumUpView su = new SumUpView();
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

            // Timers
            su.Timer.Tick += new EventHandler(OnSuTimerEvent);
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

                if (!File.Exists(user.UserFilePath))
                {
                    if(newUser == true)
                    {
                        File.Create(user.UserFilePath);
                        LoadMainMenu();
                    }
                    else
                    {
                        MessageBox.Show(@"ERROR: Username " + user.Name + " doesn't exist." +
                        "Click New User or enter a different username");
                    }
                       
                }
                else
                {
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
            SumUp = CreateButton("Sum up", Game2Pos, Const.GameSquareWidth, Const.GameSquareHeight, forecolor, backcolor, f1);
            SumUp.Click += new EventHandler(OnSumUpClick);
            Controls.Add(SumUp);
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
        public void OnSumUpClick(object sender, EventArgs args)
        {
            RemoveOnPlayClickButtons();
            current = Const.SumUp;
            su.Timer.Start();
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
            Controls.Remove(SumUp);
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
                PfOnPaint(g);
            }
            else if (current == Const.SumUp)
            {
                SuOnPaint(g);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (current == Const.PathFinding && pf.WallsHidden) // no need to click anything in this game if walls arent hidden
            {
                PfOnMouseDown(e);
            }
            else if (current == Const.SumUp)
            {
                SuOnMouseDown(e);
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
            if(current == Const.FirstWindow)
            {
                editBox = new TextBox
                {
                    Font = f1
                };
                editBox.SetBounds(UsernameBoxPos.X, UsernameBoxPos.Y, Const.UsernameWidth, Const.UsernameHeight);
                Controls.Add(editBox);
                editBox.Focus();
            }
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
            g.DrawString("Score: " + Games.score.ToString(), bigFont, Brushes.Lavender, UpperLeft.X, UpperLeft.Y);
        }


        #endregion

        // GAME REQUIREMENTS:
        // Each game has to have 5 main things: model (in a separate file), controls for mouse down and onPaint, score eval function, general drawing and a reset function
        // all functions and global variables related to the new game should start with an abbreviation for that game.
        // eg. all Path finding related functions/vars start with Pf
        #region Statistics
        void DrawStatistics(Graphics g)
        {
            ExitButton();
        }
        #endregion

        #region Path Finding View
        // NOTE: x-axis corresponds to j and y-axis corresponds to i
        void PathFindingOnePuzzle(Graphics g)
        {
            pf.Game.userPath = new List<MyPoint>();
            pf.Game.wallsHit = new List<MyPoint>();
            pf.NumOfPuzzlesPlayed += 1;

            // initialize game
            // first half of one sesion will be easier (smaller puzzles), and second half will be harder
            if (pf.NumOfPuzzlesPlayed <= pf.MaxPuzzles / 2) 
            {
                pf.Game = new Game1(random.Next(5, 8));
            }
            else
            {
                pf.Game = new Game1(random.Next(7, 10));
            }
            pf.Game.userPath.Add(pf.Game.graph.start);

            PfDrawGrid(g);

            // draw walls
            PfDrawWalls(g, Brushes.Crimson);

            pf.WallsHidden = false;

            // let the user memorize the placement of the walls
            Program.WaitSec(4);

            // delete walls by redrawing them with different color
            PfDrawWalls(g, WindowBrush);

            pf.WallsHidden = true;

            PfDrawStartAndEnd(g);
        }
        void PfOnMouseDown(MouseEventArgs e)
        {
            MyPoint square = new MyPoint // derermine which square was clicked
            (
                (e.Y - pf.UpperLeft.Y) / pf.OneSquareSize,
                (e.X - pf.UpperLeft.X) / pf.OneSquareSize
            );


            if (PfIsValidSquare(e, square))
            {
                pf.AddSquare = true;
                pf.Game.userPath.Add(square);

                if (pf.Game.graph.grid[square.i,square.j] == 1) // if square is a wall
                {
                    pf.Game.wallsHit.Add(square);
                }

                if (square.IsConnectedTo(pf.Game.graph.end)) // our starting and ending points are connected with a path
                {
                    pf.DrawHitWalls = true;
                    pf.Game.EvalScore();
                    Invalidate();
                    Program.WaitSec(2); // aesthetics
                    if (pf.NumOfPuzzlesPlayed < pf.MaxPuzzles) // repeat if 10 puzzles weren't played
                    {
                        PfReset(pf.NumOfPuzzlesPlayed);
                    }
                    else // if they were, show the score, reset and exit
                    {
                        user.StoreData("Path Finding", Games.score);
                        current = Const.Score;
                        PfReset(0);
                    }
                }
                Invalidate();
            }
        }
        void PfOnPaint(Graphics g)
        {
            if (pf.AddSquare || pf.DrawHitWalls) // if we are continuing on current puzzle
            {
                PfDrawGrid(g);
                PfDrawStartAndEnd(g);
                PfDrawListOfPoints(g, pf.Game.userPath, Brushes.Aquamarine);
                if (pf.DrawHitWalls)
                {
                    PfDrawWalls(g, Brushes.Crimson);
                    PfDrawListOfPoints(g, pf.Game.wallsHit, Brushes.LightSeaGreen);
                    pf.DrawHitWalls = false;
                }
                pf.AddSquare = false;
            }
            else // otherwise we generate a new one
            {
                PathFindingOnePuzzle(g);
            }
        }
        void PfDrawGrid(Graphics g)
        {
            // draw the outline of the square
            pf.DrawnGridSize = pf.OneSquareSize * pf.Game.graph.gridSize;

            pf.UpperLeft = new Point
            {
                X = Center.X - (pf.DrawnGridSize / 2),
                Y = Center.Y - (pf.DrawnGridSize / 2)
            };
            Rectangle rect = new Rectangle(pf.UpperLeft.X, pf.UpperLeft.Y, pf.DrawnGridSize, pf.DrawnGridSize);
            g.DrawRectangle(p, rect);

            // draw the grid inside
            for (int i = 0; i <= pf.DrawnGridSize; i += pf.OneSquareSize)
            {
                Point fromVertical = new Point(pf.UpperLeft.X + i, pf.UpperLeft.Y);
                Point toVertical = new Point(pf.UpperLeft.X + i, pf.UpperLeft.Y + pf.DrawnGridSize);
                g.DrawLine(p, fromVertical, toVertical);

                Point fromHorizontal = new Point(pf.UpperLeft.X, pf.UpperLeft.Y + i);
                Point toHorizontal = new Point(pf.UpperLeft.X + pf.DrawnGridSize, pf.UpperLeft.Y + i);
                g.DrawLine(p, fromHorizontal, toHorizontal);
            }
        }
        void PfDrawWalls(Graphics g, Brush b)
        {
            for (int i = 0; i < pf.Game.graph.gridSize; i++)
            {
                for (int j = 0; j < pf.Game.graph.gridSize; j++)
                {
                    if (pf.Game.graph.grid[i,j] == 1)
                    {
                        Rectangle rect = new Rectangle(pf.UpperLeft.X + (pf.OneSquareSize * j), pf.UpperLeft.Y + (pf.OneSquareSize * i), pf.OneSquareSize, pf.OneSquareSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p, rect);
                    }
                }
            }
        }
        void PfDrawStartAndEnd(Graphics g)
        {
            MyPoint start = pf.Game.graph.start;
            MyPoint end = pf.Game.graph.end;
            Rectangle rect = new Rectangle(pf.UpperLeft.X + (pf.OneSquareSize * start.j), pf.UpperLeft.Y + (pf.OneSquareSize * start.i), pf.OneSquareSize, pf.OneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("S", f1, Brushes.Plum, (pf.UpperLeft.X + (pf.OneSquareSize * start.j)) + pf.OneSquareSize / 2, (pf.UpperLeft.Y + (pf.OneSquareSize * start.i)) + pf.OneSquareSize / 2, format);

            rect = new Rectangle(pf.UpperLeft.X + (pf.OneSquareSize * end.j), pf.UpperLeft.Y + (pf.OneSquareSize * end.i), pf.OneSquareSize, pf.OneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("E", f1, Brushes.Plum, (pf.UpperLeft.X + (pf.OneSquareSize * end.j)) + pf.OneSquareSize / 2, (pf.UpperLeft.Y + (pf.OneSquareSize * end.i)) + pf.OneSquareSize / 2, format);
        }
        void PfDrawListOfPoints(Graphics g, List<MyPoint> l, Brush b)
        {
            Rectangle rect;
            foreach (MyPoint entry in l)
            {
                if (!entry.Equals(pf.Game.graph.start) && !entry.Equals(pf.Game.graph.end))
                {
                    rect = new Rectangle(pf.UpperLeft.X + (pf.OneSquareSize * entry.j), pf.UpperLeft.Y + (pf.OneSquareSize * entry.i), pf.OneSquareSize, pf.OneSquareSize);
                    g.FillRectangle(b, rect);
                    g.DrawRectangle(p, rect);
                }
            }
        }
        bool PfIsValidSquare(MouseEventArgs e, MyPoint square)
        {
            if (PfIsInsideGrid(e) && square.IsConnectedTo(pf.Game.userPath[^1]))
            {
                return true;
            }
            return false;
        }
        bool PfIsInsideGrid(MouseEventArgs e)
        {
            if (e.X > pf.UpperLeft.X && e.X < pf.UpperLeft.X + pf.DrawnGridSize && e.Y > pf.UpperLeft.Y && e.Y < pf.UpperLeft.Y + pf.DrawnGridSize)
            {
                return true;
            }
            return false;
        }
        void PfReset(int n)
        {
            pf.AddSquare = false;
            pf.WallsHidden = false;
            pf.DrawHitWalls = false;

            pf.Game.userPath.Clear();
            pf.Game.wallsHit.Clear();

            pf.NumOfPuzzlesPlayed = n;
        }
        #endregion

        #region Sum Up View
        void SumUpOneRound(Graphics g)
        {
            su.Game = new SumUp(random.Next(15, 35));
            SuDrawNumber(g);
            SuDrawSum(g);
        }
        void SuOnMouseDown(MouseEventArgs e)
        {
            if (SuIsValidSquare(e))
            {
                su.UserSum += su.Game.sum[su.Clicked.X, su.Clicked.Y];
                su.AllClicked.Add(su.Clicked);
                if (su.UserSum == su.Game.number)
                {
                    SumUpView.CorrectAnswers += 1;
                    SuReset(SumUpView.CorrectAnswers);
                }
                else if (su.UserSum > su.Game.number)
                {
                    SuReset(0);
                }
                Invalidate();
            }
        }
        void SuOnPaint(Graphics g)
        {
            int t = 60 - su.Seconds;
            g.DrawString($"Time left: {t}", f1, Brushes.Red, su.NumUpperLeft.X + 600, su.NumUpperLeft.Y - 50, format);
            if (su.initialized && su.UserSum < su.Game.number)
            {
                SuDrawNumber(g);
                SuDrawSum(g);
            }
            else if (!su.initialized)
            {
                su.initialized = true;
                SumUpOneRound(g);
            }
        }
        void OnSuTimerEvent(object sender, EventArgs e)
        {
            if(su.Seconds++ >= su.Duration)
            {
                su.Game.EvalScore();
                current = Const.Score;
                su.Timer.Stop();
                su.Seconds = 0;
                SuReset(0);
            }
            Invalidate();
        }
        Point SuSumUpperLeftInitialValue()
        {
            return new Point
            (
                Center.X - (su.SquareSize * 4 + su.Margin * 3)/2,
                su.NumUpperLeft.Y + su.SquareSize + 100
            );
        }
        void SuDrawNumber(Graphics g)
        {
            su.NumUpperLeft = new Point
           (
               Center.X - su.SquareSize / 2,
               Center.Y - Const.WindowHeight / 4 - su.SquareSize / 2 - 50 // quater of the screen and -50 to make it look nicer
           );
            g.DrawString(su.Game.number.ToString(), bigFont, Brushes.Navy, su.NumUpperLeft.X + su.SquareSize / 2, su.NumUpperLeft.Y + su.SquareSize / 2, format);
        }
        void SuDrawSum(Graphics g)
        {
            su.SumUpperLeft = SuSumUpperLeftInitialValue();
            // colors at random
            Brush[] brushes = new Brush[] { Brushes.CadetBlue, Brushes.Crimson, Brushes.SeaGreen, Brushes.DarkViolet };

            // draw the sum in 4 rows and 3 columns
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (su.AllClicked.Contains(new Point(i, j))) // shade already selected squares
                    {
                        SuDrawOneSquare(g, su.SumUpperLeft, su.Game.sum[i, j], Brushes.LightSteelBlue);

                    }
                    else
                    {
                        SuDrawOneSquare(g, su.SumUpperLeft, su.Game.sum[i, j], brushes[i]);
                    }
                    su.SumUpperLeft.Y += su.SquareSize + su.Margin;
                }
                su.SumUpperLeft.X += su.SquareSize + su.Margin;
                su.SumUpperLeft.Y -= (su.SquareSize + su.Margin) * 3;
            }
            su.SumUpperLeft = SuSumUpperLeftInitialValue();
        }
        void SuDrawOneSquare(Graphics g, Point upperLeft, int i, Brush b)
        {
            Rectangle rect = new Rectangle(upperLeft.X, upperLeft.Y, su.SquareSize, su.SquareSize);
            g.FillRectangle(b, rect);
            g.DrawRectangle(p, rect);
            g.DrawString(i.ToString(), f1, Brushes.Plum, upperLeft.X + su.SquareSize / 2, upperLeft.Y + su.SquareSize / 2, format);
        }
        bool SuIsValidSquare(MouseEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    su.Clicked = new Point(i, j);
                    if (e.X > su.SumUpperLeft.X && e.X < su.SumUpperLeft.X + 120 && e.Y > su.SumUpperLeft.Y && e.Y < su.SumUpperLeft.Y + 120
                        && !su.AllClicked.Contains(su.Clicked))
                    {
                        su.SumUpperLeft = SuSumUpperLeftInitialValue();
                        return true;
                    }
                    su.SumUpperLeft.Y += 150;
                }
                su.SumUpperLeft.X += 150;
                su.SumUpperLeft.Y -= 150 * 3;
            }
            su.SumUpperLeft = SuSumUpperLeftInitialValue();
            return false;
        }
        void SuReset(int n)
        {
            su.UserSum = 0;
            SumUpView.CorrectAnswers = n;
            su.initialized = false;
            su.AllClicked.Clear();
        }
        #endregion
    }
}
