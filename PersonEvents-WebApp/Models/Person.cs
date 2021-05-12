using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Models
{
    [Table("Person")]
    public class Person
    {
        [JsonProperty("personid")]
        [Key]
        public Guid PersonID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dob")]
        public DateTime DOB { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("postcode")]
        public string postcode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
