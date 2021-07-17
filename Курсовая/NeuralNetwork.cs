using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class NeuralNetwork
    {
        private List<List<double>> W = new List<List<double>>();
        //Скалярное произведение двух Листов
        private double Dot(List<double> a, List<double> b)
        {
            double ans = 0.0;
            for (int i = 0; i < a.Count; i++)
            {
                ans += a[i] * b[i];
            }
            return ans;
        }
        //Вычитание
        private List<double> Minus(List<double> a, List<double> b)
        {
            List<double> answer = new List<double>();
            for (int i = 0; i < a.Count; i++)
            {
                answer.Add(a[i] - b[i]);
            }
            return answer;
        }
        //Инициализация весов
        private List<double> InitWeight(int n)
        {
            List<double> Weight = new List<double>();
            for (int i = 0; i < n; i++)
            {
                Weight.Add(0.1);
            }
            return Weight;
        }
        //Обучение нейронки
        public List<double> fit(List<List<double>> X_input, List<double> Y_output, int epochs)
        {
            W = new List<List<double>>() { InitWeight(3), InitWeight(3), InitWeight(3), InitWeight(3),
                                               InitWeight(4), InitWeight(4), InitWeight(2) };
            List<List<double>> Y = new List<List<double>>();
            List<double> y = new List<double>();
            int ki = 0;
            while (ki != epochs)
            {
                //Вычисление ответов всех нейронов
                Y = new List<List<double>>();
                y = new List<double>();
                for (int i = 0; i < X_input.Count; i++)
                {
                    Y.Add(new List<double> {Dot(X_input[i], W[0]),
                        Dot(X_input[i], W[1]),
                        Dot(X_input[i], W[2]),
                        Dot(X_input[i], W[3])});
                    Y[i].Add(Dot(new List<double> { Y[i][0], Y[i][1], Y[i][2], Y[i][3] }, W[4]));
                    Y[i].Add(Dot(new List<double> { Y[i][0], Y[i][1], Y[i][2], Y[i][3] }, W[5]));
                    Y[i].Add(Dot(new List<double> { Y[i][4], Y[i][5] }, W[6]));

                    y.Add(Y[i][6]);
                }

                //Вычисление ошибки
                List<double> d = Minus(y, Y_output);
                List<List<double>> D = new List<List<double>>();
                for (int i = 0; i < X_input.Count; i++)
                {
                    D.Add(new List<double>());
                    D[i].Add(W[6][1] * d[i]);
                    D[i].Add(W[6][0] * d[i]);
                    D[i].Add(W[4][3] * D[i][1] + W[5][3] * D[i][0]);
                    D[i].Add(W[4][2] * D[i][1] + W[5][2] * D[i][0]);
                    D[i].Add(W[4][1] * D[i][1] + W[5][1] * D[i][0]);
                    D[i].Add(W[4][0] * D[i][1] + W[5][0] * D[i][0]);
                }

                //Обновление весов
                for (int k = 0; k < X_input.Count; k++)
                {
                    for (int i = 0; i < W.Count; i++)
                    {
                        for (int j = 0; j < W[i].Count; j++)
                        {
                            if (i == 0)
                            {
                                W[i][j] -= 0.00001 * D[k][-(i - 5)] * X_input[k][j];
                            }
                            else if (i <= 5)
                            {
                                W[i][j] -= 0.00001 * D[k][-(i - 5)] * Y[k][i];
                            }
                            else if (i == 6)
                            {
                                W[i][j] -= 0.00001 * d[k] * Y[k][i];
                            }
                        }
                    }
                }
                ki++;
            }
            return y;
        }
        //Предсказание нейронки
        public double predict(List<List<double>> X_input)
        {
            int i = 0;
            List<List<double>> Y = new List<List<double>>();
            Y.Add(new List<double> {Dot(X_input[i], W[0]),
                        Dot(X_input[i], W[1]),
                        Dot(X_input[i], W[2]),
                        Dot(X_input[i], W[3])});
            Y[i].Add(Dot(new List<double> { Y[i][0], Y[i][1], Y[i][2], Y[i][3] }, W[4]));
            Y[i].Add(Dot(new List<double> { Y[i][0], Y[i][1], Y[i][2], Y[i][3] }, W[5]));
            Y[i].Add(Dot(new List<double> { Y[i][4], Y[i][5] }, W[6]));
            return Y[i][6];
        }
    }
}
