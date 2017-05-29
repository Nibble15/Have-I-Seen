using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class Program {
        static void Main(string[] args) {

            RunApp runApp = new RunApp();
            runApp.UserInterface();
            
            GetListings listings = new GetListings();
            string currDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currDirectory);
            var fileName = Path.Combine(directory.FullName, "genre.Json");

            //used to get JSON from tmdb and store in a file
            //var genres = GetGenres();
            //SerializeGenresToFile(genres, fileName);

            //print genre.json file to console
            //var genres = GetListings.DeserializeGenres(fileName);
            //foreach (var genre in genres) {
            //    Console.WriteLine("Genre: " + genre.GenreType + "Genre Id: " + genre.Id);
            //}
        }

        public static List<Genre> GetGenres() {
            var results = new List<Genre>();
            var webClient = new WebClient();
            byte[] searchResults = webClient.DownloadData(string.Format("https://api.themoviedb.org/3/genre/movie/list?api_key=7cc33ddda390c1e661b0c6e87e0e5cd0&language=en-US"));
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader)) {
                results = serializer.Deserialize<GenresList>(jsonReader).Genres;
            }
            return results;
        }

        public static void SerializeGenresToFile(List<Genre> genres, string fileName) {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer)) {
                serializer.Serialize(jsonWriter, genres);
            }
        }
    }
}
