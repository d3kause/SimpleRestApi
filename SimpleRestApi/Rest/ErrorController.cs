using Microsoft.AspNetCore.Mvc;

namespace SimpleRestApi.Rest;

[Route("rest/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    public ErrorController()
    {
        
    }
    
    
}