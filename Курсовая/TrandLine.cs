using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class TrandLine
    {
        public List<double> F(List<double> X, List<double> Y)
        {
            double a = 0.0;
            double b = 0.0;
            double MidX = X.Average();
            double MidY = Y.Average();
            for (int i = 0; i < X.Count; i++)
            {
                a += (X[i] - MidX) * (Y[i] - MidY);
                b += (X[i] - MidX) * (X[i] - MidX);
            }
            a = a / b;
            b = MidY - a * MidX;
            List<double> y = new List<double>();
            for (int i = 0; i < X.Count; i++)
                y.Add(a * X[i] + b);
            return y;
        }
    }
}
