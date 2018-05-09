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
        public neuronZ(double z_in)
        {
            this.z_in = z_in;
        }

        public double out_z()
        {
            return f(z_in);
        }

        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">z_in</param>
        /// <returns>результат работы активационной функции</returns>
        private double f(double in_)
        {
            return (1.0 / (1 + Math.Pow(Math.E, -in_)));
        }
        /// <summary>
        /// Считается по формуле
        /// </summary>
        public double  z_in;
        
    }
}
