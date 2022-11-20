using Microsoft.AspNetCore.Mvc;
using SimpleRestApi.Common.Models;
using SimpleRestApi.Features;

namespace SimpleRestApi.Rest;

[Route("rest/simple")]
[ApiController]
public class SimpleRestController: ControllerBase
{
    public SimpleRestController()
    {
        
    }

    [HttpGet]
    public async Task<JsonResult> Index()
    {
        return new JsonResult(StaticContent.Context);
    }


    [HttpPost]
    public async Task<IActionResult> Update([FromBody] List<Dictionary<string,string>> json)
    {
        var data = CodeValueExtensions.GetFromDictionaryArray(json);

        StaticContent.Context = data;
        return Ok();
    }
}