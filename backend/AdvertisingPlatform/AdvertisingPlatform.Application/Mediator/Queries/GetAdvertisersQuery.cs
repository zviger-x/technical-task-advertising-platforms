using MediatR;

namespace AdvertisingPlatform.Application.Mediator.Queries
{
    public sealed class GetAdvertisersQuery : IRequest<string[]>
    {
        public required string LocationPath { get; init; }
    }
}
