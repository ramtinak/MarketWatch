
namespace MarketWatch
{
    partial class Form1
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
            this.TimeLabel = new System.Windows.Forms.Label();
            this.XCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLabel.Location = new System.Drawing.Point(3, 7);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(135, 33);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "16:45:20";
            // 
            // XCancelButton
            // 
            this.XCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.XCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.XCancelButton.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XCancelButton.ForeColor = System.Drawing.Color.Silver;
            this.XCancelButton.Location = new System.Drawing.Point(144, -5);
            this.XCancelButton.Name = "XCancelButton";
            this.XCancelButton.Size = new System.Drawing.Size(75, 56);
            this.XCancelButton.TabIndex = 1;
            this.XCancelButton.Text = "x";
            this.XCancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.XCancelButton.UseVisualStyleBackColor = true;
            this.XCancelButton.Click += new System.EventHandler(this.XCancelButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(185, 46);
            this.Controls.Add(this.XCancelButton);
            this.Controls.Add(this.TimeLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MarketWatch";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button XCancelButton;
    }
}

