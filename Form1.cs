using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter3
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootMedia;
        WindowsMediaPlayer explosionMedia;

        PictureBox[] stars;
        PictureBox[] munitions;
        PictureBox[] enemies;

        int enemiesSpeed;
        int backgroundSpeed;
        int playerSpeed = 15;
        int munitionSpeed;

        Image enemie1;
        Image enemie2;
        Image enemie3;
        Image boss1;
        Image boss2;
        Image munitionImg;

        Random rnd;

        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            enemie1 = Image.FromFile(Path.Combine(basePath, "assets", "E1.png"));
            enemie2 = Image.FromFile(Path.Combine(basePath, "assets", "E2.png"));
            enemie3 = Image.FromFile(Path.Combine(basePath, "assets", "E3.png"));
            boss1 = Image.FromFile(Path.Combine(basePath, "assets", "Boss1.png"));
            boss2 = Image.FromFile(Path.Combine(basePath, "assets", "Boss2.png"));
            munitionImg = Image.FromFile(Path.Combine(basePath, "assets", "munition.png"));

            backgroundSpeed = 4;
            munitionSpeed = 20;
            enemiesSpeed = 6;
        

            rnd = new Random();

            stars = new PictureBox[10];

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox
                {
                    BorderStyle = BorderStyle.None,
                    Location = new Point(
                        rnd.Next(20, Width),
                        rnd.Next(-Height, Height)
                    ),
                    Size = (i % 2 == 0) ? new Size(3, 3) : new Size(2, 2),
                    BackColor = (i % 2 == 0) ? Color.DarkGray : Color.Wheat
                };

                Controls.Add(stars[i]);
            }

            munitions = new PictureBox[3];

            for (int i = 0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox
                {
                    Size = new Size(8, 8),
                    Image = munitionImg,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None,
                    Visible = false
                };

                Controls.Add(munitions[i]);
            }

            enemies = new PictureBox[10];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new PictureBox
                {
                    Size = new Size(40, 40),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None,
                    Visible = false,
                    Location = new Point((i + 1) * 50, -50)
                };

                Controls.Add(enemies[i]);
            }

            enemies[0].Image = boss1;
            enemies[1].Image = enemie2;
            enemies[2].Image = enemie3;
            enemies[3].Image = enemie3;
            enemies[4].Image = enemie1;
            enemies[5].Image = enemie3;
            enemies[6].Image = enemie2;
            enemies[7].Image = enemie3;
            enemies[8].Image = enemie2;
            enemies[9].Image = boss2;

            gameMedia = new WindowsMediaPlayer();
            shootMedia = new WindowsMediaPlayer();
            explosionMedia = new WindowsMediaPlayer();

            gameMedia.URL = Path.Combine(basePath, "songs", "GameSong.mp3");
            shootMedia.URL = Path.Combine(basePath, "songs", "shoot.mp3");
            explosionMedia.URL = Path.Combine(basePath, "songs", "boom.mp3");

            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootMedia.settings.volume = 10;

            explosionMedia.settings.volume = 6;

            gameMedia.controls.play();
        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            foreach (var star in stars)
            {
                star.Top += backgroundSpeed;
                if (star.Top >= Height)
                    star.Top = -star.Height;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && !LeftMoveTimer.Enabled)
                LeftMoveTimer.Start();

            if (e.KeyCode == Keys.Right && !RightMoveTimer.Enabled)
                RightMoveTimer.Start();

            if (e.KeyCode == Keys.Up && !UpMoveTimer.Enabled)
                UpMoveTimer.Start();

            if (e.KeyCode == Keys.Down && !DownMoveTimer.Enabled)
                DownMoveTimer.Start();

            if (e.KeyCode == Keys.Space)
                Shoot();
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
            if (Player.Right < Width - 10)
                Player.Left += playerSpeed;
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
                Player.Top -= playerSpeed;
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Bottom < Height - 10)
                Player.Top += playerSpeed;
        }

        private void Shoot()
        {
            foreach (var m in munitions)
            {
                if (!m.Visible)
                {
                    m.Location = new Point(
                        Player.Left + Player.Width / 2 - m.Width / 2,
                        Player.Top
                    );

                    m.Visible = true;
                    shootMedia.controls.play();
                    break;
                }
            }
        }

        private void MoveMunitionsTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < munitions.Length; i++)
            {
                if (munitions[i].Visible)
                {
                    munitions[i].Top -= munitionSpeed;
                    Collision();

                    if (munitions[i].Top < 0)
                        munitions[i].Visible = false;
                }
            }
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemiesSpeed);
            Collision();
        }

        private void Collision()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                for (int j = 0; j < munitions.Length; j++)
                {
                    if (munitions[j].Visible &&
                        enemies[i].Visible &&
                        munitions[j].Bounds.IntersectsWith(enemies[i].Bounds))
                    {
                        explosionMedia.controls.play();

                        munitions[j].Visible = false;
                        enemies[i].Location = new Point((i + 1) * 50, -100);
                    }
                }

                if (Player.Bounds.IntersectsWith(enemies[i].Bounds))
                {
                    explosionMedia.settings.volume = 30;
                    explosionMedia.controls.play();

                    Player.Visible = false;
                }
            }
        }

        private void MoveEnemies(PictureBox[] array, int speed)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Visible = true;
                array[i].Top += speed;

                if (array[i].Top > Height)
                {
                    array[i].Location = new Point((i + 1) * 50, -200);
                }
            }
        }
    }
}
