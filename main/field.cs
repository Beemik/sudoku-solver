using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace main
{
    [Serializable]
    class field
    {
        public bool visibility = true;      //widoczność: czy do wpisania czy wpisane z automatu (false nie można, true można)
        public List<int> value_tmp;          //możliwości do wpisania w kratce

        public int value = 0;               //wartość w kratce

        public field()                       //konstruktor inicjalizujący
        {
            value_tmp = new List<int>();
        }

        public field(int number)             //konstruktor inicjalizujący i wpisujący określoną wartość do pola planszy sudoku
        {
            value_tmp = new List<int>();
            value = number;
            visibility = false;             //zablokowanie możliwości zmiany wartości przez użytkownika
        }
    }
}
