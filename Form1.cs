using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter3
{
    public partial class Form1 : Form
    {
        // Sons
        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootMedia;
        WindowsMediaPlayer explosionMedia;

        // Objetos do jogo
        PictureBox[] stars;
        PictureBox[] munitions;
        PictureBox[] enemies;
        PictureBox[] enemiesMunition;

        // Variáveis de jogo
        int score;
        int level;
        int dificulty;
        bool pause;
        bool gameIsOver;

        int enemiesSpeed;
        int backgroundSpeed;
        int playerSpeed = 15;
        int munitionSpeed;
        int enemiesMunitionSpeed;

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
            // Label inicializada corretamente
            label.Text = "";
            label.Visible = false;
            label.Location = new Point((this.ClientSize.Width - label.Width) / 2, 150);

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            enemie1 = Image.FromFile(Path.Combine(basePath, "assets", "E1.png"));
            enemie2 = Image.FromFile(Path.Combine(basePath, "assets", "E2.png"));
            enemie3 = Image.FromFile(Path.Combine(basePath, "assets", "E3.png"));
            boss1 = Image.FromFile(Path.Combine(basePath, "assets", "Boss1.png"));
            boss2 = Image.FromFile(Path.Combine(basePath, "assets", "Boss2.png"));
            munitionImg = Image.FromFile(Path.Combine(basePath, "assets", "munition.png"));

            pause = false;
            gameIsOver = false;
            score = 0;
            level = 1;
            dificulty = 9;

            backgroundSpeed = 4;
            munitionSpeed = 20;
            enemiesSpeed = 6;
            enemiesMunitionSpeed = 6;

            rnd = new Random();

            stars = new PictureBox[10];
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox
                {
                    BorderStyle = BorderStyle.None,
                    Location = new Point(rnd.Next(20, Width), rnd.Next(-Height, Height)),
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
                    Visible = true,
                    Location = new Point((i + 1) * 60, -rnd.Next(100, 600))
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
            shootMedia = new WindowsMediaPlayer();
            explosionMedia = new WindowsMediaPlayer();

            gameMedia.URL = Path.Combine(basePath, "songs", "GameSong.mp3");
            shootMedia.URL = Path.Combine(basePath, "songs", "shoot.mp3");
            explosionMedia.URL = Path.Combine(basePath, "songs", "boom.mp3");

            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootMedia.settings.volume = 10;
            explosionMedia.settings.volume = 8;

            gameMedia.controls.play();

            StartTimers();
        }

        // ==================== LABEL ====================
        private void label_Click(object sender, EventArgs e)
        {
            // vazio, não interfere
        }
        // ================================================

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            foreach (var star in stars)
            {
                star.Top += backgroundSpeed;
                if (star.Top >= Height) star.Top = -star.Height;
            }
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemiesSpeed);
            MoveEnemiesMunition();

            if (rnd.Next(0, 100) < 3) EnemiesShoot();

            Collision();
        }

        private void MoveMunitionsTimer_Tick(object sender, EventArgs e)
        {
            foreach (var m in munitions)
            {
                if (m.Visible)
                {
                    m.Top -= munitionSpeed;
                    if (m.Top < 0) m.Visible = false;
                }
            }
        }

        private void EnemiesMunitionTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemiesMunition();
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10) Player.Left -= playerSpeed;
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right < Width - 10) Player.Left += playerSpeed;
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10) Player.Top -= playerSpeed;
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Bottom < Height - 10) Player.Top += playerSpeed;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pause)
            {
                if (e.KeyCode == Keys.Left) LeftMoveTimer.Start();
                if (e.KeyCode == Keys.Right) RightMoveTimer.Start();
                if (e.KeyCode == Keys.Up) UpMoveTimer.Start();
                if (e.KeyCode == Keys.Down) DownMoveTimer.Start();
                if (e.KeyCode == Keys.Space) Shoot();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            LeftMoveTimer.Stop();
            RightMoveTimer.Stop();
            UpMoveTimer.Stop();
            DownMoveTimer.Stop();

            if (e.KeyCode == Keys.P)
            {
                if (!gameIsOver)
                {
                    if (pause)
                    {
                        StartTimers();
                        label.Visible = false;
                        gameMedia.controls.play();
                        pause = false;
                    }
                    else
                    {
                        label.Text = "PAUSED";
                        label.Location = new Point((this.ClientSize.Width - label.Width) / 2, 150);
                        label.Visible = true;
                        gameMedia.controls.pause();
                        StopTimers();
                        pause = true;
                    }
                }
            }
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

        private void EnemiesShoot()
        {
            foreach (var m in enemiesMunition)
            {
                if (!m.Visible)
                {
                    int index = rnd.Next(0, enemies.Length);
                    if (enemies[index].Visible)
                    {
                        m.Location = new Point(
                            enemies[index].Left + enemies[index].Width / 2 - m.Width / 2,
                            enemies[index].Bottom
                        );
                        m.Visible = true;
                        break;
                    }
                }
            }
        }

        private void MoveEnemiesMunition()
        {
            foreach (var m in enemiesMunition)
            {
                if (m.Visible)
                {
                    m.Top += enemiesMunitionSpeed;

                    if (m.Top > Height)
                        m.Visible = false;

                    if (Player.Visible && m.Bounds.IntersectsWith(Player.Bounds))
                    {
                        m.Visible = false;
                        explosionMedia.settings.volume = 30;
                        explosionMedia.controls.play();
                        Player.Visible = false;
                        GameOver("Game Over");
                    }
                }
            }
        }

        private void MoveEnemies(PictureBox[] array, int speed)
        {
            foreach (var e in array)
            {
                e.Top += speed;
                if (e.Top > Height) e.Top = -rnd.Next(100, 600);
            }
        }

        private void Collision()
        {
            foreach (var enemy in enemies)
            {
                foreach (var m in munitions)
                {
                    if (m.Visible && enemy.Visible && m.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        explosionMedia.controls.play();
                        m.Visible = false;
                        enemy.Top = -rnd.Next(200, 600);
                        score += 10;
                    }
                }

                if (enemy.Bounds.IntersectsWith(Player.Bounds))
                {
                    explosionMedia.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

        private void GameOver(string message)
        {
            label.Text = message;
            label.Location = new Point(120, 120);
            label.Visible = true;
            ReplayBtn.Visible = true;
            ExitBtn.Visible = true;

            gameMedia.controls.stop();
            StopTimers();
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

        private void ReplayBtn_Click(object sender, EventArgs e) {
            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(e, e);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
