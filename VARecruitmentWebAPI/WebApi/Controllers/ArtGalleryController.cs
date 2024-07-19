using MediatR;
using Microsoft.AspNetCore.Mvc;
using VAArtGalleryWebAPI.Application.Queries;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.WebApi.Controllers
{
    [Route("api/art-galleries")]
    [ApiController]
    public class ArtGalleryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GetAllArtGalleriesResult>>> GetAllGalleries()
        {
            var galleries = await mediator.Send(new GetAllArtGalleriesQuery());

            var result = galleries.Select(g => new GetAllArtGalleriesResult(g.Id, g.Name, g.City, g.Manager, g.ArtWorksOnDisplay?.Count ?? 0)).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateArtGalleryResult>> Create([FromBody] CreateArtGalleryRequest request)
        {
            try
            {
                var gallery = await mediator.Send(new CreateArtGalleryQuery(request));

                if (gallery is null)
                {
                    return NotFound();
                }

                var result = new CreateArtGalleryResult(gallery.Id, gallery.Name, gallery.City, gallery.Manager);

                return Ok(result);
            }
            catch (ArgumentException ex) when (request is null)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
