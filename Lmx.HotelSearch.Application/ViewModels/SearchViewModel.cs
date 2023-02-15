using UnitsNet;

namespace Lmx.HotelSearch.Application.ViewModels
{
    public class SearchViewModel : HotelViewModel
    {
        public double DistanceMetersRaw { get; set; }
        public double DistanceKilometers => Math.Round(Length.FromMeters(DistanceMetersRaw).Kilometers, 2);
        public double DistanceMeters => Math.Round(DistanceMetersRaw, 2);
    }
}
