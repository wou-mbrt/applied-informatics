using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_1
{
    public partial class Convert1 : Form
    {
        private int numeralSystem;
        private int number;
        public Convert1()
        {
            InitializeComponent();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            numeralSystem = 2;
            textBox2.Text = Convert.ToString(number, numeralSystem);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            numeralSystem = 8;
            textBox2.Text = Convert.ToString(number, numeralSystem);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            numeralSystem = 16;
            textBox2.Text = Convert.ToString(number, numeralSystem);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            number = int.Parse(textBox1.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(number, numeralSystem);
        }
    }
}
