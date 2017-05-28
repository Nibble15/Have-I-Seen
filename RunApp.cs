using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Have_I_Seen {
    class RunApp {

        public void UserInterface() {
            var listings = new GetListings();
            Console.Write("Enter a movie title to get a list of movies: ");
            while (true) {
                string userInput = Console.ReadLine();
                if (userInput.ToLower().Trim() == "quit") {
                    break;
                }
                var movies = listings.GetMovies(userInput);
                foreach (var movie in movies) {
                    Console.WriteLine(string.Format(@"Title: 
{0}
Rating: 
{1} 
Overview: 
{2}", movie.Title, movie.Rating, movie.Overview));
                    Console.WriteLine();
                }
                Console.Write("Search for another movie or quit?: ");
            }
        }
    }
}
