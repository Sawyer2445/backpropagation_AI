using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpropagation
{
    class neuronX
    {
        public neuronX(bool signal)
        {
            x_in = signal;
        }
        public double out_x() { return x_in ? 1 : 0; }
        private bool x_in;
    }
}
