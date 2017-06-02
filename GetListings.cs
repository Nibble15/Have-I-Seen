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
        public int _page { get; private set; } = 1;
        public int _genre { get; private set; }
        private List<MovieSearchResult> _results = new List<MovieSearchResult>();
        public int _pageCount { get; private set; }
        

        public void PageNextOrBack(string choice) {
            if (choice == "next" && _page < _pageCount) {
                _page++;
            }
            if (choice == "back" && _page > 1) {
                _page--;
            }
        }

        public void ResetPageNumber() {
            _page = 1;
        }

        public List<MovieSearchResult> GetMovies(string typeOfSearch, string query) {
            byte[] searchResults = SearchByMovieOrGenre(typeOfSearch, query);
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader)) {
                //_results = serializer.Deserialize<MovieSearch>(jsonReader).Results;
                //_pageCount = serializer.Deserialize<MovieSearch>(jsonReader).Pages;
                var movieSearch = serializer.Deserialize<MovieSearch>(jsonReader);
                _results = movieSearch.Results;
                _pageCount = movieSearch.Pages;
            }
            return _results;
        }

        private byte[] SearchByMovieOrGenre(string typeOfSearch, string query) {
            byte[] searchResults = null;
            var webClient = new WebClient();
            if (typeOfSearch == "movie") {
                searchResults = webClient.DownloadData(string.Format($"{_tmdbBaseUrl}{_searchMovies}" +
                                                                     $"?api_key={_apiKey}&{_language}&query={query}&page={_page}"));
            }
            if (typeOfSearch == "genre") {
                query = _genre.ToString();
                searchResults = webClient.DownloadData(string.Format($"{_tmdbBaseUrl}genre/{query}/movies" +
                                                                 $"?api_key={_apiKey}&{_language}&page={_page}&include_adult=false&sort_by=created_at.asc"));
            }
            return searchResults;
        }
        
        public int GetGenreId(string genreType) {
            var genres = DeserializeGenres();
            foreach (var genre in genres) {
                if(genre.GenreType.ToLower().Trim() == genreType.ToLower().Trim()) {
                    _genre = genre.Id;
                    return _genre;
                }
            }
            return -1;
        }

        // used to deserialize genre.Json
        private List<Genre> DeserializeGenres() {
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

        public void PrintGenres() {
            var genreList = DeserializeGenres();
            foreach (var genre in genreList) {
                Console.WriteLine(genre.GenreType);
            }
        }

        public void ShowPage() {
            Console.WriteLine("Page: " + _page);
        }
    }
}