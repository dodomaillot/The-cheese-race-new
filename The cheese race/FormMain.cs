using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Web;

namespace The_cheese_race
{
    public partial class FormMain : Form
    {
        //initializez o lista care imi permite sa grupez toate bucatile de cascaval
        PictureBox[] Cheese = new PictureBox[13];

        //initializez o lista care imi permite sa grupez cele 16 pozitii: 8 pe axa Ox si 8 pe axa Oy
        int[] NbPositionX = new int[8];
        int[] NbPositionY = new int[8];

        public FormMain()
        {
            InitializeComponent();

            //crearea un listbox pt soarecele albastru, unde vor fi imaginile cu pozitiile soarecelui
            //vector: pozitia 0=blue_mouse0, pozitia 1=blue_mouse90...
            TheBlueMouse.Images.Add(Properties.Resources.blue_mouse0);
            TheBlueMouse.Images.Add(Properties.Resources.blue_mouse90);
            TheBlueMouse.Images.Add(Properties.Resources.blue_mouse180);
            TheBlueMouse.Images.Add(Properties.Resources.blue_mouse270);

            //crearea un listbox pt soarecele verde, unde vor fi imaginile cu pozitiile soarecelui
            //vector: pozitia 0=green_mouse0, pozitia 1=green_mouse90...
            TheGreenMouse.Images.Add(Properties.Resources.green_mouse0);
            TheGreenMouse.Images.Add(Properties.Resources.green_mouse90);
            TheGreenMouse.Images.Add(Properties.Resources.green_mouse180);
            TheGreenMouse.Images.Add(Properties.Resources.green_mouse270);

            //crearea un listbox pt soarecele mov, unde vor fi imaginile cu pozitiile soarecelui
            //vector: pozitia 0=purple_mouse0, pozitia 1=purple_mouse90...
            ThePurpleMouse.Images.Add(Properties.Resources.purple_mouse0);
            ThePurpleMouse.Images.Add(Properties.Resources.purple_mouse90);
            ThePurpleMouse.Images.Add(Properties.Resources.purple_mouse180);
            ThePurpleMouse.Images.Add(Properties.Resources.purple_mouse270);

            //crearea un listbox pt soarecele rosu, unde vor fi imaginile cu pozitiile soarecelui
            //vector: pozitia 0=red_mouse0, pozitia 1=red_mouse90...
            TheRedMouse.Images.Add(Properties.Resources.red_mouse0);
            TheRedMouse.Images.Add(Properties.Resources.red_mouse90);
            TheRedMouse.Images.Add(Properties.Resources.red_mouse180);
            TheRedMouse.Images.Add(Properties.Resources.red_mouse270);

            //crearea unei liste pt a usura programul la functia ActionCasCani
            //vectorul incepe de la 0, dar pozita 0 este null deoarece am vrut sa incep de la 1. Vectorul meu(sau lista mea) are 13 pozitii in total
            //Am facut asa sa fie pozitia 1 cu cascaval 1
            //Asta este un vector/o lista de imagine, cele a cascavarurilor aflate pe platou
            Cheese[1] = Cascaval1;
            Cheese[2] = Cascaval2;
            Cheese[3] = Cascaval3;
            Cheese[4] = Cascaval4;
            Cheese[5] = Cascaval5;
            Cheese[6] = Cascaval6;
            Cheese[7] = Cascaval7;
            Cheese[8] = Cascaval8;
            Cheese[9] = Cascaval9;
            Cheese[10] = Cascaval10;
            Cheese[11] = Cascaval11;
            Cheese[12] = Cascaval12;

            //crearea unei liste care retine cele 16 pozitii ale imaginilor pe platou: 8 pe axa Ox si 8 pe axa Oy
            //aici am inceput de la 0 pozitiile
            NbPositionX[0] = NbPositionY[0] = 0;
            NbPositionX[1] = 88; NbPositionY[1] = 85;
            NbPositionX[2] = 176;NbPositionY[2] = 170;
            NbPositionX[3] = 264;NbPositionY[3] = 255;
            NbPositionX[4] = 352;NbPositionY[4] = 340;
            NbPositionX[5] = 440;NbPositionY[5] = 425;
            NbPositionX[6] = 528;NbPositionY[6] = 510;
            NbPositionX[7] = 616;NbPositionY[7] = 595;
            
            //initializez timere pt miscarile care vor fi executate de fiecare soarece
            //
            TimerBlueMouse.Tick += new EventHandler(MoveBlueMouse);
            TimerGreenMouse.Tick += new EventHandler(MoveGreenMouse);
            TimerPurpleMouse.Tick += new EventHandler(MovePurpleMouse);
            TimerRedMouse.Tick += new EventHandler(MoveRedMouse);

            //initializarea unui eveniment pt timerului: timerdice
            //
            TimerZar.Tick += new EventHandler(changenb);
        }

        //initializez 2 variabile CordX si CordY care o sa retina pozitia X si Y a soarecelui inainte de a se misca
        //CordX si CordY ajuta in cazul in care soarecele iese din marginile platoului
        //Angle retine unghiul soarecelui, in cazul in care cand iese din marginile platoului s-a schimbat si pozitia lui de pe loc(de exemplu se uita in jos si cand a iesit de pe platou se uita in sus)
        int CordX = 0, CordY = 0;
        int angle = 0;

        //Declararea paginilor secundare:
        //Form care permite schimbarea numelor jucatorilor
        //Form pentru iesirea din joc
        //Form pentru alegerea limbii regulilor jocului
        //Form pt anuntarea castigatorului
        Form formname = new FormName();
        Form formexit = new Formexit();
        Form formchooselang = new ChooseLanguage();
        Form WinnerAnnoucement = new WhoWinTheGame();

        //NbPlayers = numarul de jucatori
        //NbCheese = numarul de bucati de cascaval ramase pe platou
        //NbMove = numarul de miscari alese (sequence)
        //Player = jucatorul care joaca in momentul acela (1blue, 2green, 3purple, 4red)\
        //CheeseBlue = Nr de bucati de cascaval mancate de blue mouse
        //Same pt restul
        int NbPlayers = 0, NbCheese = 12, NbMove = 0, player = 1, CheeseBlue = 0, CheeseGreen = 0, CheesePurple = 0, CheeseRed = 0;

        //Avand in vedere ca nu am gasit cum sa compar 2img, dau un unghi specific fiecarei imagine
        int AngleBlue = 0, IncliGreen = 180, IncliPurple = 0, IncliRed = 180;

        //Avand in vedere ca nu am gasit cum sa compar 2img, dau o cifra specifica fiecarei miscare(Ex: Left = 1; Right = 2, etc)
        int movement1 = 0, movement2 = 0, movement3 = 0, movement4 = 0;

        //Sa se stie daca un numar de jucatori a fost ales ca sa nu mai poata fi schimbat in timpul partidei
        //Sa se stie daca o actiune a fost executate ca nu mai poata fi modificat modurile de joaca
        bool gameon = false;
        bool firstactdid = false;

        //sequence este in curs de executare
        bool ExeCurs = false;

        //inițializarea timpului de afișare a erorilor
        int Timp = 6;

        //inițializarea unui bool pentru a nu permite jucătorului să arunce zarurile de câte ori dorește + să știe dacă zarul este aruncat (când imaginile se tot interschimba)
        bool NextPlayer = true;
        bool ZarInCurs = false;

        //inițializarea zarului folosind funcția aleatorie pentru a obține un număr aleator
        Random NbZar = new Random();
        int NbDice;

        //inițializarea unei variabile care ne va permite să știm care eroare s-a produs
        int eroare = 0;

        //Initializare variabilelor pentru modurile extra(sa se stie care mod a fost ales)
        bool secmode = false;
        bool canimode = false;
        bool teleportmode = false;

