using Laba5.IntegratorFolder;

class Program
{
    static void Main(string[] args)
    {
        Func<double, double> function = x => 20 * x;
        IntegratorBase integrator = new IntegratorMethodRectangle(function);

        // Обработчики как локальные переменные, чтобы можно было отписаться
        EventHandler<EventArgs> startHandler = (sender, e) => Console.WriteLine("Начало расчётов!");
        EventHandler<IntegratorStepEventArgs> stepConsoleHandler = WriteToConsole;
        EventHandler<IntegratorStepEventArgs> stepFileHandler = WriteToFile;
        EventHandler<IntegratorFinishEventArgs> finishMessageHandler = ShowMessage;
        EventHandler<IntegratorFinishEventArgs> finishStringHandler = AppendToString;

        // Тест 1: С двумя обработчиками
        Console.WriteLine("Тест с двумя обработчиками:");
        integrator.OnStart += startHandler;
        integrator.OnStep += stepConsoleHandler;
        integrator.OnStep += stepFileHandler;
        integrator.OnFinish += finishMessageHandler;
        integrator.OnFinish += finishStringHandler;

        double result = integrator.Integrate(10, 30, 100);
        Console.WriteLine($"Итог: {result}\n");

        // Очищаем подписчиков с помощью -=
        integrator.OnStart -= startHandler;
        integrator.OnStep -= stepConsoleHandler;
        integrator.OnStep -= stepFileHandler;
        integrator.OnFinish -= finishMessageHandler;
        integrator.OnFinish -= finishStringHandler;

        // Тест 2: С одним обработчиком
        Console.WriteLine("Тест с одним обработчиком (только консоль):");
        integrator.OnStart += startHandler;
        integrator.OnStep += stepConsoleHandler;
        integrator.OnFinish += finishMessageHandler;

        result = integrator.Integrate(10, 30, 100);
        Console.WriteLine($"Итог: {result}\n");

        // Очищаем подписчиков
        integrator.OnStart -= startHandler;
        integrator.OnStep -= stepConsoleHandler;
        integrator.OnFinish -= finishMessageHandler;

        // Тест 3: Без обработчиков
        Console.WriteLine("Тест без обработчиков:");
        result = integrator.Integrate(10, 30, 100);
        Console.WriteLine($"Итог: {result}");

        Console.ReadLine();
    }

    static void WriteToConsole(object sender, IntegratorStepEventArgs e)
    {
        Console.WriteLine($"x={e.X:F2}, f={e.F:F2}, sum={e.Integr:F2}");
    }

    static void WriteToFile(object sender, IntegratorStepEventArgs e)
    {
        using (StreamWriter writer = new StreamWriter("integration_steps.txt", true))
        {
            writer.WriteLine($"x={e.X:F2}, f={e.F:F2}, sum={e.Integr:F2}");
        }
    }

    static void ShowMessage(object sender, IntegratorFinishEventArgs e)
    {
        Console.WriteLine($"Расчёты завершены! Интеграл = {e.Integr:F2}");
    }

    static string resultString = "";
    static void AppendToString(object sender, IntegratorFinishEventArgs e)
    {
        resultString += $"Итоговый интеграл: {e.Integr:F2}\n";
        Console.WriteLine("Строка обновлена, проверь resultString.");
    }
}