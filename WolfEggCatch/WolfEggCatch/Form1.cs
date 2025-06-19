using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WolfEggCatch
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private int speed = 5;
        private Random random = new Random();
        private List<PictureBox> eggsList = new List<PictureBox>();
        private bool isGameOver = false;
        private int lives = 3;
        private List<PictureBox> livesDisplay = new List<PictureBox>();
        private Panel gameOverPanel;
        private float wolfPositionX; 
        private float wolfSpeed = 0f;
        private const float MaxSpeed = 15f;
        private const float Acceleration = 0.5f;
        private const float Friction = 0.9f;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; //двойна€ буферизаци€
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            InitializeGame();

            wolfPositionX = wolfPictureBox.Left;
            this.Activated += (s, e) => { if (!isGameOver) this.Focus(); };
        }

        private void UpdateWolfImage()
        {
            if (wolfSpeed < -1f)
            {
                wolfPictureBox.Image = Properties.Resources.wolf_left; 
            }
            else if (wolfSpeed > 1f)
            {
                wolfPictureBox.Image = Properties.Resources.wolf_right; 
            }
            else
            {
                wolfPictureBox.Image = Properties.Resources.wolf; 
            }
        }

        private void InitializeGame()
        {
            this.BackgroundImage = Properties.Resources.backgroundd;
            this.BackgroundImageLayout = ImageLayout.Stretch;


            wolfPictureBox.Image = Properties.Resources.wolf;
            wolfPositionX = ClientSize.Width / 2 - wolfPictureBox.Width / 2;
            wolfPictureBox.Left = (int)wolfPositionX;
            wolfPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            wolfPictureBox.BackColor = Color.Transparent;
            wolfPictureBox.Size = new Size(200, 240);
            wolfPictureBox.Location = new Point(ClientSize.Width / 4, ClientSize.Height - 250);

            scoreLabel.Text = "—чет: 0";
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Regular);
            scoreLabel.ForeColor = Color.White;
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.BringToFront();

            InitializeLivesDisplay();


            gameTimer.Interval = 16; 
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();


            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp; 
            this.KeyPreview = true;
        }

        private void InitializeLivesDisplay()
        {
            
            foreach (var life in livesDisplay)
            {
                Controls.Remove(life);
            }
            livesDisplay.Clear();

            
            for (int i = 0; i < lives; i++)
            {
                var lifeIcon = new PictureBox
                {
                    Image = Properties.Resources.wolf, 
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(40, 32),
                    Location = new Point(ClientSize.Width - 50 - (i * 45), 10),
                    BackColor = Color.Transparent
                };
                livesDisplay.Add(lifeIcon);
                Controls.Add(lifeIcon);
                lifeIcon.BringToFront();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            wolfSpeed *= Friction;

            if (Math.Abs(wolfSpeed) < 0.1f) wolfSpeed = 0f;
            wolfPositionX += wolfSpeed;
            wolfPositionX = Math.Max(0, Math.Min(ClientSize.Width - wolfPictureBox.Width, wolfPositionX));
            wolfPictureBox.Left = (int)wolfPositionX;

            UpdateWolfImage();

            if (random.Next(0, 30 - Math.Min(score / 5, 20)) == 1)
            {
                CreateEgg();
            }

            int moveStep = 3 + Math.Min(score / 20, 7); 

            for (int i = eggsList.Count - 1; i >= 0; i--)
            {
                var egg = eggsList[i];
                egg.Top += moveStep;

                if (egg.Bounds.IntersectsWith(wolfPictureBox.Bounds))
                {
                    score++;
                    scoreLabel.Text = $"—чет: {score}";
                    eggsList.RemoveAt(i);
                    Controls.Remove(egg);
                    continue;
                }

                if (egg.Top > ClientSize.Height)
                {
                    eggsList.RemoveAt(i);
                    Controls.Remove(egg);
                    lives--;
                    InitializeLivesDisplay();

                    if (lives <= 0)
                    {
                        GameOver();
                    }
                }
            }

            this.Invalidate();
        }

        private void CreateEgg()
        {
            PictureBox egg = new PictureBox
            {
                Image = Properties.Resources.egg,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Size = new Size(60, 40),
                Location = new Point(random.Next(20, ClientSize.Width - 80), -40)
            };

            eggsList.Add(egg);
            Controls.Add(egg);
            egg.BringToFront();
            scoreLabel.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                wolfSpeed = -MaxSpeed; 
            }
            else if (e.KeyCode == Keys.Right)
            {
                wolfSpeed = MaxSpeed; 
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                wolfSpeed *= 0.5f; 
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();

            gameOverPanel = new Panel//menu
            {
                Size = new Size(400, 300),
                Location = new Point((ClientSize.Width - 400) / 2, (ClientSize.Height - 300) / 2),
                BackColor = Color.FromArgb(200, 0, 0, 0) 
            };

            var titleLabel = new Label
            {
                Text = "»гра окончена!",
                Font = new Font("Arial", 24, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(90, 30)
            };

            var scoreLabel = new Label
            {
                Text = $"¬аш счет: {score}",
                Font = new Font("Arial", 18),
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(135, 110)
            };

            var restartButton = new Button
            {
                Text = "»грать снова",
                Font = new Font("Arial", 14),
                Size = new Size(200, 50),
                ForeColor = Color.White,
                Location = new Point(100, 180),
                BackColor = Color.Purple,
                FlatStyle = FlatStyle.Flat
            };
            restartButton.Click += (s, e) => RestartGame();

            gameOverPanel.Controls.Add(titleLabel);
            gameOverPanel.Controls.Add(scoreLabel);
            gameOverPanel.Controls.Add(restartButton);
            Controls.Add(gameOverPanel);
            gameOverPanel.BringToFront();
        }

        private void RestartGame()
        {
            Controls.Remove(gameOverPanel);
            gameOverPanel.Dispose();
            gameOverPanel = null;

            score = 0;
            lives = 3;
            scoreLabel.Text = "—чет: 0";

            foreach (var egg in eggsList)
            {
                Controls.Remove(egg);
                egg.Dispose();
            }
            eggsList.Clear();

            InitializeLivesDisplay();

            isGameOver = false;
            gameTimer.Start();

            this.Focus();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                e.Graphics.DrawImage(this.BackgroundImage, this.ClientRectangle);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }
    }
}