using GeoCoordinatePortable;
using Lmx.HotelSearch.Domain;

namespace Lmx.HotelSearch.Application.Validators
{
    public interface IHotelValidator
    {
        void ValidateHotel(Hotel hotel);
        void ValidateUserLocation(GeoCoordinate geoCoordinate);
        void HotelExist(Guid id);
    }
}
