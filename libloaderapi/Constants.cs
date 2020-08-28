namespace libloaderapi
{
    public class Constants
    {
        public const uint MaxClientsPerUser = 2;
        public const string TokenIssuer = "api.libloader.net";
        public static string BlobContainer = "Serve";
    }

    public class PredefinedRoles
    {
        public const string Client = "LibClient";
        public const string User = "LibUser";
        public const string Admin = "LibAdmin";
    }
}
