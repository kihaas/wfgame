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
            components = new System.ComponentModel.Container();
            wolfPictureBox = new PictureBox();
            scoreLabel = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)wolfPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            

            wolfPictureBox.Location = new Point(0, 0);
            wolfPictureBox.Margin = new Padding(4, 3, 4, 3);
            wolfPictureBox.Name = "wolfPictureBox";
            wolfPictureBox.Size = new Size(117, 58);
            wolfPictureBox.TabIndex = 0;
            wolfPictureBox.TabStop = false;

 
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(0, 0);
            scoreLabel.Margin = new Padding(4, 0, 4, 0);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(36, 15);
            scoreLabel.TabIndex = 1;
            scoreLabel.Text = "Счет:";


            gameTimer.Interval = 20;

 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 692);
            Controls.Add(scoreLabel);
            Controls.Add(wolfPictureBox);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Волк ловит яйца";
            ((System.ComponentModel.ISupportInitialize)wolfPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.PictureBox wolfPictureBox;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Timer gameTimer;
        private BindingSource bindingSource1;
    }
}