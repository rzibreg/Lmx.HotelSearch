using Lmx.HotelSearch.Application.Service;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Lmx.HotelSearch.Application.Commands
{
    public class DeleteHotelCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IHotelService __hotelService;

        public DeleteHotelCommandHandler(IHotelService hotelService)
        {
            __hotelService = hotelService;
        }

        /// <summary>
        /// Delete command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            __hotelService.Delete(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }
}
