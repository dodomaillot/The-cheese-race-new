using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_cheese_race
{
    class Public_Date
    {
        //declar aici variabile care o sa mi permita sa transmita numele jucatorilor dintr un form in altu
        public static string player1 = "Player 1:";
        public static string player2 = "Player 2:";
        public static string player3 = "Player 3:";
        public static string player4 = "Player 4:";

        //variabila pt a confirma iesirea din joc
        //variabile pt final de partida sa stim daca se doreste sa se continuie sau nu
        //variabile care permite sa stim cine a castigat
        public static bool exit = false;
        public static bool Many = false;
        public static bool WantCompetition = false;
        public static bool blueWin = false;
        public static bool greenWin = false;
        public static bool purpleWin = false;
        public static bool redWin = false;
        public static int HasWin = 1;
    }
}
