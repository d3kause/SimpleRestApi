using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimpleRestApi.Common.Database.CodeValue.Contracts;
using SimpleRestApi.Common.Database.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Common.Database.CodeValue;

public sealed class CodeValueTypeDataStorage : ICodeValueTypeDataStorage
{
    private readonly CodeValueDbContext _dbContext;

    public CodeValueTypeDataStorage([NotNull] CodeValueDbContext dbContext)
    {
        _dbContext = Guard.NotNull(dbContext, nameof(dbContext));
    }
    public async Task AddAsync(CodeValueDto data)
    {
        var preparedData = GetDataFromDto(new[] { data });
        
        await _dbContext.AddAsync(preparedData);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<CodeValueDto> data)
    {
        var preparedData = GetDataFromDto(data);

        await _dbContext.AddRangeAsync(preparedData);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<CodeValueType> GetAll()
    {
        return _dbContext.CodeValueTypes.ToList();
    }

    public IEnumerable<CodeValueType> GetByFilter(DataFilterModel filterModel)
    {
        throw new NotImplementedException();
    }

    public async Task Truncate()
    {
        _dbContext.RemoveRange(_dbContext.CodeValueTypes);
        // var tableNames = _dbContext.Model.GetEntityTypes()
        //     .Select(t => t.GetTableName())
        //     .Distinct()
        //     .ToList();
        //
        // foreach (var table in tableNames)
        // {
        //    await _dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {table}");
        // }
    }

    private static IEnumerable<CodeValueType> GetDataFromDto(IEnumerable<CodeValueDto> data)
    {
        var sortedData = data.OrderBy(x => x.Code);
        
        var rowNum = 1;
        var result = sortedData.Select(model => new CodeValueType()
            { Id = rowNum++, Code = model.Code, Value = model.Value }).ToList();

        return result;
    }
}