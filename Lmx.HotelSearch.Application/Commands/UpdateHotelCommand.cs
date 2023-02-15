using AutoMapper;
using Lmx.HotelSearch.Application.ViewModels;
using Lmx.HotelSearch.Domain.Repository;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Lmx.HotelSearch.Application.Service;
using Lmx.HotelSearch.Application.Validators;
using Lmx.HotelSearch.Domain;

namespace Lmx.HotelSearch.Application.Commands
{
    public class UpdateHotelCommand : IRequest<HotelViewModel>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public GeoLocation GeoLocation { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelViewModel>
    {
        private readonly IHotelService __hotelService;
        private readonly IMapper __mapper;

        public UpdateHotelCommandHandler(IHotelService hotelService, IMapper mapper)
        {
            __hotelService = hotelService;
            __mapper = mapper;
        }

        /// <summary>
        /// Update command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HotelViewModel> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var result = __hotelService.Update(__mapper.Map<Hotel>(request)).Result;
            return Task.FromResult(__mapper.Map<HotelViewModel>(result));
        }
    }
}
