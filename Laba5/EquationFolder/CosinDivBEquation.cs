namespace Laba5.EquationFolder
{
    /// <summary>
    /// Класс, представляющий функцию (x^2)*cos(x−a)/b"
    /// </summary>
    public class CosinDivBEquation : Equation
    {
        private readonly double a;
        private readonly double b;

        public CosinDivBEquation(double a, double b)
        {
            if (b == 0)
            {
                throw new ArgumentException("Делить на 0 нельзя");
            }
            this.a = a;
            this.b = b;
        }

        public override double GetValue(double x)
        {
            return ((x * x) * Math.Cos(x - a)) / b;
        }
    }
}