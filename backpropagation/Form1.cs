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
        network neuronNetwork;
        bool[] signals;
        int sec;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            signals = new bool[3];
            neuronNetwork = new network(signals.Length);
            richTextBox1.Text += "Нейронная сеть готова к работе\n";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (cb_drink.Checked)
                signals[0] = true;
            else
                signals[0] = false;

            if (cb_rain.Checked)
                signals[1] = true;
            else
                signals[1] = false;

            if (cb_friend.Checked)
                signals[2] = true;
            else
                signals[2] = false;

            neuronNetwork.work(signals);
            double Y = neuronNetwork.getY();
            if (Y > 0)
                richTextBox1.Text += "ИДЁМ В ГОСТИ\n";
            richTextBox1.Text += neuronNetwork.getY().ToString() + '\n';

            sec = 2;
            timer1.Start();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            --sec;
            if (sec < 0)
            {
                cb_drink.Checked = false;
                cb_rain.Checked = false;
                cb_friend.Checked = false;
                timer1.Stop();
            }
            
        }
    }
}
