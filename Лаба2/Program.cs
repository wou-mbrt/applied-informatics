using System;

namespace Лаба2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Немного объяснений
             * xi,yi это в мат. формулах i+1
             * x,y это в мат. формулах i
             * x1,y1 это в мат. формулах i-1
             * x2,y2 это в мат. формулах i-2
             * x3,y3 это в мат. формулах i-3
            */
            double x = 3.5;
            double y = Y_formula(x);
            double h = 0.5;
            double yi = RungeKutta(h, x, y);

            Console.WriteLine("h = {0} \t x = {1}", h, x);
            Console.WriteLine("y = {0:F3} \t yi = {1:F3}", y, yi);
        }
        /// <summary>
        /// Формула для нахождения Y, можно менять
        /// </summary>
        /// <param name="x">Значение X</param>
        /// <returns>System.double Y</returns>
        public static double Y_formula(double x)
        {
            double y = x;
            return y;
        }
        /// <summary>
        /// Формула для X, Y, можно изменять
        /// </summary>
        /// <param name="x">Значение X</param>
        /// <param name="y">Значение Y</param>
        /// <returns>double результат расчета</returns>
        public static double FFormula(double x, double y)
        {
            return Math.Pow(x, y);
        }
        /// <summary>
        /// Формула для одного K
        /// </summary>
        /// <param name="h">Шаг</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>double K</returns>
        public static double KFormula(double h, double x, double y)
        {
            double k;
            k = h * FFormula(x, y);
            return k;
        }
        /// <summary>
        /// формула Рунге-Кутта, рассчитывает Yi
        /// </summary>
        /// <param name="h">Шаг</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>double Yi</returns>
        public static double RungeKutta(double h, double x, double y)
        {
            double k0 = h * FFormula(x, y);
            double k1 = KFormula(h, x + h / 2, y + k0 / 2);
            double k2 = KFormula(h, x + h / 2, y + k1 / 2);
            double k3 = KFormula(h, x + h, y + k2);

            double yi = y + (k0 + 2 * k1 + 2 * k2 + k3) / 6;
            return yi;
        }
        /// <summary>
        /// Метод Прогноза и коррекции, расчитывает Yi
        /// </summary>
        /// <param name="h">h</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        /// <param name="x3">x3</param>
        /// <param name="y3">y3</param>
        /// <returns>double Yi</returns>
        public static double PredictionAndCorrection(double h, double x, double y, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double yi = y * h / 24 * (55 * FFormula(x, y) - 59 * FFormula(x1, y1) + 37 * FFormula(x2, y2) - 9 * FFormula(x3, y3));
            return yi;
        }
        /// <summary>
        /// Метод Адамса, рассчитывает Yi
        /// </summary>
        /// <param name="h">h</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        /// <param name="x3">x3</param>
        /// <param name="y3">y3</param>
        /// <returns>double Yi</returns>
        public static double Adams(double h, double x, double y, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double deltaF1 = FFormula(x, y) - FFormula(x1, y1);
            double deltaF2 = FFormula(x, y) - 2 * FFormula(x1, y1) + FFormula(x2, y2);
            double deltaF3 = FFormula(x, y) - 3 * FFormula(x1, y1) + 3 * FFormula(x2, y2) - FFormula(x3, y3);

            double yi = y + h * y + h * h / 2 * deltaF1 + 5 * h * h * h / 12 * deltaF2 + 3 * h * h * h * h / 8 * deltaF3;
            return yi;
        }
        /// <summary>
        /// Метод последовательных приближений, рассчитывается Yi
        /// </summary>
        /// <param name="x0">Начальный X</param>
        /// <param name="y0">Начальный Y</param>
        /// <param name="x">Конечный X</param>
        /// <param name="y">Конечный y</param>
        /// <returns>double yi</returns>
        public static double ConsecutiveApproximations(double x0, double y0, double x, double y)
        {
            double yi = y0 + FFormula(x, y) - FFormula(x0, y);
            return yi;
        }
    }
}
