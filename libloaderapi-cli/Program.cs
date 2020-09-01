using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandLine;
using ConsoleTables;
using libloaderapi.Common.Dto.Auth;
using libloaderapi.Common.Dto.Client;
using Pastel;

namespace libloaderapi_cli
{
    public static class CanBeNullEx
    {
        public static string CanBeNull(this object x)
        {
            return !(x is string) ? "N/A" : x.ToString();
        }
    }

    class Program
    {
        /// <summary>
        /// This one client is going to be used throughout the entire lifetime of the app
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// The API endpoint URI
        /// </summary>
        private const string ApiUrl = "http://localhost:32768";

        /// <summary>
        /// Contains the file name to save the token into
        /// </summary>
        private const string TokenFileName = "token.txt";

        /// <summary>
        /// The token handler object for parsing JWT tokens
        /// </summary>
        private static JwtSecurityTokenHandler _tokenHandler;

        /// <summary>
        /// The object holding an access token
        /// </summary>
        private static JwtSecurityToken _token;

        /// <summary>
        /// Checks if we have a valid token already
        /// </summary>
        /// <returns></returns>
        private static bool CheckToken()
        {
            if (_token == null)
            {
                Console.WriteLine(
                    $"{"[error]".Pastel(Color.Red)} You do not seem to have a token. Please get one using the login command.");
                return false;
            }

            var time = DateTime.UtcNow;
            if (_token.ValidFrom <= time && _token.ValidTo >= time) return true;
            Console.WriteLine(
                $"{"[error]".Pastel(Color.Red)} Your token appears to be invalid. Please re-authenticate yourself with the login command.");
            return false;

        }

        private static async Task Main(string[] args)
        {
            // Setting up the token handler
            _tokenHandler = new JwtSecurityTokenHandler();

            // Setting up the http client here
            Client.BaseAddress = new Uri(ApiUrl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));

            // Set the user-agent
            var header = new ProductHeaderValue("libloaderapi-cli",
                Assembly.GetExecutingAssembly().GetName().Version?.ToString());
            var userAgent = new ProductInfoHeaderValue(header);
            Client.DefaultRequestHeaders.UserAgent.Add(userAgent);

            // Check if we have a token saved already
            if (File.Exists(TokenFileName))
            {
                var token = await File.ReadAllTextAsync(TokenFileName);
                _token = _tokenHandler.ReadJwtToken(token);
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // Parse args
            if (args.Length == 0)
            {
                Console.WriteLine(
                    $"Welcome to the {"Libloaderapi".Pastel(Color.Chartreuse)} interactive shell. " +
                    $"Please enter your commands below or type {"help".Pastel(Color.Aqua)} to get help.\nType exit to quit the shell.");

                var regex = new Regex("[\\\"\"].+?[\\\"\"]|[^ ]+");

                while (true)
                {
                    Console.Write($"{"api".Pastel(Color.Coral)}{"> ".Pastel(Color.Yellow)}");
                    var command = Console.ReadLine();
                    var c = regex.Matches(command!).Select(x => x.Value).ToArray();
                    if (command.Trim() != "exit")
                        await Parse(c);
                    else
                        return;
                }
            }
            else
            {
                await Parse(args);
            }
        }

        private static async Task Parse(IEnumerable<string> args)
        {
            var parserResult =
                Parser.Default
                    .ParseArguments<LoginCommand, RegisterClientCommand, ListClientsCommand, DeleteClientCommand, TagClientCommand>(args);
            await parserResult.WithParsedAsync<LoginCommand>(OnLoginCommand);
            await parserResult.WithParsedAsync<RegisterClientCommand>(OnRegisterClientCommand);
            await parserResult.WithParsedAsync<ListClientsCommand>(OnListClientsCommand);
            await parserResult.WithParsedAsync<DeleteClientCommand>(OnDeleteClientCommand);
            await parserResult.WithParsedAsync<TagClientCommand>(OnTagClientCommand);
        }

        /// <summary>
        /// Processes the login command
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static async Task OnLoginCommand(LoginCommand arg)
        {
            var response = await Client.PostAsJsonAsync("/users/authenticate", new AuthRequest
            {
                Username = arg.Username,
                Password = arg.Password
            });

            var result = await response.Content.ReadAsAsync<AuthResult>();

            if (result.Success)
            {
                _token = _tokenHandler.ReadJwtToken(result.Token);
                
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Token);

                if (!arg.NoCache)
                    await File.WriteAllTextAsync(TokenFileName, result.Token);

                Console.WriteLine(
                    $"{"[success]".Pastel(Color.Lime)} Successfully logged in as {arg.Username.Pastel(Color.DarkCyan)}, " +
                    $"session will expire at {_token.ValidTo.ToLocalTime().ToString(CultureInfo.CurrentCulture).Pastel(Color.DarkCyan)} local time.\n" +
                    $"Your token has been saved to {Path.Join(Environment.CurrentDirectory, TokenFileName).Pastel(Color.DarkCyan)}");
            }
            else
            {
                Console.WriteLine(
                    $"{"[error]".Pastel(Color.Red)} An error has occurred: {response.StatusCode}, {result.Message}.");
            }
        }

