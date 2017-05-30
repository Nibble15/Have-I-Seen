using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class GetListings {
        // Genre ID search path = https://api.themoviedb.org/3/genre/12/movies?api_key=7cc33ddda390c1e661b0c6e87e0e5cd0&language=en-US&include_adult=false&sort_by=created_at.asc
        //Movie search path = https://api.themoviedb.org/3/search/movie?api_key=7cc33ddda390c1e661b0c6e87e0e5cd0&language=en-US&query=

        private string _tmdbBaseUrl = "https://api.themoviedb.org/3/";
        private string _searchMovies = "search/movie";
        private string _apiKey = "7cc33ddda390c1e661b0c6e87e0e5cd0";
        private string _language = "language=en-US";
        public int _page { get; set; } = 1;
        public int _genre { get; private set; }
        

        //NOTE TO SELF: returns null if movie doesn't exist
        private byte[] SearchByMovieOrGenre(string typeOfSearch, string query) {
            byte[] searchResults;
            var webClient = new WebClient();
            if (typeOfSearch.ToLower().Trim() == "movie") {
                searchResults = webClient.DownloadData(string.Format($"{_tmdbBaseUrl}{_searchMovies}" +
                                                                     $"?api_key={_apiKey}&{_language}&query={query}&page={_page}"));
                return searchResults;
            }
            if (typeOfSearch.ToLower().Trim() == "genre") {
                query = _genre.ToString();
                searchResults = webClient.DownloadData(string.Format($"{_tmdbBaseUrl}genre/{query}/movies" +
                                                                 $"?api_key={_apiKey}&{_language}&include_adult=false&sort_by=created_at.asc"));
                return searchResults;
            }
            return searchResults = null;
        }
        
        //NOTE TO SELF: returns -1 if no match found
        public int GetGenreId(string genreType) {
            var genres = GetListings.DeserializeGenres();
            foreach (var genre in genres) {
                if(genre.GenreType.ToLower().Trim() == genreType) {
                    _genre = genre.Id;
                    return _genre;
                }
            }
            return -1;
        }
        
        public List<MovieSearchResult> GetMovies(string typeOfSearch, string query) {
            var results = new List<MovieSearchResult>();
            byte[] searchResults = SearchByMovieOrGenre(typeOfSearch, query);
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader)) {
                results = serializer.Deserialize<MovieSearch>(jsonReader).Results;
            }
            return results;
        }

        // used to deserialize genre.Json
        public static List<Genre> DeserializeGenres() {
            GetListings listings = new GetListings();
            string currDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currDirectory);
            var fileName = Path.Combine(directory.FullName, "genre.Json");
            var genres = new List<Genre>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader)) {
                genres = serializer.Deserialize<List<Genre>>(jsonReader);
            }
            return genres;
        }
    }
}