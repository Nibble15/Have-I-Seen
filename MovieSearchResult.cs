using Newtonsoft.Json;
using System.Collections.Generic;

namespace Have_I_Seen {

    public class MovieSearch {
        public int page { get; set; }
        [JsonProperty(PropertyName = "results")]
        public List<MovieSearchResult> Results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }

    public class MovieSearchResult {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }
        public string release_date { get; set; }
        public int?[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        public string backdrop_path { get; set; }
        public float popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        [JsonProperty(PropertyName = "vote_average")]
        public float Rating { get; set; }
    }
}
