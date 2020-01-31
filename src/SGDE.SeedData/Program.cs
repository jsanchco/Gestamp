// ReSharper disable InconsistentNaming
namespace SGDE.SeedData
{
    #region Using

    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;
    using System.Linq;
    using DataEFCoreSQL;
    using DataEFCoreMongoDB;
    using DataEFCoreMongoDB.Repositories;
    using System.Globalization;
    using Domain.Helpers;

    #endregion

    internal static class Program
    {
        public const string PATH = "..\\..\\..\\data\\data.csv";
        public const char DELIMITER_CHARACTER = ',';

        static void Main()
        {
            Console.Clear();
            Console.WriteLine("*****************************");
            Console.WriteLine("*         Seed Data         *");
            Console.WriteLine("*****************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Choose Provider ...");
            Console.WriteLine("");
            Console.WriteLine("1 - SQL");
            Console.WriteLine("2 - MongoDB");
            Console.WriteLine("");

            try
            {
                var typeSeed = Console.ReadKey();
                Console.WriteLine("");
                Console.WriteLine("wait ...");
                switch (typeSeed.KeyChar)
                {
                    case '1':
                        SeedFromSQL();
                        break;

                    case '2':
                        SeedFromMongoDB();
                        break;

                    default:
                        Console.WriteLine("Error: Seed no contemplated!!!");
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("");
                Console.WriteLine("Press any key to exit ...");
                Console.ReadKey();
            }
        }

        private static void SeedFromSQL()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            Console.WriteLine("");

            var stopWatch = new Stopwatch();
            stopWatch.Start();           

            var optionsBuilder = new DbContextOptionsBuilder<EFContextSQL>();
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["SQL"]);

            var cultureInfo = new CultureInfo("en-US");

            using (var reader = new StreamReader(PATH))
            using (var context = new EFContextSQL(optionsBuilder.Options))
            {
                var lineCount = File.ReadLines(PATH).Count();
                var cont = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(DELIMITER_CHARACTER);

                    var order = new Order
                    {
                        Id = Guid.NewGuid().ToString(),
                        Region = values[0],
                        Country = values[1],
                        ItemType = values[2],
                        SalesChannel = values[3],
                        OrderPriority = values[4],
                        OrderDate = DateTime.Parse(values[5], cultureInfo),
                        OrderId = int.Parse(values[6]),
                        ShipDate = DateTime.Parse(values[7], cultureInfo),
                        UnitsSlod = int.Parse(values[8]),
                        UnitPrice = decimal.Parse(values[9], cultureInfo),
                        UnitCost = decimal.Parse(values[10], cultureInfo),
                        TotalCost = decimal.Parse(values[11], cultureInfo),
                        TotalRevenue = decimal.Parse(values[12], cultureInfo),
                        TotalProfi = decimal.Parse(values[13], cultureInfo)
                    };

                    context.Add(order);
                    context.SaveChanges();

                    Console.Write("\r{0} de {1}", cont, lineCount);
                    cont ++;
                }

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;

                Console.WriteLine("");

                Console.WriteLine($"Table 'Order' -> {context.Order.Count()} rows");
                Console.WriteLine($"\t{ts.Seconds}.{ts.Milliseconds} sg.ms");

                Console.WriteLine("");
            }
        }

        private static void SeedFromMongoDB()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var orderConfiguration = new InfrastructureAppSettings {
                ConnectionString = configuration.GetSection("GestamstoreDatabaseSettings")["ConnectionString"],
                DatabaseName = configuration.GetSection("GestamstoreDatabaseSettings")["DatabaseName"]
            };

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var cultureInfo = new CultureInfo("en-US");

            var repository = new OrderRepository(orderConfiguration);

            using (var reader = new StreamReader(PATH))
            {
                var lineCount = File.ReadLines(PATH).Count();
                var cont = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(DELIMITER_CHARACTER);

                    var order = new Order
                    {
                        Region = values[0],
                        Country = values[1],
                        ItemType = values[2],
                        SalesChannel = values[3],
                        OrderPriority = values[4],
                        OrderDate = DateTime.Parse(values[5], cultureInfo),
                        OrderId = int.Parse(values[6]),
                        ShipDate = DateTime.Parse(values[7], cultureInfo),
                        UnitsSlod = int.Parse(values[8]),
                        UnitPrice = decimal.Parse(values[9], cultureInfo),
                        UnitCost = decimal.Parse(values[10], cultureInfo),
                        TotalCost = decimal.Parse(values[11], cultureInfo),
                        TotalRevenue = decimal.Parse(values[12], cultureInfo),
                        TotalProfi = decimal.Parse(values[13], cultureInfo)
                    };

                    repository.Add(order);

                    Console.Write("\r{0} de {1}", cont, lineCount);
                    cont ++;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");
            var orders = repository.GetAll();
            Console.WriteLine($"{orders.Count()} regs. in accounts");
            Console.WriteLine("");

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine($"\t{ts.Seconds}.{ts.Milliseconds} sg.ms");

            Console.WriteLine("");
        }
    }
}
