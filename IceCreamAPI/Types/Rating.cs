using System;
using System.Collections.Generic;
using System.Text;

namespace IceCreamAPI
{
    public class RatingInfo
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Rating { get; set; }
        public string LocationName { get; set; }
        public string UserNotes { get; set; }
    }
}
