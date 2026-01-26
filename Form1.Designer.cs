namespace SpaceShooter3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
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
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            SuspendLayout();
            


            Player.BackColor = Color.Transparent;
            Player.BackgroundImage = (Image)resources.GetObject("Player.BackgroundImage");
            Player.Location = new Point(226, 356);
            Player.Name = "Player";
            Player.Size = new Size(96, 94);
            Player.TabIndex = 0;
            Player.TabStop = false;
           

            LeftMoveTimer.Tick += LeftMoveTimer_Tick;
            
            RightMoveTimer.Tick += RightMoveTimer_Tick;
            
            UpMoveTimer.Tick += UpMoveTimer_Tick;
            

            DownMoveTimer.Tick += DownMoveTimer_Tick;
           
            MoveBgTimer.Enabled = true;
            MoveBgTimer.Interval = 20;
            MoveBgTimer.Tick += MoveBgTimer_Tick;
           
            MoveMunitionsTimer.Enabled = true;
            MoveMunitionsTimer.Interval = 20;
            MoveMunitionsTimer.Tick += MoveMunitionsTimer_Tick;
            
            MoveEnemiesTimer.Enabled = true;
            MoveEnemiesTimer.Tick += MoveEnemiesTimer_Tick;
           
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(582, 450);
            Controls.Add(Player);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Space Shooter";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ResumeLayout(false);
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
    }
}
