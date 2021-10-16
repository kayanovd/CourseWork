using System;
using System.Collections.Generic;

namespace CourseWork
{
    public class Integral
    {
        private static double startPoint, endPoint, width, total, eps, diff,
            lRecResult, rRecResult, mRecResult, trapResult, sympResult;

        private Func<double, double, double> Func;
        private delegate double Delegate(int numberOfParts);

        public double StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public double EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public double Eps
        {
            get { return eps; }
            set { eps = value; }
        }

        public Func<double, double, double> Function
        {
            get { return Func; }
            set { Func = value; }
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

            int k = 10;
            int i = 0;
            do
            {
                i++;
                diff = Math.Abs(delegates[method].Invoke(k * i) - delegates[method].Invoke(k * (i + 1)));
            } while (diff > eps);

            return delegates[method].Invoke(k * (i + 1));
        }

        private double LeftRectangle(int numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 0; i < numberOfParts; i++)
                total += Func(startPoint + width * i, width);
            lRecResult = width * total;
            return lRecResult;
        }

        private double MiddleRectangle(int numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 1; i <= numberOfParts; i++)
                total += Func(startPoint + width * i - width / 2, width);
            mRecResult = width * total;
            return mRecResult;
        }

        private double RightRectangle(int numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = 0;
            for (int i = 1; i <= numberOfParts; i++)
                total += Func(startPoint + width * i, width);
            rRecResult = width * total;
            return rRecResult;
        }

        private double Trapezium(int numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = (width / 2) * (Func(startPoint, width) + Func(endPoint, width));
            for (int i = 1; i < numberOfParts; i++)
                total += Func(startPoint + width * i, width) * width;
            trapResult = total;
            return trapResult;
        }

        private double Sympson(int numberOfParts)
        {
            width = (endPoint - startPoint) / numberOfParts;
            total = (Func(startPoint, width) + Func(endPoint, width));
            for (int i = 1; i < numberOfParts; i++)
            {
                int k = 2 + 2 * (i % 2);
                total += k * Func(startPoint + width * i, width);
            }
            sympResult = total * width / 3;
            return sympResult;
        }
    }
}