        private void ToolStripMenuItem2Players_Click(object sender, EventArgs e)
        {
            //Daca n-a inceput partida, atunci:
            if (gameon == false)
            {
                //Se pregateste terenul de joc
                //Se pun cei 2 soareci pe locul lor de start
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new Point(616, 0);
                NbPlayers = 2;

                //Labelurile cu Player 3 si 4 sunt retrase din plan vizual
                lbplayer3.Text = null;
                lbplayer4.Text = null;

                //gameon este true, insemnand ca partida poate sa inceapa
                gameon = true;

                //celula 2players este check
                ToolStripMenuItem2Players.Checked = true;
            }
        }

        private void ToolStripMenuItem3Players_Click(object sender, EventArgs e)
        {
            //Daca n-a inceput partida, atunci:
            if (gameon == false)
            {
                //Se pregateste terenul de joc
                //Se pun cei 3 soareci pe locul lor de start
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new Point(616, 0);
                purplemouse.Image = Properties.Resources.purple_mouse0;
                purplemouse.Location = new Point(0, 595);
                NbPlayers = 3;

                //Label cu Player 4 este retras din plan vizual
                lbplayer4.Text = null;

                //gameon este true, insemnand ca partida poate sa inceapa
                gameon = true;

                //celula 3players este bifata
                ToolStripMenuItem3Players.Checked = true;
            }
        }

        private void ToolStripMenuItem4Players_Click(object sender, EventArgs e)
        {
            //Daca n-a inceput partida, atunci:
            if (gameon == false)
            {
                //Se pregateste terenul de joc
                //Se pun cei 4 soareci pe locul lor de start
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new Point(616, 0);
                purplemouse.Image = Properties.Resources.purple_mouse0;
                purplemouse.Location = new Point(0, 595);
                redmouse.Image = Properties.Resources.red_mouse180;
                redmouse.Location = new Point(616, 595);
                NbPlayers = 4;

                //gameon este true, insemnand ca partida poate sa inceapa
                gameon = true;

                //celula 4players este bifata
                ToolStripMenuItem4Players.Checked = true;
            }
        }

        private void ToolStripMenuItemNewGame_Click(object sender, EventArgs e)
        {
            //Se repune tot la zero deoarece se incepe un joc nou

            //imaginile cu soarecii sunt retrase de pe platou(dispari din plan vizual)
            bluemouse.Image = null;
            greenmouse.Image = null;
            purplemouse.Image = null;
            redmouse.Image = null;

            //contoarele de bucati de cascaval fiecarui soarece sunt puse inapoi la 0
            CheeseBlue = 0;
            CheeseGreen = 0;
            CheesePurple = 0;
            CheeseRed = 0;

            //Zarul se opreste si se pune la 0 in cazul in care a fost aruncat
            btnZar.Text = "Roll the dice";
            NumarZar.Image = null;
            TimerZar.Stop();

            //Numarul total de branza ramase pe platou sunt puse inapoi la 12, iar numarul de jucator inapoi la 0
            //jucatorul a carui este randul este 1, cel cu soarecele albastru
            NbCheese = 12;
            NbPlayers = 0;
            player = 1;

            //unghiul soarecilor(directia in care se uit) reiau valoarea de start
            AngleBlue = 0;
            IncliGreen = 180;
            IncliPurple = 0;
            IncliRed = 180;

            //Sagatile de la sequence sunt retrase din plan visual si numarul de miscari revine la 0
            Miscare1.Image = null;
            Miscare2.Image = null;
            Miscare3.Image = null;
            Miscare4.Image = null;
            NbMove = 0;

            //punem inapoi numele "player" in labelurile respective
            lbplayer1.Text = "Player 1:";
            lbplayer2.Text = "Player 2:";
            lbplayer3.Text = "Player 3:";
            lbplayer4.Text = "Player 4:";

            //celule din meniul cu alegerea jucatorilor sunt nebifate
            LabelMesajFirstHome.Text = null;
            ToolStripMenuItem2Players.Checked = false;
            ToolStripMenuItem3Players.Checked = false;
            ToolStripMenuItem4Players.Checked = false;

            //"Datele publice" sunt resetate la zero
            Public_Date.player1 = "Player 1:";
            Public_Date.player2 = "Player 2:";
            Public_Date.player3 = "Player 3:";
            Public_Date.player4 = "Player 4:";
            Public_Date.exit = false;
            Public_Date.Many = false;
            Public_Date.WantCompetition = false;
            Public_Date.blueWin = false;
            Public_Date.greenWin = false;
            Public_Date.purpleWin = false;
            Public_Date.redWin = false;
            Public_Date.HasWin = 1;

            //zarul pote din nou sa fie aruncat de urmatorul jucator
            NextPlayer = true;

            //partida nu mai este inceputa + prima actiune n a fost executata + secventa de miscari nu mai este in curs de executie
            gameon = false;
            firstactdid = false;
            ExeCurs = false;

            //in cazul in care nu mai erau bucati de cascaval pe platou si titlul a fost inlocuit cu "jocul s a terminat"(in engleza)
            lbTitlu.Text = "T\rH\rE\r\rC\rH\rE\rE\rS\rE\r\rR\rA\rC\rE";

            //bucatile de cascaval reapari si sunt aduse in primul plan
            for (int i = 1; i <= 12; i++)
            {
                Cheese[i].Image = Properties.Resources.Cascaval;
                Cheese[i].BringToFront();
            }

            //Daca cumva modul second chance a fost activat, se dezactiveaza modul si nu mai este bifata celula in meniu
            //iar butonul de realegere a miscarilor dispare, tot cu marginea sa(borderstyle)
            secmode = false;
            ToolStripMenuItemSecondChance.Checked = false;
            ResetareSirMiscari.Image = null;
            ResetareSirMiscari.BorderStyle = BorderStyle.None;

            //Daca cumva modul canibal a fost activat, se dezactiveaza modul si nu mai este bifata celula in meniu
            canimode = false;
            ToolStripMenuItemCannibal.Checked = false;

            //Daca cumva modul teleporting food a fost activat, se dezactiveaza modul si nu mai este bifata celula in meniu
            //iar bucatile de cascavali revin la locurile lor de start
            teleportmode = false;
            ToolStripMenuItemTeleportingFood.Checked = false;
            Cheese[1].Left = 176; Cheese[1].Top = 85;
            Cheese[2].Left = 352; Cheese[2].Top = 85;
            Cheese[3].Left = 0; Cheese[3].Top = 170;
            Cheese[4].Left = 616; Cheese[4].Top = 170;
            Cheese[5].Left = 264; Cheese[5].Top = 255;
            Cheese[6].Left = 440; Cheese[6].Top = 255;
            Cheese[7].Left = 0; Cheese[7].Top = 340;
            Cheese[8].Left = 616; Cheese[8].Top = 340;
            Cheese[9].Left = 176; Cheese[9].Top = 425;
            Cheese[10].Left = 352; Cheese[10].Top = 425;
            Cheese[11].Left = 440; Cheese[11].Top = 510;
            Cheese[12].Left = 264; Cheese[12].Top = 595;
        }

        private void ToolStripMenuItemPlayersName_Click(object sender, EventArgs e)
        {
            //pagina/formularul "parinte" paginei/formularului cu alegerea numelor jucatorilor este acesta(FormMain) + afisarea paginei/formularului nume
            formname.Owner = this;
            formname.ShowDialog();

            //Schimbarea numelor dupa inchiderea paginei
            lbplayer1.Text = Public_Date.player1;
            lbplayer2.Text = Public_Date.player2;
            lbplayer3.Text = Public_Date.player3;
            lbplayer4.Text = Public_Date.player4;

            //Daca partida a inceput si se doreste schimbarea numelor in timpul acesteia
            if (gameon == true)
            {
                //daca sunt numai 2 jucatori, nu se va afisa numele jucatorilor 3 si 4 chiar si daca li s-a atribuit un nume
                //iar dupa player 1 si 2 se va afisa numarul de bucati de cascavali pe care-l are fiecare
                if (NbPlayers == 2)
                {
                    lbplayer3.Text = null;
                    lbplayer4.Text = null;
                    lbplayer1.Text = Public_Date.player1 + CheeseBlue;
                    lbplayer2.Text = Public_Date.player2 + CheeseGreen;
                }
                //daca sunt numai 3 jucatori, nu se va afisa numele jucatorului 4 chiar si daca li s-a atribuit un nume
                //iar dupa player 1, 2 si 3 se va afisa numarul de bucati de cascavali pe care-l are fiecare
                else if (NbPlayers == 3)
                {
                    lbplayer4.Text = null;
                    lbplayer1.Text = Public_Date.player1 + CheeseBlue;
                    lbplayer2.Text = Public_Date.player2 + CheeseGreen;
                    lbplayer3.Text = Public_Date.player3 + CheesePurple;
                }
                //dupa player 1, 2, 3 si 4 se va afisa numarul de bucati de cascavali pe care-l are fiecare
                else
                {
                    lbplayer1.Text = Public_Date.player1 + CheeseBlue;
                    lbplayer2.Text = Public_Date.player2 + CheeseGreen;
                    lbplayer3.Text = Public_Date.player3 + CheesePurple;
                    lbplayer4.Text = Public_Date.player4 + CheeseRed;
                }
            }
        }

