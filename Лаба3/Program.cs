using System;

namespace Лаба3
{
    class Program
    {
        class SLAU
        {
            float[,] A = new float[4, 4];
            float[] X = new float[4];
            float[] B = new float[4];
            // Конструктор, переписывать не обязательно ибо у меня 1 вариант
            public SLAU()
            {
                this.A = new float[,]
                {
                    {1,2,2,1},
                    {2,3,3,2},
                    {3,5,4,2},
                    {4,7,6,2}
                };
                this.B = new float[] { 0, 0, 1, 2 };
                this.X = new float[] { 0, 0, 0, 0 };
            }
            // Перегрузка работает по принципу объявленных типов переменных, а не их имен
            public SLAU(float nothing)
            {
                this.A = new float[,]
                {
                    {1.5f,0.5f,0,0},
                    {-1,2,-0.4f,0},
                    {0,2.5f,5,-2},
                    {0,0,1,3},
                };
                this.B = new float[] { 3.2f, -1, 4, 3 };
                this.X = new float[] { 0, 0, 0, 0 };
            }
            // Перегрузка метода, чтобы выводить значения которые сюда закидываются
            public void Print(float[,] A, float[] B)
            {
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    Console.WriteLine("{0}X1 {1}X2 {2}X3 {3}X4 = {4}", A[i, 0], A[i, 1], A[i, 2], A[i, 3], B[i]);
                }
                for (int i = 0; i < this.X.GetLength(0); i++)
                    Console.WriteLine("X{0} = {1}", i, this.X[i]);
                Console.WriteLine("");
            }
            // Вывод значений
            public void Print()
            {
                for (int i = 0; i < this.A.GetLength(0); i++)
                {
                    Console.WriteLine("{0}X1 {1}X2 {2}X3 {3}X4 = {4}", this.A[i, 0], this.A[i, 1], this.A[i, 2], this.A[i, 3], this.B[i]);
                }
                Console.WriteLine("");
            }
            // Метод Гаусса, берет значения из конструктора
            public void GaussMethod()
            {
                //Создание временных переменных, чтобы не менять начальные
                float[,] GaussA = this.A;
                float[] GaussB = this.B;
                // Сам метод Гаусса
                for (int i = 0; i < GaussA.GetLength(0); i++)
                {
                    GaussB[i] /= GaussA[i, i];
                    for (int j = 0; j < GaussA.GetLength(1); j++)
                    {
                        GaussA[i, j] /= GaussA[i, i];
                    }
                    for (int k = i + 1; k < GaussA.GetLength(0); k++)
                    {
                        for (int j = i; j < GaussA.GetLength(0); j++)
                        {
                            GaussA[k, j] -= GaussA[i, j] * GaussA[k, i];
                        }
                        GaussB[k] -= GaussB[i] * GaussA[k, i];
                        GaussA[k, i] = 0;
                    }
                }
                // Считаем X
                for (int i = GaussB.GetLength(0) - 1; i >= 0; i--)
                {
                    this.X[i] = GaussB[i];
                    for (int j = i + 1; j < GaussB.GetLength(0); j++)
                        this.X[i] -= this.X[j] * GaussA[i, j];
                }
                // Вывод полученных результатов
                Print(GaussA, GaussB);
            }
            // Метод прогона
            public void RunThroughMethod()
            {
                // Временные переменные
                float[] a = new float[B.GetLength(0)];
                float[] b = new float[B.GetLength(0)];
                float[] c = new float[B.GetLength(0)];
                //Задаем значения
                // Тут условия, ибо в задаче пропадают a и с, а b везде
                for (int i = 0; i < this.A.GetLength(0); i++)
                {
                    if (i == 0)
                        a[i] = 0;
                    else
                        a[i] = A[i, i - 1];

                    b[i] = A[i, i];

                    if (i == this.A.GetLength(0) - 1)
                        c[i] = 0;
                    else
                        c[i] = A[i, i + 1];
                }
                float[] tempA = new float[a.GetLength(0)];
                float[] tempB = new float[a.GetLength(0)];

                // Какие то математические формулы
                tempA[0] = -c[0] / b[0];
                tempB[0] = this.B[0] / b[0];
                for (int i = 1; i < tempA.GetLength(0) - 1; i++)
                {
                    float e = a[i] * tempA[i - 1] + b[i];
                    tempA[i] = -c[i] / e;
                    tempB[i] = this.B[i] - a[i] * tempB[i - 1] / e;
                }
                // Абсолютная магия из методички
                X[this.B.GetLength(0) - 1] = (B[this.B.GetLength(0) - 1] - a[this.B.GetLength(0) - 1] * tempB[this.B.GetLength(0) - 1]) / (b[this.B.GetLength(0) - 1] + a[this.B.GetLength(0) - 2] * tempA[this.B.GetLength(0) - 1]);
                for (int i = this.B.GetLength(0) - 2; i >= 0; i--)
                {
                    this.X[i] = tempA[i] * this.X[i + 1] + tempB[i];
                }
                Print(A, B);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Метод Гаусса");
                SLAU slau = new SLAU();
                slau.Print();
                slau.GaussMethod();

                Console.WriteLine("Метод Прогона");
                SLAU slauRun = new SLAU(0.0f);
                slauRun.RunThroughMethod();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}