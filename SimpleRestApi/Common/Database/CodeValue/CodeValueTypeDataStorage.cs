using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimpleRestApi.Common.Database.CodeValue.Contracts;
using SimpleRestApi.Common.Database.CodeValue.Models;

namespace SimpleRestApi.Common.Database.CodeValue;

public sealed class CodeValueTypeDataStorage : ICodeValueTypeDataStorage
{
    private readonly CodeValueDbContext _dbContext;

    public CodeValueTypeDataStorage([NotNull] CodeValueDbContext dbContext)
    {
        _dbContext = Guard.NotNull(dbContext, nameof(dbContext));
    }
    public async Task AddAsync(CodeValueType data)
    {
        await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<CodeValueType> data)
    {
        await _dbContext.AddRangeAsync(data);
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
}