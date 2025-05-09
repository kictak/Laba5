namespace Laba5.EquationFolder
{
    /// <summary>
    /// Класс, представляющий функцию xcos(x)/a.
    /// </summary>
    public class CosinDivAEquation : Equation
    {
        private readonly double a;

        public CosinDivAEquation(double a)
        {
            if (a == 0)
            {
                throw new ArgumentException("Параметр a не может быть равен нулю");
            }
            this.a = a;
        }

        public override double GetValue(double x)
        {
            return (x * Math.Cos(x)) / a;
        }
    }
}