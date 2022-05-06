using System;

namespace Lab1FilterGabora
{
    class Program
    {
        private static MatrixService matrixService = new MatrixService();
        private static FilterGaboraServise filterGaboraServise = new FilterGaboraServise();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите D");
            int D = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите S");
            int S = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите Rf");
            int Rf = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите θ");
            double O = double.Parse(Console.ReadLine());

            var martix = matrixService.CreateMatrix(D, S);
            var filter = filterGaboraServise.CreateFelter(Rf, O);
            Console.WriteLine($"Матрица фильтра Габора\n{filter}");

            var filteredMatrix = filterGaboraServise.FilterMatrix(martix, filter);

            Console.WriteLine($"Отфильтрованная матрица:\n{filteredMatrix}");
        }
    }
}
