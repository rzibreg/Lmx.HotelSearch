using Lmx.HotelSearch.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lmx.HotelSearch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> __logger;
        private readonly IMediator __mediator;

        public SearchController(ILogger<SearchController> logger, IMediator mediator)
        {
            __logger = logger;
            __mediator = mediator;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBestHotels([FromQuery] SearchQuery searchQuery)
        {
            __logger.LogInformation("SearchController/GetBestHotels call '{query}'", searchQuery);
            var hotels = await __mediator.Send(searchQuery);

            return Ok(hotels);
        }
    }
}
