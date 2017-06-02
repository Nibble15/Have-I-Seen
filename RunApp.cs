using System;
using System.Collections.Generic;

namespace Have_I_Seen {
    class RunApp {

        GetListings listings = new GetListings();

        public void UserInterface() {
            
            while (true) {
                MainMenu();
                string searchType = Console.ReadLine().ToLower().Trim();
                string query = string.Empty;
                
                if (searchType == "quit") {
                    break;
                }
                if (searchType == "genre") {
                    Console.WriteLine("Here are your genre choices: ");
                    listings.PrintGenres();
                    Console.Write("Please type the genre you wish to view: ");
                    query = listings.GetGenreId(Console.ReadLine()).ToString();
                    OutputResults(searchType, query);
                }
                if(searchType == "movie") {
                    Console.Write("What is the name of the movie you're looking for: ");
                    query = Console.ReadLine().ToLower().Trim();
                    OutputResults(searchType, query);
                }
                
                //Pagination or new Search
                while (true) {
                    Console.Write("Type search or quit to return to main menu, next to go to the next page or back to go back a page: ");
                    string continueOrNewSearch = Console.ReadLine().ToLower().Trim();
                    if(continueOrNewSearch == "quit" || continueOrNewSearch == "search") {
                        listings.ResetPageNumber();
                        break;
                    }
                    else if (continueOrNewSearch == "next" || continueOrNewSearch == "back") {
                        listings.PageNextOrBack(continueOrNewSearch);
                        OutputResults(searchType, query);
                    }
                }
            }
        }
        
        private void MainMenu() {
            Console.WriteLine(" ------------- Menu ------------- ");
            Console.WriteLine("Type one of the following commands to begin searching:");
            Console.WriteLine("'movie': to search by movie title");
            Console.WriteLine("'genre': to search by genre: ");
            Console.WriteLine("'quit': to exit");
        }

        private void OutputResults(string searchType, string query) {
            var movies = listings.GetMovies(searchType, query);
            foreach (var movie in movies) {
                Console.WriteLine();
                Console.WriteLine(string.Format(@"Title - {0}
Rating: {1} 

Overview: 
{2}", movie.Title, movie.Rating, movie.Overview));
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine();
            Console.WriteLine(string.Format($"{movies.Count} movies out of a total {listings.TotalResults}."));
            listings.ShowPage();
            Console.WriteLine(string.Format("Page{0} total: {1}\r\n", (listings._pageCount > 1) ? "s" : null, listings._pageCount));
            Console.WriteLine("----------------------------------------------------------------\r\n\r\n\r\n");
        }
    }
}


//if (movie.GenreId != null) {
//    foreach (var id in movie.GenreId) {
//        Console.WriteLine(id.Value);
//    }
//}