
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
            this.HandView = new global::BlackjackGUI.BlackjackHandView();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 221);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(50, 20);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "label1";
            // 
            // bankLabel
            // 
            this.bankLabel.AutoSize = true;
            this.bankLabel.Location = new System.Drawing.Point(3, 260);
            this.bankLabel.Name = "bankLabel";
            this.bankLabel.Size = new System.Drawing.Size(50, 20);
            this.bankLabel.TabIndex = 1;
            this.bankLabel.Text = "label2";
            // 
            // HandView
            // 
            this.HandView.Location = new System.Drawing.Point(3, 4);
            this.HandView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.HandView.Name = "HandView";
            this.HandView.Size = new System.Drawing.Size(305, 213);
            this.HandView.TabIndex = 2;
            // 
            // BlackjackPlayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HandView);
            this.Controls.Add(this.bankLabel);
            this.Controls.Add(this.nameLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BlackjackPlayerView";
            this.Size = new System.Drawing.Size(334, 291);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label bankLabel;
        internal BlackjackHandView HandView;
    }
}
