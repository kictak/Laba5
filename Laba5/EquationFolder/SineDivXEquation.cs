namespace Laba5.EquationFolder
{
    /// <summary>
    /// Класс, представляющий функцию sin(ax)/x.
    /// </summary>
    public class SineDivXEquation : Equation
    {
        private readonly double a;

        public SineDivXEquation(double a)
        {
            this.a = a;
        }

        public override double GetValue(double x)
        {
            if (Math.Abs(x) < 1e-6) // 1e-6 = Math.Pow(10, -6)
            {
                return a;
            }
            return (Math.Sin(a * x)) / x;
        }
    }
}