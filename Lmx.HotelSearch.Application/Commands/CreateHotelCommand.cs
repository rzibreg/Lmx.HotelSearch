using Lmx.HotelSearch.Application.Service;
using Lmx.HotelSearch.Application.ViewModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lmx.HotelSearch.Domain;

namespace Lmx.HotelSearch.Application.Commands
{
    public class CreateHotelCommand : IRequest<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public GeoLocation GeoLocation { get; set; }
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelService __hotelService;
        private readonly IMapper __mapper;

        public CreateHotelCommandHandler(IHotelService hotelService, IMapper mapper)
        {
            __hotelService = hotelService;
            __mapper = mapper;
        }

        /// <summary>
        /// Create command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>New hotel id</returns>
        public Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var result = __hotelService.Save(__mapper.Map<Hotel>(request));
            return Task.FromResult(result.Result);
        }
    }
}
