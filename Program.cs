﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("\nWeb API Client\n\n");

            await ProcessRepositories();
        }

        private static readonly HttpClient client = new HttpClient();
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            System.Console.WriteLine(msg);
        }
    }
}
