using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.Library.Interfaces;

namespace RestaurantReviews.DataAccess.Repositories
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members,
    /// using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This class ought to have better exception handling and logging.
    /// </remarks>
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantReviewsDbContext _dbContext;

        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new restaurant repository given a suitable restaurant data source.
        /// </summary>
        /// <param name="dbContext">The data source</param>
        public RestaurantRepository(RestaurantReviewsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get all restaurants with deferred execution,
        /// including associated reviews.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        public IEnumerable<Library.Models.Restaurant> GetRestaurants(string search = null)
        {
            // disable unnecessary tracking for performance benefit
            IQueryable<Restaurant> items = _dbContext.Restaurant
                .Include(r => r.Review).AsNoTracking();
            if (search != null)
            {
                items = items.Where(r => r.Name.Contains(search));
            }
            return items.Select(rt => new Library.Models.Restaurant
            {
                Id = rt.Id,
                Name = rt.Name,
                Reviews = rt.Review.Select(rw => new Library.Models.Review
                {
                    Id = rw.Id,
                    ReviewerName = rw.ReviewerName,
                    Score = rw.Score,
                    Text = rw.Text
                }).ToList()
            });
        }

        /// <summary>
        /// Get a restaurant by ID, not including associated reviews.
        /// </summary>
        /// <returns>The restaurant</returns>
        public Library.Models.Restaurant GetRestaurantById(int id)
        {
            var restaurant = _dbContext.Restaurant.Find(id);
            return new Library.Models.Restaurant
            {
                Id = restaurant.Id,
                Name = restaurant.Name
            };
        }

        /// <summary>
        /// Add a restaurant, including any associated reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant</param>
        public void AddRestaurant(Library.Models.Restaurant restaurant)
        {
            if (restaurant.Id != 0)
            {
                s_logger.Warn($"Restaurant to be added has an ID ({restaurant.Id}) already: ignoring.");
            }

            s_logger.Info($"Adding restaurant");

            // ID left at default 0
            var entity = new Restaurant
            {
                Name = restaurant.Name,
                Review = restaurant.Reviews.Select(r => new Review
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Score = r.Score ?? throw new ArgumentException("review score cannot be null.", nameof(restaurant)),
                    Text = r.Text
                }).ToList()
            };
            _dbContext.Add(entity);
        }

        /// <summary>
        /// Delete a restaurant by ID. Any reviews associated to it will also be deleted.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant</param>
        public void DeleteRestaurant(int restaurantId)
        {
            s_logger.Info($"Deleting restaurant with ID {restaurantId}");
            Restaurant entity = _dbContext.Restaurant.Find(restaurantId);
            _dbContext.Remove(entity);
        }

        /// <summary>
        /// Update a restaurant as well as its reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant with changed values</param>
        public void UpdateRestaurant(Library.Models.Restaurant restaurant)
        {
            s_logger.Info($"Updating restaurant with ID {restaurant.Id}");

            // calling Update would mark every property as Modified.
            // this way will only mark the changed properties as Modified.
            Restaurant currentEntity = _dbContext.Restaurant.Find(restaurant.Id);
            var newEntity = new Restaurant
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Review = restaurant.Reviews.Select(r => new Review
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Score = r.Score ?? throw new ArgumentException("review score cannot be null.", nameof(restaurant)),
                    Text = r.Text
                }).ToList()
            };

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Add a review and associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant</param>
        public void AddReview(Library.Models.Review review, Library.Models.Restaurant restaurant = null)
        {
            if (restaurant.Id != 0)
            {
                s_logger.Warn($"Review to be added has an ID ({review.Id}) already: ignoring.");
            }

            s_logger.Info($"Adding review to restaurant with ID {restaurant.Id}");

            if (restaurant != null)
            {
                // get the db's version of that restaurant
                // (can't use Find with Include)
                Restaurant restaurantEntity = _dbContext.Restaurant
                    .Include(r => r.Review).First(r => r.Id == restaurant.Id);
                var newEntity = new Review
                {
                    Id = review.Id,
                    ReviewerName = review.ReviewerName,
                    Score = review.Score ?? throw new ArgumentException("review score cannot be null.", nameof(review)),
                    Text = review.Text
                };
                restaurantEntity.Review.Add(newEntity);
                // also, modify the parameters
                restaurant.Reviews.Add(review);
            }
            else
            {
                Review newEntity = new Review
                {
                    Id = review.Id,
                    ReviewerName = review.ReviewerName,
                    Score = review.Score ?? throw new ArgumentException("review score cannot be null.", nameof(review)),
                    Text = review.Text
                };
                _dbContext.Add(newEntity);
            }
        }

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        public void DeleteReview(int reviewId)
        {
            s_logger.Info($"Deleting review with ID {reviewId}");

            Review entity = _dbContext.Review.Find(reviewId);
            _dbContext.Remove(entity);
        }

        /// <summary>
        /// Update a review.
        /// </summary>
        /// <param name="review">The review with changed values</param>
        public void UpdateReview(Library.Models.Review review)
        {
            s_logger.Info($"Updating review with ID {review.Id}");

            Review currentEntity = _dbContext.Review.Find(review.Id);
            var newEntity = new Review
            {
                Id = review.Id,
                ReviewerName = review.ReviewerName,
                Score = review.Score ?? throw new ArgumentException("review score cannot be null.", nameof(review)),
                Text = review.Text
            };

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            s_logger.Info("Saving changes to the database");
            _dbContext.SaveChanges();
        }
    }
}
