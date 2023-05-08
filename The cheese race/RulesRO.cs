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
    public partial class RulesRO : Form
    {
        public RulesRO()
        {
            InitializeComponent();
        }

        //aceasta regiune este pentru prezentarea platoului
        #region Platou

        private void pctBoxMenu_MouseMove(object sender, MouseEventArgs e)
        {
            lbMenu.Text = "În menu, se poate alege numărul\rde jucători, numele jucătorilor,\rmodurile jocului, și se poate citi\rregulile.";
            lbMenu.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxMenu_MouseLeave(object sender, EventArgs e)
        {
            lbMenu.Text = null;
            lbMenu.BorderStyle = BorderStyle.None;
        }

        private void pctBoxTitle_MouseMove(object sender, MouseEventArgs e)
        {
            lbTitle.Text = "Este titlul jocului, dar din momentul\rîn care nu mai sunt cașcavaluri pe\rplatoul, va fi scris: jocul este gata,\rdar în engleză.";
            lbTitle.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxTitle_MouseLeave(object sender, EventArgs e)
        {
            lbTitle.Text = null;
            lbTitle.BorderStyle = BorderStyle.None;
        }

        private void pctBoxBlue_MouseMove(object sender, MouseEventArgs e)
        {
            lbBlue.Text = "Casa/Punctul de plecare a\rșoarecelui albastru.";
            lbBlue.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxBlue_MouseLeave(object sender, EventArgs e)
        {
            lbBlue.Text = null;
            lbBlue.BorderStyle = BorderStyle.None;
        }

        private void pctBoxGreen_MouseMove(object sender, MouseEventArgs e)
        {
            lbGreen.Text = "Casa/Punctul de plecare a\rșoarecelui verde.";
            lbGreen.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxGreen_MouseLeave(object sender, EventArgs e)
        {
            lbGreen.Text = null;
            lbGreen.BorderStyle = BorderStyle.None;
        }

        private void pctBoxPurple_MouseMove(object sender, MouseEventArgs e)
        {
            lbPurple.Text = "Casa/Punctul de plecare a\rșoarecelui mov.";
            lbPurple.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxPurple_MouseLeave(object sender, EventArgs e)
        {
            lbPurple.Text = null;
            lbPurple.BorderStyle = BorderStyle.None;
        }

        private void pctBoxRed_MouseMove(object sender, MouseEventArgs e)
        {
            lbRed.Text = "Casa/Punctul de plecare a\rșoarecelui roșu.";
            lbRed.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxRed_MouseLeave(object sender, EventArgs e)
        {
            lbRed.Text = null;
            lbRed.BorderStyle = BorderStyle.None;
        }

        private void pctboxChooseMove_MouseMove(object sender, MouseEventArgs e)
        {
            lbChooseMove.Visible = true;
        }

        private void pctboxChooseMove_MouseLeave(object sender, EventArgs e)
        {
            lbChooseMove.Visible = false;
        }

        private void pctBoxSequence_MouseMove(object sender, MouseEventArgs e)
        {
            lbSequence.Text = "Aici se vor afișa acțiunile \ralese si pe care șoarecele\rle va executa.";
            lbSequence.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxSequence_MouseLeave(object sender, EventArgs e)
        {
            lbSequence.Text = null;
            lbSequence.BorderStyle = BorderStyle.None;
        }

        private void pctBoxPlayers_MouseMove(object sender, MouseEventArgs e)
        {
            lbPlayers.Text = "Numele jucătorilor.";
            lbPlayers.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxPlayers_MouseLeave(object sender, EventArgs e)
        {
            lbPlayers.Text = null;
            lbPlayers.BorderStyle = BorderStyle.None;
        }

        private void pctBoxNbDes_MouseMove(object sender, MouseEventArgs e)
        {
            lbNbDes.Text = "Locul unde se va afișa numărul \rfăcut de zarul.";
            lbNbDes.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxNbDes_MouseLeave(object sender, EventArgs e)
        {
            lbNbDes.Text = null;
            lbNbDes.BorderStyle = BorderStyle.None;
        }

        private void pctBoxError_MouseMove(object sender, MouseEventArgs e)
        {
            lbError.Text = "Aici se vor afișa erorile,\rîn cazul in care apar.";
            lbError.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxError_MouseLeave(object sender, EventArgs e)
        {
            lbError.Text = null;
            lbError.BorderStyle = BorderStyle.None;
        }

        private void pctBoxThrowDice_MouseMove(object sender, MouseEventArgs e)
        {
            lbThrowDice.Text = "Butonul care, când va fi \rapăsat, va arunca sau opri\rzarul.";
            lbThrowDice.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxThrowDice_MouseLeave(object sender, EventArgs e)
        {
            lbThrowDice.Text = null;
            lbThrowDice.BorderStyle = BorderStyle.None;
        }

        private void pctBoxRun_MouseMove(object sender, MouseEventArgs e)
        {
            lbRun.Text = "Butonul care, când va fi \rapăsat, va executa secvența\rde acțiune aleasa.";
            lbRun.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxRun_MouseLeave(object sender, EventArgs e)
        {
            lbRun.Text = null;
            lbRun.BorderStyle = BorderStyle.None;
        }


        #endregion Platou

        //aceasta regiune este pentru prezentarea fiecarei miscare
        #region Action

        int RightPart = 1, time = 1, LeftPart = 1, BackPart = 1;

        //cand este apasat pictureboxul tryleft, atunci se executa miscarea la stanga a soarecelui
        private void pctBoxTryLeft_Click(object sender, EventArgs e)
        {
            if (LeftPart == 1)
            {
                pctBoxLeft.Image = Properties.Resources.Blue90;
                LeftPart++;
            }
            else if(LeftPart == 2)
            {
                pctBoxLeft.Image = Properties.Resources.Blue180;
                LeftPart++;
            }
            else if (LeftPart == 3)
            {
                pctBoxLeft.Image = Properties.Resources.Blue270;
                LeftPart++;
            }
            else if (LeftPart == 4)
            {
                pctBoxLeft.Image = Properties.Resources.Blue0;
                LeftPart = 1;
            }
        }

        private void pctBoxTryRight_Click(object sender, EventArgs e)
        {
            if (RightPart == 1)
            {
                pctBoxRight.Image = Properties.Resources.Blue270;
                RightPart++;
            }
            else if (RightPart == 2)
            {
                pctBoxRight.Image = Properties.Resources.Blue180;
                RightPart++;
            }
            else if (RightPart == 3)
            {
                pctBoxRight.Image = Properties.Resources.Blue90;
                RightPart++;
            }
            else if (RightPart == 4)
            {
                pctBoxRight.Image = Properties.Resources.Blue0;
                RightPart = 1;
            }
        }

        private void pctBoxTryForward_Click(object sender, EventArgs e)
        {
            pctBoxForward.Image = Properties.Resources.BlueForward1;
            time = 1;
            timerForward.Start();
        }

        private void timerForward_Tick(object sender, EventArgs e)
        {
            if(time == 1)
            {
                pctBoxForward.Image = Properties.Resources.BlueForward2;
                time++;
            }
            else
            {
                timerForward.Stop();
            }
        }

        private void pctBoxTryBackward_Click(object sender, EventArgs e)
        {
            if (BackPart == 1)
            {
                pctBoxBackward.Image = Properties.Resources.Blue180;
                BackPart++;
            }
            else if(BackPart == 2)
            {
                pctBoxBackward.Image = Properties.Resources.Blue0;
                BackPart = 1;
            }
        }

        #endregion Action

        //aceasta regiune este pentru tutorialele modurilor de joc
        #region Mod

        int ExCanibal = 1, ExTeleport = 1;

        private void buttonTutorialCanibal_Click(object sender, EventArgs e)
        {
            ExCanibal = 1;
            PctBoxCaniEx.Image = Properties.Resources.RuleCanibal1;
            timerCani.Start();
        }

        private void timerCani_Tick(object sender, EventArgs e)
        {
            if (ExCanibal == 1)
            {
                PctBoxCaniEx.Image = Properties.Resources.RuleCanibal2;
                ExCanibal++;
            }
            else if (ExCanibal == 2)
            {
                PctBoxCaniEx.Image = Properties.Resources.RuleCanibal3;
                ExCanibal++;
            }
            else if (ExCanibal == 3)
            {
                PctBoxCaniEx.Image = Properties.Resources.RuleCanibal4;
                ExCanibal++;
            }
            else if(ExCanibal==4)
            {
                PctBoxCaniEx.Image = Properties.Resources.RuleCanibal5;
                ExCanibal++;
            }
        }

        private void buttonTutorialTeleport_Click(object sender, EventArgs e)
        {
            ExTeleport = 1;
            pctBoxTeleport.Image = Properties.Resources.RuleTeleport1;
            timerTeleport.Start();
        }

        private void timerTeleport_Tick(object sender, EventArgs e)
        {
            if(ExTeleport == 1)
            {
                pctBoxTeleport.Image = Properties.Resources.RuleTeleport2;
                ExTeleport++;
            }
            else if(ExTeleport == 2)
            {
                pctBoxTeleport.Image = Properties.Resources.RuleTeleport3;
                ExTeleport++;
            }
            else if(ExTeleport == 3)
            {
                pctBoxTeleport.Image = Properties.Resources.RuleTeleport4;
                ExTeleport++;
            }
            else if(ExTeleport == 4)
            {
                pctBoxTeleport.Image = Properties.Resources.RuleTeleport5;
                timerTeleport.Stop();
            }
        }

        #endregion Mod

    }
}
