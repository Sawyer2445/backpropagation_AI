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
        public float out_x() { return x_in ? 1.0f : 0.0f; }
        private bool x_in;
    }
}
