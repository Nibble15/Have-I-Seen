using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaveIseen {

    public class RootObject {
        public int id { get; set; }
        public string name { get; set; }
        public string overview { get; set; }
        public object poster_path { get; set; }
        public string backdrop_path { get; set; }
        [JsonProperty(PropertyName = "parts")]
        public List<MovieSearchResult> MovieSearchResults { get; set; }
    }

    public class MovieSearchResult {
        public bool adult { get; set; }
        public object backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }
        public float popularity { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

}
