using AuthenticationDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace AuthenticationRepository
{
    public class AuthenticationContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;

        public DbSet<Credential> Credentials { get; set; }

        public AuthenticationContext(string connectionString, bool? useConsoleLogger)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString needed");
            if (!useConsoleLogger.HasValue)
                throw new ArgumentNullException("useConsoleLogger needed");

            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(a =>
            {
                a
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                .AddConsole();
            });

            dbContextOptionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useConsoleLogger)
            {
                dbContextOptionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>(a =>
            {
                a.ToTable("Credential").HasKey(b => b.Id);
            });
        }
    }
}