        private void theRulesToolStripMenuItemTheRules_Click(object sender, EventArgs e)
        {
            //pagina/formularul "parinte" paginei/formularului cu alegerea limbii pt regulile este acesta(FormMain) + afisarea paginei/formularului 
            formchooselang.Owner = this;
            formchooselang.ShowDialog();
        }

        private void ToolStripMenuItemSecondChance_Click(object sender, EventArgs e)
        {
            //Daca prima actiune nu a fost efectuata si celula second chance nu este bifata, atunci
            if (firstactdid == false && ToolStripMenuItemSecondChance.Checked == false)
            {
                //modul second chance a fost activat, celula din meniu bifata, si apare imaginea cu butonul reset si conturul ei(BorderStyle)
                secmode = true;
                ToolStripMenuItemSecondChance.Checked = true;
                ResetareSirMiscari.Image = Properties.Resources.reset;
                ResetareSirMiscari.BorderStyle = BorderStyle.Fixed3D;
            }
            //Daca prima actiune nu a fost efectuata si celula second chance este bifata, atunci
            else if (firstactdid == false && ToolStripMenuItemSecondChance.Checked == true)
            {
                //modul second chance a fost dezactivat, celula din meniu nebifata, si dispare imaginea cu butonul reset si conturul ei(BorderStyle)
                secmode = false;
                ToolStripMenuItemSecondChance.Checked = false;
                ResetareSirMiscari.Image = null;
                ResetareSirMiscari.BorderStyle = BorderStyle.None;
            }
        }

        private void ToolStripMenuItemCannibal_Click(object sender, EventArgs e)
        {
            //Daca prima actiune nu a fost efectuata si celula Canibal nu este bifata, atunci
            if (firstactdid == false && ToolStripMenuItemCannibal.Checked == false)
            {
                //modul canibal este activat si celula din meniu bifata
                canimode = true;
                ToolStripMenuItemCannibal.Checked = true;
            }
            //Daca prima actiune nu a fost efectuata si celula second chance este bifata, atunci
            else if (firstactdid == false && ToolStripMenuItemCannibal.Checked == true)
            {
                //modul canibal este dezactivat si celula din meniu nebifata
                canimode = false;
                ToolStripMenuItemCannibal.Checked = false;
            }
        }

