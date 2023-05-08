
namespace The_cheese_race
{
    partial class WhoWinTheGame
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
            this.PctBoxFourthWinner = new System.Windows.Forms.PictureBox();
            this.PctBoxThirdWinner = new System.Windows.Forms.PictureBox();
            this.PctBoxSecondWinner = new System.Windows.Forms.PictureBox();
            this.pctBoxSingleWinner = new System.Windows.Forms.PictureBox();
            this.pctBoxTitle = new System.Windows.Forms.PictureBox();
            this.ManyWinners = new The_cheese_race.ButtonEllipse();
            this.OneWinner = new The_cheese_race.ButtonEllipse();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxFourthWinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxThirdWinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxSecondWinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxSingleWinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // PctBoxFourthWinner
            // 
            this.PctBoxFourthWinner.Location = new System.Drawing.Point(258, 387);
            this.PctBoxFourthWinner.Name = "PctBoxFourthWinner";
            this.PctBoxFourthWinner.Size = new System.Drawing.Size(230, 210);
            this.PctBoxFourthWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PctBoxFourthWinner.TabIndex = 4;
            this.PctBoxFourthWinner.TabStop = false;
            // 
            // PctBoxThirdWinner
            // 
            this.PctBoxThirdWinner.Location = new System.Drawing.Point(505, 240);
            this.PctBoxThirdWinner.Name = "PctBoxThirdWinner";
            this.PctBoxThirdWinner.Size = new System.Drawing.Size(230, 210);
            this.PctBoxThirdWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PctBoxThirdWinner.TabIndex = 3;
            this.PctBoxThirdWinner.TabStop = false;
            // 
            // PctBoxSecondWinner
            // 
            this.PctBoxSecondWinner.Location = new System.Drawing.Point(12, 240);
            this.PctBoxSecondWinner.Name = "PctBoxSecondWinner";
            this.PctBoxSecondWinner.Size = new System.Drawing.Size(230, 210);
            this.PctBoxSecondWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PctBoxSecondWinner.TabIndex = 2;
            this.PctBoxSecondWinner.TabStop = false;
            // 
            // pctBoxSingleWinner
            // 
            this.pctBoxSingleWinner.Location = new System.Drawing.Point(258, 173);
            this.pctBoxSingleWinner.Name = "pctBoxSingleWinner";
            this.pctBoxSingleWinner.Size = new System.Drawing.Size(230, 210);
            this.pctBoxSingleWinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBoxSingleWinner.TabIndex = 1;
            this.pctBoxSingleWinner.TabStop = false;
            // 
            // pctBoxTitle
            // 
            this.pctBoxTitle.Image = global::The_cheese_race.Properties.Resources.ManyMousesWin;
            this.pctBoxTitle.Location = new System.Drawing.Point(32, 13);
            this.pctBoxTitle.Name = "pctBoxTitle";
            this.pctBoxTitle.Size = new System.Drawing.Size(685, 175);
            this.pctBoxTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBoxTitle.TabIndex = 0;
            this.pctBoxTitle.TabStop = false;
            // 
            // ManyWinners
            // 
            this.ManyWinners.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManyWinners.Location = new System.Drawing.Point(496, 501);
            this.ManyWinners.Name = "ManyWinners";
            this.ManyWinners.Size = new System.Drawing.Size(221, 93);
            this.ManyWinners.TabIndex = 8;
            this.ManyWinners.Text = "It\'s ok for us many winners";
            this.ManyWinners.UseVisualStyleBackColor = true;
            this.ManyWinners.Click += new System.EventHandler(this.buttonManyWinners_Click);
            // 
            // OneWinner
            // 
            this.OneWinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OneWinner.Location = new System.Drawing.Point(32, 511);
            this.OneWinner.Name = "OneWinner";
            this.OneWinner.Size = new System.Drawing.Size(196, 74);
            this.OneWinner.TabIndex = 7;
            this.OneWinner.Text = "We want only one winner";
            this.OneWinner.UseVisualStyleBackColor = true;
            this.OneWinner.Click += new System.EventHandler(this.buttonOneWinner_Click);
            // 
            // WhoWinTheGame
            // 
            this.AcceptButton = this.OneWinner;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(754, 616);
            this.Controls.Add(this.ManyWinners);
            this.Controls.Add(this.OneWinner);
            this.Controls.Add(this.PctBoxFourthWinner);
            this.Controls.Add(this.PctBoxThirdWinner);
            this.Controls.Add(this.PctBoxSecondWinner);
            this.Controls.Add(this.pctBoxSingleWinner);
            this.Controls.Add(this.pctBoxTitle);
            this.KeyPreview = true;
            this.Name = "WhoWinTheGame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinnerAnnoucement";
            this.Load += new System.EventHandler(this.AnnonceVainqueur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxFourthWinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxThirdWinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxSecondWinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxSingleWinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBoxTitle;
        private System.Windows.Forms.PictureBox pctBoxSingleWinner;
        private System.Windows.Forms.PictureBox PctBoxSecondWinner;
        private System.Windows.Forms.PictureBox PctBoxThirdWinner;
        private System.Windows.Forms.PictureBox PctBoxFourthWinner;
        private ButtonEllipse OneWinner;
        private ButtonEllipse ManyWinners;
    }
}