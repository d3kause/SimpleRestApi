namespace SimpleRestApi.Common.Database.CodeValue.Models;

public class DataFilterModel
{
    public int Limit { get; set; }
    
    public int Offset { get; set; }
    
    public string Value { get; set; }
    
    public int Code { get; set; }
}