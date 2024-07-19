namespace VAArtGalleryWebAPI.WebApi.Models
{
    public class DeleteArtGalleryArtWorkResult(bool success)
    {
        public bool Success { get; set; } = success;
    }
}
