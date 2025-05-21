namespace Laba5.IntegratorFolder
{
    public abstract class IntegratorBase
    {
        protected readonly Func<double, double> function;

        // События с использованием EventHandler
        public event EventHandler<IntegratorStepEventArgs> OnStep;   // Для каждого шага
        public event EventHandler<IntegratorFinishEventArgs> OnFinish; // Для окончания
        public event EventHandler<EventArgs> OnStart;                // Для начала расчётов

        public IntegratorBase(Func<double, double> function)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));
            this.function = function;
        }

        public virtual string MethodName => "Общий метод интегрирования";

        public abstract double Integrate(double x1, double x2, int N);

        // Методы для вызова событий
        protected void RaiseStepEvent(double x, double f, double sum)
        {
            OnStep?.Invoke(this, new IntegratorStepEventArgs(x, f, sum));
        }

        protected void RaiseFinishEvent(double sum)
        {
            OnFinish?.Invoke(this, new IntegratorFinishEventArgs(sum));
        }

        protected void RaiseStartEvent()
        {
            OnStart?.Invoke(this, EventArgs.Empty);
        }
    }
}