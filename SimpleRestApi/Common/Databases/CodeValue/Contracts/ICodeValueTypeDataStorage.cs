using SimpleRestApi.Common.Databases.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Common.Databases.CodeValue.Contracts;

public interface ICodeValueTypeDataStorage
{
    Task AddAsync(CodeValueDto data);

    Task AddRangeAsync(IEnumerable<CodeValueDto> data);

    IEnumerable<CodeValueType> GetAll();

    IEnumerable<CodeValueType> GetByFilter(CodeValuesFilterModel filterModel);

    Task Truncate();
}