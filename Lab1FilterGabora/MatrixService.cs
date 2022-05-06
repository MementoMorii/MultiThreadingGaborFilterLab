namespace Lab1FilterGabora
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class MatrixService
    {
        public double[,] CreateMatrix(int D, int S)
        {
            var randon = new Random();
            var n = (int)Math.Pow(2, D);
            var m = (int)Math.Pow(2, S);
            var matrix = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = randon.NextDouble() * 100;
                }
            }

            return matrix;
        }
    }
}
