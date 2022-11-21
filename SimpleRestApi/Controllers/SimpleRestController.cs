using Microsoft.AspNetCore.Mvc;
using SimpleRestApi.Common;
using SimpleRestApi.Common.Databases.CodeValue.Contracts;
using SimpleRestApi.Common.Databases.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Controllers;

[Route("rest/simple")]
[ApiController]
public class SimpleRestController: ControllerBase
{
    private readonly ICodeValueTypeDataStorage _codeValueTypeDataStorage;
    private readonly ILogger<SimpleRestController> _logger;
    
    public SimpleRestController(ICodeValueTypeDataStorage codeValueTypeDataStorage,
        ILogger<SimpleRestController> logger)
    {
        _codeValueTypeDataStorage = Guard.NotNull(codeValueTypeDataStorage, nameof(codeValueTypeDataStorage));
        _logger = logger;
    }

    [HttpGet]
    public Task<JsonResult> Index([FromQuery] CodeValuesFilterModel filterModel)
    {
        var result = _codeValueTypeDataStorage.GetByFilter(filterModel);
        
        return Task.FromResult(new JsonResult(result));
    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] List<Dictionary<string,string>> json)
    {
        try
        {
            var data = CodeValueDto.GetFromDictionaryArray(json);
            await _codeValueTypeDataStorage.Truncate();
            await _codeValueTypeDataStorage.AddRangeAsync(data);
        
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest();
        }
    }
}