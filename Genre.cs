using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Have_I_Seen {
    public class GenresList {
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
    }

    public class Genre {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string GenreType { get; set; }
    }

}
