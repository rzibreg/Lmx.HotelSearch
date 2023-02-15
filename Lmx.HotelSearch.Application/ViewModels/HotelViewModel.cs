namespace Lmx.HotelSearch.Application.ViewModels
{
    public class HotelViewModel
    {
        public HotelViewModel()
        {
            GeoLocation = new GeoLocation();
        }

        public Guid Id {get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public GeoLocation GeoLocation { get; set; }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
