using Lmx.HotelSearch.Domain;
using Lmx.HotelSearch.Domain.Repository;

namespace Lmx.HotelSearch.Infrastructure
{
    /// <summary>
    /// In memory data provider
    /// </summary>
    public class HotelRepository : IHotelRepository
    {
        // Static data source for demo 
        private static List<Hotel> __hotels;

        public HotelRepository()
        {
            __hotels = new List<Hotel>();
        }

        public async Task<Guid> Save(Hotel hotel)
        {
            hotel.Id = Guid.NewGuid();
            __hotels.Add(hotel);

            return await Task.FromResult(hotel.Id);
        }

        public async Task<Hotel> Update(Hotel hotel)
        {
            var record = __hotels.First(x => x.Id == hotel.Id);
            record.Name = hotel.Name;
            record.Price = hotel.Price;
            record.GeoCoordinate = hotel.GeoCoordinate;

            return await Task.FromResult(record);
        }

        public async Task Delete(Guid id)
        {
            __hotels.RemoveAll(x => x.Id == id);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Hotel>> GetAll()
        {
            return await Task.FromResult(__hotels);
        }
    }
}
