using System;
using static CourseWork.Integral;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            static double Function(double x, double h) => Math.Cos(x);
            Integral integral = new();
            integral.StartPoint = 0;
            integral.EndPoint = 1;
            integral.Eps = 0.00000000001;
            integral.Function = Function;
            Console.WriteLine(integral.CalculateIntegral(Methods.TrapeziumMethod));
        }
    }
}