using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Net5Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Net5Api.Context
{
    public interface IProductDbContext
    {
        DbSet<T> SetEntity<T>() where T : class;

        DatabaseFacade DatabaseFacade { get; }

        IDbConnection Connection { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    //https://docs.microsoft.com/vi-vn/ef/core/get-started/overview/first-app?tabs=visual-studio
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public DbSet<Product> Products { get; set; }

        public string ConnectionString { get;  set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            ConnectionString = ConfigurationExtensions.GetConnectionString(config, "ApplicationConnection");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(ConnectionString);

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public DatabaseFacade DatabaseFacade { get; }

        public DbSet<T> SetEntity<T>() where T : class
        {
            return this.Set<T>();
        }

    }
}
