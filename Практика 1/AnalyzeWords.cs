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
    public partial class AnalyzeWords : Form
    {
        private string fileText;
        public AnalyzeWords()
        {
            InitializeComponent();
            openFileDialog2.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog2.FileName;
            this.fileText = System.IO.File.ReadAllText(filename);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string inputLetter = this.textBox1.Text;
            int letterCount = 0;
            string errors = "";

            if (this.fileText == null)
            {
                errors += "Выберите файл\n";
            }
            if (inputLetter == null)
            {
                errors += "Не введена буква\n";
            }
            if (inputLetter.Length != 1 || !char.IsLetter(inputLetter[0]))
            {
                errors += inputLetter + " не буква\n";
            }
            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            foreach (var c in this.fileText.ToLower())
            {
                if (c == inputLetter[0])
                {
                    letterCount++;
                }
            }
            MessageBox.Show("Буква повторяется: " + letterCount);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.fileText == null)
            {
                MessageBox.Show("Выберите файл");
                return;
            }

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int wordsCount = this.fileText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

            MessageBox.Show("Количество слов в тексте: " + wordsCount);
        }
        private void letter_TextChanged(object sender, EventArgs e)
        {
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
