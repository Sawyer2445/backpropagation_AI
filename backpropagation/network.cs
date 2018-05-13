using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public double[] sigma_k;
        /// <summary>
        /// Величина ошибки весов v_ij
        /// </summary>
        public double[] sigma_j;
     


        /// <summary>
        /// весы связей X -> Z
        /// </summary>
        public static double[,] v_ij;
        public double[] v_Oj;
        public double[] delta_v_Oj;
        /// <summary>
        /// корректировки весов v_ij
        /// </summary>
        public double[,] delta_v_ij;
        
        /// <summary>
        /// весы связей Z -> Y
        /// </summary>
        public static double[,] w_jk;
        /// <summary>
        /// корректировки весов w_jk
        /// </summary>
        public double[,] delta_w_jk;
        public double[] w_Ok;
        public double[] delta_w_Ok;
        /// <summary>
        /// скорость обучения
        /// </summary>
        public double  learining_rate;

        /// <summary>
        /// Инициализация нейронной сети
        /// </summary>
        /// <param name="countSignal">Значения для сравнения</param>
        public network(int countSignal)
        {
            i = countSignal;
            j = i - 1;
            k = 1;
            learining_rate = 0.1;

            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];
            v_ij = new double [i, j];
            delta_v_ij = new double [i, j];
            v_Oj = new double[j];
            delta_v_Oj = new double[j];

            w_jk = new double [j, k];
            delta_w_jk = new double [j, k];
            w_Ok = new double[k];
            delta_w_Ok = new double[k];

            sigma_j = new double [j];
            sigma_k = new double [k];

            ;

            //Выбор певоначального значение весов 

            Random rand1 = new Random();
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[ii, jj] = rand1.NextDouble();
                    v_Oj[jj] = rand1.NextDouble();
                }
            }

            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[jj, kk] = rand1.NextDouble();
                    w_Ok[kk] = rand1.NextDouble();
                }
            }
            
        }

        /// <summary>
        /// Работа сети
        /// </summary>
        /// <param name="signals"></param>
        public void work(bool[] signals)
        {
            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];
            //отправка сигнала X-нейронам 
            
            for (int ii = 0; ii < i; ii++)
            {
                if (signals[ii])
                    Xi[ii] = new neuronX(true);
                else
                    Xi[ii] = new neuronX(false);
            }

            double z_in = 0.0;
            //отправка сигналов Z-нейронам
            for (int jj = 0; jj < j; jj++)
            {
                for (int ii = 0; ii < i; ii++)
                {
                    z_in += Xi[ii].out_x() * v_ij[ii, jj];
                }
                Zj[jj] = new neuronZ(z_in + v_Oj[jj]);
                z_in = 0.0;
            }

            double y_in = 0.0;
            //отправка сигналов Y-нейронам
            for (int kk = 0; kk < k; kk++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    y_in += Zj[jj].out_z() * w_jk[jj, kk];
                }
                Yk[kk] = new neuronY(y_in + w_Ok[kk]);
                y_in = 0.0;
            }
            
            
        }
        
        /// <summary>
        /// Обучение сети
        /// </summary>
        /// <param name="expect"></param>
        public void backpropagation(double expect)
        {
            for (int kk = 0; kk < k; kk++)
            {
                sigma_k[kk] = (expect - Yk[kk].out_y()) * (Yk[kk].out_y() * (1 - Yk[kk].out_y()));
            }
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    delta_w_jk[jj, kk] = learining_rate * sigma_k[kk] * Zj[jj].out_z();
                    w_Ok[kk] = learining_rate * sigma_k[kk];
                }
            }


            double[] sigma_in_j = new double[j];
            for (int jj = 0; jj < j; jj++)
            {
                double sum = 0.0;
                for (int kk = 0; kk < k; kk++)
                {
                    sum += sigma_k[kk] * w_jk[jj, kk];
                }
                sigma_in_j[jj] = sum;
            }
            for (int jj = 0; jj < j; jj++)
            {
                sigma_j[jj] = sigma_in_j[jj] * (Zj[jj].out_z() * (1 - Zj[jj].out_z()));
            }
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    delta_v_ij[ii, jj] = learining_rate * sigma_j[jj] * Xi[ii].out_x();
                    v_Oj[jj] = learining_rate * sigma_j[jj];
                }
            }

            //Изменение весов 
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[jj, kk] += delta_w_jk[jj, kk];
                    w_Ok[kk] += delta_w_Ok[kk];
                }
            }
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[ii, jj] += delta_v_ij[ii, jj];
                    v_Oj[jj] += delta_v_Oj[jj];
                }
            }
        }
  
        /// <summary>
        /// активационная функция
        /// </summary>
        /// <param name="in_">аргумент</param>
        /// <returns>значение</returns>
        private double  f(double  in_)
        {
            return (1.0 / (1 + Math.Pow(Math.E, -in_)));
        }
       
        /// <summary>
        /// Выход сети
        /// </summary>
        /// <returns>Выход сети</returns>
        public double getY()
        {
            return Yk[0].out_y();
        }
       
        /// <summary>
        /// Обучение сети
        /// </summary>
        public void learning()
        {
            bool[,] train = new bool[8, 4]{
                {false, false ,false, false},
                {false, false, true, true},
                {false, true, false,  false},
                {false, true, true, false },
                {true, false, false, true},
                {true, false, true, true},
                {true, true, false, false },
                {true, true, true, true}
            };
            double[] correct = new double[8];
            for (int ii = 0; ii < 8; ii++)
            {
                if (train[ii, 3])
                    correct[ii] = 1.0;
                else
                    correct[ii] = 0.0;
            }

            int n = 0;
            while (n != 1000)
            {
                double[] actualY = new double[8];
                for (int ii = 0; ii < 8; ii++)
                {
                    bool[] sub = new bool[3]
                    {
                       train[ii, 0], train[ii, 1], train[ii, 2]
                    };
                    work(sub);
                    double Y = getY();
                    if (Y > 0.5 && train[ii, 3] == false)
                        backpropagation(0.0);
                    if (Y < 0.5 && train[ii, 3] == true)
                        backpropagation(1.0);


                    actualY[ii] = Y;
                  

                }
                n++;
                Console.WriteLine("{0}: {1}", n, MSB(correct, actualY));
                if (MSB(correct, actualY) > 0.9) break;
            }
         }
               
        /// <summary>
        /// Среднее квадратичное отклонение
        /// </summary>
        /// <param name="coorect">Массив правильных ответов</param>
        /// <param name="Yk">Массив ответов сети</param>
        /// <returns>Отклонение</returns>
        private double MSB(double[] coorect, double[] Yk)
        {
            double sum = 0.0;
            for (int ii = 0; ii < 8; ii++)
            {
                sum += (coorect[ii] - Yk[ii]) * (coorect[ii] - Yk[ii]);
            }
            ;
            return Math.Sqrt(sum/8);
        }
    }
}
