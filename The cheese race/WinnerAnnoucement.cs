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
    public partial class WhoWinTheGame : Form
    {
        public WhoWinTheGame()
        {
            InitializeComponent();
        }

        private void AnnonceVainqueur_Load(object sender, EventArgs e)
        {
            if (Public_Date.Many == false)
            {
                if (Public_Date.blueWin == true)
                    pctBoxSingleWinner.Image = Properties.Resources.blue_mouse270;
                else if (Public_Date.greenWin == true)
                    pctBoxSingleWinner.Image = Properties.Resources.green_mouse270;
                else if (Public_Date.purpleWin == true)
                    pctBoxSingleWinner.Image = Properties.Resources.purple_mouse270;
                else if (Public_Date.redWin == true)
                    pctBoxSingleWinner.Image = Properties.Resources.red_mouse270;

                pctBoxTitle.Image = Properties.Resources.The_Winner_Is;
                pctBoxSingleWinner.Location = new Point(175, 130);
                pctBoxSingleWinner.Size = new Size(400, 400);
                pctBoxSingleWinner.BringToFront();
                PctBoxSecondWinner.Image = null;
                PctBoxThirdWinner.Image = null;
                PctBoxFourthWinner.Image = null;
                OneWinner.Hide();
                ManyWinners.BringToFront();
                ManyWinners.Text = "END";
            }
            else
            {
                pctBoxTitle.Image = Properties.Resources.ManyMousesWin;
                pctBoxSingleWinner.Location = new Point(258, 173);
                pctBoxSingleWinner.Size = new Size(230, 210);
                OneWinner.Show();
                ManyWinners.Text = "It's ok for us many winners";
                if (Public_Date.HasWin == 2)
                {
                    if (Public_Date.blueWin == true && Public_Date.greenWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.green_mouse270;
                    }
                    else if (Public_Date.blueWin == true && Public_Date.purpleWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Public_Date.blueWin == true && Public_Date.redWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Public_Date.greenWin == true && Public_Date.purpleWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.green_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Public_Date.greenWin == true && Public_Date.redWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.green_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Public_Date.purpleWin == true && Public_Date.redWin == true)
                    {
                        PctBoxSecondWinner.Image = Properties.Resources.purple_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                }
                else if (Public_Date.HasWin == 3)
                {
                    if (Public_Date.blueWin == true && Public_Date.greenWin == true && Public_Date.purpleWin == true)
                    {
                        pctBoxSingleWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxSecondWinner.Image = Properties.Resources.green_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Public_Date.blueWin == true && Public_Date.greenWin == true && Public_Date.redWin == true)
                    {
                        pctBoxSingleWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxSecondWinner.Image = Properties.Resources.green_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Public_Date.blueWin == true && Public_Date.purpleWin == true && Public_Date.redWin == true)
                    {
                        pctBoxSingleWinner.Image = Properties.Resources.blue_mouse270;
                        PctBoxSecondWinner.Image = Properties.Resources.purple_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Public_Date.greenWin == true && Public_Date.purpleWin == true && Public_Date.redWin == true)
                    {
                        pctBoxSingleWinner.Image = Properties.Resources.green_mouse270;
                        PctBoxSecondWinner.Image = Properties.Resources.purple_mouse270;
                        PctBoxThirdWinner.Image = Properties.Resources.red_mouse270;
                    }
                }
                else if (Public_Date.HasWin == 4)
                {
                    pctBoxSingleWinner.Image = Properties.Resources.blue_mouse270;
                    PctBoxSecondWinner.Image = Properties.Resources.green_mouse270;
                    PctBoxThirdWinner.Image = Properties.Resources.purple_mouse270;
                    PctBoxFourthWinner.Image = Properties.Resources.red_mouse270;
                }
            }
        }

        private void buttonManyWinners_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonOneWinner_Click(object sender, EventArgs e)
        {
            Public_Date.WantCompetition = true;
            this.Hide();
        }
    }
}
