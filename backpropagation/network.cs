using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpropagation
{
    /// <summary>
    /// Описывает нейронную сеть 
    /// </summary>
    class network
    {
        /// <summary>
        /// количество нейронов X, Z, Y соответственно
        /// </summary>
        public int i, j, k; 
        public neuronX[] Xi;
        public neuronZ[] Zj;
        public neuronY[] Yk;

        /// <summary>
        /// Значения для сравнения
        /// (даны учителем)
        /// </summary>
        public double[] T;
        /// <summary>
        /// Корректировки весов w_jk
        /// </summary>
        public double[] sigma_k;
        /// <summary>
        /// Корректировки весов v_ij
        /// </summary>
        public double[] sigma_j;

        /// <summary>
        /// смещение 
        /// </summary>
        public double[] v_Oj;
        /// <summary>
        /// смещение
        /// </summary>
        public double[] w_Ok;

        /// <summary>
        /// весы связей X -> Z
        /// </summary>
        double[][] v_ij;
        /// <summary>
        /// весы связей Z -> Y
        /// </summary>
        double[][] w_jk;

        /// <summary>
        /// скорость обучения
        /// </summary>
        public double a;

        /// <summary>
        /// Инициализация нейронной сети
        /// </summary>
        /// <param name="pic">Значения для сравнения</param>
        public network(Bitmap pic)
        {
            i = pic.Size.Width * pic.Size.Height;
            j = i;
            k = j;
            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];

            //Заполнение примера для сравнения 
            int N = 0;
            for (int ii = 0; ii < pic.Size.Width; ii++)
            {
                for (int jj = 0; j < pic.Size.Height; jj++)
                {
                    if (pic.GetPixel(ii, jj).R == 0)
                        T[N] = 1.0;
                    else
                        T[N] = 0.0;
                    N++;
                }
            }

            //Выбор певоначального значение весов и смещения 
  
            for (int ii = 0; ii < j; ii++)
            {
                for (int jj = 0; j < pic.Size.Height; jj++)
                {
                    Random rand1 = new Random();
                    double min1 = -0.5;
                    double max1 = 0.5;
                    v_ij[ii][jj] = rand1.NextDouble() * (max1 - min1) + min1;
                }
                Random rand = new Random();
                double min = -0.5;
                double max = 0.5;
                v_Oj[ii] = rand.NextDouble() * (max - min) + min;
            }

            for (int ii = 0; ii < j; ii++)
            {
                for (int jj = 0; j < pic.Size.Height; jj++)
                {
                    Random rand1 = new Random();
                    double min1 = -0.5;
                    double max1 = 0.5;
                    w_jk[ii][jj] = rand1.NextDouble() * (max1 - min1) + min1;
                }
                Random rand = new Random();
                double min = -0.5;
                double max = 0.5;
                w_Ok[ii] = rand.NextDouble() * (max - min) + min;
            }
        }
    }
}
