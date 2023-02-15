using GeoCoordinatePortable;

namespace Lmx.HotelSearch.Domain
{
    public class Hotel
    {
        public Hotel()
        {
            GeoCoordinate = new GeoCoordinate();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public GeoCoordinate GeoCoordinate { get; set; }
    }
}
