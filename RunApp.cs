using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class RunApp {

        GetListings listings = new GetListings();

        public void UserInterface() {
            Console.WriteLine("Search movies by Title or Genre");
            Console.WriteLine("Open Menu - type a command to begin searching:");
            Console.WriteLine("movie: to get a list of relevent movies");
            Console.WriteLine("genre: to get a list of movies in that genre: ");
            
            while (true) {
                string searchType = Console.ReadLine().ToLower().Trim();
                string query = string.Empty;
                List<MovieSearchResult> movies;
                if (searchType == "genre") {
                    var genres = GetListings.DeserializeGenres();
                    foreach (var genre in genres) {
                        Console.WriteLine(genre.GenreType);
                    }
                    Console.Write("Please type the genre you wish to view: ");
                    query = Console.ReadLine().ToLower().Trim();
                    int genreId = listings.GetGenreId(query);
                    query = genreId.ToString();
                    movies = listings.GetMovies(searchType, query);
                    OutputResults(movies);
                    continue;
                }
                if(searchType == "movie") {
                    query = Console.ReadLine().ToLower().Trim();
                    movies = listings.GetMovies(searchType, query);
                    OutputResults(movies);
                    continue;
                }
                
                if (searchType == "quit" || query == "quit") {
                    break;
                }
            }
        }
        private void OutputResults(List<MovieSearchResult> movies) {
            Console.WriteLine(string.Format($"There are: {movies.Count} movies that match your search \r\n"));
            foreach (var movie in movies) {
                Console.WriteLine(string.Format(@"Title - {0}

Rating: {1} 

Overview: 
{2}", movie.Title, movie.Rating, movie.Overview));

                //if (movie.GenreId != null) {
                //    foreach (var id in movie.GenreId) {
                //        Console.WriteLine(id.Value);
                //    }
                //}
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

//        public void UserInterface() {
//            Console.Write("Enter a title to get a list of possible matches: ");
//            while (true) {
//                string userInput = Console.ReadLine();
//                if (userInput.ToLower().Trim() == "quit") {
//                    break;
//                }
//                var movies = listings.GetMovies(userInput);
//                Console.WriteLine(string.Format($"There are: {movies.Count} movies that match your search \r\n"));
//                foreach (var movie in movies) {
//                    Console.WriteLine(string.Format(@"Title - {0}

//Rating: {1} 

//Overview: 
//{2}", movie.Title, movie.Rating, movie.Overview));

//                    if (movie.GenreId != null) {
//                        foreach (var id in movie.GenreId) {
//                            Console.WriteLine(id.Value);
//                        }
//                    }
//                    Console.WriteLine();
//                    Console.WriteLine();
//                }

//                Console.Write("Search for another movie or quit?: ");
//            }
//        }

