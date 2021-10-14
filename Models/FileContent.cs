using System;
using Newtonsoft.Json;

public class FileContent
{
    [JsonProperty(PropertyName = "batchid")]
    public string BatchId {get;set;}
    [JsonProperty(PropertyName = "id")]
    public string Id {get;set;} = Guid.NewGuid().ToString();
    public string Content {get;set;}
}