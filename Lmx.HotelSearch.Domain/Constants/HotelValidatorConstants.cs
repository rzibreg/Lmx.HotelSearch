namespace Lmx.HotelSearch.Domain.Constants
{
    public static class HotelValidatorConstants
    {
        public const int NameLength = 50;
        public const double MinimumLatitude = -90.0;
        public const double MaximumLatitude = 90.0;
        public const double MinimumLongitude = -180.0;
        public const double MaximumLongitude = 180.0;
        public const double MinimumHotelPrice = 0;
        public const double MaximumHotelPrice = double.MaxValue;
    }
}
