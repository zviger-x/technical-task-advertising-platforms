using MediatR;

namespace AdvertisingPlatform.Application.Mediator.Commands
{
    public sealed class SetAdvertisersCommand : IRequest
    {
        public required Stream FileStream { get; init; }
    }
}
