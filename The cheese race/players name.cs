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
    public partial class FormName : Form
    {
        public FormName()
        {
            InitializeComponent();
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            //variabilele public retin numele jucatorilor alese in form pentru a le transmite formului principal
            if(textBoxplayer1.Text != "")
                Public_Date.player1 = textBoxplayer1.Text + ":";
            if(textBoxplayer2.Text != "")
                Public_Date.player2 = textBoxplayer2.Text + ":";
            if(textBoxplayer3.Text != "")
                Public_Date.player3 = textBoxplayer3.Text + ":";
            if(textBoxplayer4.Text != "")
                Public_Date.player4 = textBoxplayer4.Text + ":";

            //inchiderea formului de alegere a numelor jucatorilor
            this.Close();
        }
    }
}
