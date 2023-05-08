using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_cheese_race
{
    public partial class Formexit : Form
    {
        public Formexit()
        {
            InitializeComponent();
        }

        private void btnyes_Click(object sender, EventArgs e)
        {
            Public_Date.exit = true;
            this.Close();
        }

        private void btnno_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnyes_MouseMove(object sender, MouseEventArgs e)
        {
            //un soarece micut apare peste butonul yes
            ImgYes.Image = Properties.Resources.Soarece_Exit_DA_NU;
        }

        private void btnyes_MouseLeave(object sender, EventArgs e)
        {
            //soarecele micut dispare de peste butonul yes
            ImgYes.Image = null;
        }

        private void btnno_MouseMove(object sender, MouseEventArgs e)
        {
            //un soarece micut apare peste butonul no
            ImgNo.Image = Properties.Resources.Soarece_Exit_DA_NU;
        }

        private void btnno_MouseLeave(object sender, EventArgs e)
        {
            //soarecele micut dispare de peste butonul no
            ImgNo.Image = null;
        }
    }
}
