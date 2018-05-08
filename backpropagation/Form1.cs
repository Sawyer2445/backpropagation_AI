using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace backpropagation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        double sum_t;
        //Brush brush;
        Pen pen;
        Bitmap pic;
        network neuronNetwork;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Brush brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 11);
            //bmp = new Bitmap(3, 5);
            pic = new Bitmap(3, 5);
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        bmp.SetPixel(i, j, Color.White);
            //    }
            //}
            bmp = new Bitmap(pictureBox1.Image);
          
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            pic = new Bitmap( bmp);
            neuronNetwork = new network(pic);
            //neuronNetwork.example(pic);
            richTextBox1.Text += "Тестовый пример загружен\n";
            sum_t = neuronNetwork.sum_T();
            richTextBox1.Text += "T sum: " + sum_t + "\n\n";
            button1.Visible = false;
            

        }
        /// <summary>
        /// Очищает холст
        /// </summary>
        private void clearBMP()
        {
            //for (int i = 0; i < bmp.Size.Height; i++)
            //{
            //    for (int j = 0; j < bmp.Size.Width; j++)
            //    {
            //        bmp.SetPixel(i, j, Color.White);
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Укажите файл с цифрой";
            openFileDialog1.ShowDialog();
            bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
            richTextBox1.Text += "Start work\n";
            neuronNetwork.work(bmp);
            double sum_sigma = neuronNetwork.sumSigma_k();
            richTextBox1.Text += "Сумма ошибки: " + sum_sigma + '\n';
            double sum_Yk = neuronNetwork.sum_Yk();
            richTextBox1.Text += "Y sum: " + sum_Yk + '\n';
            richTextBox1.Text += "diff: " + (sum_t - sum_Yk) + '\n';
            richTextBox1.Text += "End work\n\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "\nStart error\n";
            neuronNetwork.backpropagation();
            richTextBox1.Text += "Сумма ошибки : " + neuronNetwork.sumSigma_k() + "\n\n";
        }
    }
}
