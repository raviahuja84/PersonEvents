using Newtonsoft.Json;
using System.Collections.Generic;

namespace PersonEvents_WebApp.Models
{
    public class PersonEvents
    {
        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("events")]
        public IEnumerable<string> Events { get; set; }
    }
}
