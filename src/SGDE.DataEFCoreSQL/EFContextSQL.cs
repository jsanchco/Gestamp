// ReSharper disable InconsistentNaming
namespace SGDE.DataEFCoreSQL
{
    #region Using

    using System.Threading;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Configurations;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    #endregion

    public class EFContextSQL : DbContext
    {
        #region Members

        public virtual DbSet<Order> Order { get; set; }

        public static long InstanceCount;

        #endregion

        public EFContextSQL(DbContextOptions options) : base(options) => Interlocked.Increment(ref InstanceCount);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new OrderConfiguration(modelBuilder.Entity<Order>());
        }

        //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFContextSQL>
        //{
        //    public EFContextSQL CreateDbContext(string[] args)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../SGDE.API/appsettings.json")
        //            .Build();
        //        var builder = new DbContextOptionsBuilder<EFContextSQL>();
        //        var connectionString = configuration.GetConnectionString("SGDEContextSQL");
        //        builder.UseSqlServer(connectionString);
        //        return new EFContextSQL(builder.Options);
        //    }
        //}
    }
}
