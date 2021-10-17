using System;
using System.Collections.Generic;

namespace CourseWork
{
    public class Integral
    {
        private double startPoint, endPoint, width, total, eps;

        private Func<double, double, double> Func;
        private delegate double Delegate(uint numberOfParts);

        public double StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }

        public double EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }

        public double Eps
        {
            get => eps;
            set => eps = value;
        }

        public Func<double, double, double> Function
        {
            get => Func;
            set => Func = value;
        }

        public enum Methods
        {
            LeftRectangleMethod,
            MiddleRectangleMethod,
            RightRectangleMethod,
            TrapeziumMethod,
            SympsonMethod
        }

        public double CalculateIntegral(Methods method)
        {
            Dictionary<Methods, Delegate> delegates = new()
            {
                [Methods.LeftRectangleMethod] = new Delegate(LeftRectangle),
                [Methods.MiddleRectangleMethod] = new Delegate(MiddleRectangle),
                [Methods.RightRectangleMethod] = new Delegate(RightRectangle),
                [Methods.TrapeziumMethod] = new Delegate(Trapezium),
                [Methods.SympsonMethod] = new Delegate(Sympson)
            };

            uint k = 10;
            uint i = 0;
            double diff;
            do
            {
                i++;
                diff = Math.Abs(delegates[method](k * i) - delegates[method](k * (i + 1)));
            } while (diff > eps);

            return delegates[method](k * (i + 1));
        }

        private double LeftRectangle(uint numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 0; i < numberOfParts; i++)
                total += Func(startPoint + width * i, width);
            return width * total;
        }

        private double MiddleRectangle(uint numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 1; i <= numberOfParts; i++)
                total += Func(startPoint + width * i - width / 2, width);
            return width * total;
        }

        private double RightRectangle(uint numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 1; i <= numberOfParts; i++)
                total += Func(startPoint + width * i, width);
            return width * total;
        }

        private double Trapezium(uint numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = (width / 2) * (Func(startPoint, width) + Func(endPoint, width));
            for (int i = 1; i < numberOfParts; i++)
                total += Func(startPoint + width * i, width) * width;
            return total;
        }

        private double Sympson(uint numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = (Func(startPoint, width) + Func(endPoint, width));
            for (int i = 1; i < numberOfParts; i++)
            {
                int k = 2 + 2 * (i % 2);
                total += k * Func(startPoint + width * i, width);
            }
            return total * width / 3;
        }
    }
}