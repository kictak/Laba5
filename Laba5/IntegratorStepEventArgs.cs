namespace Laba5.IntegratorFolder
{
    // Аргументы для события OnStep
    public class IntegratorStepEventArgs : EventArgs
    {
        public double X { get; set; }
        public double F { get; set; }
        public double Integr { get; set; }

        public IntegratorStepEventArgs(double x, double f, double integr)
        {
            X = x;
            F = f;
            Integr = integr;
        }
    }

    // Аргументы для события OnFinish
    public class IntegratorFinishEventArgs : EventArgs
    {
        public double Integr { get; set; }

        public IntegratorFinishEventArgs(double integr)
        {
            Integr = integr;
        }
    }
}