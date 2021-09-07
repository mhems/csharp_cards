
namespace BlackjackGUI
{
    partial class BlackjackPlayerView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.bankLabel = new System.Windows.Forms.Label();
            this.blackjackHandView1 = new BlackjackGUI.BlackjackHandView();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 166);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "label1";
            // 
            // bankLabel
            // 
            this.bankLabel.AutoSize = true;
            this.bankLabel.Location = new System.Drawing.Point(3, 195);
            this.bankLabel.Name = "bankLabel";
            this.bankLabel.Size = new System.Drawing.Size(38, 15);
            this.bankLabel.TabIndex = 1;
            this.bankLabel.Text = "label2";
            // 
            // blackjackHandView1
            // 
            this.blackjackHandView1.Location = new System.Drawing.Point(3, 3);
            this.blackjackHandView1.Name = "blackjackHandView1";
            this.blackjackHandView1.Size = new System.Drawing.Size(119, 160);
            this.blackjackHandView1.TabIndex = 2;
            // 
            // BlackjackPlayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.blackjackHandView1);
            this.Controls.Add(this.bankLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "BlackjackPlayerView";
            this.Size = new System.Drawing.Size(127, 218);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label bankLabel;
        private BlackjackHandView blackjackHandView1;
    }
}
