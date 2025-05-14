using Laba5.IntegratorFolder;

namespace Laba5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region QuadEquation

            Equation e = new QuadEquation(0, 20, 0);    //создаем объект класса "кв. уравнение"
            IntegratorMethodT i1 = new IntegratorMethodT(e); //создаем интегратор для этого уравнения
            double integrValue = i1.Integrate(10, 30, 1000); //вызываем интегрирование на интвервале 10;30
            Console.WriteLine(integrValue);
            //создаем интегратор для другого уравнения:
            IntegratorMethodT i2 = new IntegratorMethodT(new QuadEquation(-3, 0, 2.3));
            integrValue = i2.Integrate(-2, 100, 1000);    //вызываем ф-ю интегрирования
            Console.WriteLine(integrValue);

            #endregion QuadEquation
        }
    }
}