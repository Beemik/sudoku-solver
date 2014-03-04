using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace main
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());
            //plansza sudo = new plansza();
            //sudo.solveSudoku();
            //for (int i = 0; i < 2; ++i)
            //{
            //    sudo.checkAll();
            //    sudo.enterNumbers();
            //    sudo.draw();
            //}
        }
    }
}

/*
 * Dla każdego pola wpisuje się jaka liczba może się w nim znajdować (tak jak pisanie ołówkiem).
 * Po wypełnieniu w ten sposób wszystkich pustych pók wpisuje na stałe wartości w tych polach,
 * w których jest tylko jedna wartość wpisana ołówkiem. Później wymazuje ołówek i wypełnia nim
 * puste pola po raz drugi. Jeżeli liczba pók się nie zmieni pomiędzy dwoma wypełnieniami ołówkniem to znaczy,
 * że są conajmniej dwa rozwiązania sudoku. Wtedy tam gdzie są tylko dwie cyfry ołówkiem wybieramy którąkolwiek.
 * Każde pole ma pole widoczności (wpisane, nie wpisane). Ołówkiem wypełniamy tylko ne wpisane.
*/