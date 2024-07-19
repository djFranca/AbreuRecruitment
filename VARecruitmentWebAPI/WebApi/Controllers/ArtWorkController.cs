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
            catch (ArgumentNullException ex) when (artGalleryId is null)
            {
                return NotFound(ex.Message);
            }
            catch (FormatException ex) when (Guid.TryParse(artGalleryId, out Guid result) is false)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteArtGalleryArtWorkResult>> DeleteArtWorkFromGallery([FromQuery] string artWorkId)
        {
            try
            {
                var success = await mediator.Send(new DeleteArtGalleryArtWorkQuery(Guid.Parse(artWorkId)));

                var result = new DeleteArtGalleryArtWorkResult(success);

                return Ok(result);
            }
            catch(FormatException ex) when (Guid.TryParse(artWorkId, out _) is false)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
