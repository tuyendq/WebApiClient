using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("\n=== Web API Client ===\n\n");

            var repositories = await ProcessRepositories();
            // Print list of repo.Name
            System.Console.WriteLine("List of repo:\n");
            foreach (var repo in repositories)
            {
                System.Console.WriteLine(repo.Name);
            }

        }

        private static readonly HttpClient client = new HttpClient();
        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var url = "https://api.github.com/orgs/dotnet/repos";
            // url = "https://api.ipify.org";

            // var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            // var msg = await stringTask;
            // var msg = await client.GetStringAsync(url);
            // System.Console.WriteLine(msg);

            var streamTask = client.GetStreamAsync(url);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            return repositories;
            
        }
    }
}
