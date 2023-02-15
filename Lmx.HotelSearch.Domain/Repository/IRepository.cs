namespace Lmx.HotelSearch.Domain.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
