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
        /// Величина ошибки весов w_jk
        /// </summary>
        public double[] sigma_k;
        /// <summary>
        /// Величина ошибки весов v_ij
        /// </summary>
        public double[] sigma_j;
        /// <summary>
        /// сумма ошибки w_jk
        /// </summary>
        public double[] sigma_in_j;

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
        public double[,] v_ij;
        /// <summary>
        /// корректировки весов v_ij
        /// </summary>
        public double[,] delta_v_ij;
        /// <summary>
        /// весы связей Z -> Y
        /// </summary>
        public double[,] w_jk;
        /// <summary>
        /// корректировки весов w_jk
        /// </summary>
        public double[,] delta_w_jk;
        /// <summary>
        /// коррестировки для смещения w_Ok
        /// </summary>
        public double[] delta_w_Ok;

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
            a = 2;
            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];
            v_ij = new double[i, j];
            v_Oj = new double[j];
            delta_v_ij = new double[i, j];

            w_jk = new double[j, k];
            w_Ok = new double[k];
            delta_w_jk = new double[j, k];
            delta_w_Ok = new double[k];

            T = new double[i];
            sigma_j = new double[j];
            sigma_k = new double[k];
            sigma_in_j = new double[j];

            //Заполнение примера для сравнения 
            int N = 0;
            for (int ii = 0; ii < pic.Size.Width; ii++)
            {
                for (int jj = 0; jj < pic.Size.Height; jj++)
                {
                    if (pic.GetPixel(ii, jj).R == 0)
                        T[N] = 1.0;
                    else
                        T[N] = 0.0;
                    N++;
                }
            }

            //Выбор певоначального значение весов и смещения 
  
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    Random rand1 = new Random();
                    double min1 = -0.5;
                    double max1 = 0.5;
                    v_ij[ii, jj] = rand1.NextDouble() * (max1 - min1) + min1;
                }
                Random rand = new Random();
                double min = -0.5;
                double max = 0.5;
                v_Oj[ii] = rand.NextDouble() * (max - min) + min;
            }

            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    Random rand1 = new Random();
                    double min1 = -0.5;
                    double max1 = 0.5;
                    w_jk[ii, jj] = rand1.NextDouble() * (max1 - min1) + min1;
                }
                Random rand = new Random();
                double min = -0.5;
                double max = 0.5;
                w_Ok[ii] = rand.NextDouble() * (max - min) + min;
            }
        }

        public void work(Bitmap bmp)
        {
            //отправка сигнала X-нейронам 
            int N = 0;
            for (int ii = 0; ii < bmp.Size.Width; ii++)
            {
                for (int jj = 0; jj < bmp.Size.Height; jj++)
                {
                    if (bmp.GetPixel(ii, jj).R == 0)
                        Xi[N] = new neuronX(1);
                    else
                        Xi[N] = new neuronX(0);
                    N++;
                }
            }

            //отправка сигналов Z-нейронам
            for (int jj = 0; jj < Zj.Length; jj++)
            {
                double z_in = 0;
                for (int ii = 0; ii < Xi.Length; i++)
                {
                    z_in += Xi[ii].out_x() * v_ij[ii, jj];
                }
                z_in += v_Oj[jj];
                Zj[jj] = new neuronZ(z_in);
            }

            //отправка сигналов Y-нейронам
            for (int kk = 0; kk < k; kk++)
            {
                double y_in = 0;
                for (int jj = 0; jj < j; jj++)
                {
                    y_in += Zj[jj].out_z() * w_jk[jj, kk];
                }
                y_in += w_Ok[kk];
                Yk[kk] = new neuronY(y_in);
            }
        }
        public void backpropagation()
        {
            //вычисление ошибок весов w_ij
            for (int kk = 0; kk < k; kk++)
            {
                sigma_k[kk] = (T[kk] - Yk[kk].out_y()) * ff(Yk[kk].y_in);
            }
            //вычисление коррекировки для весов w_jk
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    delta_w_jk[jj, kk] = a * sigma_k[kk] * Zj[jj].out_z();
                }
            }
            //вычисление корректировки смещения w_Ok
            for (int kk = 0; kk < k; kk++)
            {
                delta_w_Ok[kk] = a * sigma_k[kk];
            }


            //вычисления суммы ошибок w_jk
            for (int jj = 0; jj < j; jj++)
            {
                double sum_signK_and_w_jk = 0;
                for (int kk = 0; kk < k; kk++)
                {

                    sum_signK_and_w_jk += sigma_k[kk] * w_jk[jj, kk];
                }
                sigma_in_j[jj] = sum_signK_and_w_jk;
            }


            //вычисление величины ошибки для v_ij
            for(int jj = 0; jj < j; j++)
            {
                sigma_j[jj] = sigma_in_j[jj] * ff(Zj[jj].z_in);
            }

            //вычисление корректировки весов v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    delta_v_ij[ii, jj] = a * sigma_j[jj] * Xi[ii].out_x();
                }
            }

            //вычисление величину корректировки смещения 
            for (int jj = 0; jj < j; jj++)
            {
                v_Oj[jj] = a * sigma_j[jj];
            }

            //ИЗМЕНЕНИЕ ВЕСОВ w_jk
            for (int jj = 0; jj < j; j++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[jj, kk] = w_jk[jj, kk] + delta_w_jk[jj, kk];
                }
            }

            //ИЗМЕНЕНИЕ ВЕСОВ v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[ii, jj] = v_ij[ii, jj] * delta_v_ij[ii, jj];
                }
            }


        }
        /// <summary>
        /// производная анализирующей функции
        /// </summary>
        /// <param name="x">аргумент</param>
        /// <returns>значение</returns>
        private double ff(double x)
        {
            return f(x) * (1 - f(x));
        }
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">аргумент</param>
        /// <returns>значение</returns>
        private double f(double in_)
        {
            return 1 / (1 + Math.Exp(-1 * in_));
        }
        public double sumSigma_k()
        {
            double sum = 0;
            for (int kk = 0; kk < k; kk++)
            {
                sum += sigma_k[kk];
            }
            return sum;
        }

    }
}
