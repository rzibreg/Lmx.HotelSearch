using Lmx.HotelSearch.Application.Commands;
using Lmx.HotelSearch.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lmx.HotelSearch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> __logger;
        private readonly IMediator __mediator;

        public HotelController(ILogger<HotelController> logger, IMediator mediator)
        {
            __logger = logger;
            __mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateHotelCommand createHotel)
        {
            __logger.LogInformation("HotelController/Save call '{command}'", createHotel);
            var id = await __mediator.Send(createHotel);

            return Ok(id);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Read()
        {
            __logger.LogInformation("HotelController/Read call");
            var hotels = await __mediator.Send(new ReadHotelsQuery());

            return Ok(hotels);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateHotelCommand updateHotel)
        {
            __logger.LogInformation("HotelController/Update call '{command}'", updateHotel);
            var hotels = await __mediator.Send(updateHotel);

            return Ok(hotels);
        }
    }
}
