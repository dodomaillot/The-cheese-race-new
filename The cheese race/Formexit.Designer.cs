
namespace The_cheese_race
{
    partial class Formexit
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
            this.BtnYes = new System.Windows.Forms.Button();
            this.BtnNo = new System.Windows.Forms.Button();
            this.LabelTextParasire = new System.Windows.Forms.Label();
            this.LabelSemnIntrebare = new System.Windows.Forms.Label();
            this.ImgNo = new System.Windows.Forms.PictureBox();
            this.PctBoxSoareceUmplut = new System.Windows.Forms.PictureBox();
            this.ImgYes = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImgNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxSoareceUmplut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgYes)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnYes
            // 
            this.BtnYes.BackColor = System.Drawing.SystemColors.Control;
            this.BtnYes.Location = new System.Drawing.Point(12, 318);
            this.BtnYes.Name = "BtnYes";
            this.BtnYes.Size = new System.Drawing.Size(79, 23);
            this.BtnYes.TabIndex = 1;
            this.BtnYes.Text = "Yes";
            this.BtnYes.UseVisualStyleBackColor = false;
            this.BtnYes.Click += new System.EventHandler(this.btnyes_Click);
            this.BtnYes.MouseLeave += new System.EventHandler(this.btnyes_MouseLeave);
            this.BtnYes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnyes_MouseMove);
            // 
            // BtnNo
            // 
            this.BtnNo.Location = new System.Drawing.Point(173, 318);
            this.BtnNo.Name = "BtnNo";
            this.BtnNo.Size = new System.Drawing.Size(75, 23);
            this.BtnNo.TabIndex = 2;
            this.BtnNo.Text = "No";
            this.BtnNo.UseVisualStyleBackColor = true;
            this.BtnNo.Click += new System.EventHandler(this.btnno_Click);
            this.BtnNo.MouseLeave += new System.EventHandler(this.btnno_MouseLeave);
            this.BtnNo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnno_MouseMove);
            // 
            // LabelTextParasire
            // 
            this.LabelTextParasire.AutoSize = true;
            this.LabelTextParasire.BackColor = System.Drawing.Color.Transparent;
            this.LabelTextParasire.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelTextParasire.Location = new System.Drawing.Point(6, 258);
            this.LabelTextParasire.Name = "LabelTextParasire";
            this.LabelTextParasire.Size = new System.Drawing.Size(255, 20);
            this.LabelTextParasire.TabIndex = 3;
            this.LabelTextParasire.Text = "Are you sure you want to exit ?";
            // 
            // LabelSemnIntrebare
            // 
            this.LabelSemnIntrebare.AutoSize = true;
            this.LabelSemnIntrebare.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSemnIntrebare.Location = new System.Drawing.Point(106, 290);
            this.LabelSemnIntrebare.Name = "LabelSemnIntrebare";
            this.LabelSemnIntrebare.Size = new System.Drawing.Size(52, 55);
            this.LabelSemnIntrebare.TabIndex = 4;
            this.LabelSemnIntrebare.Text = "?";
            // 
            // ImgNo
            // 
            this.ImgNo.BackColor = System.Drawing.Color.Transparent;
            this.ImgNo.Location = new System.Drawing.Point(193, 281);
            this.ImgNo.Name = "ImgNo";
            this.ImgNo.Size = new System.Drawing.Size(35, 32);
            this.ImgNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgNo.TabIndex = 5;
            this.ImgNo.TabStop = false;
            // 
            // PctBoxSoareceUmplut
            // 
            this.PctBoxSoareceUmplut.Image = global::The_cheese_race.Properties.Resources.Soarece_Exit;
            this.PctBoxSoareceUmplut.Location = new System.Drawing.Point(-2, 0);
            this.PctBoxSoareceUmplut.Name = "PctBoxSoareceUmplut";
            this.PctBoxSoareceUmplut.Size = new System.Drawing.Size(263, 253);
            this.PctBoxSoareceUmplut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PctBoxSoareceUmplut.TabIndex = 0;
            this.PctBoxSoareceUmplut.TabStop = false;
            // 
            // ImgYes
            // 
            this.ImgYes.BackColor = System.Drawing.Color.Transparent;
            this.ImgYes.Location = new System.Drawing.Point(33, 281);
            this.ImgYes.Name = "ImgYes";
            this.ImgYes.Size = new System.Drawing.Size(35, 32);
            this.ImgYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgYes.TabIndex = 6;
            this.ImgYes.TabStop = false;
            // 
            // Formexit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(260, 353);
            this.Controls.Add(this.ImgYes);
            this.Controls.Add(this.ImgNo);
            this.Controls.Add(this.LabelSemnIntrebare);
            this.Controls.Add(this.LabelTextParasire);
            this.Controls.Add(this.BtnNo);
            this.Controls.Add(this.BtnYes);
            this.Controls.Add(this.PctBoxSoareceUmplut);
            this.Name = "Formexit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formexit";
            ((System.ComponentModel.ISupportInitialize)(this.ImgNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBoxSoareceUmplut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgYes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PctBoxSoareceUmplut;
        private System.Windows.Forms.Button BtnYes;
        private System.Windows.Forms.Button BtnNo;
        private System.Windows.Forms.Label LabelTextParasire;
        private System.Windows.Forms.Label LabelSemnIntrebare;
        private System.Windows.Forms.PictureBox ImgNo;
        private System.Windows.Forms.PictureBox ImgYes;
    }
}