
namespace BlackjackGUI
{
    partial class BlackjackTableView
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.countView = new BlackjackGUI.BlackjackCountView();
            this.shoeView = new BlackjackGUI.ShoeView();
            this.dealerSlotView = new BlackjackGUI.BlackjackTableSlotView();
            this.playerSlotView = new BlackjackGUI.BlackjackTableSlotView();
            this.button1 = new System.Windows.Forms.Button();
            this.bankView = new BlackjackGUI.BankView();
            this.decisionView = new BlackjackGUI.BlackjackDecisionView();
            this.betView = new BlackjackGUI.BetView();
            this.configView = new BlackjackGUI.BlackjackConfigView();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(706, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(203, 451);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // countView
            // 
            this.countView.Location = new System.Drawing.Point(518, 90);
            this.countView.Name = "countView";
            this.countView.Size = new System.Drawing.Size(161, 205);
            this.countView.TabIndex = 20;
            // 
            // shoeView
            // 
            this.shoeView.Count = 0;
            this.shoeView.CutIndex = 0;
            this.shoeView.Index = 0;
            this.shoeView.Location = new System.Drawing.Point(541, 331);
            this.shoeView.Name = "shoeView";
            this.shoeView.Size = new System.Drawing.Size(108, 78);
            this.shoeView.TabIndex = 21;
            // 
            // dealerSlotView
            // 
            this.dealerSlotView.Index = 0;
            this.dealerSlotView.Location = new System.Drawing.Point(230, 3);
            this.dealerSlotView.Name = "dealerSlotView";
            this.dealerSlotView.Size = new System.Drawing.Size(213, 170);
            this.dealerSlotView.TabIndex = 22;
            // 
            // playerSlotView
            // 
            this.playerSlotView.Index = 0;
            this.playerSlotView.Location = new System.Drawing.Point(230, 284);
            this.playerSlotView.Name = "playerSlotView";
            this.playerSlotView.Size = new System.Drawing.Size(213, 170);
            this.playerSlotView.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 73);
            this.button1.TabIndex = 19;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bankView
            // 
            this.bankView.Balance = 0;
            this.bankView.Location = new System.Drawing.Point(527, 29);
            this.bankView.Name = "bankView";
            this.bankView.Size = new System.Drawing.Size(49, 22);
            this.bankView.TabIndex = 24;
            // 
            // decisionView
            // 
            this.decisionView.Location = new System.Drawing.Point(42, 29);
            this.decisionView.Name = "decisionView";
            this.decisionView.Size = new System.Drawing.Size(126, 205);
            this.decisionView.TabIndex = 25;
            // 
            // betView
            // 
            this.betView.Location = new System.Drawing.Point(42, 273);
            this.betView.Name = "betView";
            this.betView.Size = new System.Drawing.Size(132, 32);
            this.betView.TabIndex = 26;
            // 
            // configView
            // 
            this.configView.Location = new System.Drawing.Point(938, 3);
            this.configView.Name = "configView";
            this.configView.Size = new System.Drawing.Size(213, 451);
            this.configView.TabIndex = 27;
            // 
            // BlackjackTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.configView);
            this.Controls.Add(this.betView);
            this.Controls.Add(this.decisionView);
            this.Controls.Add(this.bankView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerSlotView);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dealerSlotView);
            this.Controls.Add(this.countView);
            this.Controls.Add(this.shoeView);
            this.Name = "BlackjackTableView";
            this.Size = new System.Drawing.Size(1155, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private BlackjackCountView countView;
        private ShoeView shoeView;
        private BlackjackTableSlotView dealerSlotView;
        private BlackjackTableSlotView playerSlotView;
        private System.Windows.Forms.Button button1;
        private BankView bankView;
        private BlackjackDecisionView decisionView;
        private BetView betView;
        private BlackjackConfigView configView;
    }
}
