using Newtonsoft.Json;

namespace SimpleRestApi.Rest.Dto;

public class KeyValuePairsDto
{

    public Dictionary<int,string> values { get; set; }
}