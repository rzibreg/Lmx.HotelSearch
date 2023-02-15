using GeoCoordinatePortable;
using Lmx.HotelSearch.Domain;
using Lmx.HotelSearch.Domain.Constants;
using Lmx.HotelSearch.Domain.Repository;

namespace Lmx.HotelSearch.Application.Validators
{
    public class HotelValidator : IHotelValidator
    {
        private readonly IHotelRepository __hotelRepository;

        public HotelValidator(IHotelRepository hotelRepository)
        {
            __hotelRepository = hotelRepository;
        }

        /// <summary>
        /// Validate Hotel properties
        /// This includes: Name, Price, Location
        /// </summary>
        /// <param name="hotel"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ValidateHotel(Hotel hotel)
        {
            if (hotel.Name.Length > HotelValidatorConstants.NameLength)
                throw new ArgumentOutOfRangeException("Name", $"Name cannot be longer than {HotelValidatorConstants.NameLength} characters.");

            if (hotel.Price is < HotelValidatorConstants.MinimumHotelPrice or > HotelValidatorConstants.MaximumHotelPrice)
                throw new ArgumentOutOfRangeException("Price", "Price must be a positive number.");

            ValidateLocation(hotel.GeoCoordinate);
        }

        /// <summary>
        /// Validate user Location
        /// </summary>
        /// <param name="geoCoordinate"></param>
        public void ValidateUserLocation(GeoCoordinate geoCoordinate)
        {
            ValidateLocation(geoCoordinate);
        }

        /// <summary>
        /// Verify that hotel with id exist in database
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void HotelExist(Guid id)
        {
            var hotels = __hotelRepository.GetAll().Result;
            if(hotels.All(x => x.Id != id))
                throw new ArgumentOutOfRangeException("Guid", "Hotel does not exist.");
        }

        private void ValidateLocation(GeoCoordinate geoCoordinate)
        {
            if (geoCoordinate.Latitude > HotelValidatorConstants.MaximumLatitude || geoCoordinate.Latitude < HotelValidatorConstants.MinimumLatitude)
                throw new ArgumentOutOfRangeException("Latitude", $"Argument must be in range of {HotelValidatorConstants.MinimumLatitude} to {HotelValidatorConstants.MaximumLatitude}");

            if (geoCoordinate.Longitude > HotelValidatorConstants.MaximumLongitude || geoCoordinate.Longitude < HotelValidatorConstants.MinimumLongitude)
                throw new ArgumentOutOfRangeException("Longitude", $"Argument must be in range of {HotelValidatorConstants.MinimumLongitude} to {HotelValidatorConstants.MaximumLongitude}");
        }
    }
}
