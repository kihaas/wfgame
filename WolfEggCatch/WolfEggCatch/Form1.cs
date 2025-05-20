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

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // ������������� �����
            wolfPictureBox.Image = Properties.Resources.wolf;
            wolfPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            wolfPictureBox.BackColor = Color.Transparent;
            wolfPictureBox.Size = new Size(100, 80);
            wolfPictureBox.Location = new Point(ClientSize.Width / 2, ClientSize.Height - 100);

            // ��������� �������
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // ��������� �����
            scoreLabel.Text = "����: 0";
            scoreLabel.Font = new Font("Arial", 16);
            scoreLabel.ForeColor = Color.Black;
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.BringToFront();

            // �������� �� ������� ����������
            KeyDown += Form1_KeyDown;
            KeyPreview = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            // �������� ���
            if (random.Next(0, 20) == 1)
            {
                CreateEgg();
            }

            // �������� ���
            for (int i = eggsList.Count - 1; i >= 0; i--)
            {
                eggsList[i].Top += speed;

                // �������� ������������
                if (eggsList[i].Bounds.IntersectsWith(wolfPictureBox.Bounds))
                {
                    score++;
                    scoreLabel.Text = $"����: {score}";
                    Controls.Remove(eggsList[i]);
                    eggsList.RemoveAt(i);
                    continue;
                }

                // �������� ������ �� �������
                if (eggsList[i].Top > ClientSize.Height)
                {
                    Controls.Remove(eggsList[i]);
                    eggsList.RemoveAt(i);
                    GameOver();
                }
            }
        }

        private void CreateEgg()
        {
            PictureBox egg = new PictureBox
            {
                Image = Properties.Resources.egg,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Size = new Size(40, 50),
                Location = new Point(random.Next(0, ClientSize.Width - 40), 0)
            };

            eggsList.Add(egg);
            Controls.Add(egg);
            egg.BringToFront();
            scoreLabel.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver) return;

            int moveStep = 20;

            if (e.KeyCode == Keys.Left && wolfPictureBox.Left > 0)
            {
                wolfPictureBox.Left -= moveStep;
            }
            else if (e.KeyCode == Keys.Right && wolfPictureBox.Right < ClientSize.Width)
            {
                wolfPictureBox.Left += moveStep;
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            MessageBox.Show($"���� ��������! ����: {score}", "���� ����� ����");

            // ����� ����
            score = 0;
            scoreLabel.Text = "����: 0";
            foreach (var egg in eggsList)
            {
                Controls.Remove(egg);
            }
            eggsList.Clear();
            isGameOver = false;
            gameTimer.Start();
        }
    }
}