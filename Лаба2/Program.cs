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
            double h = 0.1;
            double x3 = 0.1;
            double y3 = Y_formula(x3);
            double x2 = x3 + h;
            double y2 = RungeKutta(h, x3, y3);
            double x1 = x2 + h;
            double y1 = RungeKutta(h, x2, y2);
            double x = x1 + h;
            double y = RungeKutta(h, x1, y1);
            double xi = x + h;
            double yi = RungeKutta(h, x, y);

            Console.WriteLine("Начальные значения");
            Console.WriteLine("h = {0:F2} \t x3 = {1:F2} \t y3 = {2:F2}", h, x3, y3);
            Console.WriteLine("h = {0:F2} \t x2 = {1:F2} \t y2 = {2:F2}", h, x2, y2);
            Console.WriteLine("h = {0:F2} \t x1 = {1:F2} \t y1 = {2:F2}", h, x1, y1);
            Console.WriteLine("h = {0:F2} \t x  = {1:F2} \t y  = {2:F2}", h, x, y);

            Console.WriteLine("Метод Рунге-Кутта:");
            Console.WriteLine("yi = {0:F3}", yi);
            Console.WriteLine();

            Console.WriteLine("Метод прогноза:");
            yi = Prediction(h, x, y, x1, y1, x2, y2, x3, y3);
            Console.WriteLine("yi = {0:F3}", yi);
            Console.WriteLine();

            Console.WriteLine("Метод коррекции:");
            yi = Correction(h, xi, yi, x, y, x1, y1, x2, y2);
            Console.WriteLine("yi = {0:F3}", yi);
            Console.WriteLine();

            Console.WriteLine("Метод Адамса:");
            yi = Adams(h, x, y, x1, y1, x2, y2, x3, y3);
            Console.WriteLine("yi = {0:F3}", yi);
            Console.WriteLine();
            
            Console.WriteLine("Метод последовательных приближений:");
            yi = ConsecutiveApproximations(x3,y3,3);
            Console.WriteLine("yi = {0:F3}", yi);



            Console.WriteLine("");
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
            return x + y;
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
            return h * FFormula(x, y);
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

            return y + (k0 + 2 * k1 + 2 * k2 + k3) / 6;
        }
        /// <summary>
        /// Прогноз, расчитывает Yi
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
        public static double Prediction(double h, double x, double y, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return y * h / 24 * (55 * FFormula(x, y) - 59 * FFormula(x1, y1) + 37 * FFormula(x2, y2) - 9 * FFormula(x3, y3));
        }
        /// <summary>
        /// Прогноз, расчитывает Yi
        /// </summary>
        /// <param name="h">h</param>
        /// <param name="x_i">x i+1</param>
        /// <param name="y_i">y i+1</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="x1">x i-1</param>
        /// <param name="y1">y i-1</param>
        /// <param name="x2">x i-2</param>
        /// <param name="y2">y i-2</param>
        /// <returns>double yi</returns>
        public static double Correction(double h, double x_i, double y_i, double x, double y, double x1, double y1, double x2, double y2)
        {
            return y * h / 24 * (9 * FFormula(x_i, y_i) - 19 * FFormula(x, y) + 5 * FFormula(x1, y1) + FFormula(x2, y2));
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

            return y + h * y + h * h / 2 * deltaF1 + 5 * h * h * h / 12 * deltaF2 + 3 * h * h * h * h / 8 * deltaF3;
        }
        /// <summary>
        /// Расчет интеграла в 4 задаче
        /// </summary>
        /// <param name="x0">X</param>
        /// <param name="steps">Количество шагов</param>
        /// <returns>Сумма интеграла</returns>
        public static double Integral(double x0, double steps)
        {
            double integral = 0;
            for (int i = 1; i < steps+1; i++)
            {
                integral += Math.Pow(x0, i) / i;
            }
            return integral;
        }
        /// <summary>
        /// Метод последовательных приближений, рассчитывается Yi
        /// </summary>
        /// <param name="x0">Начальный X</param>
        /// <param name="y0">Начальный Y</param>
        /// <param name="x">Конечный X</param>
        /// <param name="y">Конечный y</param>
        /// <returns>double yi</returns>
        public static double ConsecutiveApproximations(double x0, double y0, double n)
        {
            return y0 + Integral(x0, n);
        }
    }
}
