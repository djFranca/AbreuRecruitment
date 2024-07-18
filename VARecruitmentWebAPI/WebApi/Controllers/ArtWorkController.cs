using MediatR;
using Microsoft.AspNetCore.Mvc;
using VAArtGalleryWebAPI.Application.Queries;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.WebApi.Controllers
{
    [Route("api/art-works")]
    [ApiController]
    public class ArtWorkController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<GetArtGalleryArtWorksResult>>> GetAllWorksFromGallery([FromQuery] string artGalleryId)
        {
            try
            {
                if (string.IsNullOrEmpty(artGalleryId) || string.IsNullOrWhiteSpace(artGalleryId))
                {
                    return BadRequest("Art Gallery identifier is required for the operation!");
                }

                var works = await mediator.Send(new GetArtGalleryArtWorksQuery(Guid.Parse(artGalleryId)));

                var result = works.Select(g => new GetArtGalleryArtWorksResult(g.Id, g.Name, g.Author, g.CreationYear, g.AskPrice)).ToList();

                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
