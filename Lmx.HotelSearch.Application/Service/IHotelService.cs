using GeoCoordinatePortable;
using Lmx.HotelSearch.Application.ViewModels;
using Lmx.HotelSearch.Domain;

namespace Lmx.HotelSearch.Application.Service
{
    public interface IHotelService
    {
        Task<Guid> Save(Hotel hotel);

        Task<Hotel> Update(Hotel hotel);

        Task Delete(Guid id);

        Task<List<SearchViewModel>> GetBestHotels(GeoCoordinate userLocation);
    }
}
