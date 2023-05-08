
namespace The_cheese_race
{
    partial class ChooseLanguage
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
            this.LabelChooseLanguage = new System.Windows.Forms.Label();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.btnRomana = new System.Windows.Forms.Button();
            this.btnFrancais = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelChooseLanguage
            // 
            this.LabelChooseLanguage.AutoSize = true;
            this.LabelChooseLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelChooseLanguage.Location = new System.Drawing.Point(58, 9);
            this.LabelChooseLanguage.Name = "LabelChooseLanguage";
            this.LabelChooseLanguage.Size = new System.Drawing.Size(241, 48);
            this.LabelChooseLanguage.TabIndex = 1;
            this.LabelChooseLanguage.Text = "- Choose the rule\'s language -\r\n          - Choisissez la langue des règles -\r\n  " +
    "                  - Alegeți limba regurilor -";
            // 
            // btnEnglish
            // 
            this.btnEnglish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnglish.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnglish.Location = new System.Drawing.Point(61, 67);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(250, 60);
            this.btnEnglish.TabIndex = 0;
            this.btnEnglish.Text = "English";
            this.btnEnglish.UseVisualStyleBackColor = true;
            this.btnEnglish.MouseEnter += new System.EventHandler(this.btnEnglish_MouseEnter);
            this.btnEnglish.MouseLeave += new System.EventHandler(this.btnEnglish_MouseLeave);
            // 
            // btnRomana
            // 
            this.btnRomana.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRomana.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRomana.Location = new System.Drawing.Point(61, 199);
            this.btnRomana.Name = "btnRomana";
            this.btnRomana.Size = new System.Drawing.Size(250, 60);
            this.btnRomana.TabIndex = 3;
            this.btnRomana.Text = "Română";
            this.btnRomana.UseVisualStyleBackColor = true;
            this.btnRomana.Click += new System.EventHandler(this.btnRomana_Click);
            this.btnRomana.MouseEnter += new System.EventHandler(this.btnRomana_MouseEnter);
            this.btnRomana.MouseLeave += new System.EventHandler(this.btnRomana_MouseLeave);
            // 
            // btnFrancais
            // 
            this.btnFrancais.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFrancais.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrancais.Location = new System.Drawing.Point(61, 133);
            this.btnFrancais.Name = "btnFrancais";
            this.btnFrancais.Size = new System.Drawing.Size(250, 60);
            this.btnFrancais.TabIndex = 2;
            this.btnFrancais.Text = "Français";
            this.btnFrancais.UseVisualStyleBackColor = true;
            this.btnFrancais.MouseEnter += new System.EventHandler(this.btnFrancais_MouseEnter);
            this.btnFrancais.MouseLeave += new System.EventHandler(this.btnFrancais_MouseLeave);
            // 
            // ChooseLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(384, 271);
            this.Controls.Add(this.btnRomana);
            this.Controls.Add(this.btnFrancais);
            this.Controls.Add(this.LabelChooseLanguage);
            this.Controls.Add(this.btnEnglish);
            this.Name = "ChooseLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChooseLanguage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnglish;
        private System.Windows.Forms.Label LabelChooseLanguage;
        private System.Windows.Forms.Button btnFrancais;
        private System.Windows.Forms.Button btnRomana;
    }
}