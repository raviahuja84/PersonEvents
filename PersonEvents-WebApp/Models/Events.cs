using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Models
{
    [Table("Events")]
    public class Events
    {
        [JsonProperty("eventid")]
        [Key]
        public Guid EventID { get; set; }

        // Ideally should be a FK/identifer to person table
        //[JsonProperty("personid")]
        //public string PersonID { get; set; }

        [JsonProperty("personname")]
        public string PersonName { get; set; }

        [JsonProperty("persondob")]
        public DateTime PersonDOB { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
