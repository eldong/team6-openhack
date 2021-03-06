using System;
using Newtonsoft.Json;

namespace Models
{
    public class RatingInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }        
        public DateTime TimeStamp { get; set; }        
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
    }
}
