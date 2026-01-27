namespace SpaceShooter3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Player = new PictureBox();
            LeftMoveTimer = new System.Windows.Forms.Timer(components);
            RightMoveTimer = new System.Windows.Forms.Timer(components);
            UpMoveTimer = new System.Windows.Forms.Timer(components);
            DownMoveTimer = new System.Windows.Forms.Timer(components);
            MoveBgTimer = new System.Windows.Forms.Timer(components);
            MoveMunitionsTimer = new System.Windows.Forms.Timer(components);
            MoveEnemiesTimer = new System.Windows.Forms.Timer(components);
            EnemiesMunitionTimer = new System.Windows.Forms.Timer(components);
            ReplayBtn = new Button();
            ExitBtn = new Button();
            label = new Label();
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            SuspendLayout();
            // 
            // Player
            // 
            Player.BackColor = Color.Transparent;
            Player.BackgroundImage = (Image)resources.GetObject("Player.BackgroundImage");
            Player.Location = new Point(226, 356);
            Player.Name = "Player";
            Player.Size = new Size(96, 94);
            Player.TabIndex = 0;
            Player.TabStop = false;
            // 
            // LeftMoveTimer
            // 
            LeftMoveTimer.Tick += LeftMoveTimer_Tick;
            // 
            // RightMoveTimer
            // 
            RightMoveTimer.Tick += RightMoveTimer_Tick;
            // 
            // UpMoveTimer
            // 
            UpMoveTimer.Tick += UpMoveTimer_Tick;
            // 
            // DownMoveTimer
            // 
            DownMoveTimer.Tick += DownMoveTimer_Tick;
            // 
            // MoveBgTimer
            // 
            MoveBgTimer.Enabled = true;
            MoveBgTimer.Interval = 20;
            MoveBgTimer.Tick += MoveBgTimer_Tick;
            // 
            // MoveMunitionsTimer
            // 
            MoveMunitionsTimer.Enabled = true;
            MoveMunitionsTimer.Interval = 20;
            MoveMunitionsTimer.Tick += MoveMunitionsTimer_Tick;
            // 
            // MoveEnemiesTimer
            // 
            MoveEnemiesTimer.Enabled = true;
            MoveEnemiesTimer.Tick += MoveEnemiesTimer_Tick;
            // 
            // EnemiesMunitionTimer
            // 
            EnemiesMunitionTimer.Enabled = true;
            EnemiesMunitionTimer.Interval = 20;
            EnemiesMunitionTimer.Tick += EnemiesMunitionTimer_Tick;
            // 
            // ReplayBtn
            // 
            ReplayBtn.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ReplayBtn.Location = new Point(184, 165);
            ReplayBtn.Name = "ReplayBtn";
            ReplayBtn.Size = new Size(191, 50);
            ReplayBtn.TabIndex = 1;
            ReplayBtn.Text = "Play Again";
            ReplayBtn.UseVisualStyleBackColor = true;
            ReplayBtn.Visible = false;
            ReplayBtn.Click += ReplayBtn_Click;
            // 
            // ExitBtn
            // 
            ExitBtn.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExitBtn.Location = new Point(184, 247);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.Size = new Size(191, 50);
            ExitBtn.TabIndex = 2;
            ExitBtn.Text = "Exit";
            ExitBtn.UseVisualStyleBackColor = true;
            ExitBtn.Visible = false;
            ExitBtn.Click += ExitBtn_Click;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("New York Escape", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label.ForeColor = Color.Snow;
            label.Location = new Point(282, 150);
            label.Name = "label";
            label.Size = new Size(0, 36);
            label.TabIndex = 5;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(582, 450);
            Controls.Add(label);
            Controls.Add(ExitBtn);
            Controls.Add(ReplayBtn);
            Controls.Add(Player);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Space Shooter";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Timer LeftMoveTimer;
        private System.Windows.Forms.Timer RightMoveTimer;
        private System.Windows.Forms.Timer UpMoveTimer;
        private System.Windows.Forms.Timer DownMoveTimer;
        private System.Windows.Forms.Timer MoveBgTimer;
        private System.Windows.Forms.Timer MoveMunitionsTimer;
        private System.Windows.Forms.Timer MoveEnemiesTimer;
        private System.Windows.Forms.Timer EnemiesMunitionTimer;
        private Button ReplayBtn;
        private Button ExitBtn;
        private Label label1;
        private Label label2;
        private Label label;
    }
}
