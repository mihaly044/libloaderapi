using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using libloaderapi.Common.Dto.Auth;

namespace libloaderapi_cli
{
    class Program
    {
        /// <summary>
        /// This one client is going to be used throughout the entire lifetime of the app
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        static async Task Main(string[] args)
        {
            // Initialize the client
            Client.BaseAddress = new Uri("https://api.libloader.net");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));

            // Parse the login command
            await Parser.Default.ParseArguments<LoginCommand>(args).WithParsedAsync(async opts =>
            {
                var response = await Client.PostAsJsonAsync("/users/authenticate", new AuthRequest
                {
                    Username = opts.Username,
                    Password = opts.Password
                });

                var result = await response.Content.ReadAsAsync<AuthResult>();

                if (result.Success)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadToken(result.Token) as JwtSecurityToken;


                    Console.WriteLine($"Token will expire at UTC {jwtSecurityToken.ValidTo}");
                    Console.WriteLine("Your token has the following claims:");

                    foreach (var claim in jwtSecurityToken.Claims.Select(x => $"{x.Type}={x.Value}"))
                    {
                        Console.WriteLine(claim);
                    }

                    Console.WriteLine($"\r\nToken={result.Token}");


                    /*if (!opts.DropToken)
                    {
                        await File.WriteAllTextAsync("user_token.txt", result.Token);
                    }*/
                }
                else
                {
                    Console.WriteLine($"{response.StatusCode}, {result.Message}");
                }
            });
        }
    }
}
