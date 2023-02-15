using AutoMapper;
using GeoCoordinatePortable;
using Lmx.HotelSearch.Application.Validators;
using Lmx.HotelSearch.Domain;
using Lmx.HotelSearch.Domain.Constants;
using Lmx.HotelSearch.Domain.Repository;
using Moq;

namespace Lmx.HotelSearch.Tests
{
    public class HotelValidatorTests
    {
        private readonly Mock<IHotelRepository> __hotelRepositoryMock = new();
        private readonly HotelValidator __hotelValidator;

        public HotelValidatorTests()
        {
            __hotelValidator = new HotelValidator(__hotelRepositoryMock.Object);
        }

        [Fact]
        public void ValidateHotel_throws_when_name_is_longer_than_fifty_characters()
        {
            // Arrange
            var hotel = new Hotel
            {
                Name = string.Join("", Enumerable.Repeat('A', HotelValidatorConstants.NameLength + 1))
            };

            // Act
            var act = () => __hotelValidator.ValidateHotel(hotel);
            
            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal($"Name cannot be longer than {HotelValidatorConstants.NameLength} characters. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public void ValidateHotel_throws_when_price_is_negative_value()
        {
            // Arrange
            var hotel = new Hotel
            {
                Name = "Hotel",
                Price = -1
            };

            // Act
            var act = () => __hotelValidator.ValidateHotel(hotel);

            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Price must be a positive number. (Parameter 'Price')", exception.Message);
        }
    }
}