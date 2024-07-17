using MediatR;
using VAArtGalleryWebAPI.Domain.Interfaces;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class DeleteArtGalleryArtWorkQueryHandler(IArtWorkRepository artWorkRepository) : IRequestHandler<DeleteArtGalleryArtWorkQuery, bool>
    {
        public async Task<bool> Handle(DeleteArtGalleryArtWorkQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await artWorkRepository.DeleteAsync(request.ArtWorkId, cancellationToken);
            }
            catch (ArgumentException ex) when (string.Compare(ex.ParamName, "artWorkId", true) == 0)
            {
                return default;
            }
        }
    }
}
