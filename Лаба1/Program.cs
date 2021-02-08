using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Лаба1
{
    class Program
    {
        static void Main(string[] args)
        {
            int size;
            size = Convert.ToInt32(Console.ReadLine());
            Random ran = new Random();
            int[] Data = new int[size];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = ran.Next();
            }
            WriteArray(Data);

            Console.ReadKey();
        }
        public int LinearSearcher(int[] data, int item)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }
        public void BubbleSort(ref int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
        }
        public void ShellSort(ref int[] mass)
        {
            int tmp;
            int n = mass.Length;
            for (int step = n / 2; step > 0; step /= 2)
                for (int i = step; i < n; i++)
                {
                    int j;
                    tmp = mass[i];
                    for (j = i; j >= step; j -= step)
                    {
                        if (tmp < mass[j - step])
                            mass[j] = mass[j - step];
                        else
                            break;
                    }
                    mass[j] = tmp;
                }
        }
        public void WriteArray(int[] Data)
        {
            for (int i=0; i<Data.Length; i++)
            {
                Console.Write("{0} ", Data[i]);
            }
            Console.WriteLine();
        }
    }
}
