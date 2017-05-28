using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.TMDb;
using System.Text;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Have_I_Seen {
    class Program {
        static void Main(string[] args) {

            RunApp runApps = new RunApp();
            runApps.UserInterface();
        }
    }
}
//Key: qat9h7t7b8xus6qe6dmcfbnz Secret: gpBuQrzeW3 - Fandango

//webClient.Headers.Add("Ocp-Apim-Subscription-Key", "716fc6ce9d6845b19532419d8611d7c5"); seach api and azure access key
