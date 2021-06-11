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
    public partial class Form1 : Form
    {
        private Input InputForm;
        private Convert1 convert;
        private AnalyzeWords analyze;
        public Form1()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.InputForm = new Input();
            this.InputForm.Show();
        }
        /// <summary>
        /// Calc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                if (InputForm == null)
                {
                    MessageBox.Show("Введите данные в Input");
                }
                else if (InputForm.isDataCorrect())
                {
                    double a = InputForm.getA();
                    double b = InputForm.getB();
                    double c = InputForm.getC();
                    if (InputForm.perimeter)
                    {
                        message += "Периметр: " + (a + b + c) + "\n";
                    }
                    if (InputForm.square)
                    {
                        double p = (a + b + c) / 2;
                        double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                        message += "Площадь: " + s + "\n";
                    }
                    MessageBox.Show(message);
                }
            }
            catch
            {
                MessageBox.Show("Введите данные");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.convert = new Convert1();
            this.convert.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мамади Берете K3220\n Контакты:\n 274506@niuitmo.ru");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.analyze = new AnalyzeWords();
            this.analyze.Show();
        }
    }
}
