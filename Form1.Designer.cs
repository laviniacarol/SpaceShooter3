namespace SpaceShooter3
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        /// name="disposing">
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
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            SuspendLayout();
          
            Player.BackColor = Color.Transparent;
            Player.BackgroundImage = (Image)resources.GetObject("Player.BackgroundImage");
            Player.Location = new Point(226, 356);
            Player.Name = "Player";
            Player.Size = new Size(96, 94);
            Player.TabIndex = 0;
            Player.TabStop = false;
            Player.Click += pictureBox1_Click;
         
            LeftMoveTimer.Tick += LeftMoveTimer_Tick;
           
            RightMoveTimer.Tick += RightMoveTimer_Tick;
           
            UpMoveTimer.Tick += UpMoveTimer_Tick;
           
            
            DownMoveTimer.Tick += DownMoveTimer_Tick;
         
            timer1.Tick += MoveBgTimer;
        
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(582, 450);
            Controls.Add(Player);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(600, 500);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Player;
        private System.Windows.Forms.Timer LeftMoveTimer;
        private System.Windows.Forms.Timer RightMoveTimer;
        private System.Windows.Forms.Timer UpMoveTimer;
        private System.Windows.Forms.Timer DownMoveTimer;
        private System.Windows.Forms.Timer timer1;
    }
}
