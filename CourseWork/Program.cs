using System;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            static double Function(double x, double h) => h / (1 + x * x);
            Integral integral = new();
            integral.StartPoint = 0;
            integral.EndPoint = 1;
            integral.Eps = 0.001;
            integral.Function = Function;
            Console.WriteLine(integral.CalculateIntegral(Integral.Methods.TrapeziumMethod));
        }
    }
}