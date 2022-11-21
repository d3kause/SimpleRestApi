using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleRestApi.Common;
using SimpleRestApi.Common.Databases.CodeValue.Contracts;
using SimpleRestApi.Common.Databases.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Controllers;

[Microsoft.AspNetCore.Mvc.Route("rest/simple")]
[ApiController]
public class SimpleRestController: ControllerBase
{
    private readonly ICodeValueTypeDataStorage _codeValueTypeDataStorage;
    public SimpleRestController([NotNull] ICodeValueTypeDataStorage codeValueTypeDataStorage)
    {
        _codeValueTypeDataStorage = Guard.NotNull(codeValueTypeDataStorage, nameof(codeValueTypeDataStorage));
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    public Task<JsonResult> Index([FromQuery] CodeValuesFilterModel filterModel)
    {
        var result = _codeValueTypeDataStorage.GetByFilter(filterModel);
        
        return Task.FromResult(new JsonResult(result));
    }
    
    [Microsoft.AspNetCore.Mvc.HttpPost]
    public async Task<IActionResult> Update([Microsoft.AspNetCore.Mvc.FromBody] List<Dictionary<string,string>> json)
    {
        var data = CodeValueDto.GetFromDictionaryArray(json);

        await _codeValueTypeDataStorage.Truncate();
        await _codeValueTypeDataStorage.AddRangeAsync(data);
        
        return Ok();
    }
}