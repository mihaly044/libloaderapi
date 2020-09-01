using System;
using CommandLine;
using libloaderapi.Common;
using libloaderapi.Common.Dto.Client;

namespace libloaderapi_cli
{
    public abstract class AuthorizedActionCommand
    {
        [Option('X', "token", Required = false, Default = null, HelpText = "Use a token other than the one in the cache.")]
        public string Token { get; set; }
    }

    [Verb("login", HelpText = "Get an authorization token to use the API.")]
    public class LoginCommand
    {
        [Option('u', "username", Required = true, HelpText = "This is your user name.")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "This is your password.")]
        public string Password { get; set; }

        [Option('n', "no-cache", Required = false, Default = false, HelpText = "Do not cache the access token. You will need to provide it with the -X switch every time you do a privileged action.")]
        public bool NoCache { get; internal set; }
    }

    [Verb("client-register", HelpText = "Upload an executable for authentication.")]
    public class RegisterClientCommand : AuthorizedActionCommand
    {
        [Option('f', "file", Required = true, HelpText = "The location of the client that is going to be sent for authentication.")]
        public string File { get; set; }

        [Option('b', "bucket", Default = BucketType.Development, HelpText = "Specifies whether to upload this client to the Development or the Production bucket.")]
        public BucketType Bucket { get; set; }

        [Option('o', "override-policy", Required = false, Default = OverridePolicy.Default, HelpText = "Specifies override behaviour when the selected bucket is full.")]
        public OverridePolicy OverridePolicy { get; set; }

        [Option('t', "tag", Default = null, HelpText = "An optional ASCII string of max 32 letters that identifies the client.")]
        public string Tag { get; set; }
    }

    [Verb("client-list", HelpText = "List all the clients tied to your account.")]
    public class ListClientsCommand : AuthorizedActionCommand
    {
        
    }

    [Verb("client-delete", HelpText = "Delete a client")]
    public class DeleteClientCommand : AuthorizedActionCommand
    {
        [Option('i', "id", Default = null, HelpText = "If specified, the client will get deleted its ID.")]
        public Guid Id { get; set; }

        [Option('t', "tag", Default = null, HelpText = "If specified, the client will get deleted by tis tag.")]
        public string Tag { get; set; }
    }

    [Verb("client-tag", HelpText = "Append a tag to the client or modify an existing one")]
    public class TagClientCommand : AuthorizedActionCommand
    {
        [Option('i', "id", Required = true, HelpText = "The ID of the target client")]
        public Guid Id { get; set; }

        [Option('t', "tag", Required = true, HelpText = "The ASCII string of max 32 letters that identifies the client.")]
        public string Tag { get; set; }
    }
}
