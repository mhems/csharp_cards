
namespace BlackjackGUI
{
    partial class BlackjackHandView
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
            this.potLabel = new System.Windows.Forms.Label();
            this.playerValue = new System.Windows.Forms.Label();
            this.blackjackCardView1 = new BlackjackGUI.BlackjackCardView();
            this.blackjackCardView2 = new BlackjackGUI.BlackjackCardView();
            this.SuspendLayout();
            // 
            // potLabel
            // 
            this.potLabel.AutoSize = true;
            this.potLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.potLabel.Location = new System.Drawing.Point(5, 11);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(33, 21);
            this.potLabel.TabIndex = 23;
            this.potLabel.Text = "pot";
            // 
            // playerValue
            // 
            this.playerValue.AutoSize = true;
            this.playerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerValue.Location = new System.Drawing.Point(71, 11);
            this.playerValue.Name = "playerValue";
            this.playerValue.Size = new System.Drawing.Size(30, 23);
            this.playerValue.TabIndex = 22;
            this.playerValue.Text = "21";
            // 
            // blackjackCardView1
            // 
            this.blackjackCardView1.Location = new System.Drawing.Point(5, 47);
            this.blackjackCardView1.Name = "blackjackCardView1";
            this.blackjackCardView1.Size = new System.Drawing.Size(75, 100);
            this.blackjackCardView1.TabIndex = 24;
            // 
            // blackjackCardView2
            // 
            this.blackjackCardView2.Location = new System.Drawing.Point(30, 47);
            this.blackjackCardView2.Name = "blackjackCardView2";
            this.blackjackCardView2.Size = new System.Drawing.Size(75, 100);
            this.blackjackCardView2.TabIndex = 25;
            // 
            // BlackjackHandView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.blackjackCardView2);
            this.Controls.Add(this.blackjackCardView1);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.playerValue);
            this.Name = "BlackjackHandView";
            this.Size = new System.Drawing.Size(118, 160);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label potLabel;
        private System.Windows.Forms.Label playerValue;
        private BlackjackCardView blackjackCardView1;
        private BlackjackCardView blackjackCardView2;
    }
}
