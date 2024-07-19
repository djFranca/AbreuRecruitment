using MediatR;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.Application.Queries
{
    public class CreateArtGalleryQuery(CreateArtGalleryRequest request) : IRequest<ArtGallery>
    {
        public string Name { get; set; } = request.Name;
        public string City { get; set; } = request.City;
        public string Manager { get; set; } = request.Manager;
    }          
}
