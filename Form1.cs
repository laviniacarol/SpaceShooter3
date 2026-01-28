using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter3
{
    public partial class Form1 : Form
    {
        // ===== ÁUDIO =====
        WindowsMediaPlayer gameMedia;

        // ===== OBJETOS =====
        PictureBox[] stars;
        PictureBox[] munitions;
        PictureBox[] enemies;
        PictureBox[] enemiesMunition;

        // ===== ESTADO =====
        bool pause;
        bool gameIsOver;

        // ===== CONFIG =====
        int backgroundSpeed = 4;
        int playerSpeed = 15;
        int munitionSpeed = 20;
        int enemiesSpeed = 6;
        int enemiesMunitionSpeed = 6;

        Image enemie1, enemie2, enemie3, boss1, boss2, munitionImg;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = false;
            gameIsOver = false;
            label.Visible = false;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            enemie1 = Image.FromFile(Path.Combine(basePath, "assets", "E1.png"));
            enemie2 = Image.FromFile(Path.Combine(basePath, "assets", "E2.png"));
            enemie3 = Image.FromFile(Path.Combine(basePath, "assets", "E3.png"));
            boss1 = Image.FromFile(Path.Combine(basePath, "assets", "Boss1.png"));
            boss2 = Image.FromFile(Path.Combine(basePath, "assets", "Boss2.png"));
            munitionImg = Image.FromFile(Path.Combine(basePath, "assets", "munition.png"));

            stars = new PictureBox[10];
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox
                {
                    Size = i % 2 == 0 ? new Size(3, 3) : new Size(2, 2),
                    BackColor = Color.Gray,
                    Location = new Point(rnd.Next(0, Width), rnd.Next(-Height, Height))
                };
                Controls.Add(stars[i]);
            }

            munitions = new PictureBox[5];
            for (int i = 0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox
                {
                    Size = new Size(8, 16),
                    Image = munitionImg,
                    SizeMode = PictureBoxSizeMode.Zoom,
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
                    Location = new Point((i + 1) * 50, -rnd.Next(100, 600))
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

            enemiesMunition = new PictureBox[10];
            for (int i = 0; i < enemiesMunition.Length; i++)
            {
                enemiesMunition[i] = new PictureBox
                {
                    Size = new Size(4, 20),
                    BackColor = Color.Yellow,
                    Visible = false
                };
                Controls.Add(enemiesMunition[i]);
            }

            gameMedia = new WindowsMediaPlayer();
            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 15;
            gameMedia.URL = Path.Combine(basePath, "songs", "GameSong.mp3");
            gameMedia.controls.play();

            Player.BringToFront();
            StartTimers();
        }

        private void PlayExplosion()
        {
            var boom = new WindowsMediaPlayer();
            boom.settings.volume = 15;
            boom.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "songs", "boom.mp3");
            boom.controls.play();
        }

        private void PlayShoot()
        {
            var shot = new WindowsMediaPlayer();
            shot.settings.volume = 12;
            shot.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "songs", "shoot.mp3");
            shot.controls.play();
        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            foreach (var s in stars)
            {
                s.Top += backgroundSpeed;
                if (s.Top > Height) s.Top = -s.Height;
            }
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            foreach (var e1 in enemies)
            {
                e1.Top += enemiesSpeed;

                if (e1.Top > Height)
                    e1.Top = -rnd.Next(100, 600);

                if (Player.Visible && e1.Bounds.IntersectsWith(Player.Bounds))
                {
                    Player.Visible = false;
                    PlayExplosion();
                    GameOver("GAME OVER");
                    return;
                }
            }

            if (rnd.Next(0, 100) < 3)
                EnemiesShoot();
        }

        private void MoveMunitionsTimer_Tick(object sender, EventArgs e)
        {
            foreach (var m in munitions)
            {
                if (!m.Visible) continue;

                m.Top -= munitionSpeed;
                if (m.Top < 0)
                {
                    m.Visible = false;
                    continue;
                }

                foreach (var e1 in enemies)
                {
                    if (m.Bounds.IntersectsWith(e1.Bounds))
                    {
                        PlayExplosion();
                        m.Visible = false;
                        e1.Top = -rnd.Next(100, 600);
                        e1.Left = rnd.Next(0, ClientSize.Width - e1.Width);
                        break;
                    }
                }
            }
        }

        private void EnemiesMunitionTimer_Tick(object sender, EventArgs e)
        {
            foreach (var m in enemiesMunition)
            {
                if (!m.Visible) continue;

                m.Top += enemiesMunitionSpeed;

                if (m.Top > Height)
                    m.Visible = false;

                if (Player.Visible && m.Bounds.IntersectsWith(Player.Bounds))
                {
                    m.Visible = false;
                    Player.Visible = false;
                    PlayExplosion();
                    GameOver("GAME OVER");
                }
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 0) Player.Left -= playerSpeed;
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right < ClientSize.Width) Player.Left += playerSpeed;
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 0) Player.Top -= playerSpeed;
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Bottom < ClientSize.Height) Player.Top += playerSpeed;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (pause || gameIsOver) return;

            if (e.KeyCode == Keys.Left) LeftMoveTimer.Start();
            if (e.KeyCode == Keys.Right) RightMoveTimer.Start();
            if (e.KeyCode == Keys.Up) UpMoveTimer.Start();
            if (e.KeyCode == Keys.Down) DownMoveTimer.Start();
            if (e.KeyCode == Keys.Space) Shoot();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            LeftMoveTimer.Stop();
            RightMoveTimer.Stop();
            UpMoveTimer.Stop();
            DownMoveTimer.Stop();
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
                    PlayShoot();
                    break;
                }
            }
        }

        private void EnemiesShoot()
        {
            foreach (var m in enemiesMunition)
            {
                if (!m.Visible)
                {
                    int i = rnd.Next(enemies.Length);
                    m.Location = new Point(
                        enemies[i].Left + enemies[i].Width / 2 - m.Width / 2,
                        enemies[i].Bottom
                    );
                    m.Visible = true;
                    break;
                }
            }
        }

        private void GameOver(string text)
        {
            if (gameIsOver) return;
            gameIsOver = true;

            StopTimers();
            gameMedia.controls.stop();

            label.Text = text;
            CenterLabel();
            label.Visible = true;
            ReplayBtn.Visible = true;
            ExitBtn.Visible = true;
        }

        private void CenterLabel()
        {
            label.Left = (ClientSize.Width - label.Width) / 2;
            label.Top = 100;
        }

        private void StartTimers()
        {
            MoveBgTimer.Start();
            MoveEnemiesTimer.Start();
            MoveMunitionsTimer.Start();
            EnemiesMunitionTimer.Start();
        }

        private void StopTimers()
        {
            MoveBgTimer.Stop();
            MoveEnemiesTimer.Stop();
            MoveMunitionsTimer.Stop();
            EnemiesMunitionTimer.Stop();
        }

        private void ReplayBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
