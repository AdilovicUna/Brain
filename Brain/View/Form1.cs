using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ViewConstants;

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
        Button Exit;
        #endregion

        #region General global variables
        Point Center = new Point
        {
            X = Const.WindowWidth / 2,
            Y = Const.WindowHeight / 2
        };
        int currentGame = Const.FirstWindow;
        bool newUser = false;
        int score = 0;
        #endregion

        #region Path Finding global variables
        Game1 pfGame = new Game1(6);
        readonly int oneSquareSize = 70;

        Point pfUpperLeft;
        int drawnGridSize;

        bool addSquare = false;
        bool wallsHidden = false;
        bool drawHitWalls = false;

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
            Play.Click += new EventHandler(MainMenu);
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
        public void ExitButton()
        {
            Exit = CreateButton("Exit", MConst.Statistics(), Const.ExitWidth, Const.ExitHeight, MConst.WindowColor(), MConst.StatisticsColor(), MConst.PlayFont());
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
            Controls.Remove(NewUser);
            Controls.Remove(ExistingUser);
            Controls.Remove(Play);
            Controls.Remove(Exit);
            //create Statistics button
            Statistics = CreateButton("Statistics", MConst.Statistics(), Const.StatisticsWidth, Const.StatisticsHeight, MConst.WindowColor(), MConst.StatisticsColor(), MConst.PlayFont());
            Statistics.Click += new EventHandler(MainMenu);
            Controls.Add(Statistics);
            //create Path finding button
            PathFinding = CreateButton("Path Finding", MConst.Game1(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.MemoryColor(), MConst.PlayFont());
            PathFinding.Click += new EventHandler(OnPathFindingClick);
            Controls.Add(PathFinding);
            //create Partial match button
            PartialMatch = CreateButton("Partial Match", MConst.Game2(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.MemoryColor(), MConst.PlayFont());
            PartialMatch.Click += new EventHandler(MainMenu);
            Controls.Add(PartialMatch);
            //create Nanogram button
            Nanogram = CreateButton("Nanogram", MConst.Game3(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            Nanogram.Click += new EventHandler(MainMenu);
            Controls.Add(Nanogram);
            //create Low to High button
            LowToHigh = CreateButton("Low To High", MConst.Game4(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.ProblemSolvingColor(), MConst.PlayFont());
            LowToHigh.Click += new EventHandler(MainMenu);
            Controls.Add(LowToHigh);
            //create Sort button
            Sort = CreateButton("Sort", MConst.Game5(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.FocusColor(), MConst.PlayFont());
            Sort.Click += new EventHandler(MainMenu);
            Controls.Add(Sort);
            //create Color read button
            ColorRead = CreateButton("Color Read", MConst.Game6(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.FocusColor(), MConst.PlayFont());
            ColorRead.Click += new EventHandler(MainMenu);
            Controls.Add(ColorRead);
            //create Word hunt button
            WordHunt = CreateButton("Word Hunt", MConst.Game7(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.LanguageColor(), MConst.PlayFont());
            WordHunt.Click += new EventHandler(MainMenu);
            Controls.Add(WordHunt);
            //create Typing button
            Typing = CreateButton("Typing", MConst.Game8(), Const.GameSquareWidth, Const.GameSquareHeight, MConst.MainMenuColor(), MConst.LanguageColor(), MConst.PlayFont());
            Typing.Click += new EventHandler(MainMenu);
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
            else if(currentGame == Const.Score)
            {
                DrawScore(g);
                ExitButton();
                score = 0;
            }
            else if (currentGame == Const.PathFinding)
            {
                if (addSquare || drawHitWalls)
                {
                    DrawGrid(g);
                    DrawStartAndEnd(g);
                    DrawListOfPoints(g,userPath, Brushes.Aquamarine);
                    if (drawHitWalls)
                    {
                        DrawWalls(g, Brushes.Crimson);
                        DrawListOfPoints(g, wallsHit, Brushes.Coral);
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
            if (currentGame == Const.PathFinding && wallsHidden) 
            {
                MyPoint square = new MyPoint
                (
                    (e.Y - pfUpperLeft.Y) / oneSquareSize,
                    (e.X - pfUpperLeft.X) / oneSquareSize
                );
                if (IsValidSquare(e, square))
                {
                    addSquare = true;
                    userPath.Add(square);

                    if (pfGame.graph.grid[square.i][square.j] == 1) // if square is a wall
                    {
                        wallsHit.Add(square);
                    }

                    if (square.IsConnectedTo(pfGame.graph.end))
                    {
                        drawHitWalls = true;
                        EvalPfScore();
                        Invalidate();
                        Program.WaitSec(2);
                        if (numOfPuzzlesPlayed < 10)
                        {
                            ResetPF(numOfPuzzlesPlayed);
                        }
                        else
                        {
                            currentGame = Const.Score;
                            ResetPF(0);
                        }
                    }
                    Invalidate();
                }
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
        #region Score
        void DrawScore(Graphics g)
        {
            Point UpperLeft = new Point
            {
                X = Center.X - (500 / 2) - 100,
                Y = Center.Y - (500 / 2) + 100
            };
            Font font = new Font("Times New Roman", 100);
            g.DrawString("Score: " + score.ToString(), font, Brushes.Lavender, UpperLeft.X, UpperLeft.Y);
        }
        void EvalPfScore()
        {
            if (wallsHit.Count == 0)
            {
                score += pfGame.gridSize * 100;
            }
            score += (userPath.Count - wallsHit.Count) * 25;
        }
        #endregion
        #region Path Finding View
        // NOTE: x-axis corresponds to j and y-axis corresponds to i
        void PathFindingOnePuzzle(Graphics g)
        {
            Random r = new Random();
            userPath = new List<MyPoint>();
            wallsHit = new List<MyPoint>();
            numOfPuzzlesPlayed += 1;

            // initialize game
            pfGame = new Game1(r.Next(6, 9));
            userPath.Add(pfGame.graph.start);

            DrawGrid(g);

            // draw walls
            DrawWalls(g, Brushes.Crimson);

            wallsHidden = false;

            // let the user memorize the placement of the walls
            Program.WaitSec(4);

            // delete walls by redrawing them with different color
            DrawWalls(g, Brushes.Thistle);

            wallsHidden = true;

            DrawStartAndEnd(g);
        }
        void DrawGrid(Graphics g)
        {
            // draw the outline of the square
            drawnGridSize = oneSquareSize * pfGame.gridSize;

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
            for (int i = 0; i < pfGame.gridSize; i++)
            {
                for (int j = 0; j < pfGame.gridSize; j++)
                {
                    if (pfGame.graph.grid[i][j] == 1)
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
            MyPoint start = pfGame.graph.start;
            MyPoint end = pfGame.graph.end;
            Rectangle rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * start.j), pfUpperLeft.Y + (oneSquareSize * start.i), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(MConst.Pen2(), rect);
            g.DrawString("S", MConst.Font(), Brushes.Black, pfUpperLeft.X + (oneSquareSize * start.j), pfUpperLeft.Y + (oneSquareSize * start.i));

            rect = new Rectangle(pfUpperLeft.X + (oneSquareSize * end.j), pfUpperLeft.Y + (oneSquareSize * end.i), oneSquareSize, oneSquareSize);
            g.FillRectangle(Brushes.PapayaWhip, rect);
            g.DrawRectangle(MConst.Pen2(), rect);
            g.DrawString("E", MConst.Font(), Brushes.Black, pfUpperLeft.X + (oneSquareSize * end.j), pfUpperLeft.Y + (oneSquareSize * end.i));
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
                    g.DrawRectangle(MConst.Pen2(), rect);
                }
            }
        }
        bool IsValidSquare(MouseEventArgs e, MyPoint square)
        {
            if (IsInsideGrid(e) && square.IsConnectedTo(userPath[^1]))
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
        void ResetPF(int n)
        {
            addSquare = false;
            wallsHidden = false;
            drawHitWalls = false;

            userPath.Clear();
            wallsHit.Clear();

            numOfPuzzlesPlayed = n;
        }
        #endregion
    }
}
