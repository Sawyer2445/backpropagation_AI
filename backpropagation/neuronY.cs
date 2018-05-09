
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpropagation
{
    class neuronY
    {
        /// <summary>
        /// создает нейрон с его сигналом
        /// </summary>
        /// <param name="y_in">y_in считает сеть</param>
        public neuronY(double  y_in)
        {
            this.y_in = y_in;
        }
        public double out_y()
        {
            return f(y_in);   
        }
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">y_in</param>
        /// <returns>результат работы активационной функции</returns>
        private double  f(double in_)
        {
            Console.WriteLine((1.0 / (1 + Math.Pow(Math.E, -in_))));
            return (1.0 / (1 + Math.Pow(Math.E, -in_)));
        }


        public double y_in;
    }
}
