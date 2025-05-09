namespace Laba5.EquationFolder
{
    /// <summary>
    /// Класс, представляющий функцию 𝑥∣sin(ax)∣
    /// </summary>
    public class XModuleEquation : Equation
    {
        private readonly double a;

        public XModuleEquation(double a)
        {
            this.a = a;
        }

        public override double GetValue(double x)
        {
            return x * (Math.Abs(Math.Sin(a * x)));
        }
    }
}