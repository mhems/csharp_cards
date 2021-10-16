
namespace BlackjackGUI
{
    partial class BetView
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
            this.betTextBox = new System.Windows.Forms.TextBox();
            this.betButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // betTextBox
            // 
            this.betTextBox.Location = new System.Drawing.Point(49, 2);
            this.betTextBox.Name = "betTextBox";
            this.betTextBox.Size = new System.Drawing.Size(80, 23);
            this.betTextBox.TabIndex = 21;
            this.betTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // betButton
            // 
            this.betButton.Location = new System.Drawing.Point(1, 2);
            this.betButton.Name = "betButton";
            this.betButton.Size = new System.Drawing.Size(40, 23);
            this.betButton.TabIndex = 22;
            this.betButton.Text = "&Bet";
            this.betButton.UseVisualStyleBackColor = true;
            this.betButton.Click += new System.EventHandler(this.BetButton_Click);
            // 
            // BetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.betButton);
            this.Controls.Add(this.betTextBox);
            this.Name = "BetView";
            this.Size = new System.Drawing.Size(132, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox betTextBox;
        private System.Windows.Forms.Button betButton;
    }
}
