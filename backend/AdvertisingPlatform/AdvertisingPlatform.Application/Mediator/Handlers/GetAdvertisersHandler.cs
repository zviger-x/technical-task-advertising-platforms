using AdvertisingPlatform.Application.Mediator.Queries;
using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Application.Utils;
using MediatR;

namespace AdvertisingPlatform.Application.Mediator.Handlers
{
    public sealed class GetAdvertisersHandler : IRequestHandler<GetAdvertisersQuery, string[]>
    {
        private readonly IAdvertiserRepository _repository;
        private readonly IAdvertiserParser _parser;

        public GetAdvertisersHandler(IAdvertiserRepository repository, IAdvertiserParser parser)
        {
            _repository = repository;
            _parser = parser;
        }
		
        public async Task<string[]> Handle(GetAdvertisersQuery request, CancellationToken cancellationToken)
        {
            var parts = await _parser.ParseLocationAsync(request.LocationPath, cancellationToken);

            return await _repository.GetAdvertisersAsync(parts, cancellationToken);
        }
    }
}
