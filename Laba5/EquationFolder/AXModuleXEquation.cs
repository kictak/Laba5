namespace Laba5.EquationFolder
{
    /// <summary>
    /// Класс, представляющий функцию 𝑎𝑥∣sin⁡(𝑥)∣
    /// </summary>
    public class AXModuleXEquation : Equation
    {
        private readonly double a;

        public AXModuleXEquation(double a)
        {
            this.a = a;
        }

        public override double GetValue(double x)
        {
            return a * x * (Math.Abs(Math.Sin(x)));
        }
    }
}