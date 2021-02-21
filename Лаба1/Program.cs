using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Лаба1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            Console.Write("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            int[] Data = new int[size];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = ran.Next(100);
            }

            Console.WriteLine("Массив: ");
            WriteArray(Data);

            Console.Write("Введите элемент: ");
            int find = Convert.ToInt32(Console.ReadLine());
            int item = LinearSearcher(Data, find);
            Console.WriteLine("Элемент: " + item);
            Console.WriteLine();

            BubbleSort(ref Data);
            Console.WriteLine("Сортировка пузырьком: ");
            WriteArray(Data);

            ShellSort(ref Data);
            Console.WriteLine("Сортировка Шелла: ");
            WriteArray(Data);
            Console.ReadKey();
        }
        public static int LinearSearcher(int[] Data, int item)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }
        public static void BubbleSort(ref int[] Data)
        {
            for (int i = 0; i < Data.Length - 1; i++)
            {
                for (int j = i + 1; j < Data.Length; j++)
                {
                    if (Data[j] > Data[i])
                    {
                        int temp = Data[j];
                        Data[j] = Data[i];
                        Data[i] = temp;
                    }
                }
            }
        }
        public static void ShellSort(ref int[] Data)
        {
            for (int step = Data.Length / 2; step > 0; step /= 2)
            {
                for (int i = step; i < Data.Length; i++)
                {
                    int temp;
                    int j;
                    temp = Data[i];
                    for (j = i; j >= step; j -= step)
                    {
                        if (temp < Data[j - step])
                            Data[j] = Data[j - step];
                        else
                            break;
                    }
                    Data[j] = temp;
                }
            }
        }
        static void WriteArray(int[] Data)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Console.Write("Data[{0}] = {1} \n", i, Data[i]);
            }
            Console.WriteLine();
        }
    }
}
