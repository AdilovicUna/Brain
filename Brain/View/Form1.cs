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
        Button SumUp;
        Button ColorRead;
        Button Exit;
        #endregion

        #region General global variables
        readonly Color WindowColor = Color.Thistle;
        readonly Brush WindowBrush = Brushes.Thistle;
        readonly Pen p = new Pen(Color.Black, 2);
        readonly Font f1 = new Font("Times New Roman", 30);
        readonly Font f2 = new Font("Times New Roman", 20);
        readonly Font bigFont = new Font("Times New Roman", 100);
        readonly StringFormat format = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };

        readonly Random r = new Random();

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
        readonly int pfOneSquareSize = 70;
        readonly int pfMaxPuzzles = 1;

        Point pfUpperLeft;
        int pfDrawnGridSize;

        bool pfAddSquare = false;
        bool pfWwallsHidden = false;
        bool pfDrawHitWalls = false;

        int pfNumOfPuzzlesPlayed = 0;
        #endregion

        #region Sum Up global variables
        SumUp suGame = new SumUp(1);
        readonly int suSquareSize = 120;
        readonly int suMargin = 30;
        int suUserSum = 0;
        Point suNumUpperLeft;
        Point suSumUpperLeft;

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
            if (current == Const.PathFinding && pfWwallsHidden) // no need to click anything in this game if walls arent hidden
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
            pfGame.userPath = new List<MyPoint>();
            pfGame.wallsHit = new List<MyPoint>();
            pfNumOfPuzzlesPlayed += 1;

            // initialize game
            // first half of one sesion will be easier (smaller puzzles), and second half will be harder
            if (pfNumOfPuzzlesPlayed <= pfMaxPuzzles / 2) 
            {
                pfGame = new Game1(r.Next(5, 8));
            }
            else
            {
                pfGame = new Game1(r.Next(7, 10));
            }
            pfGame.userPath.Add(pfGame.graph.start);

            PfDrawGrid(g);

            // draw walls
            PfDrawWalls(g, Brushes.Crimson);

            pfWwallsHidden = false;

            // let the user memorize the placement of the walls
            Program.WaitSec(4);

            // delete walls by redrawing them with different color
            PfDrawWalls(g, WindowBrush);

            pfWwallsHidden = true;

            PfDrawStartAndEnd(g);
        }
        void PfOnMouseDown(MouseEventArgs e)
        {
            MyPoint square = new MyPoint // derermine which square was clicked
            (
                (e.Y - pfUpperLeft.Y) / pfOneSquareSize,
                (e.X - pfUpperLeft.X) / pfOneSquareSize
            );


            if (PfIsValidSquare(e, square))
            {
                pfAddSquare = true;
                pfGame.userPath.Add(square);

                if (pfGame.graph.grid[square.i,square.j] == 1) // if square is a wall
                {
                    pfGame.wallsHit.Add(square);
                }

                if (square.IsConnectedTo(pfGame.graph.end)) // our starting and ending points are connected with a path
                {
                    pfDrawHitWalls = true;
                    pfGame.EvalScore();
                    Invalidate();
                    Program.WaitSec(2); // aesthetics
                    if (pfNumOfPuzzlesPlayed < pfMaxPuzzles) // repeat if 10 puzzles weren't played
                    {
                        PfReset(pfNumOfPuzzlesPlayed);
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
            if (pfAddSquare || pfDrawHitWalls) // if we are continuing on current puzzle
            {
                PfDrawGrid(g);
                PfDrawStartAndEnd(g);
                PfDrawListOfPoints(g, pfGame.userPath, Brushes.Aquamarine);
                if (pfDrawHitWalls)
                {
                    PfDrawWalls(g, Brushes.Crimson);
                    PfDrawListOfPoints(g, pfGame.wallsHit, Brushes.LightSeaGreen);
                    pfDrawHitWalls = false;
                }
                pfAddSquare = false;
            }
            else // otherwise we generate a new one
            {
                PathFindingOnePuzzle(g);
            }
        }
        void PfDrawGrid(Graphics g)
        {
            // draw the outline of the square
            pfDrawnGridSize = pfOneSquareSize * pfGame.graph.gridSize;

            pfUpperLeft = new Point
            {
                X = Center.X - (pfDrawnGridSize / 2),
                Y = Center.Y - (pfDrawnGridSize / 2)
            };
            Rectangle rect = new Rectangle(pfUpperLeft.X, pfUpperLeft.Y, pfDrawnGridSize, pfDrawnGridSize);
            g.DrawRectangle(p, rect);

            // draw the grid inside
            for (int i = 0; i <= pfDrawnGridSize; i += pfOneSquareSize)
            {
                Point fromVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y);
                Point toVertical = new Point(pfUpperLeft.X + i, pfUpperLeft.Y + pfDrawnGridSize);
                g.DrawLine(p, fromVertical, toVertical);

                Point fromHorizontal = new Point(pfUpperLeft.X, pfUpperLeft.Y + i);
                Point toHorizontal = new Point(pfUpperLeft.X + pfDrawnGridSize, pfUpperLeft.Y + i);
                g.DrawLine(p, fromHorizontal, toHorizontal);
            }
        }
        void PfDrawWalls(Graphics g, Brush b)
        {
            for (int i = 0; i < pfGame.graph.gridSize; i++)
            {
                for (int j = 0; j < pfGame.graph.gridSize; j++)
                {
                    if (pfGame.graph.grid[i,j] == 1)
                    {
                        Rectangle rect = new Rectangle(pfUpperLeft.X + (pfOneSquareSize * j), pfUpperLeft.Y + (pfOneSquareSize * i), pfOneSquareSize, pfOneSquareSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p, rect);
                    }
                }
            }
        }
        void PfDrawStartAndEnd(Graphics g)
        {
            MyPoint start = pfGame.graph.start;
            MyPoint end = pfGame.graph.end;
            Rectangle rect = new Rectangle(pfUpperLeft.X + (pfOneSquareSize * start.j), pfUpperLeft.Y + (pfOneSquareSize * start.i), pfOneSquareSize, pfOneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("S", f1, Brushes.Plum, (pfUpperLeft.X + (pfOneSquareSize * start.j)) + pfOneSquareSize / 2, (pfUpperLeft.Y + (pfOneSquareSize * start.i)) + pfOneSquareSize / 2, format);

            rect = new Rectangle(pfUpperLeft.X + (pfOneSquareSize * end.j), pfUpperLeft.Y + (pfOneSquareSize * end.i), pfOneSquareSize, pfOneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(p, rect);
            g.DrawString("E", f1, Brushes.Plum, (pfUpperLeft.X + (pfOneSquareSize * end.j)) + pfOneSquareSize / 2, (pfUpperLeft.Y + (pfOneSquareSize * end.i)) + pfOneSquareSize / 2, format);
        }
        void PfDrawListOfPoints(Graphics g, List<MyPoint> l, Brush b)
        {
            Rectangle rect;
            foreach (MyPoint entry in l)
            {
                if (!entry.Equals(pfGame.graph.start) && !entry.Equals(pfGame.graph.end))
                {
                    rect = new Rectangle(pfUpperLeft.X + (pfOneSquareSize * entry.j), pfUpperLeft.Y + (pfOneSquareSize * entry.i), pfOneSquareSize, pfOneSquareSize);
                    g.FillRectangle(b, rect);
                    g.DrawRectangle(p, rect);
                }
            }
        }
        bool PfIsValidSquare(MouseEventArgs e, MyPoint square)
        {
            if (PfIsInsideGrid(e) && square.IsConnectedTo(pfGame.userPath[^1]))
            {
                return true;
            }
            return false;
        }
        bool PfIsInsideGrid(MouseEventArgs e)
        {
            if (e.X > pfUpperLeft.X && e.X < pfUpperLeft.X + pfDrawnGridSize && e.Y > pfUpperLeft.Y && e.Y < pfUpperLeft.Y + pfDrawnGridSize)
            {
                return true;
            }
            return false;
        }
        void PfReset(int n)
        {
            pfAddSquare = false;
            pfWwallsHidden = false;
            pfDrawHitWalls = false;

            pfGame.userPath.Clear();
            pfGame.wallsHit.Clear();

            pfNumOfPuzzlesPlayed = n;
        }
        #endregion

        #region Sum Up View
        void SumUpOneRound(Graphics g)
        {
            suGame = new SumUp(r.Next(15, 35));

            SuDrawNumber(g);

            SuDrawSum(g);
            
            suSumUpperLeft = SuSumUpperLeftInitialValue();
        }
        void SuOnMouseDown(MouseEventArgs e)
        {
            if (SuIsValidSquare(e, out (int, int) pos))
            {
                suUserSum += suGame.sum[pos.Item1, pos.Item2];
                Invalidate();
            }
        }
        void SuOnPaint(Graphics g)
        {
            if (suUserSum == 0)
            {
                SumUpOneRound(g);
            }
            else if(suUserSum == suGame.number)
            {
                suGame.correctAnswers += 1;
                SumUpOneRound(g);
            }
            else if (suUserSum > suGame.number)
            {
                //SumUpOneRound(g);
                user.StoreData("Sum up", Games.score);
                current = Const.Score;
                SuReset();
            }
            else
            {
                SuDrawNumber(g);
                SuDrawSum(g);
            }
        }
        Point SuSumUpperLeftInitialValue()
        {
            return new Point
            (
                Center.X - (suSquareSize * 4 + suMargin * 3)/2,
                suNumUpperLeft.Y + suSquareSize + 100
            );
        }
        void SuDrawNumber(Graphics g)
        {
            suNumUpperLeft = new Point
           (
               Center.X - suSquareSize / 2,
               Center.Y - Const.WindowHeight / 4 - suSquareSize / 2 - 50 // quater of the screen and -50 to make it look nicer
           );
            g.DrawString(suGame.number.ToString(), bigFont, Brushes.Navy, suNumUpperLeft.X + suSquareSize / 2, suNumUpperLeft.Y + suSquareSize / 2, format);
        }
        void SuDrawSum(Graphics g)
        {
            suSumUpperLeft = SuSumUpperLeftInitialValue();

            // colors at random
            Brush[] brushes = new Brush[] { Brushes.CadetBlue, Brushes.Crimson, Brushes.SeaGreen, Brushes.DarkViolet };

            // draw the sum in 4 rows and 3 columns
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    SuDrawOneSquare(g, suSumUpperLeft, suGame.sum[i, j], brushes[i]);
                    suSumUpperLeft.Y += suSquareSize + suMargin;
                }
                suSumUpperLeft.X += suSquareSize + suMargin;
                suSumUpperLeft.Y -= (suSquareSize + suMargin) * 3;
            }
        }
        void SuDrawOneSquare(Graphics g, Point upperLeft, int i, Brush b)
        {
            Rectangle rect = new Rectangle(upperLeft.X, upperLeft.Y, suSquareSize, suSquareSize);
            g.FillRectangle(b, rect);
            g.DrawRectangle(p, rect);
            g.DrawString(i.ToString(), f1, Brushes.Plum, upperLeft.X + suSquareSize / 2, upperLeft.Y + suSquareSize / 2, format);
        }
        bool SuIsValidSquare(MouseEventArgs e, out (int,int) pos)
        {
            pos = (-1, -1);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (e.X > suSumUpperLeft.X && e.X < suSumUpperLeft.X + 120 && e.Y > suSumUpperLeft.Y && e.Y < suSumUpperLeft.Y + 120)
                    {
                        pos = (i, j);
                        suSumUpperLeft = SuSumUpperLeftInitialValue();
                        return true;
                    }
                    suSumUpperLeft.Y += 150;
                }
                suSumUpperLeft.X += 150;
                suSumUpperLeft.Y -= 150 * 3;
            }
            suSumUpperLeft = SuSumUpperLeftInitialValue();
            return false;
        }
        void SuReset()
        {
            suUserSum = 0;
            suGame.correctAnswers = 0;
        }
        #endregion
    }
}