        private void ToolStripMenuItemTeleportingFood_Click(object sender, EventArgs e)
        {
            //Daca prima actiune nu a fost efectuata si celula Teleport nu este bifata, atunci
            if (firstactdid == false && ToolStripMenuItemTeleportingFood.Checked == false)
            {
                //modul Teleport este activat si celula din meniu bifata
                teleportmode = true;
                ToolStripMenuItemTeleportingFood.Checked = true;
            }
            //Daca prima actiune nu a fost efectuata si celula Teleport este bifata, atunci
            else if (firstactdid == false && ToolStripMenuItemTeleportingFood.Checked == true)
            {
                //modul Teleport este dezactivat si celula din meniu nebifata
                teleportmode = false;
                ToolStripMenuItemTeleportingFood.Checked = false;
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            //pagina/formularul "parinte" paginei/formularului exit este acesta(FormMain) + afisarea paginei/formularului 
            formexit.Owner = this;
            formexit.ShowDialog();

            //daca raspunsul este da, atunci se inchide aplicatia, daca raspunsul este nu, atunci aplicatia ramane deschisa
            if (Public_Date.exit == true)
                Application.Exit();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //daca se inchide aplicatia fara sa se foloseasca butonul exit din meniu, se deschide pagina/formularul de confirmare
            if (Public_Date.exit == false)
                formexit.ShowDialog();

            //Daca raspunsul este da, atunci se iese din aplicatie, daca nu, nu 
            if (Public_Date.exit == true)
                Application.Exit();
            else if (Public_Date.exit == false)
                e.Cancel = true;
        }

        private void btndice_Click(object sender, EventArgs e)
        {
            //daca numarul de jucator este intre 2 si 4 si partida a inceput, atunci
            if (NbPlayers >= 2 && NbPlayers <= 4 && gameon == true)
            {
                //daca pe butonul de aruncare a zarului este scris "Roll the dice" si este randul urmatorului jucator, atunci
                if (btnZar.Text == "Roll the dice" && NextPlayer == true)
                {
                    //o actiune a fost executata => modurile de joc nu mai pot sa fi schimbate
                    firstactdid = true;

                    //inceperea timerului pt schimbarea de imagine a zarului
                    //se schimba textul de pe butonul zarului cu "Stop the dice"
                    //zarul este in curs de aruncare
                    TimerZar.Start();
                    btnZar.Text = "Stop the dice";
                    ZarInCurs = true;
                }
                //daca textul de pe butonul "Roll the dice" si secventa de miscare este in curs de desfasurare, atunci
                else if(btnZar.Text == "Roll the dice" && ExeCurs == true)
                {
                    //este eroare 6 se da drumul timer eror
                    eroare = 6;
                    TimerEroare.Start();
                }
                else
                {
                    //timerul "timerdice" se opreste
                    TimerZar.Stop();

                    //if-ul este aici pt ca chiar si daca butonul "Roll the dice" apasat, imaginea numarului ramane neschimbat lafel ca si valoarea zarului
                    //daca este randul urmatorului jucator, atunci
                    if (NextPlayer == true)
                    {
                        //nbdice ia o valoare aleatoriu intre [1,5), se scrie pe buton "Roll the dice"
                        //zarul nu mai este in curs de aruncare, s a oprit
                        NbDice = NbZar.Next(1, 5);
                        btnZar.Text = "Roll the dice";
                        ZarInCurs = false;

                        //determinarea cifrului facut de zar si afisarea acestuia in imagine
                        if (NbDice % 5 == 1)
                            NumarZar.Image = Properties.Resources.Unu;
                        else if (NbDice % 5 == 2)
                            NumarZar.Image = Properties.Resources.Doi;
                        else if (NbDice % 5 == 3)
                            NumarZar.Image = Properties.Resources.Trei;
                        else if (NbDice % 5 == 4)
                            NumarZar.Image = Properties.Resources.Patru;
                    }
                    else
                    {
                        //determinarea erorii, plus timerul "timererror" incepe
                        eroare = 1;
                        TimerEroare.Start();
                    }

                    //zarul nu mai poate fi aruncat
                    NextPlayer = false;
                }
            }
        }

        private void changenb(object sender, EventArgs e)
        {
            //program de interschimbare a imaginilor semn intrebare si semn exclamatie
            int index = DateTime.Now.Millisecond % 2;
            if(index == 1)
                NumarZar.Image = Properties.Resources.SemnExclamatie;
            else
                NumarZar.Image = Properties.Resources.SemnIntrebare;
        }

        private void Left_Click(object sender, EventArgs e)
        {
            //daca partida a inceput si zarul nu este in curs de aruncare si numarul zarului nu este zero si secventa de miscare nu este in curs de executie, atunci
            if (gameon == true && ZarInCurs == false && NbDice != 0 && ExeCurs == false)
            {
                //daca cumva primul picturebox este ocupat se trece la urmator, daca al doilea este tot ocupat se trece iar la urmatorul etc
                //se pune in picturebox imaginea potrivit miscarii
                if (Miscare1.Image == null && NbMove < NbDice)
                {
                    Miscare1.Image = Properties.Resources.RotatieStanga;
                    movement1 = 1;
                    NbMove++;
                }
                else if (Miscare2.Image == null && NbMove < NbDice)
                {
                    Miscare2.Image = Properties.Resources.RotatieStanga;
                    movement2 = 1;
                    NbMove++;
                }
                else if (Miscare3.Image == null && NbMove < NbDice)
                {
                    Miscare3.Image = Properties.Resources.RotatieStanga;
                    movement3 = 1;
                    NbMove++;
                }
                else if (Miscare4.Image == null && NbMove < NbDice)
                {
                    Miscare4.Image = Properties.Resources.RotatieStanga;
                    movement4 = 1;
                    NbMove++;
                }
            }
            //daca secventa de miscare este in curs de desfasurare, atunci
            else if (ExeCurs == true)
            {
                //se determina eroare si se afiseaza acesta timp de 3 secunde cu ajutorul timerului "timereroare"
                eroare = 5;
                TimerEroare.Start();
            }
        }

        private void Right_Click(object sender, EventArgs e)
        {
            //daca partida a inceput si zarul nu este in curs de aruncare si numarul zarului nu este zero si secventa de miscare nu este in curs de executie, atunci
            if (gameon == true && ZarInCurs == false && NbDice != 0 && ExeCurs == false)
            {
                //daca cumva primul picturebox este ocupat se trece la urmator, daca al doilea este tot ocupat se trece iar la urmatorul etc
                //se pune in picturebox imaginea potrivit miscarii
                if (Miscare1.Image == null && NbMove < NbDice)
                {
                    Miscare1.Image = Properties.Resources.RotatieDreapta;
                    movement1 = 2;
                    NbMove++;
                }
                else if (Miscare2.Image == null && NbMove < NbDice)
                {
                    Miscare2.Image = Properties.Resources.RotatieDreapta;
                    movement2 = 2;
                    NbMove++;
                }
                else if (Miscare3.Image == null && NbMove < NbDice)
                {
                    Miscare3.Image = Properties.Resources.RotatieDreapta;
                    movement3 = 2;
                    NbMove++;
                }
                else if (Miscare4.Image == null && NbMove < NbDice)
                {
                    Miscare4.Image = Properties.Resources.RotatieDreapta;
                    movement4 = 2;
                    NbMove++;
                }
            }
            //daca secventa de miscare este in curs de desfasurare, atunci
            else if (ExeCurs == true)
            {
                //se determina eroare si se afiseaza acesta timp de 3 secunde cu ajutorul timerului "timereroare"
                eroare = 5;
                TimerEroare.Start();
            }
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            //daca partida a inceput si zarul nu este in curs de aruncare si numarul zarului nu este zero si secventa de miscare nu este in curs de executie, atunci
            if (gameon == true && ZarInCurs == false && NbDice != 0 && ExeCurs == false)
            {
                //daca cumva primul picturebox este ocupat se trece la urmator, daca al doilea este tot ocupat se trece iar la urmatorul etc
                //se pune in picturebox imaginea potrivit miscarii
                if (Miscare1.Image == null && NbMove < NbDice)
                {
                    Miscare1.Image = Properties.Resources.PasInainte;
                    movement1 = 3;
                    NbMove++;
                }
                else if (Miscare2.Image == null && NbMove < NbDice)
                {
                    Miscare2.Image = Properties.Resources.PasInainte;
                    movement2 = 3;
                    NbMove++;
                }
                else if (Miscare3.Image == null && NbMove < NbDice)
                {
                    Miscare3.Image = Properties.Resources.PasInainte;
                    movement3 = 3;
                    NbMove++;
                }
                else if (Miscare4.Image == null && NbMove < NbDice)
                {
                    Miscare4.Image = Properties.Resources.PasInainte;
                    movement4 = 3;
                    NbMove++;
                }
            }
            //daca secventa de miscare este in curs de desfasurare, atunci
            else if (ExeCurs == true)
            {
                //se determina eroare si se afiseaza acesta timp de 3 secunde cu ajutorul timerului "timereroare"
                eroare = 5;
                TimerEroare.Start();
            }
        }

        private void Backward_Click(object sender, EventArgs e)
        {
            //daca partida a inceput si zarul nu este in curs de aruncare si numarul zarului nu este zero si secventa de miscare nu este in curs de executie, atunci
            if (gameon == true && ZarInCurs == false && NbDice != 0 && ExeCurs == false)
            {
                //daca cumva primul picturebox este ocupat se trece la urmator, daca al doilea este tot ocupat se trece iar la urmatorul etc
                //se pune in picturebox imaginea potrivit miscarii
                if (Miscare1.Image == null && NbMove < NbDice)
                {
                    Miscare1.Image = Properties.Resources.Rotatie180;
                    movement1 = 4;
                    NbMove++;
                }
                else if (Miscare2.Image == null && NbMove < NbDice)
                {
                    Miscare2.Image = Properties.Resources.Rotatie180;
                    movement2 = 4;
                    NbMove++;
                }
                else if (Miscare3.Image == null && NbMove < NbDice)
                {
                    Miscare3.Image = Properties.Resources.Rotatie180;
                    movement3 = 4;
                    NbMove++;
                }
                else if (Miscare4.Image == null && NbMove < NbDice)
                {
                    Miscare4.Image = Properties.Resources.Rotatie180;
                    movement4 = 4;
                    NbMove++;
                }
            }
            //daca secventa de miscare este in curs de desfasurare, atunci
            else if (ExeCurs == true)
            {
                //se determina eroare si se afiseaza acesta timp de 3 secunde cu ajutorul timerului "timereroare"
                eroare = 5;
                TimerEroare.Start();
            }
        }

        private void resetmove_Click(object sender, EventArgs e)
        {
            //daca modul second chance este activat si secventa nu este in curs de desfasurare, atunci secventa poate fi modificata
            if (secmode == true && ExeCurs == false)
            {
                //numarul de miscari alese revin la 0
                NbMove = 0;

                //se sterg imaginile din secventa
                Miscare1.Image = null;
                Miscare2.Image = null;
                Miscare3.Image = null;
                Miscare4.Image = null;

                //se pun miscarile la 0, cum ca nu s a ales inca nici o miscare si au toate valoarea "null"(sau zero)
                movement1 = 0;
                movement2 = 0;
                movement3 = 0;
                movement4 = 0;
            }
            
            //daca secventa este in curs de desfasurare si se apasa butonul reset, atunci se afiseaza eroarea 5 la ecran timp de 3sec
            else if (ExeCurs == true)
            {
                eroare = 5;
                TimerEroare.Start();
            }
        }

        private void btnRUN_Click(object sender, EventArgs e)
        {
            //daca partida a inceput, atunci
            if (gameon == true)
            {
                //daca nr jucatorilor este intre 2 si 4, numarul de miscari egale cu cele a zarului, zarul nu este in curs de aruncare si pima actiune a fost facuta, atunci
                if (NbPlayers >= 2 && NbPlayers <= 4 && NbMove == NbDice && ZarInCurs == false && firstactdid == true)
                {
                    //secventa este in curs de executie
                    ExeCurs = true;

                    //Daca este randul jucatorului 1 (cel albastru)
                    if (player == 1)
                    {
                        //retinem pozitia de start si unghiul sau in cazul in care iese din platou
                        CordX = bluemouse.Left;
                        CordY = bluemouse.Top;
                        angle = AngleBlue;

                        //Se porneste timer pt a efectua secventa de miscari, facand un pas la fiecare secunda(asa a fost setat timerul)
                        TimerBlueMouse.Start();
                    }
                    //Daca este randul jucatorului 2 (cel verde)
                    else if (player == 2)
                    {
                        //retinem pozitia de start si unghiul sau in cazul in care iese din platou
                        CordX = greenmouse.Left;
                        CordY = greenmouse.Top;
                        angle = IncliGreen;

                        //Se porneste timer pt a efectua secventa de miscari, facand un pas la fiecare secunda(asa a fost setat timerul)
                        TimerGreenMouse.Start();
                    }
                    else if (player == 3)
                    {
                        //retinem pozitia de start si unghiul sau in cazul in care iese din platou
                        CordX = purplemouse.Left;
                        CordY = purplemouse.Top;
                        angle = IncliPurple;

                        TimerPurpleMouse.Start();
                    }
                    else if (player == 4)
                    {
                        //retinem pozitia de start si unghiul sau in cazul in care iese din platou
                        CordX = redmouse.Left;
                        CordY = redmouse.Top;
                        angle = IncliRed;

                        TimerRedMouse.Start();
                    }

                    //urmatorul jucator nu poate sa joace
                    NextPlayer = false;
                }
                else
                {
                    //se determina eroarea cauzat
                    //daca zarul este in curs de aruncare si se apasa butonul RUN, atunci se va afisa mesajul 2
                    //daca nu s a selectionat un numar de miscari egal cu cele facute de zar, atunci se afiseaza mesajul 3
                    if (ZarInCurs == true)
                        eroare = 2;
                    else if (NbMove != NbDice)
                        eroare = 3;
                    
                    TimerEroare.Start();
                }
            }
        }

        private void MoveBlueMouse(object sender, EventArgs e)
        {
            if (NbMove != 0)
            {
                //randul soarecelui albastru, deci il punem in primul plan(in cazul in care se afla 2 soareci pe acelasi patrat
                bluemouse.BringToFront();

                //daca miscarea 1 n a fost executata, atunci
                if (Miscare1.Image != null)
                {
                    //se determina prima miscare(daca merge la stanga, dreapta etc), si executa miscarea
                    if (movement1 == 1)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 2)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 3)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Left += 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Top -= 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Left -= 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Top += 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 4)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                }

