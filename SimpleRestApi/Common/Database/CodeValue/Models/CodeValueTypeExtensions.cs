namespace SimpleRestApi.Common.Database.CodeValue.Models;

public static class CodeValueTypeExtensions
{
    public static IQueryable<CodeValueType> ApplyFilter(this IQueryable<CodeValueType> data,
        CodeValuesFilterModel filterModel)
    {
        if (!string.IsNullOrEmpty(filterModel.Value))
        {
            data = data.Where(x => x.Value == filterModel.Value);
        }

        if (filterModel.Code != null)
        {
            data = data.Where(x => x.Code == filterModel.Code);
        }

        if (filterModel.Offset != null)
        {
            data = data.Skip(filterModel.Offset.Value);
        }
        
        if (filterModel.Limit != null)
        {
            data = data.Take(filterModel.Limit.Value);
        }
        
        return data;
    }
}