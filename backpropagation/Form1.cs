﻿using System;
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
        int sec = 0;
        Bitmap bmp;
        Pen pen;
        network neuronNetwork;
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += "START\n";
            Brush brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 5);
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            
            neuronNetwork = new network(pictureBox1.Width / 4, pictureBox1.Height / 4);
            clearBMP();
            pictureBox1.Image = bmp;
            
            richTextBox1.Text += "Нейронная сеть готова к работе\n";

        }

     
      
        /// <summary>
        /// Очищает холст
        /// </summary>
        private void clearBMP()
        {
            for (int i = 0; i < bmp.Size.Width; i++)
            {
                for (int j = 0; j < bmp.Size.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.White);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Title = "Укажите файл с цифрой";
            //openFileDialog1.ShowDialog();
            //bmp = new Bitmap(openFileDialog1.FileName);
            //pictureBox1.Image = bmp;
            richTextBox1.Text += "Start work\n";
            Bitmap pic = new Bitmap(bmp, new Size(bmp.Width / 4, bmp.Height/ 4));
            //Bitmap pic = new Bitmap(Properties.Resources._1, new Size(bmp.Width / 100, bmp.Height / 100));
            //pictureBox1.Image = pic;
            neuronNetwork.work(pic);
            //sum_Yk = neuronNetwork.sum_Yk();
            richTextBox1.Text += "Y = " + neuronNetwork.getY() + '\n';
            richTextBox1.Text += "End work\n\n";
          
            timer1.Start();
            sec = 2;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Backpropagation\n";
            if (neuronNetwork.getY() < 0.5)
                neuronNetwork.backpropagation(1.0);
            else
                neuronNetwork.backpropagation(0.0);
            richTextBox1.Text += "Backpropagation END\n";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.DrawEllipse(pen, e.X, e.Y, 5, 5);
                }
                pictureBox1.Image = bmp;
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            --sec;
            if (sec <= 0)
            {
                timer1.Stop();
                clearBMP();
                pictureBox1.Image = bmp;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            neuronNetwork.readVandW();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            neuronNetwork.writeVandW();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Start learninh\n";
            neuronNetwork.learning();
            richTextBox1.Text += "Stop learning\n";
        }
    }
}