        /// <summary>
        /// Processes the client-register command
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static async Task OnRegisterClientCommand(RegisterClientCommand arg)
        {
            if (arg.Token != null)
                _token = _tokenHandler.ReadJwtToken(arg.Token);

            if (!CheckToken())
                 return;

            await using var fs = File.OpenRead(arg.File);
            var content = new MultipartFormDataContent
            {
                { new StringContent(arg.OverridePolicy.ToString()), "OverridePolicy" },
                { new StringContent(arg.Bucket.ToString()), "Bucket" },
                { new StreamContent(fs), "\"File\"", $"\"{Path.GetFileName(arg.File)}\""}
            };

            if(!string.IsNullOrEmpty(arg.Tag))
                content.Add(new StringContent(arg.Tag), "Tag");

            var response = await Client.PostAsync("/clients/register", content);

            var result = await response.Content.ReadAsAsync<ClientRegistrationResult>();

            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Created:
                        Console.WriteLine(
                            $"{"[success]".Pastel(Color.Lime)} Client Successfully registered to be used in the " +
                            $"{arg.Bucket.ToString().Pastel(Color.Yellow)} bucket.\nKey={result.ApiKey.Pastel(Color.DarkCyan)}");
                        break;
                    case HttpStatusCode.OK when result.Skipped:
                        Console.WriteLine($"{"[warning]".Pastel(Color.Orange)} You have already registered this client. Skipping.");
                        break;
                }
            }
            else if (result != null)
            {
                Console.WriteLine($"{"[error]".Pastel(Color.Red)} {result.Message}");
            }
            else
            {
                Console.WriteLine($"{"[error]".Pastel(Color.Red)} {response.StatusCode}");
            }
        }

        private static async Task OnListClientsCommand(ListClientsCommand arg)
        {
            if (arg.Token != null)
                _token = _tokenHandler.ReadJwtToken(arg.Token);

            if (!CheckToken())
                return;

            var response = await Client.GetAsync("/clients");

            if (response.IsSuccessStatusCode)
            {
                var clients = await response.Content.ReadAsAsync<IList<ClientDtoObject>>();
                if (clients.Any())
                {
                    Console.WriteLine(
                        $"{"[information]".Pastel(Color.Cyan)} You have {clients.Count.ToString().Pastel(Color.DarkCyan)} clients total.");

                    var table = new ConsoleTable("Guid", "Tag", "Bucket", "Created", "LastUsed", "RegistrantIp")
                    {
                        Options =
                        {
                            EnableCount = false
                        }
                    };

                    foreach (var client in clients)
                    {
                        table.AddRow(client.Id, client.Tag.CanBeNull(),
                            client.BucketType.ToString(), client.CreatedAt, client.LastUsed.CanBeNull(), client.RegistrantIp);
                    }

                    table.Write();
                }
                else
                {
                    Console.WriteLine($"{"[information]".Pastel(Color.Cyan)} You do not have any clients registered.");
                }
            }
            else
            {
                Console.WriteLine($"{"[error]".Pastel(Color.Red)} {response.StatusCode}");
            }
            
        }

        private static async Task OnDeleteClientCommand(DeleteClientCommand arg)
        {
            if (arg.Token != null)
                _token = _tokenHandler.ReadJwtToken(arg.Token);

            if (!CheckToken())
                return;

            var endpoint = !string.IsNullOrEmpty(arg.Tag) ? $"/clients/tag/{arg.Tag}" : $"/clients/id/{arg.Id}";
            var response = await Client.DeleteAsync(endpoint);

            Console.WriteLine(response.IsSuccessStatusCode
                ? $"{"[success]".Pastel(Color.Lime)} Client has been deleted."
                : $"{"[error]".Pastel(Color.Red)} {response.StatusCode}");
        }

        /// <summary>
        /// Processes the tag client command
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static async Task OnTagClientCommand(TagClientCommand arg)
        {
            if (arg.Token != null)
                _token = _tokenHandler.ReadJwtToken(arg.Token);

            if (!CheckToken())
                return;

            var response = await Client.PostAsJsonAsync("/clients/tag", new ClientDtoObject
            {
                Id = arg.Id,
                Tag = arg.Tag
            });

            Console.WriteLine(response.IsSuccessStatusCode
                ? $"{"[success]".Pastel(Color.Lime)} Client tag successfully updated."
                : $"{"[error]".Pastel(Color.Red)} An error has occurred: {response.StatusCode}");
        }
    }
}
