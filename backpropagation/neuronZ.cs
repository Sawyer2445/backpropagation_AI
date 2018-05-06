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
        /// <param name="out_x">массив выходных сзначений нейронов X</param>
        /// <param name="v_ij">корректировки весов</param>
        /// <param name="v_Oj">смещение</param>
        public neuronZ(ref double[] out_x, ref double[][] v_ij, double[] v_Oj)
        {
            ///тут считается z_in
        }

        public double out_z()
        {
            //возвращается f(z_in)
            return 1.0;
        }

        /// <summary>
        /// активационная функция СДЕЛАТЬ
        /// </summary>
        /// <param name="in_">z_in</param>
        /// <returns>результат работы активационной функции</returns>
        private double f(double in_)
        {
            return in_;
        }
        /// <summary>
        /// Считается по формуле
        /// </summary>
        private double z_in;
        
    }
}
