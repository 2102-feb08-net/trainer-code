using System;
using RestaurantReviews.Library.Models;
using Xunit;

namespace RestaurantReviews.Testing.Library.Models
{
    public class ReviewTest
    {
        private readonly Review _review = new Review();

        [Fact]
        public void ReviewerName_NonEmptyValue_StoresCorrectly()
        {
            const string RandomReviewerNameValue = "Al Gore";
            _review.ReviewerName = RandomReviewerNameValue;
            Assert.Equal(RandomReviewerNameValue, _review.ReviewerName);
        }

        [Fact]
        public void ReviewerName_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => _review.ReviewerName = string.Empty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(10)]
        public void Score_ValueBetween0And10_StoresCorrectly(int scoreValue)
        {
            _review.Score = scoreValue;
            Assert.Equal(scoreValue, _review.Score);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void Score_ValueNotBetween0And10_ThrowsArgumentException(int scoreValue)
        {
            Assert.ThrowsAny<ArgumentException>(() => _review.Score = scoreValue);
        }

        // being able to store null values is not necessarily required behavior
        [Theory]
        [InlineData("Perfect")]
        [InlineData("")]
        public void Text_NonNullValue_StoresCorrectly(string textValue)
        {
            _review.Text = textValue;
            Assert.Equal(textValue, _review.Text);
        }
    }
}
