
namespace BlackjackGUI
{
    partial class BlackjackApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hitButton = new System.Windows.Forms.Button();
            this.standButton = new System.Windows.Forms.Button();
            this.doubleButton = new System.Windows.Forms.Button();
            this.splitButton = new System.Windows.Forms.Button();
            this.surrenderButton = new System.Windows.Forms.Button();
            this.betPicker = new System.Windows.Forms.NumericUpDown();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dealerView = new global::BlackjackGUI.BlackjackPlayerView();
            this.playerView = new global::BlackjackGUI.BlackjackPlayerView();
            this.betLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.betPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // hitButton
            // 
            this.hitButton.Location = new System.Drawing.Point(48, 81);
            this.hitButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hitButton.Name = "hitButton";
            this.hitButton.Size = new System.Drawing.Size(136, 44);
            this.hitButton.TabIndex = 6;
            this.hitButton.Text = "Hit";
            this.hitButton.UseVisualStyleBackColor = true;
            this.hitButton.Click += new System.EventHandler(this.hitButton_Click);
            // 
            // standButton
            // 
            this.standButton.Location = new System.Drawing.Point(48, 133);
            this.standButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.standButton.Name = "standButton";
            this.standButton.Size = new System.Drawing.Size(136, 47);
            this.standButton.TabIndex = 7;
            this.standButton.Text = "Stand";
            this.standButton.UseVisualStyleBackColor = true;
            // 
            // doubleButton
            // 
            this.doubleButton.Location = new System.Drawing.Point(47, 188);
            this.doubleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.doubleButton.Name = "doubleButton";
            this.doubleButton.Size = new System.Drawing.Size(137, 45);
            this.doubleButton.TabIndex = 8;
            this.doubleButton.Text = "Double";
            this.doubleButton.UseVisualStyleBackColor = true;
            // 
            // splitButton
            // 
            this.splitButton.Location = new System.Drawing.Point(48, 241);
            this.splitButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitButton.Name = "splitButton";
            this.splitButton.Size = new System.Drawing.Size(136, 45);
            this.splitButton.TabIndex = 9;
            this.splitButton.Text = "Split";
            this.splitButton.UseVisualStyleBackColor = true;
            // 
            // surrenderButton
            // 
            this.surrenderButton.Location = new System.Drawing.Point(48, 295);
            this.surrenderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(136, 48);
            this.surrenderButton.TabIndex = 10;
            this.surrenderButton.Text = "Surrender";
            this.surrenderButton.UseVisualStyleBackColor = true;
            // 
            // betPicker
            // 
            this.betPicker.Location = new System.Drawing.Point(99, 439);
            this.betPicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.betPicker.Name = "betPicker";
            this.betPicker.Size = new System.Drawing.Size(86, 27);
            this.betPicker.TabIndex = 11;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(729, 27);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(118, 600);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // dealerView
            // 
            this.dealerView.Location = new System.Drawing.Point(312, 3);
            this.dealerView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dealerView.Name = "dealerView";
            this.dealerView.Size = new System.Drawing.Size(340, 299);
            this.dealerView.TabIndex = 16;
            // 
            // playerView
            // 
            this.playerView.Location = new System.Drawing.Point(312, 335);
            this.playerView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.playerView.Name = "playerView";
            this.playerView.Size = new System.Drawing.Size(340, 293);
            this.playerView.TabIndex = 17;
            // 
            // betLabel
            // 
            this.betLabel.AutoSize = true;
            this.betLabel.Location = new System.Drawing.Point(48, 441);
            this.betLabel.Name = "betLabel";
            this.betLabel.Size = new System.Drawing.Size(34, 20);
            this.betLabel.TabIndex = 18;
            this.betLabel.Text = "Bet:";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(49, 517);
            this.playButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(136, 97);
            this.playButton.TabIndex = 19;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 661);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.betLabel);
            this.Controls.Add(this.playerView);
            this.Controls.Add(this.dealerView);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.betPicker);
            this.Controls.Add(this.surrenderButton);
            this.Controls.Add(this.splitButton);
            this.Controls.Add(this.doubleButton);
            this.Controls.Add(this.standButton);
            this.Controls.Add(this.hitButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.betPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button hitButton;
        private System.Windows.Forms.Button standButton;
        private System.Windows.Forms.Button doubleButton;
        private System.Windows.Forms.Button splitButton;
        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.NumericUpDown betPicker;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private BlackjackPlayerView dealerView;
        private BlackjackPlayerView playerView;
        private System.Windows.Forms.Label betLabel;
        private System.Windows.Forms.Button playButton;
    }
}