                //daca miscarea 2 n a fost executata, atunci
                else if (Miscare2.Image != null)
                {
                    //se determina a doua miscare(daca merge la stanga, dreapta etc), si executa miscarea
                    if (movement2 == 1)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 2)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 3)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Left += 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Top -= 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Left -= 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Top += 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 4)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                }

                //daca miscarea 3 n a fost executata, atunci
                else if (Miscare3.Image != null)
                {
                    //se determina a trei miscare(daca merge la stanga, dreapta etc), si executa miscarea
                    if (movement3 == 1)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 2)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 3)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Left += 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Top -= 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Left -= 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Top += 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 4)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                }

                //daca miscarea 4 n a fost executata, atunci
                else if (Miscare4.Image != null)
                {
                    //se determina a patra miscare(daca merge la stanga, dreapta etc), si executa miscarea
                    if (movement4 == 1)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 2)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 3)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Left += 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Top -= 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Left -= 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Top += 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 4)
                    {
                        if (AngleBlue == 0)
                        {
                            bluemouse.Image = TheBlueMouse.Images[2];
                            AngleBlue = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 90)
                        {
                            bluemouse.Image = TheBlueMouse.Images[3];
                            AngleBlue = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 180)
                        {
                            bluemouse.Image = TheBlueMouse.Images[0];
                            AngleBlue = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (AngleBlue == 270)
                        {
                            bluemouse.Image = TheBlueMouse.Images[1];
                            AngleBlue = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                }
            }

            //daca secventa de miscare s a terminat, atunci
            if (NbMove == 0)
            {
                //daca cumva soarecele ajunge in afara platoului de joc, se va repune la locul lui initial de inainte de prima miscare
                if (bluemouse.Left < 0 || bluemouse.Left > 616 || bluemouse.Top < 0 || bluemouse.Top > 595)
                {
                    eroare = 4;
                    bluemouse.Left = CordX;
                    bluemouse.Top = CordY;
                    AngleBlue = angle;
                    NbMove = 0;
                    switch (angle)
                    {
                        case 0:
                            bluemouse.Image = TheBlueMouse.Images[0];
                            break;
                        case 90:
                            bluemouse.Image = TheBlueMouse.Images[1];
                            break;
                        case 180:
                            bluemouse.Image = TheBlueMouse.Images[2];
                            break;
                        case 270:
                            bluemouse.Image = TheBlueMouse.Images[3];
                            break;
                    }
                    ExeCurs = false;
                    TimerBlueMouse.Stop();
                    TimerEroare.Start();
                }
                //daca s a terminat jocul si au fost mai multi castigatori si s a ales sa se joace cine ajunge primul in casa lui si ajunge albastru in casa lui, atunci
                else if(Public_Date.WantCompetition == true && Public_Date.blueWin == true)
                {
                    NbDice = 0;
                    TimerBlueMouse.Stop();
                    WantComp();
                }
                //se pregateste valorile pt urmatorul jucator
                else
                {
                    TimerBlueMouse.Stop();
                    NbDice = 0;
                    //sa se afle daca s a oprit pe o bucata de cascaval sau un alt soarece daca modul canibal a fost activat
                    ActionCasCani();
                    ExeCurs = false;
                    player = 2;
                    NextPlayer = true;
                    NumarZar.Image = null;

                    //randul soarecelui verde, asa ca el este adus in prin plan
                    greenmouse.BringToFront();
                }
            }

        }

        private void MoveGreenMouse(object sender, EventArgs e)
        {
            if (NbMove != 0)
            {
                if (Miscare1.Image != null)
                {
                    if (movement1 == 1)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 2)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 3)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Left += 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Top -= 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Left -= 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Top += 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 4)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare2.Image != null)
                {
                    if (movement2 == 1)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 2)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 3)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Left += 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Top -= 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Left -= 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Top += 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 4)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare3.Image != null)
                {
                    if (movement3 == 1)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 2)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 3)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Left += 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Top -= 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Left -= 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Top += 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 4)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare4.Image != null)
                {
                    if (movement4 == 1)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 2)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 3)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Left += 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Top -= 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Left -= 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Top += 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 4)
                    {
                        if (IncliGreen == 0)
                        {
                            greenmouse.Image = TheGreenMouse.Images[2];
                            IncliGreen = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 90)
                        {
                            greenmouse.Image = TheGreenMouse.Images[3];
                            IncliGreen = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 180)
                        {
                            greenmouse.Image = TheGreenMouse.Images[0];
                            IncliGreen = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliGreen == 270)
                        {
                            greenmouse.Image = TheGreenMouse.Images[1];
                            IncliGreen = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                }
            }
            if (NbMove == 0)
            {
                if (greenmouse.Left < 0 || greenmouse.Left > 616 || greenmouse.Top < 0 || greenmouse.Top > 595)
                {
                    eroare = 4;
                    greenmouse.Left = CordX;
                    greenmouse.Top = CordY;
                    IncliGreen = angle;
                    NbMove = 0;
                    switch (angle)
                    {
                        case 0:
                            greenmouse.Image = TheGreenMouse.Images[0];
                            break;
                        case 90:
                            greenmouse.Image = TheGreenMouse.Images[1];
                            break;
                        case 180:
                            greenmouse.Image = TheGreenMouse.Images[2];
                            break;
                        case 270:
                            greenmouse.Image = TheGreenMouse.Images[3];
                            break;
                    }
                    ExeCurs = false;
                    TimerGreenMouse.Stop();
                    TimerEroare.Start();
                }
                else if (Public_Date.WantCompetition == true && Public_Date.greenWin == true)
                {
                    TimerGreenMouse.Stop();
                    NbDice = 0;
                    WantComp();
                }
                else
                {
                    //daca numarul de jucatori este mai mare decat 3 atunci urmeaza soarecele mov, daca nu atunci cel albastru
                    if (NbPlayers >= 3)
                    {
                        TimerGreenMouse.Stop();
                        NbDice = 0;
                        ActionCasCani();
                        ExeCurs = false;
                        player = 3;
                        NextPlayer = true;
                        NumarZar.Image = null;

                        purplemouse.BringToFront();
                    }
                    else
                    {
                        TimerGreenMouse.Stop();
                        NbDice = 0;
                        ActionCasCani();
                        ExeCurs = false;
                        player = 1;
                        NextPlayer = true;
                        NumarZar.Image = null;

                        bluemouse.BringToFront();
                    }
                }
            }
        }

        private void MovePurpleMouse(object sender, EventArgs e)
        {
            purplemouse.BringToFront();

            if (NbMove != 0)
            {
                if (Miscare1.Image != null)
                {
                    if (movement1 == 1)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 2)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 3)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Left += 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Top -= 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Left -= 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Top += 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 4)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare2.Image != null)
                {
                    if (movement2 == 1)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 2)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 3)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Left += 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Top -= 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Left -= 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Top += 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 4)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare3.Image != null)
                {
                    if (movement3 == 1)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 2)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 3)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Left += 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Top -= 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Left -= 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Top += 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 4)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare4.Image != null)
                {
                    if (movement4 == 1)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 2)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 3)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Left += 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Top -= 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Left -= 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Top += 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 4)
                    {
                        if (IncliPurple == 0)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            IncliPurple = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 90)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            IncliPurple = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 180)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            IncliPurple = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliPurple == 270)
                        {
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            IncliPurple = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                }
            }
            if (NbMove == 0)
            {
                if (purplemouse.Left < 0 || purplemouse.Left > 616 || purplemouse.Top < 0 || purplemouse.Top > 595)
                {
                    eroare = 4;
                    purplemouse.Left = CordX;
                    purplemouse.Top = CordY;
                    IncliPurple = angle;
                    NbMove = 0;
                    switch (angle)
                    {
                        case 0:
                            purplemouse.Image = ThePurpleMouse.Images[0];
                            break;
                        case 90:
                            purplemouse.Image = ThePurpleMouse.Images[1];
                            break;
                        case 180:
                            purplemouse.Image = ThePurpleMouse.Images[2];
                            break;
                        case 270:
                            purplemouse.Image = ThePurpleMouse.Images[3];
                            break;
                    }
                    ExeCurs = false;
                    TimerPurpleMouse.Stop();
                    TimerEroare.Start();
                }
                else if (Public_Date.WantCompetition == true && Public_Date.purpleWin == true)
                {
                    TimerPurpleMouse.Stop();
                    NbDice = 0;
                    WantComp();
                }
                else
                {
                    //daca sunt 4 jucatori, urmatorul jucator este soarecele rosu, daca nu atunci este soarecele albastru
                    if (NbPlayers == 4)
                    {
                        TimerPurpleMouse.Stop();
                        NbDice = 0;
                        ActionCasCani();
                        ExeCurs = false;
                        player = 4;
                        NextPlayer = true;
                        NumarZar.Image = null;

                        redmouse.BringToFront();
                    }
                    else
                    {
                        TimerPurpleMouse.Stop();
                        NbDice = 0;
                        ActionCasCani();
                        ExeCurs = false;
                        player = 1;
                        NextPlayer = true;
                        NumarZar.Image = null;

                        bluemouse.BringToFront();
                    }
                }
            }
            

        }

        private void MoveRedMouse(object sender, EventArgs e)
        {
            redmouse.BringToFront();

            if (NbMove != 0)
            {
                if (Miscare1.Image != null)
                {
                    if (movement1 == 1)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 2)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 3)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Left += 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Top -= 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Left -= 88;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Top += 85;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement1 == 4)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare1.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare2.Image != null)
                {
                    if (movement2 == 1)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 2)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 3)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Left += 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Top -= 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Left -= 88;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Top += 85;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement2 == 4)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare2.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare3.Image != null)
                {
                    if (movement3 == 1)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 2)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 3)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Left += 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Top -= 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Left -= 88;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Top += 85;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement3 == 4)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare3.Image = null;
                            NbMove--;
                        }
                    }
                }

                else if (Miscare4.Image != null)
                {
                    if (movement4 == 1)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 2)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 3)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Left += 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Top -= 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Left -= 88;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Top += 85;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                    else if (movement4 == 4)
                    {
                        if (IncliRed == 0)
                        {
                            redmouse.Image = TheRedMouse.Images[2];
                            IncliRed = 180;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 90)
                        {
                            redmouse.Image = TheRedMouse.Images[3];
                            IncliRed = 270;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 180)
                        {
                            redmouse.Image = TheRedMouse.Images[0];
                            IncliRed = 0;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                        else if (IncliRed == 270)
                        {
                            redmouse.Image = TheRedMouse.Images[1];
                            IncliRed = 90;
                            Miscare4.Image = null;
                            NbMove--;
                        }
                    }
                }
            }
            if (NbMove == 0)
            {
                if (redmouse.Left < 0 || redmouse.Left > 616 || redmouse.Top < 0 || redmouse.Top > 595)
                {
                    eroare = 4;
                    redmouse.Left = CordX;
                    redmouse.Top = CordY;
                    IncliRed = angle;
                    NbMove = 0;
                    switch (angle)
                    {
                        case 0:
                            redmouse.Image = TheRedMouse.Images[0];
                            break;
                        case 90:
                            redmouse.Image = TheRedMouse.Images[1];
                            break;
                        case 180:
                            redmouse.Image = TheRedMouse.Images[2];
                            break;
                        case 270:
                            redmouse.Image = TheRedMouse.Images[3];
                            break;
                    }
                    ExeCurs = false;
                    TimerRedMouse.Stop();
                    TimerEroare.Start();
                }
                else if (Public_Date.WantCompetition == true && Public_Date.redWin == true)
                {
                    TimerRedMouse.Stop();
                    NbDice = 0;
                    WantComp();
                }
                else
                {
                    TimerRedMouse.Stop();
                    NbDice = 0;
                    ActionCasCani();
                    ExeCurs = false;
                    player = 1;
                    NextPlayer = true;
                    NumarZar.Image = null;

                    bluemouse.BringToFront();
                }
            }
        }

        private void ActionCasCani()
        {
            //verificam daca a aterizat pe o bucata de cascaval. Daca da, atunci ii se adauga un punct si dispare bucata mancata

            //initalizarea unei variabile care permite sa stim daca s a soarecele pe o bucata de cascaval
            bool etresurfro = false;

            //parcurgem lista de bucatile de cascaval
            for (int i = 1; i <= 12; i++)
            {
                //daca pozitia soarecelui si cea a bucatei de cascaval coincide, si cascavalul respectiv n a mai fost mancat si joaca jucatorul 1, atunci
                if (Cheese[i].Top == bluemouse.Top && Cheese[i].Left == bluemouse.Left && Cheese[i].Image != null && player == 1)
                {
                    //dispare bucata de cascaval pe care s a oprit soarecele, se scade numarul total de bucati de cascaval, jucatorul albastru primeste o bucata in plus la contor, se scrie dupa jucator numarul total de bucati la contor
                    Cheese[i].Image = null;
                    NbCheese--;
                    CheeseBlue++;
                    lbplayer1.Text = Public_Date.player1 + " " + Convert.ToString(CheeseBlue);
                    etresurfro = true;

                    //daca modul teleport este bifat, atunci se executa programul de teleportare a bucatilor de cascaval
                    if (teleportmode == true)
                    {
                        TouchTeleport(remainder);
                        remainder = 1;
                    }
                    //se iese din for, deoarece nu mai are rost sa l parcurgem fiind ca nu ne putem afla pe 2 bucati in acelas timp
                    break;
                }
                //lafel ca primul if doar ca pt soarecele verde
                else if (Cheese[i].Top == greenmouse.Top && Cheese[i].Left == greenmouse.Left && Cheese[i].Image != null && player == 2)
                {
                    Cheese[i].Image = null;
                    NbCheese--;
                    CheeseGreen++;
                    lbplayer2.Text = Public_Date.player2 + " " + Convert.ToString(CheeseGreen);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(remainder);
                        remainder = 1;
                    }
                    break;
                }
                else if (Cheese[i].Top == purplemouse.Top && Cheese[i].Left == purplemouse.Left && Cheese[i].Image != null && player == 3)
                {
                    Cheese[i].Image = null;
                    NbCheese--;
                    CheesePurple++;
                    lbplayer3.Text = Public_Date.player3 + " " + Convert.ToString(CheesePurple);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(remainder);
                        remainder = 1;
                    }
                    break;
                }
                else if (Cheese[i].Top == redmouse.Top && Cheese[i].Left == redmouse.Left && Cheese[i].Image != null && player == 4)
                {
                    Cheese[i].Image = null;
                    NbCheese--;
                    CheeseRed++;
                    lbplayer4.Text = Public_Date.player4 + " " + Convert.ToString(CheeseRed);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(remainder);
                        remainder = 1;
                    }
                    break;
                }
            }

            if(canimode == true && etresurfro == false)
            {
                //daca modul canibal a fost activat si nu ne aflam pe o bucata de cascaval
                //atunci verificam daca nu s a terminat secventa de iscare pe un alt soarece
                //daca joaca jucatorul 1 si pica pe unul dintre ceilalti soareci, trimite soarecele pe care a pica la patratul sau de start
                if(player == 1)
                {
                    if (bluemouse.Top == greenmouse.Top && bluemouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if(bluemouse.Top == purplemouse.Top && bluemouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                    else if(bluemouse.Top == redmouse.Top && bluemouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 2)
                {
                    if(greenmouse.Top == bluemouse.Top && greenmouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if(greenmouse.Top == purplemouse.Top && greenmouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                    else if(greenmouse.Top == redmouse.Top && greenmouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 3)
                {
                    if(purplemouse.Top == bluemouse.Top && purplemouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if(purplemouse.Top == greenmouse.Top && purplemouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if(purplemouse.Top == redmouse.Top && purplemouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 4)
                {
                    if (redmouse.Top == bluemouse.Top && redmouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if (redmouse.Top == greenmouse.Top && redmouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if (redmouse.Top == purplemouse.Top && redmouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                }
            }

            //daca nu mai sunt bucati de cascavali in joc, atunci se spune "end"(the game is over)
            if (NbCheese == 0)
            {
                //in loc de titlu, se va scrie "The game is over", pe partea dreapta a platoului
                //se apeleaza functia care determina castigatorul/castigatorii
                //partida s a terminat
                lbTitlu.Text = "T\rH\rE\r\rG\rA\rM\rE\r\rI\rS\r\rO\rV\rE\rR";
                WhoWin();
                gameon = false;

                //se afiseaza pagina/formularul cu soarecele care a castigat
                //daca se doreste competitie finala in caz de egalitate, jocul porneste inapoi
                WinnerAnnoucement.ShowDialog();
                if (Public_Date.WantCompetition == true)
                {
                    gameon = true;
                    LabelMesajFirstHome.Text = "GO! GO! GO! The first who get home win ☺";
                }
            }
        }

        private void WhoWin()
        {
            //Se determina cine a castigat
            if(NbPlayers == 2)
            {
                if(CheeseBlue == CheeseGreen)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;

                }
                else if(CheeseBlue > CheeseGreen)
                {
                    Public_Date.blueWin = true;
                    Public_Date.HasWin = 1;
                }
                else if(CheeseGreen > CheeseBlue)
                {
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 1;
                }
            }
            else if(NbPlayers == 3)
            {
                if (CheeseBlue == CheeseGreen && CheeseBlue == CheesePurple)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 3;
                    Public_Date.Many = true;
                }
                else if(CheeseBlue == CheeseGreen)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if(CheeseBlue == CheesePurple)
                {
                    Public_Date.blueWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseGreen == CheesePurple)
                {
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue > CheeseGreen && CheeseBlue > CheesePurple)
                {
                    Public_Date.blueWin = true;
                    Public_Date.HasWin = 1;
                }
                else if (CheeseGreen > CheeseBlue && CheeseGreen > CheesePurple)
                {
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 1;
                }
                else if (CheesePurple > CheeseBlue && CheesePurple > CheeseGreen)
                {
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 1;
                }
            }
            else if(NbPlayers == 4)
            {
                if(CheeseBlue == CheeseGreen && CheeseBlue == CheesePurple && CheeseBlue == CheeseRed)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 4;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheeseGreen && CheeseBlue == CheesePurple)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 3;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheeseGreen && CheeseBlue == CheeseRed)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 3;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheesePurple && CheeseBlue == CheeseRed)
                {
                    Public_Date.blueWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 3;
                    Public_Date.Many = true;
                }
                else if (CheeseGreen == CheesePurple && CheeseGreen == CheeseRed)
                {
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 3;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheeseGreen)
                {
                    Public_Date.blueWin = true;
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheesePurple)
                {
                    Public_Date.blueWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue == CheeseRed)
                {
                    Public_Date.blueWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseGreen == CheesePurple)
                {
                    Public_Date.greenWin = true;
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseGreen == CheeseRed)
                {
                    Public_Date.greenWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheesePurple == CheeseRed)
                {
                    Public_Date.purpleWin = true;
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 2;
                    Public_Date.Many = true;
                }
                else if (CheeseBlue > CheeseGreen && CheeseBlue > CheesePurple && CheeseBlue > CheeseRed)
                {
                    Public_Date.blueWin = true;
                    Public_Date.HasWin = 1;
                }
                else if (CheeseGreen > CheeseBlue && CheeseGreen > CheesePurple && CheeseGreen > CheeseRed)
                {
                    Public_Date.greenWin = true;
                    Public_Date.HasWin = 1;
                }
                else if (CheesePurple > CheeseBlue && CheesePurple > CheeseGreen && CheesePurple > CheeseRed)
                {
                    Public_Date.purpleWin = true;
                    Public_Date.HasWin = 1;
                }
                else if (CheeseRed > CheeseBlue && CheeseRed > CheeseGreen && CheeseRed > CheesePurple)
                {
                    Public_Date.redWin = true;
                    Public_Date.HasWin = 1;
                }
            }
        }

        private void WantComp()
        {
            //se verifica pentru la fiecare tura a jucatorului daca a ajuns pe pozitia lui de start si se afiseaza din nou pagina/formularul unde se va afisa singurul castigator
            if (player == 1)
            {
                if (bluemouse.Left == 0 && bluemouse.Top == 0)
                {
                    Public_Date.Many = false;
                    LabelMesajFirstHome.Text = null;
                    Public_Date.greenWin = false;
                    Public_Date.purpleWin = false;
                    Public_Date.redWin = false;
                    WinnerAnnoucement.ShowDialog();
                    gameon = false;
                }
                else
                {
                    ExeCurs = false;
                    player = 2;
                    NextPlayer = true;
                    NumarZar.Image = null;
                    greenmouse.BringToFront();
                }
            }
            else if (player == 2)
            {
                if (greenmouse.Left == 616 && greenmouse.Top == 0)
                {
                    Public_Date.Many = false;
                    LabelMesajFirstHome.Text = null;
                    Public_Date.blueWin = false;
                    Public_Date.purpleWin = false;
                    Public_Date.redWin = false;
                    WinnerAnnoucement.ShowDialog();
                    gameon = false;
                }
                else
                {
                    if (NbPlayers >= 3)
                    {
                        ExeCurs = false;
                        player = 3;
                        NextPlayer = true;
                        NumarZar.Image = null;
                        purplemouse.BringToFront();
                    }
                    else
                    {
                        ExeCurs = false;
                        player = 1;
                        NextPlayer = true;
                        NumarZar.Image = null;
                        bluemouse.BringToFront();
                    }
                }
            }
            else if (player == 3)
            {
                if (purplemouse.Left == 0 && purplemouse.Top == 595)
                {
                    Public_Date.Many = false;
                    LabelMesajFirstHome.Text = null;
                    Public_Date.blueWin = false;
                    Public_Date.greenWin = false;
                    Public_Date.redWin = false;
                    WinnerAnnoucement.ShowDialog();
                    gameon = false;
                }
                else
                {
                    if (NbPlayers == 4)
                    {
                        ExeCurs = false;
                        player = 4;
                        NextPlayer = true;
                        NumarZar.Image = null;
                        redmouse.BringToFront();
                    }
                    else
                    {
                        ExeCurs = false;
                        player = 1;
                        NextPlayer = true;
                        NumarZar.Image = null;
                        bluemouse.BringToFront();
                    }
                }
            }
            else if(player == 4)
            {
                if (redmouse.Left == 616 && redmouse.Top == 595)
                {
                    Public_Date.Many = false;
                    LabelMesajFirstHome.Text = null;
                    Public_Date.blueWin = false;
                    Public_Date.greenWin = false;
                    Public_Date.purpleWin = false;
                    WinnerAnnoucement.ShowDialog();
                    gameon = false;
                }
                else
                {
                    ExeCurs = false;
                    player = 1;
                    NextPlayer = true;
                    NumarZar.Image = null;
                    bluemouse.BringToFront();
                }
            }
        }

        //initializarea unei functie remainder care permite, in cazul in care functia random da aceasi pozitie a 2 bucati de cascaval, reluam for de la pozitia "remainder" pt a ii da o noua pozitie
        int remainder = 1;

        //declararea unei functie care genereaza aleatoriu o noua pozitie pt bucatile de cascaval, daca modul teleport a fost bifat
        Random NewLocTeleport = new Random();
        int Left = 0, Top = 0;

        private void TouchTeleport(int remainder)
        {
            for(int i = remainder; i <= 12; i++)
            {
                //o noua pozitie este aleasa random pt bucatile de cascaval
                Left = NewLocTeleport.Next(0, NbPositionX.Length);
                Top = NewLocTeleport.Next(0, NbPositionY.Length);

                //noua pozitie aleasa le este dat bucatilor
                Cheese[i].Left = NbPositionX[Left];
                Cheese[i].Top = NbPositionY[Top];

                //Verificam ca noua pozitie a bucatilor de cascaval nu au aceasi pozitie ca unui alt bucat sau a unui soarece
                for (int j = 1; j <= 12; j++)
                {
                    if (i != j && (Cheese[i].Left == Cheese[j].Left && Cheese[i].Top == Cheese[j].Top || Cheese[i].Left == bluemouse.Left && Cheese[i].Top == bluemouse.Top || Cheese[i].Left == greenmouse.Left && Cheese[i].Top == greenmouse.Top || Cheese[i].Left == purplemouse.Left && Cheese[i].Top == purplemouse.Top || Cheese[i].Left == redmouse.Left && Cheese[i].Top == redmouse.Top))
                    {
                        remainder = i;
                        SameLocTeleport(remainder);
                    }
                    else if (Cheese[i].Left == 0 && Cheese[i].Top == 0 || Cheese[i].Left == 616 && Cheese[i].Top == 0 || Cheese[i].Left == 0 && Cheese[i].Top == 595 || Cheese[i].Left == 616 && Cheese[i].Top == 595)
                    {
                        remainder = i;
                        SameLocTeleport(remainder);
                    }
                }
            }
        }

        private void SameLocTeleport(int a)
        {
            //declaram o variabila care ne permite sa stim daca noua locatie aleasa este aceeasi ca alta bucata sau soarece
            bool SameToo = false;

            Left = NewLocTeleport.Next(0, NbPositionX.Length);
            Top = NewLocTeleport.Next(0, NbPositionY.Length);

            Cheese[a].Left = NbPositionX[Left];
            Cheese[a].Top = NbPositionY[Top];

            //repetam SameLocTeleport pana cand pozitia nu mai este aceasi cu alta bucata sau soarece
            for (int i = 1; i <= 12; i++)
                if (a != i && (Cheese[a].Left == Cheese[i].Left && Cheese[a].Top == Cheese[i].Top || Cheese[a].Left == bluemouse.Left && Cheese[a].Top == bluemouse.Top || Cheese[a].Left == greenmouse.Left && Cheese[a].Top == greenmouse.Top || Cheese[a].Left == purplemouse.Left && Cheese[a].Top == purplemouse.Top || Cheese[a].Left == redmouse.Left && Cheese[a].Top == redmouse.Top))
                {
                    SameToo = true;
                    break;
                }
            if (Cheese[a].Left == 0 && Cheese[a].Top == 0 || Cheese[a].Left == 616 && Cheese[a].Top == 0 || Cheese[a].Left == 0 && Cheese[a].Top == 595 || Cheese[a].Left == 616 && Cheese[a].Top == 595)
                SameToo = true;

            if (SameToo == true)
                SameLocTeleport(a);
        }

        private void btnRUN_MouseMove(object sender, MouseEventArgs e)
        {
            //Pt a nu se suprapune textul de la buton cu cel din imaginea care apare cand se trece cu mouse-ul peste buton
            //Se sterge textul de la buton(se da valoarea null la text), si apare pe buton imaginea
            btnRUN.Text = null;
            
            //se determina a carui jucator este randul pt afisarea culorii lui pe butonul RUN
            if (player == 1)
                btnRUN.BackgroundImage = Properties.Resources.Blue_Run_Button;
            else if (player == 2)
                btnRUN.BackgroundImage = Properties.Resources.Green_Run_Button;
            else if (player == 3)
                btnRUN.BackgroundImage = Properties.Resources.Purple_Run_Button;
            else if (player == 4)
                btnRUN.BackgroundImage = Properties.Resources.Red_Run_Button;
        }

        private void btnRUN_MouseLeave(object sender, EventArgs e)
        {
            //Pt a nu se suprapune textul de la buton cu cel din imaginea care apare cand se trece cu mouse-ul peste buton
            //Atunci cand mouse-ul paraseste butonul, imaginea dispare si apare textul inapoi
            btnRUN.Text = "RUN";

            //Se sterge culoarea jucatorului a carui este randul atunci cand mouse ul paraseste butonul RUN
            btnRUN.BackgroundImage = null;
        }

        private void error(object sender, EventArgs e)
        {
            if (Timp == 0)
            {
                TimerEroare.Stop();
                Timp = 3;
                LabelMesajEroare.Text = null;
            }
            else
            {
                //se afiseaza mesajul potrivit valoarei eroare
                switch (eroare)
                {
                    case 1:

                        LabelMesajEroare.Text = "! The dice has already\r\nbeen cast !";

                        break;

                    case 2:

                        LabelMesajEroare.Text = "!  The dice hasn't stopped  !";

                        break;

                    case 3:

                        LabelMesajEroare.Text = "!  The number of movements\r\nchosen doesn\'t correspond \r\nwith those shown on the dice !";

                        break;

                    case 4:

                        LabelMesajEroare.Text = "!  This action is impossible  !";

                        break;
                    case 5:

                        LabelMesajEroare.Text = "!  The sequence is running,\r   cannot be changed!";

                        break;
                    case 6:

                        LabelMesajEroare.Text = "!  The sequence is running,\r the dice cannot be rolled!";

                        break;
                }
                Timp--;
            }
        }

        /*
            Erorile:
                - eroarea 1 = zarul a fost deja aruncat, nu mai poate fi aruncat(jucatorul nu poate sa l arunce doar decat o singura data)
                - eroarea 2 = zarul este in curs de aruncare, jucatorul nu poate nici sa joace nici sa aleaga miscarile pe care ar vrea sa le faca
                - eroarea 3 = numarul de miscari nu corespunde cu cel facut de zar
                - eroarea 4 = soarecele a iesit de pe platou
                - eroarea 5 = secventa de miscare este in curs de executare, nu poate fi reinitilizata
                - eroarea 6 = secventa de miscare este in curs de executare, nu poate fi aruncat zarul
        */
    }
}
