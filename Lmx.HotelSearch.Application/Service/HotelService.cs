using GeoCoordinatePortable;
using Lmx.HotelSearch.Application.Validators;
using Lmx.HotelSearch.Application.ViewModels;
using Lmx.HotelSearch.Domain;
using Lmx.HotelSearch.Domain.Repository;
using MediatR;

namespace Lmx.HotelSearch.Application.Service
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository __hotelRepository;
        private readonly IHotelValidator __hotelValidator;

        public HotelService(IHotelRepository hotelRepository, IHotelValidator hotelValidator)
        {
            __hotelRepository = hotelRepository;
            __hotelValidator = hotelValidator;
        }

        /// <summary>
        /// Save hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Id</returns>
        public Task<Guid> Save(Hotel hotel)
        {
            __hotelValidator.ValidateHotel(hotel);
            var result = __hotelRepository.Save(hotel);

            return Task.FromResult(result.Result);
        }

        /// <summary>
        /// Update hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Updated hotel</returns>
        public Task<Hotel> Update(Hotel hotel)
        {
            __hotelValidator.HotelExist(hotel.Id);
            var result =__hotelRepository.Update(hotel);

            return Task.FromResult(result.Result);
        }

        /// <summary>
        /// Delete hotel with requested id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task Delete(Guid id)
        {
            __hotelValidator.HotelExist(id);
            __hotelRepository.Delete(id);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get best hotels, ordered by distance then by price
        /// Closest hotels are preferred.
        /// </summary>
        /// <param name="userLocation"></param>
        /// <returns>Ordered list of hotels.</returns>
        public Task<List<SearchViewModel>> GetBestHotels(GeoCoordinate userLocation)
        {
            __hotelValidator.ValidateUserLocation(userLocation);
            var hotels = __hotelRepository.GetAll().Result;
            var result = hotels.Select(hotel => new SearchViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Price = hotel.Price,
                GeoLocation = new GeoLocation { Latitude = hotel.GeoCoordinate.Latitude, Longitude = hotel.GeoCoordinate.Longitude },
                DistanceMetersRaw = hotel.GeoCoordinate.GetDistanceTo(userLocation)
            }).OrderBy(x => x.DistanceMetersRaw).ThenBy(x => x.Price).ToList();

            return Task.FromResult(result);
        }
    }
}
