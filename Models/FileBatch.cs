using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models
{
    public class FileBatch
    {
        [JsonProperty(PropertyName = "batchid")]
        public string BatchId {get;set;}
        public List<ProcessFile> Files {get;set;}
    }
}