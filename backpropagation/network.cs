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
        /// Величина ошибки весов w_jk
        /// </summary>
        public float[] sigma_k;
        /// <summary>
        /// Величина ошибки весов v_ij
        /// </summary>
        public float[] sigma_j;
     


        /// <summary>
        /// весы связей X -> Z
        /// </summary>
        public float[,] v_ij;
        /// <summary>
        /// корректировки весов v_ij
        /// </summary>
        public float[,] delta_v_ij;
        /// <summary>
        /// весы связей Z -> Y
        /// </summary>
        public float[,] w_jk;
        /// <summary>
        /// корректировки весов w_jk
        /// </summary>
        public float[,] delta_w_jk;

        /// <summary>
        /// скорость обучения
        /// </summary>
        public float  learining_rate;

        /// <summary>
        /// Инициализация нейронной сети
        /// </summary>
        /// <param name="pic">Значения для сравнения</param>
        public network(int x, int y)
        {
            i = x * y;
            j = i;
            k = 1;
            learining_rate = 1;
            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];
            v_ij = new float [i, j];
            delta_v_ij = new float [i, j];

            w_jk = new float [j, k];
            delta_w_jk = new float [j, k];

            //T = new float [i];
            sigma_j = new float [j];
            sigma_k = new float [k];



            //Выбор певоначального значение весов 

            Random rand1 = new Random();
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[ii, jj] = (float)rand1.NextDouble();
                }
            }

            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[jj, kk] = (float)rand1.NextDouble();
                }
            }
            ;
        }

        public void work(ref Bitmap bmp)
        {
            //отправка сигнала X-нейронам 
            int N = 0;
            for (int ii = 0; ii < bmp.Size.Width; ii++)
            {
                for (int jj = 0; jj < bmp.Size.Height; jj++)
                {
                    if (bmp.GetPixel(ii, jj).R == 0)
                        Xi[N] = new neuronX(true);
                    else
                        Xi[N] = new neuronX(false);
                    N++;
                }
            }

            //отправка сигналов Z-нейронам
            for (int jj = 0; jj < j; jj++)
            {
                float  z_in = 0;
                for (int ii = 0; ii < i; ii++)
                {
                    z_in += Xi[ii].out_x() * v_ij[ii, jj];
                }
                Zj[jj] = new neuronZ(z_in);
            }

            //отправка сигналов Y-нейронам
            for (int kk = 0; kk < k; kk++)
            {
                float  y_in = 0;
                for (int jj = 0; jj < j; jj++)
                {
                    y_in += Zj[jj].out_z() * w_jk[jj, kk];
                }
                Yk[kk] = new neuronY(y_in);
            }
            
        }
        public void backpropagation(int expect)
        {
            //вычисление ошибок весов w_ij
            for (int kk = 0; kk < k; kk++)
            {
                sigma_k[kk] = (Yk[kk].out_y() - expect);
            }
            ;
            //вычисление коррекировки для весов w_jk
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    delta_w_jk[jj, kk] = sigma_k[kk] * ff(Yk[kk].out_y());
                }
            }
            //ИЗМЕНЕНИЕ ВЕСОВ w_jk
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[jj, kk] = w_jk[jj, kk] - Zj[jj].out_z() * delta_w_jk[jj, kk] * learining_rate;
                }
            }


            //вычисление величины ошибки для v_ij
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    sigma_j[jj] = delta_w_jk[jj,kk] * w_jk[jj, kk];
                }
            }
            ////вычисление корректировки весов v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    delta_v_ij[ii, jj] = sigma_j[jj] * ff(Zj[jj].out_z());
                }
            }
            //ИЗМЕНЕНИЕ ВЕСОВ v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[ii, jj] = v_ij[ii, jj] - Xi[ii].out_x() * delta_v_ij[ii, jj] * learining_rate;
                }
            }
        }
        /// <summary>
        /// производная анализирующей функции
        /// </summary>
        /// <param name="x">аргумент</param>
        /// <returns>значение</returns>
        private float ff(float  x)
        {
            return f(x) * (1 - f(x));
        }
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">аргумент</param>
        /// <returns>значение</returns>
        private float  f(float  in_)
        {
            double x = Convert.ToDouble(in_);
            return (float)(1.0 / (1.0 + Math.Exp(-x)));
        }
        public float  sumSigma_k()
        {
            float  sum = 0;
            for (int kk = 0; kk < k; kk++)
            {
                sum += sigma_k[kk];
            }
            return sum;
        }

     

        public float  sum_Yk()
        {
            float sum = 0.0f;
            float d;
            for (int kk = 0; kk < k; kk++)
            {
                d = Yk[kk].out_y();
                sum += Yk[kk].out_y();
            }
            return sum;
        }
       
    }
}
