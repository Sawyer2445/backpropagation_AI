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
        Brush brush;
        Pen pen;
        private void Form1_Load(object sender, EventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            pen = new Pen(brush, 11);
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.White);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.DrawEllipse(pen, e.X, e.Y, 11, 11);
                }
                pictureBox1.Image = bmp;
            }
        }
    }
}
