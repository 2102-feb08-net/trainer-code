using System;
using System.Data.Common;
using EmailApp.DataAccess.EfModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.IntegrationTests
{
    public class TestEmailContextFactory : IDisposable
    {
        private DbConnection _connection;
        private bool _disposedValue;

        private DbContextOptions<EmailContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<EmailContext>()
                .UseSqlite(_connection).Options;
        }

        public EmailContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                DbContextOptions<EmailContext> options = CreateOptions();
                using var context = new EmailContext(options);
                context.Database.EnsureCreated();

                // add extra test seed data here (or, in each test method)
            }

            return new EmailContext(CreateOptions());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
