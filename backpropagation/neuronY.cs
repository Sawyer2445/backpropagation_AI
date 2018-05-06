
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
        /// <param name="out_z">массив выходных сзначения нейронов Z</param>
        /// <param name="w_jk">корректировки весов</param>
        /// <param name="w_Ok">смещение</param>
        public neuronY(ref double[] out_z, ref double[][] w_jk, double[] w_Ok)
        {
            //тут считется y_in
        }
        public double out_y()
        {
            ///возвращается f(y_in)
            return 1.0;
            
        }
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">y_in</param>
        /// <returns>результат работы активационной функции</returns>
        private double f(double in_)
        {
            return in_;
        }
        private double y_in;
    }
}
