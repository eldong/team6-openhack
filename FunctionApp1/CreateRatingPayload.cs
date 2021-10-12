using System;
using System.Collections.Generic;
using System.Text;

namespace OpenhackFY22
{
    public class CreateRatingPayload
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
    }
}
