
using SimpleRestApi.Common.Database.CodeValue.Models;

namespace SimpleRestApi.Common.Database.CodeValue.Contracts;

public interface ICodeValueTypeDataStorage
{
    Task AddAsync(CodeValueType data);

    Task AddRangeAsync(IEnumerable<CodeValueType> data);

    IEnumerable<CodeValueType> GetAll();

    IEnumerable<CodeValueType> GetByFilter(DataFilterModel filterModel);

    Task Truncate();
}