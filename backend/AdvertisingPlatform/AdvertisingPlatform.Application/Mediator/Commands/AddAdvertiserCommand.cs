using MediatR;

namespace AdvertisingPlatform.Application.Mediator.Commands
{
    public sealed class AddAdvertisersCommand : IRequest
    {
        public required Stream FileStream { get; init; }
    }
}
