using Laba5.EquationFolder;

namespace Laba5
{
    /// <summary>
    /// Класс, представляющий квадратное уравнение
    /// </summary>
    public class QuadEquation : Equation
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public QuadEquation(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double GetValue(double x)
        {
            return a * x * x + b * x + c;
        }
    }
}