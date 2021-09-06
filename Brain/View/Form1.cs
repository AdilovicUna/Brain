using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ViewConstants;
using Brain.Model;

namespace Brain
{
    public partial class Form1 : Form
    {
        readonly PathFindingView pathFinding = new PathFindingView();
        readonly SumUpView sumUp = new SumUpView();
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
            sumUp.Timer.Tick += new EventHandler(OnSuTimerEvent);
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
            sumUp.Timer.Start();
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
                pathFinding.OnPaint(g);
            }
            else if (current == Const.SumUp)
            {
                sumUp.OnPaint(g);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (current == Const.PathFinding && pathFinding.WallsHidden) // no need to click anything in this game if walls arent hidden
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

        #region Statistics
        void DrawStatistics(Graphics g)
        {
            ExitButton();
        }
        #endregion

        // GAME REQUIREMENTS:
        // Each game has to have 5 main things: model (in a separate file), controls for mouse down and onPaint, score eval function, general drawing and a reset function
        // all functions and global variables related to the new game should start with an abbreviation for that game.
        // eg. all Path finding related functions/vars start with Pf

        #region Path Finding View
        void PfOnMouseDown(MouseEventArgs e)
        {
            MyPoint square = new MyPoint // derermine which square was clicked
            (
                (e.Y - pathFinding.UpperLeft.Y) / pathFinding.OneSquareSize,
                (e.X - pathFinding.UpperLeft.X) / pathFinding.OneSquareSize
            );


            if (pathFinding.IsValidSquare(e, square))
            {
                pathFinding.AddSquare = true;
                pathFinding.Game.userPath.Add(square);

                if (pathFinding.Game.graph.grid[square.i, square.j] == 1) // if square is a wall
                {
                    pathFinding.Game.wallsHit.Add(square);
                }

                if (square.IsConnectedTo(pathFinding.Game.graph.end)) // our starting and ending points are connected with a path
                {
                    pathFinding.DrawHitWalls = true;
                    pathFinding.Game.EvalScore();
                    Invalidate();
                    Program.WaitSec(2); // aesthetics
                    if (pathFinding.NumOfPuzzlesPlayed < pathFinding.MaxPuzzles) // repeat if 10 puzzles weren't played
                    {
                        pathFinding.Reset(pathFinding.NumOfPuzzlesPlayed);
                    }
                    else // if they were, show the score, reset and exit
                    {
                        user.StoreData("Path Finding", Games.score);
                        current = Const.Score;
                        pathFinding.Reset(0);
                    }
                }
                Invalidate();
            }
        }
        #endregion

        #region Sum Up
        void SuOnMouseDown(MouseEventArgs e)
        {
            if (sumUp.IsValidSquare(e))
            {
                sumUp.UserSum += sumUp.Game.sum[sumUp.Clicked.X, sumUp.Clicked.Y];
                sumUp.AllClicked.Add(sumUp.Clicked);
                if (sumUp.UserSum == sumUp.Game.number)
                {
                    SumUpView.CorrectAnswers += 1;
                    sumUp.Reset(SumUpView.CorrectAnswers);
                }
                else if (sumUp.UserSum > sumUp.Game.number)
                {
                    sumUp.Reset(0);
                }
                Invalidate();
            }
        }

        void OnSuTimerEvent(object sender, EventArgs e)
        {
            if (sumUp.Seconds++ >= sumUp.Duration)
            {
                sumUp.Game.EvalScore();
                current = Const.Score;
                sumUp.Timer.Stop();
                sumUp.Seconds = 0;
                sumUp.Reset(0);
            }
            Invalidate();
        }
        #endregion
    }
}
