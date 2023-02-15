using AutoMapper;
using Lmx.HotelSearch.Application.ViewModels;
using Lmx.HotelSearch.Domain.Repository;
using MediatR;

namespace Lmx.HotelSearch.Application.Queries
{
    public class ReadHotelsQuery : IRequest<List<HotelViewModel>>
    {
    }

    public class ReadHotelsQueryHandler : IRequestHandler<ReadHotelsQuery, List<HotelViewModel>>
    {
        private readonly IHotelRepository __hotelRepository;
        private readonly IMapper __mapper;

        public ReadHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            __hotelRepository = hotelRepository;
            __mapper = mapper;
        }

        public Task<List<HotelViewModel>> Handle(ReadHotelsQuery request, CancellationToken cancellationToken)
        {
            var result = __hotelRepository.GetAll();
            return Task.FromResult(__mapper.Map<List<HotelViewModel>>(result.Result));
        }
    }
}
