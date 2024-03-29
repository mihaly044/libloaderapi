﻿namespace libloaderapi.Domain
{
    public class Constants
    {
        public const uint MaxClientsInDevBucket = 10;
        public const uint MaxClientsInProductionBucket = 5;
        public const string TokenIssuer = "api.libloader.net";
    }

    public class PredefinedRoles
    {
        public const string Client = "LibClient";
        public const string User = "LibUser";
        public const string Admin = "LibAdmin";
    }
}
