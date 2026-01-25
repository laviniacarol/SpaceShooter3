using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooter3
{
    public partial class Form1 : Form
    {
        PictureBox[] stars;
        int backgroundspeed;
        Random rnd;
        int playerSpeed = 5;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundspeed = 4;
            stars = new PictureBox[10];
            rnd = new Random();

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 500), rnd.Next(-10, 400));

                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkGray;
                }

                this.Controls.Add(stars[i]);
            }

            timer1.Tick += MoveBgTimer_Tick;
            timer1.Start();
        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Top += backgroundspeed;
                if (stars[i].Top >= this.Height)
                    stars[i].Top = -stars[i].Height;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) LeftMoveTimer.Start();
            if (e.KeyCode == Keys.Right) RightMoveTimer.Start();
            if (e.KeyCode == Keys.Up) UpMoveTimer.Start();
            if (e.KeyCode == Keys.Down) DownMoveTimer.Start();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) LeftMoveTimer.Stop();
            if (e.KeyCode == Keys.Right) RightMoveTimer.Stop();
            if (e.KeyCode == Keys.Up) UpMoveTimer.Stop();
            if (e.KeyCode == Keys.Down) DownMoveTimer.Stop();
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10)
                Player.Left -= playerSpeed;
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right < 500)
                Player.Left += playerSpeed;
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top < 400)
                Player.Top += playerSpeed;
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
                Player.Top -= playerSpeed;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
