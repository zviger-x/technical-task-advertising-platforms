using AdvertisingPlatform.Application.Mediator.Commands;
using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Application.Utils;
using MediatR;

namespace AdvertisingPlatform.Application.Mediator.Handlers
{
    public sealed class SetAdvertisersHandler : IRequestHandler<SetAdvertisersCommand>
    {
        private readonly IAdvertiserRepository _repository;
        private readonly IAdvertiserParser _parser;

        public SetAdvertisersHandler(IAdvertiserRepository repository, IAdvertiserParser parser)
        {
            _repository = repository;
            _parser = parser;
        }

        public async Task Handle(SetAdvertisersCommand request, CancellationToken cancellationToken)
        {
            var fileData = await _parser.ParseFileAsync(request.FileStream, cancellationToken);

            await _repository.SetAllAsync(fileData, cancellationToken);
        }
    }
}
