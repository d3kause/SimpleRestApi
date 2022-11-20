using Microsoft.AspNetCore.Mvc;

namespace SimpleRestApi.Controllers;

[Route("rest/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    public ErrorController()
    {
        //TODO: Implement error handler
    }
}