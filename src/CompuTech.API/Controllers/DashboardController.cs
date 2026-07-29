using CompuTech.Application.Dashboard.Queries.GetDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

[Route("api/dashboard")]
[ApiController]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetDashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetDashboardQuery(), cancellationToken);
        return Ok(result);
    }
}
