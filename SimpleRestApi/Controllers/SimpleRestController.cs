using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using SimpleRestApi.Common;
using SimpleRestApi.Common.Database.CodeValue.Contracts;
using SimpleRestApi.Common.Database.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Controllers;

[Route("rest/simple")]
[ApiController]
public class SimpleRestController: ControllerBase
{
    private readonly ICodeValueTypeDataStorage _codeValueTypeDataStorage;
    public SimpleRestController([NotNull] ICodeValueTypeDataStorage codeValueTypeDataStorage)
    {
        _codeValueTypeDataStorage = Guard.NotNull(codeValueTypeDataStorage, nameof(codeValueTypeDataStorage));
    }

    [HttpGet]
    public async Task<JsonResult> Index()
    {
        return new JsonResult(_codeValueTypeDataStorage.GetAll());
    }
    
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] List<Dictionary<string,string>> json)
    {
        var data = CodeValueDto.GetFromDictionaryArray(json);

        await _codeValueTypeDataStorage.Truncate();
        await _codeValueTypeDataStorage.AddRangeAsync(data);
        
        return Ok();
    }
}