using SimpleRestApi.Common.Database.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Common.Database.CodeValue.Contracts;

public interface ICodeValueTypeDataStorage
{
    Task AddAsync(CodeValueDto data);

    Task AddRangeAsync(IEnumerable<CodeValueDto> data);

    IEnumerable<CodeValueType> GetAll();

    IEnumerable<CodeValueType> GetByFilter(CodeValuesFilterModel filterModel);

    Task Truncate();
}