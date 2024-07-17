using System.Text.Json;
using VAArtGalleryWebAPI.Domain.Interfaces;
using VAArtGalleryWebAPI.Domain.Entities;

namespace VAArtGalleryWebAPI.Infrastructure
{
    public class ArtWorkRepository(string filePath) : IArtWorkRepository
    {
        private readonly string _filePath = filePath;

        public async Task<ArtWork> CreateAsync(Guid artGalleryId, ArtWork artWork, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(g => g.Id == artGalleryId) ?? throw new ArgumentException("unknown art gallery", nameof(artGalleryId));
            artWork.Id = Guid.NewGuid();

            if (gallery.ArtWorksOnDisplay == null)
            {
                gallery.ArtWorksOnDisplay = [artWork];
            }
            else
            {
               gallery.ArtWorksOnDisplay.Add(artWork);
            }

            cancellationToken.ThrowIfCancellationRequested();

            await UpdateGalleries(galleries);

            return artWork; 
        }

        public async Task<bool> DeleteAsync(Guid artWorkId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            bool? response = default(bool);

            var artWorks = galleries.SelectMany(x => x.ArtWorksOnDisplay is null ? [] : x.ArtWorksOnDisplay).Distinct().FirstOrDefault(x => x.Id == artWorkId);

            if (artWorks is not null)
            {
                var gallery = galleries.Find(x => x.ArtWorksOnDisplay is not null && x.ArtWorksOnDisplay.Contains(artWorks));

                response = gallery?.ArtWorksOnDisplay?.Remove(artWorks);

                if (response is not null && response is not false)
                {
                    var index = galleries.FindIndex(x => x.Id == gallery?.Id);

                    if (gallery is not null) 
                    {
                        galleries[index] = gallery;

                        cancellationToken.ThrowIfCancellationRequested();

                        await UpdateGalleries(galleries);
                    }
                }
            }

            return response is not null && (bool)response;
        }

        public async Task<List<ArtWork>> GetArtWorksByGalleryIdAsync(Guid artGalleryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var galleries = await new ArtGalleryRepository(_filePath).GetAllArtGalleriesAsync(cancellationToken);

            var gallery = galleries.Find(g => g.Id == artGalleryId) ?? throw new ArgumentException("unknown art gallery", nameof(artGalleryId));
            if (gallery.ArtWorksOnDisplay == null)
            {
                return [];
            }
            return gallery.ArtWorksOnDisplay;
        }

        private async Task UpdateGalleries(List<ArtGallery> galleries)
        {
            await Task.Run(() =>
            {
                using TextWriter tw = new StreamWriter(_filePath, false);
                tw.Write(JsonSerializer.Serialize(galleries));
            });
        }
    }
}