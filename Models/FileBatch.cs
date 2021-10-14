using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class FileBatch
    {
        [JsonProperty(PropertyName = "batchid")]
        public string BatchId {get;set;}
        public List<ProcessFile> Files {get;set;}
    }
}