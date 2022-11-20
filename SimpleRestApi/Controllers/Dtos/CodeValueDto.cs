using SimpleRestApi.Common.Exceptions;

namespace SimpleRestApi.Controllers.Dtos;

public class CodeValueDto
{
    public int Code { get; set; }
    
    public string? Value { get; set; }
    
    public static IEnumerable<CodeValueDto> GetFromDictionaryArray(IEnumerable<Dictionary<string, string>> data)
    {
        var result = new List<CodeValueDto>();
        
        foreach (var rowData in data)
        {
            if (rowData.Count != 1)
            {
                throw new IncorrectDataException("The string must contain only one [key:value] pair");
            }

            var keyValuePair = rowData.FirstOrDefault();

            result.Add(new CodeValueDto
            {
                Code = int.Parse(keyValuePair.Key),
                Value = keyValuePair.Value
            });
        }
        
        return result;
    }
}