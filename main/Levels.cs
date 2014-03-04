using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class Levels : Form
    {
        int fieldWith = 0;
        public Levels()
        {
            InitializeComponent();
        }

        private void level1Button_Click(object sender, EventArgs e)
        {
            fieldWith = 45;
            this.Close();
        }

        private void level2Button_Click(object sender, EventArgs e)
        {
            fieldWith = 35;
            this.Close();
        }

        private void level3Button_Click(object sender, EventArgs e)
        {
            fieldWith = 30;
            this.Close();
        }

        private void level4Button_Click(object sender, EventArgs e)
        {
            fieldWith = 17;
            this.Close();
        }

        public int returnFieldToErase()
        {
            return 81 - fieldWith;
        }
    }
}