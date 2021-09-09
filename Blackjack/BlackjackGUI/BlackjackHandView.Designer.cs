
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
            this.cardTable = new System.Windows.Forms.TableLayoutPanel();
            this.blackjackCardView3 = new BlackjackGUI.BlackjackCardView();
            this.blackjackCardView1 = new BlackjackGUI.BlackjackCardView();
            this.cardTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // potLabel
            // 
            this.potLabel.AutoSize = true;
            this.potLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.potLabel.Location = new System.Drawing.Point(6, 15);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(43, 28);
            this.potLabel.TabIndex = 23;
            this.potLabel.Text = "pot";
            // 
            // playerValue
            // 
            this.playerValue.AutoSize = true;
            this.playerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerValue.Location = new System.Drawing.Point(81, 15);
            this.playerValue.Name = "playerValue";
            this.playerValue.Size = new System.Drawing.Size(36, 30);
            this.playerValue.TabIndex = 22;
            this.playerValue.Text = "21";
            // 
            // cardTable
            // 
            this.cardTable.BackColor = System.Drawing.SystemColors.Control;
            this.cardTable.ColumnCount = 2;
            this.cardTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.cardTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cardTable.Controls.Add(this.blackjackCardView3, 1, 0);
            this.cardTable.Controls.Add(this.blackjackCardView1, 0, 0);
            this.cardTable.Location = new System.Drawing.Point(6, 65);
            this.cardTable.Margin = new System.Windows.Forms.Padding(0);
            this.cardTable.Name = "cardTable";
            this.cardTable.RowCount = 1;
            this.cardTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cardTable.Size = new System.Drawing.Size(207, 125);
            this.cardTable.TabIndex = 26;
            // 
            // blackjackCardView3
            // 
            this.blackjackCardView3.BackColor = System.Drawing.SystemColors.Control;
            this.blackjackCardView3.Location = new System.Drawing.Point(23, 4);
            this.blackjackCardView3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.blackjackCardView3.Name = "blackjackCardView3";
            this.blackjackCardView3.Size = new System.Drawing.Size(108, 117);
            this.blackjackCardView3.TabIndex = 0;
            // 
            // blackjackCardView1
            // 
            this.blackjackCardView1.BackColor = System.Drawing.SystemColors.Control;
            this.blackjackCardView1.Location = new System.Drawing.Point(3, 4);
            this.blackjackCardView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.blackjackCardView1.Name = "blackjackCardView1";
            this.blackjackCardView1.Size = new System.Drawing.Size(14, 117);
            this.blackjackCardView1.TabIndex = 1;
            // 
            // BlackjackHandView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardTable);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.playerValue);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BlackjackHandView";
            this.Size = new System.Drawing.Size(259, 207);
            this.cardTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label potLabel;
        private System.Windows.Forms.Label playerValue;
        private System.Windows.Forms.TableLayoutPanel cardTable;
        private BlackjackCardView blackjackCardView3;
        private BlackjackCardView blackjackCardView1;
    }
}
