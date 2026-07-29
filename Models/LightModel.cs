using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AISample.Models
{
    public class LightModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_on")]
        public bool? Is_On { get; set; }
    }
}
