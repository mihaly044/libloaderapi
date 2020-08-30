using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace libloaderapi_cli
{
    [Verb("login")]
    public class LoginCommand
    {
        [Option('u', "username", Required = true, HelpText = "This is your user name.")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "This is your password.")]
        public string Password { get; set; }

        [Option('d', "drop-token", Required = false, HelpText = "The authorization token will not be saved for later use. " +
                                                                "This means you will have to log in each time you perform an action.", Default = false)]
        public bool DropToken { get; set; }
    }
}
