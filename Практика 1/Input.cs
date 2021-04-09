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
    public partial class Input : Form
    {
        private string A;
        private string B;
        private string C;
        public bool perimeter;
        public bool square;
        private bool correctData = false;
        public Input()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            perimeter = checkBox1.Checked;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            square = checkBox2.Checked;
        }
        /// <summary>
        /// Считает
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            double doubleA = -1;
            double doubleB = -1;
            double doubleC = -1;
            string Error = "";
            try
            {
                doubleA = Double.Parse(A);
                doubleB = Double.Parse(B);
                doubleC = Double.Parse(C);
            }
            catch
            {
                Error += "Введите числа\n";
            }
            if (!(doubleA > 0 && doubleB > 0 && doubleC > 0 &&
            !(doubleA + doubleB <= doubleC
            || doubleA + doubleC <= doubleB
            || doubleB + doubleC <= doubleA)))
                Error += "Значения должны быть больше нуля и существовать для треугольника\n";

            if (!(perimeter || square))
            {
                Error += "Выберите предмет для расчета\n";
            }
            if (Error != "")
            {
                MessageBox.Show(Error);
            }
            else
            {
                MessageBox.Show("Введены корректные данные");
                correctData = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.A = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.B = textBox2.Text;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.C = textBox3.Text;
        }

        // Тут уже начинаются функции
        public bool isDataCorrect()
        {
            return correctData;
        }
        public double getA()
        {
            return Double.Parse(A);
        }
        public double getB()
        {
            return Double.Parse(B);
        }
        public double getC()
        {
            return Double.Parse(C);
        }
    }
}
