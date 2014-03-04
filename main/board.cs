using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace main
{
    [Serializable]
    class board
    {
        const int N = 9;                            //wielkość planszy
        public field[,] tab;                        //plansza sudoku
        public bool saved;
        static List<field[,]> solutions;

        public board()                              //zainicjalizowanie planszy sudoku
        {
            tab = new field[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    tab[i, j] = new field();
                }
            }
            saved = false;
            solutions = new List<field[,]>();
        }

        field[,] CopyField(field[,] table)
        {
            field[,] tmp = new field[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    tmp[i, j] = new field();
                    tmp[i, j].value = table[i, j].value;
                    tmp[i, j].visibility = table[i, j].visibility;
                    foreach (int value in table[i,j].value_tmp)
                        tmp[i, j].value_tmp.Add(value);
                }
            }
            return tmp;
        }

        public void clear()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    tab[i, j].value = 0;
                    tab[i, j].value_tmp.Clear();
                    tab[i, j].visibility = true;
                    solutions.Clear();
                    saved = false;
                }
            }
        }

        //public void draw()                          //wypisanie planszy na ekranie
        //{
        //    for (int i = 0; i < N; i++)
        //    {
        //        for (int j = 0; j < N; j++)
        //            Console.Write(tab[i, j].value + " ");
        //        Console.Write("\n");
        //    }
        //}

        bool checkRow(int i, int j, int number)     //sprawdzenie czy w rzędzie nie ma już wartości number
        {   
            for (int k = 0; k < N; k++)
            {
                if (k == j)                         //jeśli ta sama co sprawdzamy to dalej
                    continue;
                if (tab[i, k].value == number)
                    return false;
            }
            return true;
        }

        bool checkColumn(int i, int j, int number)  //sprawdzenie czy w kolumnie nie ma już wartości number
        {
            for (int k = 0; k < N; k++)
            {
                if (k == i)                         //jeśli ta sama co sprawdzamy to dalej
                    continue;
                else if (tab[k, j].value == number)
                    return false;
            }
            return true;
        }

        bool checkSquare(int i, int j, int number)  //sprawdzenie czy w kwadracie, w który chcemy wpisać liczbę o indeksie i,j nie ma już wartości number
        {
            for (int k = i / 3 * 3; k < i / 3 * 3 + 3; k++)
            {
                for (int l = j / 3 * 3; l < j / 3 * 3 + 3; l++)
                {
                    if (k == i && l == j)           //jeśli ta sama co sprawdzamy to dalej
                        continue;
                    if (tab[k, l].value == number)
                        return false;               //nie można wpisać
                }
            }
            return true;                            //można wpisać
        }

        public bool checkRCS(int i, int j, int value)//sprawdza czy dana cyfra pasuje w dane miejsce
        {
            bool ok = false;
            if (checkRow(i, j, value) == true && checkColumn(i, j, value) == true && checkSquare(i, j, value) == true)
                ok = true;
            return ok;
        }

        public bool checkCorrectness()              //sprawdza czy poprawnie wpisano cyfry
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].visibility == true && tab[i, j].value != 0 && checkRCS(i, j, tab[i, j].value) == false)
                        return false;
                }
            }
            return true;
        }

        void firstCheck()                           //wpisuje możliwości w każde puste pole
        {                                           //w pierwszym przebiegu pętli
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int value = 1; value <= N; value++)
                    {
                        if (checkRCS(i, j, value) == true && (tab[i, j].visibility == true) && (tab[i, j].value == 0))
                            tab[i, j].value_tmp.Add(value);
                    }
                }
            }
        }

        bool checkAll()                      //wpisanie wszystkich możliwych cyfr jakie mogą znaleźć się w danym polu planszy sudoku                    
        {
            bool end = true;                        //jeśli true to znaczy, że jest wieloznaczność
            for (int i = 0; i < N; i++)             //2 pętle sprawdzające poszczególne ideksy czy można wstawić liczbę value
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].value_tmp.Count != 0)          //kolejne sprawdzanie pól, value_tmp wypełniona, teraz tylko usuwanie
                    {
                        List<int> tmp = new List<int>();        //tablica tymczasowa do przechowywania cyfr wpisywanych w pole
                        foreach (int value in tab[i, j].value_tmp)
                        {
                            if ((checkRow(i, j, value) == false) || (checkColumn(i, j, value) == false) || (checkSquare(i, j, value) == false))
                                tmp.Add(value);                                     //w momencie, gdy dana cyfra wcześniej mogła
                        }                                                           //być wpisana w dane pole, a w tej iteracji już tu nie pasuje
                        foreach (int value in tmp)
                            tab[i, j].value_tmp.Remove(value);
                        tmp.Clear();
                        end = false;
                    }
                    else if (tab[i, j].value_tmp.Count == 0 && tab[i, j].value == 0) //jeżeli są puste pola, których nie da się uzupełnić
                    {
                        if (solutions.Count != 0)
                        {
                            tab = CopyField(solutions[0]);
                            solutions.RemoveAt(0);
                            end = false;
                        }
                    }
                }
            }
            return end;
        }

        void enterNumbers()                                             //wpisanie wartości do planszy sudoku
        {
            bool ok = false;                                            //false - kilka rozwiązań, true - jedno
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].value_tmp.Count == 1)                  //wpisanie wartości do sudoku, jeżeli jest tylko jedna możliwość
                    {
                        int value = (int)tab[i, j].value_tmp[0];
                        if ((checkRow(i, j, value) == true) && (checkColumn(i, j, value) == true) && (checkSquare(i, j, value) == true))
                        {
                            tab[i, j].value = value;                    //wpisanie wartości do pola planszy sudoku
                            tab[i, j].value_tmp.Clear();                 //wyczyszczenie tablicy tymczasowej, teraz już nie jest potrzebna, bo już wpisaliśmy liczbę :)
                            ok = true;                                  //wszystko w porządku, nie ma dwóch rozwiązań
                        }
                    }
                }
            }
            if (ok == false)
                chooseNumber();
        }

        void chooseNumber()
        {
            int iMin = 0, jMin = 0;
            bool first = true;                                          //czy pierwszy poprawny
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].value_tmp.Count > 1 && first == true)  //jeśli pierwszy i zgodny to zapamiętuje indeksy i zmienia na nie pierwszy
                    {
                        iMin = i;
                        jMin = j;
                        first = false;
                    }
                    else if (tab[i, j].value_tmp.Count > 1 && first == false && tab[i, j].value_tmp.Count < tab[iMin, jMin].value_tmp.Count)
                    {
                        iMin = i;                                       //nie pierwszy, poprawny, o mniejszej liczbie możliwości
                        jMin = j;                                       //niż najmniejszy wcześniejszy to zapamiętuje indeksy
                    }
                }
            }
            if (tab[iMin, jMin].value_tmp.Count != 0)
            {
                List<int> tmp = new List<int>(tab[iMin, jMin].value_tmp);
                tab[iMin, jMin].value_tmp.Clear();
                for (int k = 1; k < tmp.Count; k++)
                {
                    tab[iMin, jMin].value = (int)tmp[k];
                    solutions.Add(this.CopyField(this.tab));
                }
                tab[iMin, jMin].value = (int)tmp[0];
                tmp.Clear();
                //draw();
                //Console.WriteLine();
            }
        }

        public bool solveSudoku()
        {
            bool finished;                                                  //czy koniec rozwiązywania
            bool ok = true;
            firstCheck();
            enterNumbers();
            while (true)
            {
                finished = checkAll();                                      //sprawdzenie całej planszy]
                enterNumbers();                                             //wpisanie cyfr w pola
                if (finished == true && isFull() == false && solutions.Count == 0)
                {
                    ok = false;
                    break;
                }
                else if (finished == true && isFull() == true)                   //jeśli koniec to skończ
                {
                    solutions.Clear();
                    break;
                }
            }
            //draw();
            return ok;
        }

        public bool isEmpty()                   //czy plansza jest pusta
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].value != 0)   //jeśli w jakimkolwiek polu coś jest to zwraca fałsz
                        return false;
                }
            }
            return true;
        }

        public bool isFull()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j].value == 0)   //jeśli jakimkolwiek pole jest puste
                        return false;
                }
            }
            return true;
        }

        void switchColumn(int box, int first, int second)
        {
            int tmp ;
            for (int i = 0; i < N; i++)
            {
                tmp = tab[i, box * 3 + first].value;
                tab[i, box * 3 + first].value = tab[i, box * 3 + second].value;
                tab[i, box * 3 + second].value = tmp;
            }
        }

        void switchRow(int box, int first, int second)
        {
            int tmp;
            for (int j = 0; j < N; j++)
            {
                tmp = tab[box * 3 + first, j].value;
                tab[box * 3 + first, j].value = tab[box * 3 + second, j].value;
                tab[box * 3 + second, j].value = tmp;
            }
        }

        void switchColumnBox(int first, int second)
        {
            int tmp;
            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    tmp = tab[i, first * 3 + k].value;
                    tab[i, first * 3 + k].value = tab[i, second * 3 + k].value;
                    tab[i, second * 3 + k].value = tmp;
                }
            }
        }

        void switchRowBox(int first, int second)
        {
            int tmp;
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < N; j++)
                {
                    tmp = tab[first * 3 + k, j].value;
                    tab[first * 3 + k, j].value = tab[second * 3 + k, j].value;
                    tab[second * 3 + k, j].value = tmp;
                }
            }
        }

        void fillBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    tab[i, j].visibility = false;
                }
            }
        }

        public void generateBoard()
        {
            fillBoard();
            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                int box = rand.Next(0, 3);
                int first = rand.Next(0, 3);
                int second = rand.Next(0, 3);
                switchColumn(box, first, second);

                box = rand.Next(0, 3);
                first = rand.Next(0, 3);
                second = rand.Next(0, 3);
                switchRow(box, first, second);

                first = rand.Next(0, 3);
                second = rand.Next(0, 3);
                switchColumnBox(first, second);

                first = rand.Next(0, 3);
                second = rand.Next(0, 3);
                switchRowBox(first, second);
            }
        }

        public void randomWithoutRepetition(int n, int k)           //losuje pola i usuwa z nich cyfry
        {
            Random rand = new Random();
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = i;

            for (int l = 0; l < k; l++)
            {
                int r = rand.Next(n);
                int count;
                if (numbers[r] / 10 + numbers[r] % 10 >= 10)
                    count = numbers[r] + numbers[r] / 10 + 1;
                else
                    count = numbers[r] + numbers[r] / 10;
                int j = count % 10;
                if (j == 9)
                {
                    count++;
                    j = count % 10;
                }
                int i = count / 10;
                tab[i, j].value = 0;
                tab[i, j].visibility = true;
                numbers[r] = numbers[n - 1];
                n--;
            }
        }
    }
}