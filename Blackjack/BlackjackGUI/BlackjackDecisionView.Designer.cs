
namespace BlackjackGUI
{
    partial class BlackjackDecisionView
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
            this.surrenderButton = new System.Windows.Forms.Button();
            this.splitButton = new System.Windows.Forms.Button();
            this.doubleButton = new System.Windows.Forms.Button();
            this.standButton = new System.Windows.Forms.Button();
            this.hitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // surrenderButton
            // 
            this.surrenderButton.Location = new System.Drawing.Point(3, 163);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(119, 36);
            this.surrenderButton.TabIndex = 15;
            this.surrenderButton.Text = "Surrender";
            this.surrenderButton.UseVisualStyleBackColor = true;
            this.surrenderButton.Click += new System.EventHandler(this.SurrenderButton_Click);
            // 
            // splitButton
            // 
            this.splitButton.Location = new System.Drawing.Point(3, 123);
            this.splitButton.Name = "splitButton";
            this.splitButton.Size = new System.Drawing.Size(119, 34);
            this.splitButton.TabIndex = 14;
            this.splitButton.Text = "Split";
            this.splitButton.UseVisualStyleBackColor = true;
            this.splitButton.Click += new System.EventHandler(this.SplitButton_Click);
            // 
            // doubleButton
            // 
            this.doubleButton.Location = new System.Drawing.Point(2, 83);
            this.doubleButton.Name = "doubleButton";
            this.doubleButton.Size = new System.Drawing.Size(120, 34);
            this.doubleButton.TabIndex = 13;
            this.doubleButton.Text = "Double";
            this.doubleButton.UseVisualStyleBackColor = true;
            this.doubleButton.Click += new System.EventHandler(this.DoubleButton_Click);
            // 
            // standButton
            // 
            this.standButton.Location = new System.Drawing.Point(3, 42);
            this.standButton.Name = "standButton";
            this.standButton.Size = new System.Drawing.Size(119, 35);
            this.standButton.TabIndex = 12;
            this.standButton.Text = "Stand";
            this.standButton.UseVisualStyleBackColor = true;
            this.standButton.Click += new System.EventHandler(this.StandButton_Click);
            // 
            // hitButton
            // 
            this.hitButton.Location = new System.Drawing.Point(3, 3);
            this.hitButton.Name = "hitButton";
            this.hitButton.Size = new System.Drawing.Size(119, 33);
            this.hitButton.TabIndex = 11;
            this.hitButton.Text = "Hit";
            this.hitButton.UseVisualStyleBackColor = true;
            this.hitButton.Click += new System.EventHandler(this.HitButton_Click);
            // 
            // BlackjackDecisionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.surrenderButton);
            this.Controls.Add(this.splitButton);
            this.Controls.Add(this.doubleButton);
            this.Controls.Add(this.standButton);
            this.Controls.Add(this.hitButton);
            this.Name = "BlackjackDecisionView";
            this.Size = new System.Drawing.Size(126, 205);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.Button splitButton;
        private System.Windows.Forms.Button doubleButton;
        private System.Windows.Forms.Button standButton;
        private System.Windows.Forms.Button hitButton;
    }
}
