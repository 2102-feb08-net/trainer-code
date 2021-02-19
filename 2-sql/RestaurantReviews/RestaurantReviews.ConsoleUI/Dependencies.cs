using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.DataAccess.Repositories;
using RestaurantReviews.Library.Interfaces;

namespace RestaurantReviews.ConsoleUI
{
    // this solution is set up to work with either database-first or code-first workflow.

    // if you want to do code-first with migrations, EF needs to be able to see the connection string somehow.
    // either you have the DbContext.OnConfiguring method, or, you implement an IDesignTimeDbContextFactory.
    // then, the command like "dotnet ef migrations add InitialCreate --startup-project ../RestaurantReviews.ConsoleUI/"
    // will work. (in an ASP.NET app, configuring the context in Startup is a third option.)

    // this class follows the disposable pattern (the standard way to implement the IDisposable interface),
    // so that it can in turn dispose of the contexts it has created.
    public class Dependencies : IDesignTimeDbContextFactory<RestaurantReviewsDbContext>, IDisposable
    {
        private bool _disposedValue;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public RestaurantReviewsDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantReviewsDbContext>();
            var connectionString = File.ReadAllText("C:/revature/restaurantreviews-connection-string.txt");
            optionsBuilder.UseSqlServer(connectionString);

            return new RestaurantReviewsDbContext(optionsBuilder.Options);
        }

        public IRestaurantRepository CreateRestaurantRepository()
        {
            var dbContext = CreateDbContext();
            _disposables.Add(dbContext);
            return new RestaurantRepository(dbContext);
        }

        public XmlSerializer CreateXmlSerializer()
        {
            return new XmlSerializer(typeof(List<Library.Models.Restaurant>));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    foreach (IDisposable disposable in _disposables)
                    {
                        disposable.Dispose();
                    }
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
