using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class RunApp {

        GetListings listings = new GetListings();

        public void UserInterface() {
            Console.Write("Enter a title to get a list of possible matches: ");
            while (true) {
                string userInput = Console.ReadLine();
                if (userInput.ToLower().Trim() == "quit") {
                    break;
                }
                var movies = listings.GetMovies(userInput);
                Console.WriteLine(string.Format($"There are: {movies.Count} movies that match your search \r\n"));
                foreach (var movie in movies) {
                    Console.WriteLine(string.Format(@"Title - {0}

Rating: {1} 

Overview: 
{2}", movie.Title, movie.Rating, movie.Overview));

                    if (movie.GenreId != null) {
                        foreach (var id in movie.GenreId) {
                            Console.WriteLine(id.Value);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
                
                Console.Write("Search for another movie or quit?: ");
            }
        }
    }
}
/*GetListings listings = new GetListings();

        public void UserInterface() {
            Console.WriteLine("Search by movie or genre?: ");
            Console.WriteLine("Type choice and hit enter then type your search: ");
            string searchType = Console.ReadLine();
            while (true) {
                string searchQuery = Console.ReadLine();
                if (searchType.ToLower().Trim() == "quit") {
                    break;
                }
                var movies = listings.GetMovies(searchType, searchQuery);
                foreach (var movie in movies) {
                    Console.WriteLine(string.Format(@"Title: 
{0}
Rating: 
{1} 
Overview: 
{2}", movie.Title, movie.Rating, movie.Overview));
                    Console.WriteLine();
                }
                Console.Write("Search for another movie or quit?: ");*/
