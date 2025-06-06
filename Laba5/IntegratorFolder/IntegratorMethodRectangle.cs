﻿#region Старый вариант
/*
//a.	Интегрирование методом прямоугольников.
namespace Laba5
{
    public class Integrator
    {
        private readonly Equation equation;

        /// <summary>
        /// Конструктор класса "интегратор"
        /// </summary>
        /// <param name="equation">интегрируемое уравнение</param>
        public Integrator(Equation equation)
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
            / для интегирования разобъем исходный отрезок на 100 точек.
              Считаем значение функции в точке, умножаем на ширину интервала.
              Площадь полученного прямоугольника приблизительно равна значению интеграла на этом отрезке
              суммируем значения площадей, получаем значение интеграла на отрезке [X1;X2]
            //int N = 100;    //количество интервалов разбиения
            //определяем ширину интервала:
            double h = (x2 - x1) / N;
            double sum = 0; //"накопитель" для значения интеграла
            for (int i = 0; i < N; i++)
            {
                sum = sum + (equation.GetValue(x1 + i * h) + equation.GetValue(x1 + i * h)) / 2 * h;
            }
            return sum;
        }
    }
}

*/
#endregion

namespace Laba5.IntegratorFolder
{
    public class IntegratorMethodRectangle : IntegratorBase
    {
        public IntegratorMethodRectangle(Func<double, double> function) : base(function) { }

        public override string MethodName => "Метод прямоугольников";

        public override double Integrate(double x1, double x2, int N)
        {
            if (x1 >= x2) throw new ArgumentException("Правая граница должна быть больше левой!");
            RaiseStartEvent();

            DateTime startTime = DateTime.Now; // Начало измерения времени
            double h = (x2 - x1) / N;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                double x = x1 + i * h;
                double fx = function(x);
                sum += fx * h;
                RaiseStepEvent(x, fx, sum);
                System.Threading.Thread.Sleep(100); // Задержка 100 мс
            }
            TimeSpan duration = DateTime.Now - startTime; // Длительность

            RaiseFinishEvent(sum);

            // Вывод времени через событие OnFinish
            OnFinish += (sender, e) => Console.WriteLine($"Время расчётов: {duration.TotalMilliseconds:F2} мс");

            return sum;
        }
    }
}