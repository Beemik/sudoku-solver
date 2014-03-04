using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace main
{
    public partial class Window : Form
    {
        board sudo = new board();

        public Window()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Window_FormClosing);    //dodanie eventu na obsługę zamknięcia programu
        }
        
        /*
         * Rozpoznawanie liczb od 1 do 9 i uniemożliwienie wpisania innego znaku.
         * Zwraca wpisaną liczbę lub zero w przypadku złej wartości.
        */
        int OnlySingleNumber(TextBox boxText)
        {
            int count = 0;
            Exception SudokuSingleNumberException = new Exception();
            try
            {
                count = System.Convert.ToInt16(boxText.Text);       //konwertuje text na liczbę
                if (count < 1 || count > 9)                         //sprawdza czy zakres się zgadza
                {
                    boxText.Clear();                                //jeśli nie to czyści textBox
                    count = 0;
                }
            }
            catch (System.FormatException)                          //jeśli się nie zgadza wprowadzony format
            {                                                       //to czyści textBox
                boxText.Clear();
                return 0;
            }
            return count;                                           //zwraca poprawnie wpisaną daną
        }

        /*
         * Wpisuje wartości do textBoxów w przypadku clear = false. Przepisuje wartości z tablicy do odpowiadających textBoxów.
         * W przypadku clear = true czyści wszystkie textBoxy.
        */
        void drawGraphics(bool clear)
        {
            if(clear==false)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        string txtSelected = "textBox" + i + j;
                        var txt = this.Controls.Find(txtSelected, true);
                        if (sudo.tab[i, j].value != 0 && String.IsNullOrEmpty(((TextBox)txt[0]).Text) == true )        //wpisanie do textBoxów cyfr z tablicy
                        {
                            ((TextBox)txt[0]).Text = System.Convert.ToString(sudo.tab[i, j].value);
                            if (sudo.tab[i, j].visibility == false)
                            {
                                ((TextBox)txt[0]).ReadOnly = true;
                                ((TextBox)txt[0]).Font = new Font(((TextBox)txt[0]).Font, FontStyle.Bold);
                            }   
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox)       //czyszczenie planszy
                    {
                        ((TextBox)c).Clear();
                        ((TextBox)c).ReadOnly = false;
                        c.Font = new Font(c.Font, FontStyle.Regular);
                    }
                }
            }
            congratulations();
            sudo.saved = false;
        }

        void congratulations()
        {
            if(sudo.isFull()==true && checkForFaults()==false)
                MessageBox.Show("Gratulacje, ukonczyłeś poprawnie planszę!","Koniec");
        }

        /*
         * Konwersja stringa do odpowiedniego textBox'a
        */
        TextBox stringToTextBox(int i, int j)
        {
            string txtSelected = "textBox" + i + j;             //podany string
            var txt = this.Controls.Find(txtSelected, true);    //sprawdza czy jest taka kontrolka
            TextBox box = ((TextBox)txt[0]);
            return box;                                         //zwraca odpowiedni textBox
        }

        /*
         * Sprawdzenie poprawności danych
        */
        bool checkForFaults()
        {
            bool fault = false;
            bool end = false;
            if (helperCheckBox.Checked == false)                                            //jesli odznaczono pomoc
                end = true;                                                                 //ustaw flagę
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox box = stringToTextBox(i, j);
                    if (String.IsNullOrEmpty(box.Text) == false)                            //jeśli textBox nie jest pusty
                    {
                        bool ok = sudo.checkRCS(i, j, System.Convert.ToInt16(box.Text));    //sprawdza czy poprawna cyfra
                        if (end == true && ok == false)
                        {
                            box.ForeColor = System.Drawing.Color.Black;
                            fault = true;
                        }
                        else if (ok == false && end == false)                                    //jeśli nie to zaznacza
                        {
                            box.ForeColor = System.Drawing.Color.Red;
                            ok = true;
                        }
                        else
                            box.ForeColor = System.Drawing.Color.Black;                     //jeśli nie to ustawia czarny
                    }
                }
            }
            return fault;
        }

        /*
         * Obsługa textBoxów
        */
        private void textBox00_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox00);        //odczytanie wpisanej cyfry
            sudo.tab[0, 0].value = count;                   //wpisanie cyfry
            checkForFaults();                               //sprawdzenie poprawności wpisanych liczb, czy nie powtarzają się
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox01_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox01);
            sudo.tab[0, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox02_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox02);
            sudo.tab[0, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox03_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox03);
            sudo.tab[0, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox04_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox04);
            sudo.tab[0, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox05_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox05);
            sudo.tab[0, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox06_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox06);
            sudo.tab[0, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox07_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox07);
            sudo.tab[0, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox08_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox08);
            sudo.tab[0, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox10);
            sudo.tab[1, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox11);
            sudo.tab[1, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox12);
            sudo.tab[1, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox13);
            sudo.tab[1, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox14);
            sudo.tab[1, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox15);
            sudo.tab[1, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox16);
            sudo.tab[1, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox17);
            sudo.tab[1, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox18);
            sudo.tab[1, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox20);
            sudo.tab[2, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox21);
            sudo.tab[2, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox22);
            sudo.tab[2, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox23);
            sudo.tab[2, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox24);
            sudo.tab[2, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox25);
            sudo.tab[2, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox26);
            sudo.tab[2, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox27);
            sudo.tab[2, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox28);
            sudo.tab[2, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox30);
            sudo.tab[3, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox31);
            sudo.tab[3, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox32);
            sudo.tab[3, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox33);
            sudo.tab[3, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox34);
            sudo.tab[3, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox35);
            sudo.tab[3, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox36);
            sudo.tab[3, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox37);
            sudo.tab[3, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox38);
            sudo.tab[3, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox40);
            sudo.tab[4, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox41);
            sudo.tab[4, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox42);
            sudo.tab[4, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox43);
            sudo.tab[4, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox44);
            sudo.tab[4, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox45);
            sudo.tab[4, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox46);
            sudo.tab[4, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox47_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox47);
            sudo.tab[4, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox48);
            sudo.tab[4, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox50);
            sudo.tab[5, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox51_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox51);
            sudo.tab[5, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox52);
            sudo.tab[5, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox53_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox53);
            sudo.tab[5, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox54_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox54);
            sudo.tab[5, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox55);
            sudo.tab[5, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox56);
            sudo.tab[5, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox57_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox57);
            sudo.tab[5, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox58_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox58);
            sudo.tab[5, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox60_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox60);
            sudo.tab[6, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox61_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox61);
            sudo.tab[6, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox62_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox62);
            sudo.tab[6, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox63_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox63);
            sudo.tab[6, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox64_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox64);
            sudo.tab[6, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox65_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox65);
            sudo.tab[6, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox66_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox66);
            sudo.tab[6, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox67_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox67);
            sudo.tab[6, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox68_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox68);
            sudo.tab[6, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox70_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox70);
            sudo.tab[7, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox71_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox71);
            sudo.tab[7, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox72_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox72);
            sudo.tab[7, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox73_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox73);
            sudo.tab[7, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox74_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox74);
            sudo.tab[7, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox75_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox75);
            sudo.tab[7, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox76_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox76);
            sudo.tab[7, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox77_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox77);
            sudo.tab[7, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox78_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox78);
            sudo.tab[7, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox80_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox80);
            sudo.tab[8, 0].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox81_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox81);
            sudo.tab[8, 1].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox82_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox82);
            sudo.tab[8, 2].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox83_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox83);
            sudo.tab[8, 3].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox84_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox84);
            sudo.tab[8, 4].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox85_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox85);
            sudo.tab[8, 5].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox86_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox86);
            sudo.tab[8, 6].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox87_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox87);
            sudo.tab[8, 7].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        private void textBox88_TextChanged(object sender, EventArgs e)
        {
            int count = OnlySingleNumber(textBox88);
            sudo.tab[8, 8].value = count;
            checkForFaults();
            sudo.saved = false;
            if (!solveButton.Capture)
                congratulations();
        }

        /*
         * Obsługa przycisku wyczyść
        */
        private void clearButton_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (sudo.isEmpty() == false && sudo.saved == false)
            {
                if (MessageBox.Show("Czy chcesz zapisać stan gry?", "Zapis", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ok = showSaveWindow();
            }
            if (ok == true)
            {
                sudo.clear();               //wyczyszczenie planszy sudoku
                drawGraphics(true);         //wyczyszczenie gui planszy sudoku
            }
        }

        /*
         * Obsługa przycisku generuj
        */
        private void generateButton_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (sudo.isEmpty() == false && sudo.saved == false)
            {
                if (MessageBox.Show("Czy chcesz zapisać przed utratą danych?", "Zapisz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ok = showSaveWindow();
            }
            if (ok == true)
            {
                Levels window2 = new Levels();
                window2.ShowDialog();
                int fieldToErase = window2.returnFieldToErase();
                if (fieldToErase != 81)                         //w przypadku, gdy ktoś nacisnął generuj, a później krzyżyk to nie czyść
                {                                               //czyść tylko jak chcesz generować nową planszę
                    sudo.clear();
                    drawGraphics(true);
                }
                sudo = unsaveFromFile("startBoard.bin");
                sudo.generateBoard();
                sudo.randomWithoutRepetition(81, fieldToErase);
                drawGraphics(false);
            }
        }

        /*
         * Obsługa przycisku rozwiąż
        */
        private void solveButton_Click(object sender, EventArgs e)
        {
            if (sudo.isEmpty() == false && sudo.checkCorrectness() == true)
            {
                bool ok = sudo.solveSudoku();                   //rozwiązanie sudoku
                if (ok == false)
                    MessageBox.Show("Niestety masz źle wypełnioną planszę, nie da się jej ułożyć poprawnie. Spróbuj jeszcze raz.");
                else
                    drawGraphics(false);                        //zaprezentowanie rozwiązania na ekranie
            }
            else if (sudo.checkCorrectness() == false)
                MessageBox.Show("Źle uzupełniona plansza.");
            else
                MessageBox.Show("Pusta plansza. Uzupełnij kilka pól i spróbuj ponownie.");
        }

        /*
         * Obsługa podświetlania błędnych liczb
        */
        private void helperCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkForFaults();               //sprawdza poprawność wpisanych danych
        }

        /*
         * Zapisanie pliku
         * sudo - plansza do zapisania
         * source - folder i nazwa pliku do zapisania
        */
        void saveToFile(board sudo, string source)
        {
            FileStream binFile = new FileStream(source, FileMode.Create);   //plik do zapisu
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(binFile, sudo);                             //serializacja klasy
            binFile.Close();                                                //zamknięcie pliku
            sudo.saved = true;
        }

        /*
         * Wczytanie pliku
         * source - źródło do odczytu
        */
        board unsaveFromFile(string source)
        {
            board s;
            try
            {
                FileStream binFile = new FileStream(source, FileMode.Open);
                BinaryFormatter binFormat = new BinaryFormatter();
                s = (board)binFormat.Deserialize(binFile);                  //odczytanie pliku
                binFile.Close();
            }
            catch (Exception)                                               //w przypadku podania złego pliku
            {                                                               //wyświetla odpowiedni komunikat
                MessageBox.Show("Nieprawidłowy plik.");
                return new board();                                         //zwraca pustą planszę
            }
            return s;                                                       //zwraca odczytaną poprawnie planszę
        }

        bool showSaveWindow()
        {
            SaveFileDialog sourceFile = new SaveFileDialog();
            sourceFile.Filter = "Pliki binarne (bin)|*.bin";
            if (sourceFile.ShowDialog() == DialogResult.OK)     //wyświetlenie okna
            {
                saveToFile(sudo, sourceFile.FileName);          //zapisanie planszy do wskazanego folderu z wybraną nazwą
                return true;
            }
            else
                return false;
        }

        /*
         * Obsługa przycisku zapisz
         * zapisywanie do pliku binarnego stanu gry
        */
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (sudo.isEmpty() == true && sudo.saved == false)      //w przypadku gdy jest pusta plansza to nic nie robi
            {
                return;
            }
            showSaveWindow();
        }

        /*
         * Obsługa przycisku wczytaj
        */
        private void unsaveButton_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (sudo.isEmpty() == false && sudo.saved == false)
            {
                if (MessageBox.Show("Czy chcesz zapisać stan gry?", "Zapis", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ok = showSaveWindow();
            }
            if (ok == true)
            {
                OpenFileDialog searchFile = new OpenFileDialog();
                searchFile.Filter = "Pliki binarne (bin)|*.bin";    //obsługa plików binarnych
                if (searchFile.ShowDialog() == DialogResult.OK)
                {
                    sudo.clear();                                   //wyczyszczenie planszy sudoku
                    drawGraphics(true);                             //wyczyszczenie gui planszy sudoku
                    sudo = unsaveFromFile(searchFile.FileName);
                    drawGraphics(false);                            //wyświetlenie zapisu na ekranie
                    sudo.saved = true;
                }
            }
        }

        /*
         * Jeśli naciśnięto przycisk zamknij to w przypadku jeżeli plansza nie jest pusta pyta czy chcemy zapisać.
        */
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sudo.isEmpty() == false && sudo.saved == false)     //jeśli plansza nie jest pusta
            {
                DialogResult exitWindow = new DialogResult();
                exitWindow = MessageBox.Show("Czy chcesz zapisać przed zamknięciem?", "Zamknij", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (exitWindow == DialogResult.Yes)                 //jeśli naciśnięto "tak"
                    showSaveWindow();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aplikacja wspomagająca rozwiązywanie sudoku.\n\nAutor: Maciej Manczyk (Beemik)\n\nW momencie, gdy nie będzie dało się generować planszy wypełnij poprawnie całą planszę sudoku i zapisz ją do pliku w folderze z grą nazwając: startBoard", "O programie");
        }
    }
}