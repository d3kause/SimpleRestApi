using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SimpleRestApi.Rest.Dto;

[DataContract]
public class KeyValueDto
{
    [DataMember]
    public int Code { get; set; }
    [DataMember]
    public string Value { get; set; }
}