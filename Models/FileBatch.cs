using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models
{
    public class FileBatch
    {
        [JsonProperty(PropertyName = "batchid")]
        public string BatchId {get;set;}
        [JsonProperty(PropertyName = "id")]
        public string Id {get;set;} = Guid.NewGuid().ToString();
        public List<ProcessFile> Files {get;set;}
    }
}