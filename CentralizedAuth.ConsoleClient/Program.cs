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
            var discoRO = await new HttpClient().GetDiscoveryDocumentAsync("https://localhost:5001");
            if(discoRO.IsError)
            {
                Console.WriteLine(discoRO.Error);
                return;
            }

            var tokenClientRO = new TokenClient(discoRO.TokenEndpoint, "ro.client", "Secret");
            var tokenResponseRO = await tokenClientRO.RequestResourceOwnerPasswordAsync("ddewulf", "password", "PermissionsApi");

            if(tokenResponseRO.IsError)
            {
                Console.WriteLine(tokenResponseRO.Error);
                return;
            }

            Console.WriteLine(tokenResponseRO.Json);
            Console.WriteLine("\n\n");


            // Using Client Credentials
            var discovery = await new HttpClient().GetDiscoveryDocumentAsync("https://localhost:5001/");
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
