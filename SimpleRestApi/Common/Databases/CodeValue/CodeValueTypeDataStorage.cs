using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimpleRestApi.Common.Databases.CodeValue.Contracts;
using SimpleRestApi.Common.Databases.CodeValue.Models;
using SimpleRestApi.Controllers.Dtos;

namespace SimpleRestApi.Common.Databases.CodeValue;

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

    public IEnumerable<CodeValueType> GetByFilter(CodeValuesFilterModel filterModel)
    {
        var data = _dbContext.CodeValueTypes.AsQueryable().ApplyFilter(filterModel);

        return data.ToList();
    }

    public async Task Truncate()
    {
        var entityType = _dbContext.CodeValueTypes.EntityType;
        var schema = entityType.FindAnnotation(Constants.SchemaPath)?.Value;
        var tableName = entityType.GetAnnotation(Constants.TablePath).Value?.ToString();
        var schemaName = schema == null ? Constants.DefaultSchemaName : schema.ToString();

        var fullTableName = $"\"{schemaName}\".\"{tableName}\"";
        
        var cmd = $"TRUNCATE TABLE {fullTableName}";

        await _dbContext.Database.ExecuteSqlRawAsync(cmd);
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