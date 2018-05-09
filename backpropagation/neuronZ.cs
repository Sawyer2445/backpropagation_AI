using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpropagation
{
    class neuronZ
    {
        /// <summary>
        /// создаёт нейрон с его сигналом 
        /// </summary>
        /// <param name="z_in">z_in считает сеть</param>
        public neuronZ(float z_in)
        {
            this.z_in = z_in;
        }

        public float out_z()
        {
            return f(z_in);
        }

        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">z_in</param>
        /// <returns>результат работы активационной функции</returns>
        private float f(float  in_)
        {
            double x = Convert.ToDouble(in_);
            return (float)(1.0 / (1.0 + Math.Exp(-x)));
        }
        /// <summary>
        /// Считается по формуле
        /// </summary>
        public float  z_in;
        
    }
}
