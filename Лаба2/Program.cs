using System;

namespace Лаба2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            double x = 3.5;
            double y = Y_formula(x);
            double h = 0.5;
            double yi = Yi_formula(h, x, y);
            Console.WriteLine("h = {0} \t x = {1}", h, x);
            Console.WriteLine("y = {0:F3} \t yi = {1:F3}", y, yi);
        }
        public static double Y_formula(double x)
        {
            double y;
            y = Math.Cos(x);
            return y;
        }
        public static double F_formula(double x, double y)
        {
            return Math.Pow(x, y);
        }
        public static double K_formula(double h, double x, double y)
        {
            double k;
            k = h * F_formula(x, y);
            return k;
        }
        public static double Yi_formula(double h, double x, double y)
        {
            double k0 = h * F_formula(x, y);
            double k1 = K_formula(h, x + h / 2, y + k0 / 2);
            double k2 = K_formula(h, x + h / 2, y + k1 / 2);
            double k3 = K_formula(h, x + h, y + k2);
            double yi = y + (k0 + 2 * k1 + 2 * k2 + k3) / 6;
            return yi;
        }
    }
}
