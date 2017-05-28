using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class GetListings {

        private string _moviesBaseUrl = "https://api.themoviedb.org/3/search/movie?api_key=7cc33ddda390c1e661b0c6e87e0e5cd0&language=en-US&query=";

        // GET
        public List<MovieSearchResult> GetMovies(string userInput) {
            var results = new List<MovieSearchResult>();
            var webClient = new WebClient();
            byte[] searchResults = webClient.DownloadData(string.Format("{0}{1}&page=1&include_adult=false", _moviesBaseUrl, userInput));
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader)) {
                results = serializer.Deserialize<MovieSearch>(jsonReader).Results;
            }
            return results;
        }
    }