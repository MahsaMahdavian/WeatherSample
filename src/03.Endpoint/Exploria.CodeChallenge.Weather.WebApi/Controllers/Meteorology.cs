using Exploria.CodeChallenge.Weather.Application.Feature.Meteorology;
using Exploria.CodeChallenge.Weather.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exploria.CodeChallenge.Weather.WebApi.Controllers;

[ApiController]
[Route("api/meteorology")]
public class Meteorology: ControllerBase
{
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetWeatherInfo(
       [FromServices] IMediator mediator,
       CancellationToken cancellationToken = default)
    {
       
        var result = await mediator.Send(new MeteorologyInquiryRequestDto(), cancellationToken);
        return Ok(result);
    }

}
