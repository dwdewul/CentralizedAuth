using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace CentralizedAuth.ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var discovery = await DiscoveryClient.GetAsync("https://localhost:5001/");
            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            var tokenClient = new TokenClient(discovery.TokenEndpoint, "client", "Secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("WebApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("https://localhost:44313/api/ruleapplications");
            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}
