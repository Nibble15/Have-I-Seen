using System;
using System.Collections.Generic;

namespace Have_I_Seen {
    class RunApp {

        GetListings listings = new GetListings();

        public void UserInterface() {
            
            while (true) {
                Console.WriteLine(" ------------- Menu ------------- ");
                Console.WriteLine("Type one of the following commands to begin searching:");
                Console.WriteLine("'movie': to search by movie title");
                Console.WriteLine("'genre': to search by genre: ");
                Console.WriteLine("'quit': to exit");
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
                    OutputResults(listings.GetMovies(searchType, query));
                    listings.ShowPage();
                }
                if(searchType == "movie") {
                    Console.Write("What is the name of the movie you're looking for: ");
                    query = Console.ReadLine().ToLower().Trim();
                    OutputResults(listings.GetMovies(searchType, query));
                    listings.ShowPage();
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
                        OutputResults(listings.GetMovies(searchType, query));
                        listings.ShowPage();
                    }
                }

            }
        }
        

        private void OutputResults(List<MovieSearchResult> movies) {
            Console.WriteLine(string.Format($"There are: {movies.Count} movies that match your search \r\n"));
            Console.WriteLine("# of pages: " + listings._pageCount);
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
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
            }
        }
    }
}
