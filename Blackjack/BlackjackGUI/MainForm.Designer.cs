
namespace BlackjackGUI
{
    partial class MainForm
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
            this.playerCard2 = new System.Windows.Forms.PictureBox();
            this.playerCard1 = new System.Windows.Forms.PictureBox();
            this.dealerCard2 = new System.Windows.Forms.PictureBox();
            this.dealerCard1 = new System.Windows.Forms.PictureBox();
            this.dealerValue = new System.Windows.Forms.Label();
            this.playerValue = new System.Windows.Forms.Label();
            this.hitButton = new System.Windows.Forms.Button();
            this.standButton = new System.Windows.Forms.Button();
            this.doubleButton = new System.Windows.Forms.Button();
            this.splitButton = new System.Windows.Forms.Button();
            this.surrenderButton = new System.Windows.Forms.Button();
            this.betPicker = new System.Windows.Forms.NumericUpDown();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.potLabel = new System.Windows.Forms.Label();
            this.shoeLabel = new System.Windows.Forms.Label();
            this.stackLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playerCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealerCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealerCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // playerCard2
            // 
            this.playerCard2.Location = new System.Drawing.Point(317, 344);
            this.playerCard2.Name = "playerCard2";
            this.playerCard2.Size = new System.Drawing.Size(109, 158);
            this.playerCard2.TabIndex = 0;
            this.playerCard2.TabStop = false;
            // 
            // playerCard1
            // 
            this.playerCard1.Location = new System.Drawing.Point(356, 344);
            this.playerCard1.Name = "playerCard1";
            this.playerCard1.Size = new System.Drawing.Size(109, 158);
            this.playerCard1.TabIndex = 1;
            this.playerCard1.TabStop = false;
            // 
            // dealerCard2
            // 
            this.dealerCard2.Location = new System.Drawing.Point(317, 22);
            this.dealerCard2.Name = "dealerCard2";
            this.dealerCard2.Size = new System.Drawing.Size(109, 158);
            this.dealerCard2.TabIndex = 2;
            this.dealerCard2.TabStop = false;
            // 
            // dealerCard1
            // 
            this.dealerCard1.Location = new System.Drawing.Point(356, 22);
            this.dealerCard1.Name = "dealerCard1";
            this.dealerCard1.Size = new System.Drawing.Size(109, 158);
            this.dealerCard1.TabIndex = 3;
            this.dealerCard1.TabStop = false;
            // 
            // dealerValue
            // 
            this.dealerValue.AutoSize = true;
            this.dealerValue.Location = new System.Drawing.Point(518, 41);
            this.dealerValue.Name = "dealerValue";
            this.dealerValue.Size = new System.Drawing.Size(16, 17);
            this.dealerValue.TabIndex = 4;
            this.dealerValue.Text = "0";
            // 
            // playerValue
            // 
            this.playerValue.AutoSize = true;
            this.playerValue.Location = new System.Drawing.Point(521, 357);
            this.playerValue.Name = "playerValue";
            this.playerValue.Size = new System.Drawing.Size(16, 17);
            this.playerValue.TabIndex = 5;
            this.playerValue.Text = "0";
            // 
            // hitButton
            // 
            this.hitButton.Location = new System.Drawing.Point(45, 55);
            this.hitButton.Name = "hitButton";
            this.hitButton.Size = new System.Drawing.Size(89, 23);
            this.hitButton.TabIndex = 6;
            this.hitButton.Text = "Hit";
            this.hitButton.UseVisualStyleBackColor = true;
            // 
            // standButton
            // 
            this.standButton.Location = new System.Drawing.Point(45, 103);
            this.standButton.Name = "standButton";
            this.standButton.Size = new System.Drawing.Size(89, 23);
            this.standButton.TabIndex = 7;
            this.standButton.Text = "Stand";
            this.standButton.UseVisualStyleBackColor = true;
            // 
            // doubleButton
            // 
            this.doubleButton.Location = new System.Drawing.Point(45, 156);
            this.doubleButton.Name = "doubleButton";
            this.doubleButton.Size = new System.Drawing.Size(89, 23);
            this.doubleButton.TabIndex = 8;
            this.doubleButton.Text = "Double";
            this.doubleButton.UseVisualStyleBackColor = true;
            // 
            // splitButton
            // 
            this.splitButton.Location = new System.Drawing.Point(45, 211);
            this.splitButton.Name = "splitButton";
            this.splitButton.Size = new System.Drawing.Size(89, 23);
            this.splitButton.TabIndex = 9;
            this.splitButton.Text = "Split";
            this.splitButton.UseVisualStyleBackColor = true;
            // 
            // surrenderButton
            // 
            this.surrenderButton.Location = new System.Drawing.Point(45, 263);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(89, 23);
            this.surrenderButton.TabIndex = 10;
            this.surrenderButton.Text = "Surrender";
            this.surrenderButton.UseVisualStyleBackColor = true;
            // 
            // betPicker
            // 
            this.betPicker.Location = new System.Drawing.Point(45, 355);
            this.betPicker.Name = "betPicker";
            this.betPicker.Size = new System.Drawing.Size(98, 22);
            this.betPicker.TabIndex = 11;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(586, 274);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(237, 228);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // potLabel
            // 
            this.potLabel.AutoSize = true;
            this.potLabel.Location = new System.Drawing.Point(365, 298);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(28, 17);
            this.potLabel.TabIndex = 13;
            this.potLabel.Text = "pot";
            // 
            // shoeLabel
            // 
            this.shoeLabel.AutoSize = true;
            this.shoeLabel.Location = new System.Drawing.Point(619, 40);
            this.shoeLabel.Name = "shoeLabel";
            this.shoeLabel.Size = new System.Drawing.Size(39, 17);
            this.shoeLabel.TabIndex = 14;
            this.shoeLabel.Text = "shoe";
            // 
            // stackLabel
            // 
            this.stackLabel.AutoSize = true;
            this.stackLabel.Location = new System.Drawing.Point(87, 428);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(41, 17);
            this.stackLabel.TabIndex = 15;
            this.stackLabel.Text = "stack";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 529);
            this.Controls.Add(this.stackLabel);
            this.Controls.Add(this.shoeLabel);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.betPicker);
            this.Controls.Add(this.surrenderButton);
            this.Controls.Add(this.splitButton);
            this.Controls.Add(this.doubleButton);
            this.Controls.Add(this.standButton);
            this.Controls.Add(this.hitButton);
            this.Controls.Add(this.playerValue);
            this.Controls.Add(this.dealerValue);
            this.Controls.Add(this.dealerCard1);
            this.Controls.Add(this.dealerCard2);
            this.Controls.Add(this.playerCard1);
            this.Controls.Add(this.playerCard2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.playerCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealerCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dealerCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox playerCard2;
        private System.Windows.Forms.PictureBox playerCard1;
        private System.Windows.Forms.PictureBox dealerCard2;
        private System.Windows.Forms.PictureBox dealerCard1;
        private System.Windows.Forms.Label dealerValue;
        private System.Windows.Forms.Label playerValue;
        private System.Windows.Forms.Button hitButton;
        private System.Windows.Forms.Button standButton;
        private System.Windows.Forms.Button doubleButton;
        private System.Windows.Forms.Button splitButton;
        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.NumericUpDown betPicker;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label potLabel;
        private System.Windows.Forms.Label shoeLabel;
        private System.Windows.Forms.Label stackLabel;
    }
}

