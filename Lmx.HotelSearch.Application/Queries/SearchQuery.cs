using Lmx.HotelSearch.Application.Service;
using Lmx.HotelSearch.Application.ViewModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GeoCoordinatePortable;

namespace Lmx.HotelSearch.Application.Queries
{
    public class SearchQuery : IRequest<List<SearchViewModel>>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }

    public class SearchQueryHandler : IRequestHandler<SearchQuery, List<SearchViewModel>>
    {
        private readonly IHotelService __hotelService;
        private readonly IMapper __mapper;

        public SearchQueryHandler(IHotelService hotelService, IMapper mapper)
        {
            __hotelService = hotelService;
            __mapper = mapper;
        }

        public Task<List<SearchViewModel>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var result = __hotelService.GetBestHotels(__mapper.Map<GeoCoordinate>(request));
            return Task.FromResult(result.Result);
        }
    }
}
