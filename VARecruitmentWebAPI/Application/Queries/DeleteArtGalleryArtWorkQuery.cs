using MediatR;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtGalleryArtWorkQuery(Guid artWorkId) : IRequest<bool>
    {
        public Guid ArtWorkId { get; set; } = artWorkId;
    }
}
