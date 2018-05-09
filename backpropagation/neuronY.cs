
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
        public neuronY(float  y_in)
        {
            this.y_in = y_in;
        }
        public float out_y()
        {
            return (float)f(y_in);   
        }
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">y_in</param>
        /// <returns>результат работы активационной функции</returns>
        private float  f(float in_)
        {
            double x = Convert.ToDouble(in_);
            return (float)(1.0 / (1.0 + Math.Exp(-x)));
            
        }


        public float y_in;
    }
}
