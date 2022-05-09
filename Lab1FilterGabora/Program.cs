namespace Lab1FilterGabora
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    class Program
    {
        private static MatrixService matrixService = new MatrixService();
        private static FilterGaboraServise filterGaboraServise = new FilterGaboraServise();
        static void Main(string[] args)
        {
            var stopWhatch = new Stopwatch();

            Console.WriteLine("Введите D");
            int D = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите S");
            int S = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите Rf");
            int Rf = int.Parse(Console.ReadLine());
            double O = Math.PI / 2;

            var martix = matrixService.CreateMatrix(D, S);
            var filter = filterGaboraServise.CreateFelter(Rf, O);

            stopWhatch.Start();
            var filteredMatrix = filterGaboraServise.FilterMatrix(martix, filter);
            stopWhatch.Stop();
            var timeSpan = stopWhatch.Elapsed;

            //Console.WriteLine($"Отфильтрованная матрица:\n{filteredMatrix}");
            Console.WriteLine($"Время выполнения фильтрации в синхронном режиме: {timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds / 10} MM:SS.Milliseconds / 10");
        }

        //static async Task Main(string[] args)
        //{
        //    var stopWhatch = new Stopwatch();

        //    Console.WriteLine("Введите D");
        //    int D = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Введите S");
        //    int S = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Введите Rf");
        //    int Rf = int.Parse(Console.ReadLine());
        //    double O = Math.PI / 2;

        //    var martix = matrixService.CreateMatrix(D, S);
        //    var filter = filterGaboraServise.CreateFelter(Rf, O);

        //    stopWhatch.Start();
        //    var filteredMatrix = await filterGaboraServise.FilterMatrixAsync(martix, filter);
        //    stopWhatch.Stop();
        //    var timeSpan = stopWhatch.Elapsed;

        //    //Console.WriteLine($"Отфильтрованная матрица:\n{filteredMatrix}");
        //    Console.WriteLine($"Время выполнения фильтрации в асинхронном режиме: {timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds / 10} MM:SS.Milliseconds / 10");
        //}

        //static void Main(string[] args)
        //{
        //    var stopWhatch = new Stopwatch();

        //    Console.WriteLine("Введите D");
        //    int D = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Введите S");
        //    int S = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Введите Rf");
        //    int Rf = int.Parse(Console.ReadLine());
        //    double O = Math.PI / 2;

        //    var martix = matrixService.CreateMatrix(D, S);
        //    var filter = filterGaboraServise.CreateFelter(Rf, O);

        //    stopWhatch.Start();
        //    var filteredMatrix = filterGaboraServise.FilterMatrixMultitreading(martix, filter, D);
        //    stopWhatch.Stop();
        //    var timeSpan = stopWhatch.Elapsed;

        //    //Console.WriteLine($"Отфильтрованная матрица:\n{filteredMatrix}");
        //    Console.WriteLine($"Время выполнения фильтрации В многопоточном режиме: {timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds / 10} MM:SS.Milliseconds / 10");
        //}
    }
}
