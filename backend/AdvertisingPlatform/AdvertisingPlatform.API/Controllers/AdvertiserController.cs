using AdvertisingPlatform.API.Contracts;
using AdvertisingPlatform.Application.Mediator.Commands;
using AdvertisingPlatform.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatform.API.Controllers
{
    [ApiController]
    [Route("api/advertisers")]
    public class AdvertisersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvertisersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SetAdvertisers([FromForm] FileUploadRequest request, CancellationToken cancellationToken = default)
        {
            var stream = request.File.OpenReadStream();

            var command = new SetAdvertisersCommand { FileStream = stream };

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisers([FromQuery] string locationPath, CancellationToken cancellationToken)
        {
            var query = new GetAdvertisersQuery { LocationPath = locationPath };

            var advertisers = await _mediator.Send(query, cancellationToken);

            return Ok(advertisers);
        }
    }
}
