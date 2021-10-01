
namespace BlackjackGUI
{
    partial class BlackjackTableSlotView
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
            this.insurancePot = new BlackjackGUI.BankView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.player = new BlackjackGUI.BlackjackPlayerView();
            this.SuspendLayout();
            // 
            // insurancePot
            // 
            this.insurancePot.Balance = 0;
            this.insurancePot.Location = new System.Drawing.Point(16, 118);
            this.insurancePot.Name = "insurancePot";
            this.insurancePot.Size = new System.Drawing.Size(49, 22);
            this.insurancePot.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // player
            // 
            this.player.DisplayBalance = true;
            this.player.Location = new System.Drawing.Point(82, 109);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(93, 46);
            this.player.TabIndex = 3;
            // 
            // BlackjackTableSlotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.player);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.insurancePot);
            this.Name = "BlackjackTableSlotView";
            this.Size = new System.Drawing.Size(213, 170);
            this.ResumeLayout(false);

        }

        #endregion
        private BankView insurancePot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private BlackjackPlayerView player;
    }
}
