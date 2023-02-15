namespace Lmx.HotelSearch.Domain.Repository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<Guid> Save(Hotel hotel);

        Task<Hotel> Update(Hotel hotel);

        Task Delete(Guid id);
    }
}
