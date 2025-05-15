namespace Laba5.IntegratorFolder
{
    // Базовый класс для всех методов интегрирования
    public abstract class IntegratorBase
    {
        protected readonly Func<double, double> function;

        public IntegratorBase(Func<double, double> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            this.function = function;
        }

        // Виртуальное свойство для названия метода
        public virtual string MethodName => "Общий метод интегрирования";

        // Абстрактный метод Integrate, который должны реализовать все наследники
        public abstract double Integrate(double x1, double x2, int N);
    }
}