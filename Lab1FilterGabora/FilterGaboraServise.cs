namespace Lab1FilterGabora
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class FilterGaboraServise
    {
        public double[,] FilterMatrix(double[,] matrix, double[,] filter)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            var Rf = filter.GetLength(0);
            var filteredMatrix = new double[n - Rf + 1, m - Rf + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    filteredMatrix[i, j] = 1 / Math.Pow(Rf, 2) * CountSummFilter(matrix, filter, i, j);
                }
            }
            return filteredMatrix;
        }

        public async Task<double[,]> FilterMatrixAsync(double[,] matrix, double[,] filter)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            var Rf = filter.GetLength(0);
            var filteredMatrix = new double[n - Rf + 1, m - Rf + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                   await Task.Run(() => 
                    filteredMatrix[i, j] = 1 / Math.Pow(Rf, 2) * CountSummFilter(matrix, filter, i, j));  
                }
            }
            return filteredMatrix;
        }

        public double[,] FilterMatrixMultitreading(double[,] matrix, double[,] filter, int D)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            var Rf = filter.GetLength(0);
            var filteredMatrix = new double[n - Rf + 1, m - Rf + 1];
            var countThreads = D;
            var countIterationsForThread = (int)n / D;
            var threads = new Thread[countThreads];
            for (int c = 0; c < countThreads; c++)
            {
                var numberStartIterationForThread = countIterationsForThread * c;
                var numberEndIterationForThread = c == countThreads - 1 ? n : countIterationsForThread * (c + 1);
                threads[c] = new Thread(() =>
                {
                    for (int i = numberStartIterationForThread; i < numberEndIterationForThread; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            filteredMatrix[i, j] = 1 / Math.Pow(Rf, 2) * CountSummFilter(matrix, filter, i, j);
                        }
                    }
                });
                threads[c].Start();
            }

            var AllThreadsDone = false;

            while (!AllThreadsDone)
            {
                AllThreadsDone = true;
                foreach (var thread in threads)
                {
                    if (thread.ThreadState == ThreadState.Running)
                        AllThreadsDone = false;
                }
            }
            
            return filteredMatrix;
        }

        public double[,] CreateFelter(int Rf, double O)
        {
            var filter = new double[Rf, Rf];
            var a = 0.0036 * Math.Pow(Rf, 2) + 0.35 * Rf + 0.18;
            var la = a / 0.8;
            for (int i = 0; i < Rf; i++)
            {
                for (int j = 0; j < Rf; j++)
                {
                    var x = i + 1;
                    var y = j + 1;
                    filter[i, j] = Math.Exp(
                                    -(Math.Pow(x * Math.Cos(O) + y * Math.Sin(O), 2) + 
                                        Math.Pow(0.3, 2) * Math.Pow(-x * Math.Cos(O) + y * Math.Sin(O), 2)) /
                                        (2 * Math.Pow(a, 2))) * 
                                        Math.Cos((2 * Math.PI / la) * (x * Math.Cos(O) + y * Math.Sin(O)));
                }
            }
            return filter;
        }

        public async Task<double[,]> CreateFelterAsync(int Rf, double O)
        {
            var filter = new double[Rf, Rf];
            var a = 0.0036 * Math.Pow(Rf, 2) + 0.35 * Rf + 0.18;
            var la = a / 0.8;
            for (int i = 0; i < Rf; i++)
            {
                for (int j = 0; j < Rf; j++)
                {
                    var x = i + 1;
                    var y = j + 1;

                    await Task.Run(() => filter[i, j] = Math.Exp(
                                    -(Math.Pow(x * Math.Cos(O) + y * Math.Sin(O), 2) +
                                        Math.Pow(0.3, 2) * Math.Pow(-x * Math.Cos(O) + y * Math.Sin(O), 2)) /
                                        (2 * Math.Pow(a, 2))) *
                                        Math.Cos((2 * Math.PI / la) * (x * Math.Cos(O) + y * Math.Sin(O))));
                    
                }
            }
            return filter;
        }

        private double CountSummFilter(double[,] maatrix, double[,] filter, int x, int y)
        {
            var n = maatrix.GetLength(0);
            var m = maatrix.GetLength(1);
            var Rf = filter.GetLength(0);
            var sumI = 0.0;
            for (int i = 0; i < n; i++)
            {
                var sumJ = 0.0;
                for (int j = 0; j < m; j++)
                {
                    var dimension1 = x - Rf / 2 + i;
                    var dimension2 = y - Rf / 2 + j;
                    if (dimension1 < 0 || dimension2 < 0)
                        continue;
                    sumJ += maatrix[dimension1, dimension2] * filter[i, j];
                }

                sumI += sumJ;
            }
            return sumI;
        }
    }
}
