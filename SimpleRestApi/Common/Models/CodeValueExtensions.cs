using SimpleRestApi.Common.Exceptions;

namespace SimpleRestApi.Common.Models;

public static class CodeValueExtensions
{
    public static IEnumerable<CodeValueType> GetFromDictionaryArray(IEnumerable<Dictionary<string, string>> data)
    {
        var result = new List<CodeValueType>();
        var rowNumber = 1;
        foreach (var rowData in data)
        {
            if (rowData.Count != 1)
            {
                throw new IncorrectDataException("The string must contain only one [key:value] pair");
            }

            var keyValuePair = rowData.FirstOrDefault();

            result.Add(new CodeValueType
            {
                Id = rowNumber++,
                Code = int.Parse(keyValuePair.Key),
                Value = keyValuePair.Value
            });
        }
        
        return result;
    }
}