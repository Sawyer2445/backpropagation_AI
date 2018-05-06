using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpropagation
{
    class neuronX
    {
        public neuronX(double signal)
        {
            x_in = signal;
        }
        public double out_x() { return x_in; }
        private double x_in;
    }
}
