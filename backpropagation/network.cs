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
        /// скорость обучения
        /// </summary>
        public double  learining_rate;

        /// <summary>
        /// Читает матрицы весов из файлов
        /// </summary>
        /// <param name="file_V">файл с v_ij</param>
        /// <param name="file_W">файл с w_jk</param>
        public void readVandW()
        {
            StreamReader read1 = new StreamReader("weghts\\v_ij.txt");
            string matrix_v_str = read1.ReadToEnd();
            read1.Close();
            var lines = matrix_v_str.Split('\n');
            var rowNumber = lines.Length;
            var columnNumber = lines.First().Split(' ').Length;

            for (int ii = 0; ii < rowNumber - 1; ii++)
            {
                string[] columns = lines[ii].Split(' ');
                for (int jj = 0; jj < columnNumber - 1; jj++)
                {
                    double.TryParse(columns[jj], out v_ij[ii, jj]);
                }
            }
            
            StreamReader read2 = new StreamReader("weghts\\w_jk.txt");
            string matrix_w_str = read2.ReadToEnd();
            read2.Close();
            var lines2 = matrix_w_str.Split('\n');
            var rowNumber2 = lines2.Length;
            var columnNumber2 = lines2.First().Split(' ').Length;

            for (int ii = 0; ii < rowNumber2-1; ii++)
            {
                string[] columns2 = lines2[ii].Split(' ');
                for (int jj = 0; jj < columnNumber2-1; jj++)
                {
                    double.TryParse(columns2[jj], out w_jk[ii, jj]);
                }
            }
            
        }
        public void writeVandW()
        {
            StreamWriter writer1 = new StreamWriter("weghts\\v_ij.txt");
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    writer1.Write("{0} ", v_ij[ii, jj]);
                }
                writer1.WriteLine();
            }
            writer1.Close();

            StreamWriter writer2 = new StreamWriter("weghts\\w_jk.txt");
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    writer2.Write("{0} ", w_jk[jj, kk]);
                }
                writer2.WriteLine();
            }
            writer2.Close();
        }

        /// <summary>
        /// Инициализация нейронной сети
        /// </summary>
        /// <param name="countSignal">Значения для сравнения</param>
        public network(int countSignal)
        {
            i = countSignal;
            j = i - 1;
            k = 1;
            learining_rate = 0.01;
            Xi = new neuronX[i];
            Zj = new neuronZ[j];
            Yk = new neuronY[k];
            v_ij = new double [j, i];
            delta_v_ij = new double [j, i];

            w_jk = new double [k, j];
            delta_w_jk = new double [k, j];

            //T = new double [i];
            sigma_j = new double [j];
            sigma_k = new double [k];



            //Выбор певоначального значение весов 

            Random rand1 = new Random();
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[jj, ii] = rand1.NextDouble();
                }
            }

            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[kk, jj] = rand1.NextDouble();
                }
            }
            ;
        }

        public void work(bool[] signals)
        {
            //отправка сигнала X-нейронам 
            int N = 0;
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
                    z_in += Xi[ii].out_x() * v_ij[jj, ii];
                }
                Zj[jj] = new neuronZ(z_in);
                z_in = 0.0;
            }

            double y_in = 0.0;
            //отправка сигналов Y-нейронам
            for (int kk = 0; kk < k; kk++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    y_in += Zj[jj].out_z() * w_jk[kk, jj];
                }
                Yk[kk] = new neuronY(y_in);
                y_in = 0.0;
            }
            
            
        }
        public void backpropagation(double expect)
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
                    delta_w_jk[kk, jj] = sigma_k[kk] * (Yk[kk].out_y() * (1 - Yk[kk].out_y()));
                }
            }
            //ИЗМЕНЕНИЕ ВЕСОВ w_jk
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    w_jk[kk, jj] -= (Zj[jj].out_z() * delta_w_jk[kk, jj] * learining_rate);
                }
            }


            //вычисление величины ошибки для v_ij
            for (int jj = 0; jj < j; jj++)
            {
                for (int kk = 0; kk < k; kk++)
                {
                    sigma_j[jj] = delta_w_jk[kk,jj] * w_jk[kk, jj];
                }
            }
            ////вычисление корректировки весов v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    delta_v_ij[jj, ii] = sigma_j[jj] * (Zj[jj].out_z()*(1 - Zj[jj].out_z()));
                }
            }
            //ИЗМЕНЕНИЕ ВЕСОВ v_ij
            for (int ii = 0; ii < i; ii++)
            {
                for (int jj = 0; jj < j; jj++)
                {
                    v_ij[jj, ii] -= (Xi[ii].out_x() * delta_v_ij[jj, ii] * learining_rate);
                }
            }
            ;
        }
        /// <summary>
        /// производная анализирующей функции
        /// </summary>
        /// <param name="x">аргумент</param>
        /// <returns>значение</returns>
        private double ff(double  x)
        {
            return f(x) * (1.0 - f(x));
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
        public double  sumSigma_k()
        {
            double  sum = 0;
            for (int kk = 0; kk < k; kk++)
            {
                sum += sigma_k[kk];
            }
            return sum;
        }

     
        public double getY()
        {
            return Yk[0].out_y();
        }
        public double  sum_Yk()
        {
            double sum = 0.0;
            double d = 0.0;
            for (int kk = 0; kk < k; kk++)
            {
                sum += Yk[kk].out_y();
            }
            return sum;
        }

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

            int n = 100;
            while(n != 0)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    bool[] sub = new bool[3]
                    {
                       train[ii, 0], train[ii, 1], train[ii, 2]
                    };
                    work(sub);
                    double Y = getY();
                    if (Y > 0.5 && train[ii, 3] == false)
                        backpropagation(1.0);
                    if (Y < 0.5 && train[ii, 3] == true)
                        backpropagation(0.0);
                    double correct = train[ii, 3] ? 1.0 : 0.0;
                    Console.WriteLine(MSB(correct, Y));
                }
                --n;
            }
        }
          
        
        private double MSB(double coorect, double Yk)
        {
            return Math.Pow(Yk - coorect, 2);
        }
       
       
    }
}
