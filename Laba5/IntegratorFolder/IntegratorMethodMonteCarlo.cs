


//сделал этот метод с помощью ии ради интереса т.к не смог разобраться



namespace Laba5.IntegratorFolder
{
    public class IntegratorMethodMonteCarlo : IntegratorBase
    {
        public IntegratorMethodMonteCarlo(Func<double, double> function) : base(function)
        {
        }

        public override string MethodName => "Метод Монте-Карло";

        public override double Integrate(double x1, double x2, int N)
        {
            if (x1 >= x2)
            {
                throw new ArgumentException("Правая граница должна быть больше левой!");
            }
            if (N <= 0)
            {
                throw new ArgumentException("Количество точек N должно быть больше 0!");
            }

            double fMax = FindMaxValue(x1, x2);

            Random rnd = new Random();
            int underCurveCount = 0;

            for (int i = 0; i < N; i++)
            {
                double x = x1 + rnd.NextDouble() * (x2 - x1);
                double y = rnd.NextDouble() * fMax;
                double fx = function(x);

                if (!double.IsNaN(fx) && !double.IsInfinity(fx) && !double.IsNegativeInfinity(fx))
                {
                    if (y <= fx)
                    {
                        underCurveCount++;
                    }
                }
            }

            double rectangleArea = (x2 - x1) * fMax;
            return rectangleArea * underCurveCount / N;
        }

        private double FindMaxValue(double x1, double x2)
        {
            const int samples = 1000;
            double h = (x2 - x1) / samples;
            double max = double.MinValue;

            for (int i = 0; i <= samples; i++)
            {
                double x = x1 + i * h;
                double fx = function(x);
                if (!double.IsNaN(fx) && !double.IsInfinity(fx) && !double.IsNegativeInfinity(fx))
                {
                    if (fx > max)
                    {
                        max = fx;
                    }
                }
            }

            return max > 0 ? max : 1;
        }
    }
}