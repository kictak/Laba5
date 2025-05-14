namespace Laba5.IntegratorFolder
{
    public class IntegratorMethodSimpson
    {
        private readonly Equation equation;

        /// <summary>
        /// Конструктор класса "интегратор"
        /// </summary>
        /// <param name="equation">интегрируемое уравнение</param>
        public IntegratorMethodSimpson(Equation equation)
        {
            //проверяем допустимость параметров:
            if (equation == null)
            {
                throw new ArgumentNullException();
            }
            this.equation = equation;
        }

        /// <summary>
        /// Функция интегрирования
        /// </summary>
        /// <param name="x1">левая граница интегрирования</param>
        /// <param name="x2">правая граница интегрирования</param>
        public double Integrate(double x1, double x2, int N)// количество интервалов вынес сюда
        {
            //проверяем допустимость параметров:
            if (x1 >= x2)
            {
                throw new ArgumentException("Правая граница интегирования должны быть больше левой!");
            }
            if (N % 2 != 0)
            {
                throw new ArgumentException("N должно быть чётным!");
            } 
            if (N <= 0)
            {
                throw new ArgumentException("N должно быть больше нуля!");
            }

            double h = (x2 - x1) / N;//определяем ширину интервала:
            double sum = 0; //"накопитель" для значения интеграла
            //Считаем сумму всех весов точек на отрезке x1,x2,x3,x4 и т.д потом делим на h/3
            for (int i = 0; i <= N; i++)
            {
                double value = equation.GetValue(x1 + i * h);//Функция подсчёта весов 
                if (i == 0 || i == N)
                    sum += value * 1;//Вес 1 для начальной и последней точки
                else if (i % 2 != 0)
                    sum += value * 4;//Вес 4 для нечётных точек
                else
                    sum += value * 2;// ВЕс 2 для чётных точек
            }
            return sum * (h/3);
        }
    }
}