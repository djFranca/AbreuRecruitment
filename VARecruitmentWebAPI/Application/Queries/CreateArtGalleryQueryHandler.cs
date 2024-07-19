using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class CreateArtGalleryQueryHandler(IArtGalleryRepository artGalleryRepository) : IRequestHandler<CreateArtGalleryQuery, ArtGallery>
    {
        public async Task<ArtGallery> Handle(CreateArtGalleryQuery request, CancellationToken cancellationToken)
        {
            ArtGallery artGallery = new(request.Name, request.City, request.Manager);
            return await artGalleryRepository.CreateAsync(artGallery, cancellationToken);
        }
    }
}
