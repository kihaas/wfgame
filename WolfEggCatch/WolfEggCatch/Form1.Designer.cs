namespace WolfEggCatch
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.wolfPictureBox = new System.Windows.Forms.PictureBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wolfPictureBox)).BeginInit();
            this.SuspendLayout();

            // wolfPictureBox
            this.wolfPictureBox.Location = new System.Drawing.Point(0, 0);
            this.wolfPictureBox.Name = "wolfPictureBox";
            this.wolfPictureBox.Size = new System.Drawing.Size(100, 50);
            this.wolfPictureBox.TabIndex = 0;
            this.wolfPictureBox.TabStop = false;

            // scoreLabel
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(0, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(35, 13);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Счет:";

            // gameTimer
            this.gameTimer.Interval = 20;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.wolfPictureBox);
            this.Name = "Form1";
            this.Text = "Волк ловит яйца";
            ((System.ComponentModel.ISupportInitialize)(this.wolfPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.PictureBox wolfPictureBox;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Timer gameTimer;
    }
}